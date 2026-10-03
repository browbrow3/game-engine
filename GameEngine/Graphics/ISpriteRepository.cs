namespace GameEngine.Graphics
{
    /// <summary>
    /// Represents a repository for managing and retrieving sprites associated with entities in the game engine.
    /// </summary>
    public interface ISpriteRepository
    {

        /// <summary>
        /// Gets the sprite associated with the specified entity ID.
        /// </summary>
        /// <param name="id">The ID of the entity for which to retrieve the sprite.</param>
        /// <returns>The sprite associated with the specified entity ID.</returns>
        /// <exception cref="KeyNotFoundException">If no sprite with the specified ID is not found.</exception>
        ISprite GetSprite(int id);
    }
}
