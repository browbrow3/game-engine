using FluentAssertions;
using GameEngine.Map.Tiles;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameEngine_Tests
{
    public class UnitTest1
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

        [Fact]
        public void TileDefinition_CanDeserializeFromJson_2()
        {
            string json = "{\"type\":1,\"name\":\"Grass\",\"tangiable\":true,\"speedMultiplier\":4.0,\"statusEffects\":3,\"printChar\":\",\"}";
            TileDefinition? tileDefinition = JsonSerializer.Deserialize<TileDefinition>(json);
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
