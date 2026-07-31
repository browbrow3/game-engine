using GameEngine.Events;
using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Entities
{

    /// <summary>
    /// Basic implementation of an entity in the game engine.
    /// </summary>
    public class Entity : IEntity, IJsonSerializable<Entity>
    {
        /// <inheritdoc/>
        public static JsonTypeInfo<Entity> JsonTypeInfo =>
            EntitySerializerContext.Default.Entity;

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public EntityType Type { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public float X { get; set; }

        /// <inheritdoc/>
        public float Y { get; set; }

        /// <inheritdoc/>
        public void HandleEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
