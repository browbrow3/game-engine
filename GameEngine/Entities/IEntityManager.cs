namespace GameEngine.Entities
{
    /// <summary>
    /// Represents a manager for managing entities in the game engine.
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        IEntity? GetEntity(int id);

        /// <summary>
        /// Adds a new entity to the manager and returns its unique identifier.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The unique identifier of the added entity.</returns>
        int AddEntity(IEntity entity);

        /// <summary>
        /// Removes an entity from the manager.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to remove.</param>
        /// <returns>Short representing the result of the operation.</returns>
        short RemoveEntity(int id);
    }
}
