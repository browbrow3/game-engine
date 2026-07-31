using GameEngine.Events;

namespace GameEngine.Entities
{
    /// <summary>
    /// Represents a basic entity in the game engine.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity, represented by the EntityType enum.
        /// </summary>
        EntityType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the x-coordinate of the entity's position.
        /// </summary>
        float X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of the entity's position.
        /// </summary>
        float Y { get; set; }

        /// <summary>
        /// Handles an event that is relevant to the entity. This method allows the entity to respond to events in the game engine.
        /// </summary>
        /// <param name="e">The event to handle.</param>
        void HandleEvent(IEvent e);
    }
}
