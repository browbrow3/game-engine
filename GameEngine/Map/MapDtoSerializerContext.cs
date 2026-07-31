using System.Text.Json.Serialization;

namespace GameEngine.Map
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of map data.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(MapDto))]
    public partial class MapDtoSerializerContext : JsonSerializerContext { }
}
