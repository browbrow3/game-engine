using FluentAssertions;
using GameEngine.Entities;
using System.Text.Json;

namespace GameEngine_Tests.Entities
{
    public class EntityDefinitionTests
    {

        [Fact]
        public void EntityDefinition_CanDeserializeFromJson()
        {
            string json = "{\"Type\": 1,\"Name\": \"player\",\"Alive\": true,\"Health\": 10,\"Attack\": 1,\"Defence\": 1,\"Arcana\": 1,\"Esotericism\": 1,\"Vulnerable\": true,\"Tangiable\": true,\"Speed\": 1,\"SpriteResolution\": 16,\"Stamina\": 10}";
            EntityDefinition entityDefinition = JsonSerializer.Deserialize<EntityDefinition>(json, EntityDefinitionSerializerContext.Default.EntityDefinition)
                ?? throw new InvalidOperationException($"Failed to deserialize {typeof(EntityDefinition).Name}.");
            entityDefinition.Should().NotBeNull();
            entityDefinition.Should().BeEquivalentTo(
                new EntityDefinition
                {
                    Type = EntityType.Player,
                    Name = "player",
                    Alive = true,
                    Health = 10,
                    Attack = 1,
                    Defence = 1,
                    Arcana = 1,
                    Esotericism = 1,
                    Vulnerable = true,
                    Tangiable = true,
                    Speed = 1,
                    Stamina = 10
                }
            );
        }
    }
}
