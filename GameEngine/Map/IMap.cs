using GameEngine.Entities;
using GameEngine.Events;
using GameEngine.Map.Tiles;

namespace GameEngine.Map
{
    /// <summary>
    /// Represents a map in the game engine, providing methods to interact with tiles and entities on the map.
    /// </summary>
    /// <remarks>
    /// The map provides an easy way to reference entities by location.
    /// </remarks>
    public interface IMap
    {
        /// <summary>
        /// Gets the width of the map in tiles.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the map in tiles.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <returns>The tile at the specified coordinates.</returns>
        ITile GetTile(int x, int y);

        /// <summary>
        /// Determines whether the tile at the specified coordinates is passable.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <returns>A boolean indicating whether the tile is passable.</returns>
        bool IsTilePassable(int x, int y);

        /// <summary>
        /// Adds an entity to the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short AddEntityToTile(int x, int y, IEntity entity);

        /// <summary>
        /// Removes an entity from the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short RemoveEntityFromTile(int x, int y, IEntity entity);

        /// <summary>
        /// Gets all entities in the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <returns>An IEnumerable of entities in the tile.</returns>
        IEnumerable<IEntity> GetEntitiesInTile(int x, int y);

        /// <summary>
        /// Publishes an event to the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        /// <param name="e">The event to publish.</param>
        /// <returns>A short indicating the result of the operation.</returns>
        short PublishEventToTile(int x, int y, IEvent e);

        /// <summary>
        /// Returns a string representation of the map.
        /// </summary>
        /// <returns>A string representing the map.</returns>
        string Print();
    }
}
