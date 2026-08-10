using System.Text.Json.Serialization;

namespace GameEngine.Map.Spawns
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of spawn points.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(Spawn))]
    partial class SpawnSerializerContext : JsonSerializerContext { }
}
