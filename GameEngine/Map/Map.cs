using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Map.Tiles;
using System.Text;

namespace GameEngine.Map
{
    /// <summary>
    /// Represents a two-dimensional map composed of tiles, where each tile can contain entities and respond to events.
    /// </summary>
    public class Map : IMap
    {
        /// <summary>
        /// A two-dimensional array representing the tiles in the map.
        /// </summary>
        public ITile[,] Tiles { get; }

        /// <inheritdoc/>
        public int Width => this.Tiles.GetLength(1);

        /// <inheritdoc/>
        public int Height => this.Tiles.GetLength(0);

        /// <inheritdoc/>
        public ITile GetTile(int x, int y)
        {
            if (x < 0 || x >= this.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Coordinates are out of bounds.");
            }
            if (y < 0 || y >= this.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Coordinates are out of bounds.");
            }
            return this.Tiles[y, x];
        }

        /// <inheritdoc/>
        public bool IsTilePassable(int x, int y) => this.GetTile(x, y).Passable;

        /// <inheritdoc/>
        public short AddEntityToTile(int x, int y, IEntity entity) => 
            this.GetTile(x, y).AddEntity(entity);

        /// <inheritdoc/>
        public short RemoveEntityFromTile(int x, int y, IEntity entity) => 
            this.GetTile(x, y).RemoveEntity(entity);

        /// <inheritdoc/>
        public IEnumerable<IEntity> GetEntitiesInTile(int x, int y) =>
            this.GetTile(x, y).GetEntities();

        /// <inheritdoc/>
        public short PublishEventToTile(int x, int y, IEvent e) =>
            this.GetTile(x, y).PublishEvent(e);

        public Map(MapDto mapDto, Dictionary<TileType, TileDefinition> tileDefinitions)
        {
            int height = mapDto.Tiles.Count;
            int width = height > 0 ? mapDto.Tiles.Select(x => x.Count).Max() : 0;

            if (height <= 0)
            {
                throw new ArgumentException("MapDto must contain at least one row of tiles.");
            }
            if (width <= 0)
            {
                throw new ArgumentException("At least one row in MapDto must contain at least one tile.");
            }

            this.Tiles = new ITile[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    TileType type = mapDto.Tiles[i].ElementAtOrDefault(j);
                    if (tileDefinitions.TryGetValue(type, out TileDefinition? definition) && definition is not null)
                    {
                        this.Tiles[i, j] = new Tile(definition);
                    }
                    else
                    {
                        throw new ArgumentException($"Tile definition for type {type} not found.");
                    }
                }
            }
        }

        public Map(ITile[,] tiles)
        {
            this.Tiles = tiles;
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    sb.Append(this.GetTile(x, y).PrintChar);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
