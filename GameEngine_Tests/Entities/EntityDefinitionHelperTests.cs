using FluentAssertions;
using GameEngine.Entities;
using System.Text.Json;

namespace GameEngine_Tests.Entities
{
    public class EntityDefinitionHelperTests
    {
        #region GetDefinitions Tests
        [Fact]
        public async Task GetDefinitions_BadPath_ThrowsArgumentException()
        {
            Func<Task> action = async () => await EntityDefinitionHelper.GetDefinitions("./Resources/bad.json");
            await action.Should().ThrowExactlyAsync<FileNotFoundException>();
        }

        [Fact]
        public async Task GetDefinitions_BadJson_ThrowsArgumentException()
        {
            Func<Task> action = async () => await EntityDefinitionHelper.GetDefinitions("./Resources/EntityDefinitions_Bad.json");
            await action.Should().ThrowExactlyAsync<JsonException>();
        }

        [Fact]
        public async Task GetDefinitions_DuplicateKey_LatterDefinitionUsed()
        {
            Dictionary<EntityType, EntityDefinition> definitions = (await EntityDefinitionHelper.GetDefinitions("./Resources/EntityDefinitions_DuplicateKey.json"));
            definitions.Should().HaveCount(3);
            definitions.Should().ContainKey(EntityType.Player);
            definitions.Should().ContainKey(EntityType.Enemy);
            definitions.Should().ContainKey(EntityType.Item);

            definitions[EntityType.Player].Type.Should().Be(EntityType.Player);
            definitions[EntityType.Player].Name.Should().Be("player");
            definitions[EntityType.Player].Alive.Should().Be(true);
            definitions[EntityType.Player].Health.Should().Be(10);
            definitions[EntityType.Player].Attack.Should().Be(1);
            definitions[EntityType.Player].Defence.Should().Be(1);
            definitions[EntityType.Player].Arcana.Should().Be(1);
            definitions[EntityType.Player].Esotericism.Should().Be(1);
            definitions[EntityType.Player].Vulnerable.Should().Be(true);
            definitions[EntityType.Player].Tangiable.Should().Be(true);
            definitions[EntityType.Player].Speed.Should().Be(1);
            definitions[EntityType.Player].Stamina.Should().Be(10);

            definitions[EntityType.Enemy].Type.Should().Be(EntityType.Enemy);
            definitions[EntityType.Enemy].Name.Should().Be("enemy_duplicate");
            definitions[EntityType.Enemy].Alive.Should().Be(false);
            definitions[EntityType.Enemy].Health.Should().Be(30);
            definitions[EntityType.Enemy].Attack.Should().Be(10);
            definitions[EntityType.Enemy].Defence.Should().Be(7);
            definitions[EntityType.Enemy].Arcana.Should().Be(9);
            definitions[EntityType.Enemy].Esotericism.Should().Be(4);
            definitions[EntityType.Enemy].Vulnerable.Should().Be(false);
            definitions[EntityType.Enemy].Tangiable.Should().Be(false);
            definitions[EntityType.Enemy].Speed.Should().Be(10);
            definitions[EntityType.Enemy].Stamina.Should().Be(30);

            definitions[EntityType.Item].Type.Should().Be(EntityType.Item);
            definitions[EntityType.Item].Name.Should().Be("item");
            definitions[EntityType.Item].Alive.Should().Be(false);
            definitions[EntityType.Item].Health.Should().Be(1);
            definitions[EntityType.Item].Attack.Should().Be(0);
            definitions[EntityType.Item].Defence.Should().Be(0);
            definitions[EntityType.Item].Arcana.Should().Be(0);
            definitions[EntityType.Item].Esotericism.Should().Be(0);
            definitions[EntityType.Item].Vulnerable.Should().Be(false);
            definitions[EntityType.Item].Tangiable.Should().Be(true);
            definitions[EntityType.Item].Speed.Should().Be(0);
            definitions[EntityType.Item].Stamina.Should().Be(0);
        }

        [Fact]
        public async Task GetDefinitions_ValidJson_ReturnsDefinitions()
        {
            Dictionary<EntityType, EntityDefinition> definitions = (await EntityDefinitionHelper.GetDefinitions("./Resources/EntityDefinitions.json"));
            definitions.Should().HaveCount(3);
            definitions.Should().ContainKey(EntityType.Player);
            definitions.Should().ContainKey(EntityType.Enemy);
            definitions.Should().ContainKey(EntityType.Item);

            definitions[EntityType.Player].Type.Should().Be(EntityType.Player);
            definitions[EntityType.Player].Name.Should().Be("player");
            definitions[EntityType.Player].Alive.Should().Be(true);
            definitions[EntityType.Player].Health.Should().Be(10);
            definitions[EntityType.Player].Attack.Should().Be(1);
            definitions[EntityType.Player].Defence.Should().Be(1);
            definitions[EntityType.Player].Arcana.Should().Be(1);
            definitions[EntityType.Player].Esotericism.Should().Be(1);
            definitions[EntityType.Player].Vulnerable.Should().Be(true);
            definitions[EntityType.Player].Tangiable.Should().Be(true);
            definitions[EntityType.Player].Speed.Should().Be(1);
            definitions[EntityType.Player].Stamina.Should().Be(10);

            definitions[EntityType.Enemy].Type.Should().Be(EntityType.Enemy);
            definitions[EntityType.Enemy].Name.Should().Be("enemy");
            definitions[EntityType.Enemy].Alive.Should().Be(true);
            definitions[EntityType.Enemy].Health.Should().Be(20);
            definitions[EntityType.Enemy].Attack.Should().Be(8);
            definitions[EntityType.Enemy].Defence.Should().Be(6);
            definitions[EntityType.Enemy].Arcana.Should().Be(6);
            definitions[EntityType.Enemy].Esotericism.Should().Be(3);
            definitions[EntityType.Enemy].Vulnerable.Should().Be(true);
            definitions[EntityType.Enemy].Tangiable.Should().Be(true);
            definitions[EntityType.Enemy].Speed.Should().Be(3);
            definitions[EntityType.Enemy].Stamina.Should().Be(20);

            definitions[EntityType.Item].Type.Should().Be(EntityType.Item);
            definitions[EntityType.Item].Name.Should().Be("item");
            definitions[EntityType.Item].Alive.Should().Be(false);
            definitions[EntityType.Item].Health.Should().Be(1);
            definitions[EntityType.Item].Attack.Should().Be(0);
            definitions[EntityType.Item].Defence.Should().Be(0);
            definitions[EntityType.Item].Arcana.Should().Be(0);
            definitions[EntityType.Item].Esotericism.Should().Be(0);
            definitions[EntityType.Item].Vulnerable.Should().Be(false);
            definitions[EntityType.Item].Tangiable.Should().Be(true);
            definitions[EntityType.Item].Speed.Should().Be(0);
            definitions[EntityType.Item].Stamina.Should().Be(0);
        }
        #endregion
    }
}

