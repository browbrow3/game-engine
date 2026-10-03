using GameEngine.Entities;
using GameEngine.Graphics;

namespace GameEngine.Graphics.Console.Sprites
{
    /// <summary>
    /// Represents a repository for managing and retrieving sprites associated with entities in the game engine.
    /// </summary>
    public class SpriteRepository : ISpriteRepository
    {
        private readonly Dictionary<int, Sprite> _sprites = new() {
            { 1, new Sprite('1') },
            { 2, new Sprite('2') },
            { 3, new Sprite('3') },
        };

        /// <inheritdoc />
        public ISprite GetSprite(int id)
        {
            if (this._sprites.TryGetValue(id, out Sprite? sprite) && sprite is not null)
            {
                return sprite;
            }
            throw new KeyNotFoundException($"Sprite with ID '{id}' not found.");
        }
    }
}
