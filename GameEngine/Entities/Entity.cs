using GameEngine.Events;
using GameEngine.Graphics;
using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Entities
{

    /// <summary>
    /// Basic implementation of an entity in the game engine.
    /// </summary>
    public class Entity(
        //ISpriteRepository spriteRepository
    ) : IEntity, IJsonSerializable<Entity>
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
        public int X { get; set; }

        /// <inheritdoc/>
        public int Y { get; set; }

        /// <inheritdoc/>
        public required ISprite Sprite { get; set; }

        /// <inheritdoc/>
        public void HandleEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
