using GameEngine.Events;
using GameEngine.Entities;
using GameEngine.Graphics;
using System.Text.Json.Serialization.Metadata;
using GameEngine;

namespace GameEngine_Tests.Helpers
{

    /// <summary>
    /// Basic implementation of an entity in the game engine.
    /// </summary>
    public class EntityStub(
        ISprite sprite
    ) : IEntity, IJsonSerializable<EntityStub>
    {
        /// <inheritdoc/>
        public static JsonTypeInfo<EntityStub> JsonTypeInfo =>
            EntityStubSerializerContext.Default.EntityStub;

        /// <inheritdoc/>
        public int Id { get; set; } = 0;

        /// <inheritdoc/>
        public EntityType Type { get; set; } = EntityType.Default;

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public int X { get; set; } = 0;

        /// <inheritdoc/>
        public int Y { get; set; } = 0;

        /// <inheritdoc/>
        public required ISprite Sprite { get; set; } = sprite;

        /// <inheritdoc/>
        public void HandleEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
