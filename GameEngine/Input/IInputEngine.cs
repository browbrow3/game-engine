namespace GameEngine.Input
{
    /// <summary>
    /// Interface for handling user input in the game engine.
    /// </summary>
    public interface IInputEngine
    {
        /// <summary>
        /// Starts the input engine's event loop.
        /// </summary>
        void Start();

        /// <summary>
        /// Clears down the input handler, releasing any resources and resetting its state. This method is called when the input handler is no longer needed or when the game engine is shutting down.
        /// </summary>
        /// <returns>A task which, when completed, indicates that the input loop has stopped running./// </returns>
        Task Stop();
    }
}
