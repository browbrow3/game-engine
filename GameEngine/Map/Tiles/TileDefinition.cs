using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Map.Tiles
{
    /// <summary>
    /// Represents the definition of a tile in the game engine.
    /// </summary>
    public class TileDefinition : IJsonSerializable<TileDefinition>
    {
        /// <summary>
        /// The type of the tile, represented by the TileType enum.
        /// </summary>
        public TileType Type { get; init; } = TileType.Default;

        /// <summary>
        /// The name of the tile.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Indicates whether the tile is tangible (i.e., can be collided with).
        /// </summary>
        public bool Tangiable { get; init; } = false;

        /// <summary>
        /// The speed multiplier entities moving through the tile.
        /// </summary>
        public float SpeedMultiplier { get; init; } = 1.0f;

        /// <summary>
        /// Status effects conferred by the tile, represented as an integer (bitmask).
        /// </summary>
        public int StatusEffects { get; init; } = 0;

        /// <summary>
        /// Indicates the character with which to represent the tile when printing to console.
        /// </summary>
        public char PrintChar { get; init; } = '?';

        /// <inheritdoc/>
        public static JsonTypeInfo<TileDefinition> JsonTypeInfo => TileDefinitionSerializerContext.Default.TileDefinition;
    }
}
