using System.Text.Json.Serialization;

namespace GameEngine.Map.Tiles
{
    /// <summary>
    /// Represents a source generation context for JSON serialization and deserialization of tile definitions.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(TileDefinition))]
    [JsonSerializable(typeof(Dictionary<TileType, TileDefinition>))]
    public partial class TileDefinitionSerializerContext : JsonSerializerContext { }
}
