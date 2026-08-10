using GameEngine.Map.Tiles;
using System.Text.Json.Serialization;

namespace GameEngine.Entities
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of entity definitions.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(EntityDefinition))]
    [JsonSerializable(typeof(Dictionary<EntityType, EntityDefinition>))]
    public partial class EntityDefinitionSerializerContext : JsonSerializerContext { }
}
