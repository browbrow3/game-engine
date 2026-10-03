namespace GameEngine.Graphics.Console.Sprites
{
    /// <summary>
    /// Represents a frame of a sprite in the console graphics system.
    /// </summary>
    /// <param name="character">The character that represents the frame.</param>
    public class Frame(char character) : IFrame
    {
        public char Character { get; set; } = character;
    }
}
