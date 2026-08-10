using System.Text.Json.Serialization;

namespace GameEngine.Entities
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of the Entity class.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(Entity))]
    partial class EntitySerializerContext : JsonSerializerContext { }
}
