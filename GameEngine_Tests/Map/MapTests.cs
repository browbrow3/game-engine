using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using Moq;

namespace GameEngine_Tests.Map
{
    public class MapTests
    {
        private readonly Dictionary<TileType, TileDefinition> tileDefinitions =
            new Dictionary<TileType, TileDefinition>
            {
                { TileType.Default, new TileDefinition() { Type = TileType.Default, Name = "Default", Tangiable = true, SpeedMultiplier = 0.0F, StatusEffects = 0, PrintChar = '?' } },
                { TileType.Grass, new TileDefinition() { Type = TileType.Grass, Name = "Grass", Tangiable = false, SpeedMultiplier = 1.0F, StatusEffects = 0, PrintChar = '"' } },
                { TileType.Water, new TileDefinition() { Type = TileType.Water, Name = "Water", Tangiable = false, SpeedMultiplier = 0.0F, StatusEffects = 0, PrintChar = '~' } },
                { TileType.Stone, new TileDefinition() { Type = TileType.Stone, Name = "Stone", Tangiable = false, SpeedMultiplier = 0.5F, StatusEffects = 0, PrintChar = 'o' } },
                { TileType.StoneWall, new TileDefinition() { Type = TileType.StoneWall, Name = "Stone Wall", Tangiable = true, SpeedMultiplier = 0.0F, StatusEffects = 0, PrintChar = '#' } },
            };

        private readonly Mock<ITile>[,] mockTiles;

        private readonly GameEngine.Map.Map map;

        public MapTests()
        {
            this.mockTiles = new Mock<ITile>[,]
            {
                { new Mock<ITile>(), new Mock<ITile>(), new Mock<ITile>() },
                { new Mock<ITile>(), new Mock<ITile>(), new Mock<ITile>() }
            };

            ITile[,] tiles = new ITile[this.mockTiles.GetLength(0), this.mockTiles.GetLength(1)];

            for (int i = 0; i < this.mockTiles.GetLength(0); i++)
            {
                for (int j = 0; j < this.mockTiles.GetLength(1); j++)
                {
                    tiles[i, j] = this.mockTiles[i, j].Object;
                }
            }

            this.map = new GameEngine.Map.Map(tiles);
        }

        #region Constructor Tests
        [Fact]
        public void Constructor_ConstructsMapFromMapDto()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = [
                    [TileType.Stone, TileType.Grass, TileType.Water],
                    [TileType.Grass, TileType.Stone, TileType.Grass],
                    [TileType.Stone, TileType.Water, TileType.Grass]
                ]
            };

            GameEngine.Map.Map result = new GameEngine.Map.Map(mapDto, this.tileDefinitions);

            result.Tiles.Should().BeEquivalentTo(new ITile[,]
            {
                { new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Water]) },
                { new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Grass]) },
                { new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Water]), new Tile(this.tileDefinitions[TileType.Grass]) }
            });
        }

        [Fact]
        public void Constructor_MapDtoJagged_UnpopulatedTilesDefaultedToRight()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = [
                    [TileType.Stone, TileType.Grass, TileType.Water],
                    [TileType.Grass, TileType.Grass],
                    [TileType.Grass]
                ]
            };

            GameEngine.Map.Map result = new GameEngine.Map.Map(mapDto, this.tileDefinitions);

            result.Tiles.Should().BeEquivalentTo(new ITile[,]
            {
                { new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Water]) },
                { new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Default]) },
                { new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Default]), new Tile(this.tileDefinitions[TileType.Default]) }
            });
        }

        [Fact]
        public void Constructor_MapDtoEmptyRow_UnpopulatedRowDefaulted()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = [
                    [TileType.Stone, TileType.Grass, TileType.Water],
                    [TileType.Grass, TileType.Stone, TileType.Grass],
                    []
                ]
            };

            GameEngine.Map.Map result = new GameEngine.Map.Map(mapDto, this.tileDefinitions);

            result.Tiles.Should().BeEquivalentTo(new ITile[,]
            {
                { new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Water]) },
                { new Tile(this.tileDefinitions[TileType.Grass]), new Tile(this.tileDefinitions[TileType.Stone]), new Tile(this.tileDefinitions[TileType.Grass]) },
                { new Tile(this.tileDefinitions[TileType.Default]), new Tile(this.tileDefinitions[TileType.Default]), new Tile(this.tileDefinitions[TileType.Default]) }
            });
        }

        [Fact]
        public void Constructor_NoHeight_ThrowsArgumentException()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = []
            };

            Assert.Throws<ArgumentException>(() => new GameEngine.Map.Map(mapDto, this.tileDefinitions));
        }

        [Fact]
        public void Constructor_NoWidth_ThrowsArgumentException()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = [
                    [],
                    [],
                    []
                ]
            };

            Assert.Throws<ArgumentException>(() => new GameEngine.Map.Map(mapDto, this.tileDefinitions));
        }

        [Fact]
        public void Constructor_InvalidTile_ThrowsArgumentException()
        {
            MapDto mapDto = new MapDto()
            {
                Tiles = [
                    [TileType.Stone, TileType.Grass, TileType.Water],
                    [TileType.Grass, TileType.Stone, TileType.Grass],
                    [TileType.Stone, TileType.Water, (TileType)8]
                ]
            };

            Assert.Throws<ArgumentException>(() => new GameEngine.Map.Map(mapDto, this.tileDefinitions));
        }
        #endregion
        #region Width and Height Tests
        [Fact]
        public void WidthAndHeight_ReturnsCorrectDimensions()
        {
            map.Width.Should().Be(3);
            map.Height.Should().Be(2);
        }
        #endregion
        #region GetTile Tests
        [Fact]
        public void GetTile_ReturnsCorrectTile()
        {
            map.GetTile(2, 1).Should().Be(this.mockTiles[1,2].Object);
        }

        [Theory]
        [InlineData(-1,-1)]
        [InlineData(2,-1)]
        [InlineData(2,3)]
        [InlineData(-1,2)]
        [InlineData(3,2)]
        [InlineData(3,3)]
        public void GetTile_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y)
        {
            GameEngine.Map.Map map = new GameEngine.Map.Map(
                new MapDto()
                {
                    Tiles = [
                        [TileType.Stone, TileType.Grass, TileType.Water],
                        [TileType.Grass, TileType.Stone, TileType.StoneWall],
                        [TileType.Stone, TileType.Water, TileType.Grass]
                    ]
                },
                this.tileDefinitions
            );

            map.Invoking(m => m.GetTile(x, y))
                .Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
        #region IsTilePassable Tests

        [Fact]
        public void IsTilePassable_ReturnsCorrectPassability()
        {
            GameEngine.Map.Map map = new GameEngine.Map.Map(
                new MapDto()
                {
                    Tiles = [
                        [TileType.Stone, TileType.Grass, TileType.Water],
                        [TileType.Grass, TileType.Stone, TileType.StoneWall],
                        [TileType.Stone, TileType.Water, TileType.Grass]
                    ]
                },
                this.tileDefinitions
            );

            map.IsTilePassable(2, 1).Should().BeFalse();
            map.IsTilePassable(1, 1).Should().BeTrue();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(2, -1)]
        [InlineData(2, 3)]
        [InlineData(-1, 2)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void IsTilePassable_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y)
        {
            GameEngine.Map.Map map = new GameEngine.Map.Map(
                new MapDto()
                {
                    Tiles = [
                        [TileType.Stone, TileType.Grass, TileType.Water],
                        [TileType.Grass, TileType.Stone, TileType.StoneWall],
                        [TileType.Stone, TileType.Water, TileType.Grass]
                    ]
                },
                this.tileDefinitions
            );

            map.Invoking(m => m.IsTilePassable(x, y))
                .Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
        #region AddEntityToTile Tests
        [Fact]
        public void AddEntityToTile_EntityAddedToCorrectTile_ReturnsSuccess()
        {
            IEntity entity = new Entity();

            this.mockTiles[1, 2].Setup(x => x.AddEntity((IEntity)entity)).Returns(0);

            this.map.AddEntityToTile(2, 1, (IEntity)entity).Should().Be(0);

            this.mockTiles[1, 2].VerifyAll();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(2, -1)]
        [InlineData(2, 3)]
        [InlineData(-1, 2)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void AddEntityToTile_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y)
        {
            this.map.Invoking(m => m.AddEntityToTile(x, y, (IEntity)new Entity()))
                .Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
        #region RemoveEntityFromTile Tests
        [Fact]
        public void RemoveEntityFromTile_RemovesEntityFromCorrectTile_ReturnsSuccess()
        {
            Entity entity = new Entity();

            this.mockTiles[1, 2].Setup(x => x.RemoveEntity((IEntity)entity)).Returns(0);

            this.map.RemoveEntityFromTile(2, 1, (IEntity)entity).Should().Be(0);

            this.mockTiles[1, 2].VerifyAll();
        }

        [Theory]
        [InlineData(-1, -1, "x")]
        [InlineData(2, -1, "y")]
        [InlineData(2, 3, "y")]
        [InlineData(-1, 2, "x")]
        [InlineData(3, 2, "x")]
        [InlineData(3, 3, "x")]
        public void RemoveEntityFromTile_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y, string paramName)
        {
            this.map.Invoking(m => m.RemoveEntityFromTile(x, y, (IEntity)new Entity()))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Coordinates are out of bounds. (Parameter '{paramName}')");
        }
        #endregion
        #region GetEntitiesInTile Tests
        [Fact]
        public void GetEntitiesInTile_ReturnsListOfEntities()
        {
            List<IEntity> entities = new List<IEntity> 
            {
                (IEntity)new Entity(),
                (IEntity)new Entity()
            };

            this.mockTiles[1, 2].Setup(x => x.GetEntities()).Returns(entities);

            this.map.GetEntitiesInTile(2, 1).Should().BeEquivalentTo(entities);

            this.mockTiles[1, 2].VerifyAll();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(2, -1)]
        [InlineData(2, 3)]
        [InlineData(-1, 2)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void GetEntitiesInTile_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y)
        {
            this.map.Invoking(m => m.GetEntitiesInTile(x, y))
                .Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
        #region PublishEventToTile Tests
        [Fact]
        public void PublishEventToTile_ReturnsSuccess()
        {
            Event e = new Event(EventType.Input);

            this.mockTiles[1, 2].Setup(x => x.PublishEvent(e)).Returns(0);

            this.map.PublishEventToTile(2, 1, e).Should().Be(0);

            this.mockTiles[1, 2].VerifyAll();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(2, -1)]
        [InlineData(2, 3)]
        [InlineData(-1, 2)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void PublishEventToTile_IndexOutOfRange_ThrowsArgumentOutOfRange(int x, int y)
        {
            this.map.Invoking(m => m.PublishEventToTile(x, y, new Event(EventType.Input)))
                .Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        #endregion
        #region Print Tests
        [Fact]
        public void Print_ReturnsStringRepresentationOfMap()
        {
            GameEngine.Map.Map map = new GameEngine.Map.Map(
                new MapDto()
                {
                    Tiles = [
                        [TileType.Stone, TileType.Grass, TileType.Water],
                        [TileType.Grass, TileType.Stone, TileType.StoneWall]
                    ]
                },
                this.tileDefinitions
            );

            map.Print().Should().Be("o\"~\r\n\"o#\r\n");
        }
        #endregion
    }
}
