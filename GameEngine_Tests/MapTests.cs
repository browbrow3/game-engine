using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using Moq;

namespace GameEngine_Tests
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

        private readonly Map map;

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

            this.map = new Map(tiles);
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

            Map result = new Map(mapDto, this.tileDefinitions);

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

            Map result = new Map(mapDto, this.tileDefinitions);

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

            Map result = new Map(mapDto, this.tileDefinitions);

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

            Assert.Throws<ArgumentException>(() => new Map(mapDto, this.tileDefinitions));
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

            Assert.Throws<ArgumentException>(() => new Map(mapDto, this.tileDefinitions));
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

            Assert.Throws<ArgumentException>(() => new Map(mapDto, this.tileDefinitions));
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
            Map map = new Map(
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
                .Should().Throw<ArgumentOutOfRangeException>().WithMessage("Coordinates are out of bounds.");
        }
        #endregion
        #region IsTilePassable Tests

        [Fact]
        public void IsTilePassable_ReturnsCorrectPassability()
        {
            Map map = new Map(
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
            Map map = new Map(
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
                .Should().Throw<ArgumentOutOfRangeException>().WithMessage("Coordinates are out of bounds.");
        }
        #endregion
        #region AddEntityToTile Tests
        [Fact]
        public void AddEntityToTile_ReturnsCorrectTile()
        {
            this.mockTiles[1, 2].Setup(x => x.Passable).Returns(false);
            this.mockTiles[1, 1].Setup(x => x.Passable).Returns(true);

            map.IsTilePassable(2, 1).Should().BeFalse();
            map.IsTilePassable(1, 1).Should().BeTrue();

            this.mockTiles[1, 2].VerifyAll();
            this.mockTiles[1, 1].VerifyAll();
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
            this.map.Invoking(m => m.IsTilePassable(x, y))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Coordinates are out of bounds.");
        }
        #endregion
    }
}
