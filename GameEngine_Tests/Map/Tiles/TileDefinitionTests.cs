using FluentAssertions;
using GameEngine.Map.Tiles;
using System.Text.Json;

namespace GameEngine_Tests.Map.Tiles
{
    public class TileDefinitionTests
    {
        [Fact]
        public void TileDefinition_CanDeserializeFromJson()
        {
            string json = "{\"Type\":1,\"Name\":\"Grass\",\"Tangiable\":true,\"SpeedMultiplier\":4.0,\"StatusEffects\":3,\"PrintChar\":\",\"}";
            TileDefinition? tileDefinition = JsonSerializer.Deserialize<TileDefinition>(json, TileDefinitionSerializerContext.Default.TileDefinition);
            tileDefinition.Should().NotBeNull();
            tileDefinition.Should().BeEquivalentTo(
            new TileDefinition
            {
                Type = TileType.Grass,
                Name = "Grass",
                Tangiable = true,
                SpeedMultiplier = 4.0f,
                StatusEffects = 3,
                PrintChar = ','
            });
        }
    }
}
