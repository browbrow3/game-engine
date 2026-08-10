using GameEngine.Entities;
using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Map.Spawns
{
    /// <summary>
    /// Represents a spawn point in the game map, specifying the location and type of entity to spawn.
    /// </summary>
    public class Spawn : IJsonSerializable<Spawn>
    {
        /// <summary>
        /// Gets or sets the X coordinate of the spawn point.
        /// </summary>
        public float X { get; init; }

        /// <summary>
        /// Gets or sets the Y coordinate of the spawn point.
        /// </summary>
        public float Y { get; init; }

        /// <summary>
        /// Gets or sets the type of entity to spawn at this spawn point. Defaults to EntityType.Default.
        /// </summary>
        public EntityType EntityType { get; init; } = EntityType.Default;

        /// <inheritdoc/>
        public static JsonTypeInfo<Spawn> JsonTypeInfo => SpawnSerializerContext.Default.Spawn;
    }
}
