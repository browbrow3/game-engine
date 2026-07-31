using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using Microsoft.Extensions.Logging;

namespace GameEngine
{
    public class GameEngine(
        Dictionary<TileType, TileDefinition> tileDefinitions,
        Dictionary<EntityType, EntityDefinition> entityDefinitions,
        MapDto mapDto,
        ILogger<GameEngine> logger
    ) : IGameEngine
    {
        public Queue<IEvent> EventQueue { get; private set; } = new Queue<IEvent>();
        
        // should this be injected like TileDefinitions and EntityDefinitions?
        // It does seem a little daft for the GameEngine to be responsible for creating the Map.
        public IMap Map { get; private set; } = new Map.Map(mapDto, tileDefinitions);

        public Dictionary<TileType, TileDefinition> TileDefinitions { get; init; } = tileDefinitions;
        public Dictionary<EntityType, EntityDefinition> EntityDefinitions { get; init; } = entityDefinitions;

        private readonly ILogger<GameEngine> logger = logger;

        /// <inheritdoc/>
        public async Task InitializeAsync()
        {
            this.logger.LogDebug("Starting game engine");
            // Implementation for initializing the game engine
        }

        /// <inheritdoc/>
        public async Task Start()
        {
            this.logger.LogDebug("Starting game engine");
            // Implementation for starting the game engine
        }

        /// <inheritdoc/>
        public async Task Stop()
        {
            this.logger.LogDebug("Stopping game engine");
            // Implementation for stopping the game engine
        }
    }
}
