namespace GameEngine
{
    /// <summary>
    /// Represents the main interface for the game engine, providing methods to initialize, start, and stop the engine.
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// Initializes the game engine asynchronously, preparing it for operation. This method MUST be called before starting the engine.
        /// </summary>
        /// <remarks>
        /// Could consider tracking a boolean "initialized" flag which would prevent Start() from being called before InitializeAsync() has completed. This would ensure that the engine is properly initialized before starting.
        /// </remarks>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task InitializeAsync();

        /// <summary>
        /// Starts the game engine, beginning its main loop and processing.
        /// This method should only be called after the engine has been initialized.
        /// </summary>
        /// <returns>A task which, when completed, indicates the engine has completed operation.</returns>
        Task Start();

        /// <summary>
        /// Stops the game engine, halting its main loop and processing.
        /// This method should be called to gracefully shut down the engine.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. 
        /// - ? should this only return when the engine has stopped gracefully, or should that only be the case for Start()? ?
        /// ^ it probably makes sense for both to return when the engine has stopped gracefully as Start and Stop are likely to be called by different threads, meaning that Start() would return when the engine has stopped gracefully, and Stop() would return when the engine has stopped gracefully. This would allow for proper synchronization between the two methods and ensure that the engine is in a consistent state when either method completes.
        /// ^^ Alternatively, since Stop could be triggered from within the game loop, it might be bad to have it block until the engine has stopped gracefully, as this could lead to deadlocks or other synchronization issues.
        /// ^^^ I think on balance, Stop should probably return when the engine has stopped gracefully. If the Start() thread needs to call Stop(), it can just not await the call?
        /// </returns>
        Task Stop();
    }
}
