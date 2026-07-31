using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using System.Text.Json;

namespace GameEngine_Tests.Map
{
    public class MapHelperTests
    {
        #region GetDefinitions Tests
        [Fact]
        public async Task GetDefinitions_BadPath_ThrowsArgumentException()
        {
            Func<Task> action = async () => await MapHelper.GetMap("./Resources/bad.json");
            await action.Should().ThrowExactlyAsync<FileNotFoundException>();
        }

        [Fact]
        public async Task GetDefinitions_BadJson_ThrowsArgumentException()
        {
            Func<Task> action = async () => await MapHelper.GetMap("./Resources/Map_Bad.json");
            await action.Should().ThrowExactlyAsync<JsonException>();
        }

        [Fact]
        public async Task GetDefinitions_NoTiles_ReturnsMapDtoWithEmptyTiles()
        {
            Func<Task> action = async () => await MapHelper.GetMap("./Resources/Map_NoTiles.json");
            await action.Should().ThrowExactlyAsync<JsonException>();
        }

        [Fact]
        public async Task GetDefinitions_NoEntities_ReturnsMapDtoWithNullEntities()
        {
            MapDto map = await MapHelper.GetMap("./Resources/Map_NoEntities.json");

            map.Tiles.Should().NotBeNull();
            map.Entities.Should().BeNull();

            map.Tiles.Count.Should().Be(3);
            map.Tiles.Should().AllSatisfy(x => x.Count.Should().Be(3));
            map.Tiles[0][0].Should().Be(TileType.Water);
            map.Tiles[0][1].Should().Be(TileType.Water);
            map.Tiles[0][2].Should().Be(TileType.Water);
            map.Tiles[1][0].Should().Be(TileType.Water);
            map.Tiles[1][1].Should().Be(TileType.Grass);
            map.Tiles[1][2].Should().Be(TileType.Grass);
            map.Tiles[2][0].Should().Be(TileType.Water);
            map.Tiles[2][1].Should().Be(TileType.Grass);
            map.Tiles[2][2].Should().Be(TileType.Grass);
        }

        [Fact]
        public async Task GetDefinitions_ValidJson_ReturnsMapDto()
        {
            MapDto map = await MapHelper.GetMap("./Resources/Map.json");

            map.Tiles.Should().NotBeNull();
            map.Entities.Should().NotBeNull();

            map.Tiles.Count.Should().Be(3);
            map.Tiles.Should().AllSatisfy(x => x.Count.Should().Be(3));
            map.Tiles[0][0].Should().Be(TileType.Water);
            map.Tiles[0][1].Should().Be(TileType.Water);
            map.Tiles[0][2].Should().Be(TileType.Water);
            map.Tiles[1][0].Should().Be(TileType.Water);
            map.Tiles[1][1].Should().Be(TileType.Grass);
            map.Tiles[1][2].Should().Be(TileType.Grass);
            map.Tiles[2][0].Should().Be(TileType.Water);
            map.Tiles[2][1].Should().Be(TileType.Grass);
            map.Tiles[2][2].Should().Be(TileType.Grass);

            map.Entities.Count.Should().Be(2);
            map.Entities[0].EntityType.Should().Be(EntityType.Player);
            map.Entities[0].X.Should().Be(1);
            map.Entities[0].Y.Should().Be(0);
            map.Entities[1].EntityType.Should().Be(EntityType.Enemy);
            map.Entities[1].X.Should().Be(2);
            map.Entities[1].Y.Should().Be(2);
        }
        #endregion
    }
}
