using GameEngine.Adapters.Console;
using GameEngine.Entities;
using GameEngine.Graphics.Console.Sprites;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using System.Reflection.PortableExecutable;

namespace GameEngine.Graphics.Console
{
    public class GraphicsEngine(
        IConsoleProvider consoleProvider,
        IMap map,
        int frameHeight,
        int frameWidth,
        int frameRateMs,
        char unknownTileCharacter = '?'
    ) : IGraphicsEngine
    {
        private bool _stop = true;
        private Task? _renderLoop;
        private readonly IMap _map = map;
        private readonly int _frameHeight = frameHeight;
        private readonly int _frameWidth = frameWidth;
        private readonly IConsoleProvider _consoleProvider = consoleProvider;
        private readonly char[,] _composite = new char[frameHeight, frameWidth];
        private readonly int _framePeriod = 1000 / frameRateMs;
        private readonly char _unknownTileCharacter = unknownTileCharacter;

        /// <summary>
        /// Renders the current state of the game by building a composite image of the map and entities, and then writing it to the console.
        /// </summary>
        /// <remarks>
        /// Public in order to facilitate testing.
        /// </remarks>
        public void Render()
        {
            this.BuildComposite();
            this._consoleProvider.Clear();
            string buffer = string.Empty;
            for (int y = 0; y < this._frameHeight; y++)
            {
                for (int x = 0; x < this._frameWidth; x++)
                {
                    buffer += this._composite[y, x];
                }
                buffer += '\n'; // could use Environment.NewLine, but this would make testing a litte more awkward.
            }
            this._consoleProvider.Write(buffer);
        }

        /// <inheritdoc/>
        public void Start()
        {
            if (
                this._renderLoop is not null &&
                this._renderLoop.Status != TaskStatus.RanToCompletion &&
                this._renderLoop.Status != TaskStatus.Canceled &&
                this._renderLoop.Status != TaskStatus.Faulted
            )
                throw new InvalidOperationException($"Start called when Graphics Engine render loop is already running. Render loop in status {this._renderLoop?.Status}");
            
            this._stop = false;

            // Start the rendeer loop in a background thread to continuously render frames from the map.
            this._renderLoop = Task.Factory.StartNew(() => this.RenderLoop(), TaskCreationOptions.LongRunning);
        }

        /// <inheritdoc/>
        public Task Stop()
        {
            this._stop = true;
            return this._renderLoop ?? Task.CompletedTask;
        }

        private void RenderLoop()
        {
            while (!this._stop)
            {
                this.Render();
                Thread.Sleep(this._framePeriod);
            }
        }

        /// <summary>
        /// Builds the composite image by combining the map and entities.
        /// </summary>
        private void BuildComposite()
        {
            // Draw the map
            for (int y = 0; y < this._frameHeight; y++)
            {
                for (int x = 0; x < this._frameWidth; x++)
                {
                    if(this._map.TryGetTile(x, y, out ITile? tile) && tile is not null)
                    {
                        this._composite[y, x] = tile.PrintChar;

                        // If there are entities on this tile, we will draw the first one on top of the tile.
                        if (tile.GetEntities().Any())
                        {
                            IEntity entity = tile.GetEntities().First();
                            this._composite[y, x] = ((Frame)entity.Sprite.GetFrame()).Character;
                        }
                    }
                    else
                    {
                        this._composite[y, x] = this._unknownTileCharacter;
                    }
                }
            }
        }
    }
}
