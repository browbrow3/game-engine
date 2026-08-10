using GameEngine.Map.Spawns;
using GameEngine.Map.Tiles;
using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Map
{
    /// <summary>
    /// Represents a data transfer object (DTO) for a game map, containing information about the tiles and entities present in the map.
    /// </summary>
    public class MapDto : IJsonSerializable<MapDto>
    {
        /// <summary>
        /// Gets or initializes a two-dimensional list representing the tiles in the map. Each inner list represents a row of tiles, and each element in the inner list represents a tile type.
        /// </summary>
        public required List<List<TileType>> Tiles { get; init; } = [[]];

        /// <summary>
        /// Gets or initializes a list of entities (spawns) present in the map. Each entity is represented by a Spawn object, which contains information about the entity's type, position, and other relevant properties.
        /// </summary>
        public List<Spawn>? Entities { get; init; }

        /// <inheritdoc/>
        public static JsonTypeInfo<MapDto> JsonTypeInfo => MapDtoSerializerContext.Default.MapDto;
    }
}
