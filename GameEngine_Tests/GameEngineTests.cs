using GameEngine.Entities;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameEngine_Tests
{
    // Note that since the game engine itself doesn't really do anything at the moment, tests in this class are really just stubs for now.
    public class GameEngineTests
    {
        private readonly Dictionary<TileType, TileDefinition> tileDefinitions = new Dictionary<TileType, TileDefinition>
        {
            { TileType.Default, new TileDefinition() { Type = TileType.Default, Name = "Default", Tangiable = false, SpeedMultiplier = 1.0f, StatusEffects = 0, PrintChar = '.' } },
            { TileType.Grass, new TileDefinition() { Type = TileType.Grass, Name = "Grass", Tangiable = false, SpeedMultiplier = 1.0f, StatusEffects = 0, PrintChar = ',' } },
            { TileType.Water, new TileDefinition() { Type = TileType.Water, Name = "Water", Tangiable = true, SpeedMultiplier = 0.5f, StatusEffects = 1, PrintChar = '~' } },
            { TileType.Stone, new TileDefinition() { Type = TileType.Stone, Name = "Stone", Tangiable = false, SpeedMultiplier = 1.5f, StatusEffects = 0, PrintChar = '#' } },
            { TileType.StoneWall, new TileDefinition() { Type = TileType.StoneWall, Name = "Stone Wall", Tangiable = true, SpeedMultiplier = 0.0f, StatusEffects = 2, PrintChar = '|' } }
        };

        private readonly Dictionary<EntityType, EntityDefinition> entityDefinitions = new Dictionary<EntityType, EntityDefinition>
        {
            { EntityType.Player, new EntityDefinition() { Type = EntityType.Player, Name = "Player", Health = 100, Speed = 1 } },
            { EntityType.Enemy, new EntityDefinition() { Type = EntityType.Enemy, Name = "Enemy", Health = 50, Speed = 1 } },
            { EntityType.Item, new EntityDefinition() { Type = EntityType.Item, Name = "Item", Health = 0, Speed = 0 } }
        };

        private readonly MapDto mapDto = new MapDto()
        {
            Tiles = [
                [TileType.Stone, TileType.Grass, TileType.Water, TileType.Water],
                [TileType.Grass, TileType.Stone, TileType.Grass, TileType.Stone],
                [TileType.Stone, TileType.Water, TileType.Grass, TileType.Stone],
                [TileType.StoneWall, TileType.StoneWall, TileType.StoneWall, TileType.Grass]
            ]
        };

        private readonly Mock<ILogger<GameEngine.GameEngine>> mockLogger = new Mock<ILogger<GameEngine.GameEngine>>(MockBehavior.Loose);

        #region InitializeAsync Tests
        [Fact]
        public async Task InitializeAsync_InitializesGameEngine()
        {
            GameEngine.GameEngine engine = new GameEngine.GameEngine(
                this.tileDefinitions,
                this.entityDefinitions,
                this.mapDto,
                this.mockLogger.Object
            );

            await engine.InitializeAsync();

            Assert.True(true);
        }
        #endregion

        #region Start Tests
        [Fact]
        public async Task Start_BeginsGameLoop_Stop_StopsGameLoop()
        {
            GameEngine.GameEngine engine = new GameEngine.GameEngine(
                this.tileDefinitions,
                this.entityDefinitions,
                this.mapDto,
                this.mockLogger.Object
            );

            await engine.InitializeAsync();

            Task gameFinished = engine.Start();

            await engine.Stop();
            await gameFinished;

            Assert.True(true);
        }
        #endregion
    }
}

