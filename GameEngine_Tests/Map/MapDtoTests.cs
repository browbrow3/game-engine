using FluentAssertions;
using GameEngine.Entities;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using System.Text.Json;

namespace GameEngine_Tests.Map
{
    public class MapDtoTests
    {

        [Fact]
        public void MapDto_CanDeserializeFromJson()
        {
            string json = "{\"Tiles\": [[ 2, 2, 2 ],[ 2, 1, 1 ],[ 2, 1, 1 ]],\"Entities\": [{\"EntityType\": 1,\"X\": 1,\"Y\": 0},{\"EntityType\": 2,\"X\": 2,\"Y\": 2}]}";
            MapDto mapDto = JsonSerializer.Deserialize<MapDto>(json, MapDtoSerializerContext.Default.MapDto)
                ?? throw new InvalidOperationException($"Failed to deserialize {typeof(MapDto).Name}.");
            mapDto.Should().NotBeNull();
            mapDto.Tiles.Should().NotBeNull();
            mapDto.Entities.Should().NotBeNull();

            mapDto.Tiles.Count.Should().Be(3);
            mapDto.Tiles.Should().AllSatisfy(x => x.Count.Should().Be(3));
            mapDto.Tiles[0][0].Should().Be(TileType.Water);
            mapDto.Tiles[0][1].Should().Be(TileType.Water);
            mapDto.Tiles[0][2].Should().Be(TileType.Water);
            mapDto.Tiles[1][0].Should().Be(TileType.Water);
            mapDto.Tiles[1][1].Should().Be(TileType.Grass);
            mapDto.Tiles[1][2].Should().Be(TileType.Grass);
            mapDto.Tiles[2][0].Should().Be(TileType.Water);
            mapDto.Tiles[2][1].Should().Be(TileType.Grass);
            mapDto.Tiles[2][2].Should().Be(TileType.Grass);

            mapDto.Entities.Count.Should().Be(2);
            mapDto.Entities[0].EntityType.Should().Be(EntityType.Player);
            mapDto.Entities[0].X.Should().Be(1);
            mapDto.Entities[0].Y.Should().Be(0);
            mapDto.Entities[1].EntityType.Should().Be(EntityType.Enemy);
            mapDto.Entities[1].X.Should().Be(2);
            mapDto.Entities[1].Y.Should().Be(2);
        }
    }
}
