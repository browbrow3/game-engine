using System.Text.Json.Serialization;

namespace GameEngine_Tests.Helpers
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of the EntityStub class.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(EntityStub))]
    partial class EntityStubSerializerContext : JsonSerializerContext { }
}
