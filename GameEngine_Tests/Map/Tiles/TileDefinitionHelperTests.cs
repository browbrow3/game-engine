using FluentAssertions;
using GameEngine.Map.Tiles;
using System.Text.Json;

namespace GameEngine_Tests.Map.Tiles
{
    public class TileDefinitionHelperTests
    {
        #region GetDefinitions Tests
        [Fact]
        public async Task GetDefinitions_BadPath_ThrowsArgumentException()
        {
            Func<Task> action = async () => await TileDefinitionHelper.GetDefinitions("./Resources/bad.json");
            await action.Should().ThrowExactlyAsync<FileNotFoundException>();
        }

        [Fact]
        public async Task GetDefinitions_BadJson_ThrowsArgumentException()
        {
            Func<Task> action = async () => await TileDefinitionHelper.GetDefinitions("./Resources/TileDefinitions_Bad.json");
            await action.Should().ThrowExactlyAsync<JsonException>();
        }

        [Fact]
        public async Task GetDefinitions_ValidJson_ReturnsDefinitions()
        {
            Dictionary<TileType, TileDefinition> definitions = (await TileDefinitionHelper.GetDefinitions("./Resources/TileDefinitions.json"));
            definitions.Should().HaveCount(5);
            definitions.Should().ContainKey(TileType.Default);
            definitions.Should().ContainKey(TileType.Grass);
            definitions.Should().ContainKey(TileType.Water);
            definitions.Should().ContainKey(TileType.Stone);
            definitions.Should().ContainKey(TileType.StoneWall);

            definitions[TileType.Default].Type.Should().Be(TileType.Default);
            definitions[TileType.Default].Name.Should().Be("default");
            definitions[TileType.Default].Tangiable.Should().Be(true);
            definitions[TileType.Default].SpeedMultiplier.Should().Be(0.0f);
            definitions[TileType.Default].StatusEffects.Should().Be(0);
            definitions[TileType.Default].PrintChar.Should().Be('?');

            definitions[TileType.Grass].Type.Should().Be(TileType.Grass);
            definitions[TileType.Grass].Name.Should().Be("grass");
            definitions[TileType.Grass].Tangiable.Should().Be(false);
            definitions[TileType.Grass].SpeedMultiplier.Should().Be(1.0f);
            definitions[TileType.Grass].StatusEffects.Should().Be(0);
            definitions[TileType.Grass].PrintChar.Should().Be(' ');

            definitions[TileType.Water].Type.Should().Be(TileType.Water);
            definitions[TileType.Water].Name.Should().Be("water");
            definitions[TileType.Water].Tangiable.Should().Be(true);
            definitions[TileType.Water].SpeedMultiplier.Should().Be(0.0f);
            definitions[TileType.Water].StatusEffects.Should().Be(0);
            definitions[TileType.Water].PrintChar.Should().Be('#');

            definitions[TileType.Stone].Type.Should().Be(TileType.Stone);
            definitions[TileType.Stone].Name.Should().Be("stone");
            definitions[TileType.Stone].Tangiable.Should().Be(false);
            definitions[TileType.Stone].SpeedMultiplier.Should().Be(1.0f);
            definitions[TileType.Stone].StatusEffects.Should().Be(0);
            definitions[TileType.Stone].PrintChar.Should().Be('.');

            definitions[TileType.StoneWall].Type.Should().Be(TileType.StoneWall);
            definitions[TileType.StoneWall].Name.Should().Be("stonewall");
            definitions[TileType.StoneWall].Tangiable.Should().Be(false);
            definitions[TileType.StoneWall].SpeedMultiplier.Should().Be(1.0f);
            definitions[TileType.StoneWall].StatusEffects.Should().Be(0);
            definitions[TileType.StoneWall].PrintChar.Should().Be('~');
        }

        [Fact]
        public async Task GetDefinitions_DuplicateKey_LatterDefinitionUsed()
        {
            Dictionary<TileType, TileDefinition> definitions = (await TileDefinitionHelper.GetDefinitions("./Resources/TileDefinitions_DuplicateKeys.json"));
            definitions.Should().HaveCount(5);
            definitions.Should().ContainKey(TileType.Default);
            definitions.Should().ContainKey(TileType.Grass);
            definitions.Should().ContainKey(TileType.Water);
            definitions.Should().ContainKey(TileType.Stone);
            definitions.Should().ContainKey(TileType.StoneWall);

            definitions[TileType.Default].Type.Should().Be(TileType.Default);
            definitions[TileType.Default].Name.Should().Be("default");
            definitions[TileType.Default].Tangiable.Should().Be(true);
            definitions[TileType.Default].SpeedMultiplier.Should().Be(0.0f);
            definitions[TileType.Default].StatusEffects.Should().Be(0);
            definitions[TileType.Default].PrintChar.Should().Be('?');

            definitions[TileType.Grass].Type.Should().Be(TileType.Grass);
            definitions[TileType.Grass].Name.Should().Be("extra smelly grass");
            definitions[TileType.Grass].Tangiable.Should().Be(true);
            definitions[TileType.Grass].SpeedMultiplier.Should().Be(0.1f);
            definitions[TileType.Grass].StatusEffects.Should().Be(10);
            definitions[TileType.Grass].PrintChar.Should().Be('$');

            definitions[TileType.Water].Type.Should().Be(TileType.Water);
            definitions[TileType.Water].Name.Should().Be("water");
            definitions[TileType.Water].Tangiable.Should().Be(true);
            definitions[TileType.Water].SpeedMultiplier.Should().Be(0.0f);
            definitions[TileType.Water].StatusEffects.Should().Be(0);
            definitions[TileType.Water].PrintChar.Should().Be('#');

            definitions[TileType.Stone].Type.Should().Be(TileType.Stone);
            definitions[TileType.Stone].Name.Should().Be("stone");
            definitions[TileType.Stone].Tangiable.Should().Be(false);
            definitions[TileType.Stone].SpeedMultiplier.Should().Be(1.0f);
            definitions[TileType.Stone].StatusEffects.Should().Be(0);
            definitions[TileType.Stone].PrintChar.Should().Be('.');

            definitions[TileType.StoneWall].Type.Should().Be(TileType.StoneWall);
            definitions[TileType.StoneWall].Name.Should().Be("stonewall");
            definitions[TileType.StoneWall].Tangiable.Should().Be(false);
            definitions[TileType.StoneWall].SpeedMultiplier.Should().Be(1.0f);
            definitions[TileType.StoneWall].StatusEffects.Should().Be(0);
            definitions[TileType.StoneWall].PrintChar.Should().Be('~');
        }
        #endregion
    }
}
