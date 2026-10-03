namespace GameEngine.Graphics
{
    /// <summary>
    /// Interface for rendering graphics for the game.
    /// </summary>
    public interface IGraphicsEngine
    {
        /// <summary>
        /// Starts the graphics engine's render loop.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the graphics engine's render loop.
        /// </summary>
        /// <returns>A task which, when completed, indicates that the render loop has stopped running./// </returns>
        Task Stop();
    }
}
