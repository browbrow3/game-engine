namespace GameEngine.Graphics.Console.Sprites
{
    /// <summary>
    /// Represents a sprite in the console graphics system, defined by a single character.
    /// </summary>
    /// <param name="character">The character that represents the sprite.</param>
    public class Sprite(char character) : ISprite
    {
        private Frame frame = new Frame(character);

        /// <inheritdoc/>
        public IFrame GetFrame()
        {
            return this.frame;
        }
    }
}
