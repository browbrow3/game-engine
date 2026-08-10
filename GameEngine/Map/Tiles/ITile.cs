using GameEngine.Entities;
using GameEngine.Events;

namespace GameEngine.Map.Tiles
{
    /// <summary>
    /// Represents a tile in the game engine, providing properties and methods for interacting with entities and events on the tile.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// The type of the tile, represented by the TileType enum.
        /// </summary>
        TileType Type { get; }

        /// <summary>
        /// The name of the tile type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indicates whether the tile is tangible (i.e., can be collided with).
        /// </summary>
        bool Tangiable { get; }

        /// <summary>
        /// The speed multiplier for entities moving through the tile.
        /// </summary>
        float SpeedMultiplier { get; }

        /// <summary>
        /// Status effects conferred by the tile, represented as an integer (bitmask).
        /// </summary>
        int StatusEffects { get; }

        /// <summary>
        /// Indicates the character with which to represent the tile when printing to console.
        /// </summary>
        char PrintChar { get; }

        /// <summary>
        /// Indicates whether the tile is passable (i.e., can be traversed by entities). 
        /// Inclues the entities on the tile in the calculation. 
        /// If any entity on the tile is not passable, the tile is considered not passable.
        /// </summary>
        bool Passable { get; }

        /// <summary>
        /// Adds an entity to the tile. Returns a short indicating the result of the operation.
        /// </summary>
        /// <param name="entity">the entity to add.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short AddEntity(IEntity entity);

        /// <summary>
        /// Removes an entity from the tile. Returns a short indicating the result of the operation.
        /// </summary>
        /// <param name="entity">the entity to remove.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short RemoveEntity(IEntity entity);

        /// <summary>
        /// Gets the entities currently on the tile.
        /// </summary>
        /// <returns>An IEnumerable of IEntity representing the entities on the tile.</returns>
        IEnumerable<IEntity> GetEntities();

        /// <summary>
        /// Publishes an event to the entities on the tile. Returns a short indicating the result of the operation.
        /// </summary>
        /// <param name="e">the event to publish.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short PublishEvent(IEvent e);
    }
}
