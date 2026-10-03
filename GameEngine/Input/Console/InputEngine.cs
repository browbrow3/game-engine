using GameEngine.Adapters.Console;
using GameEngine.Events;

namespace GameEngine.Input.Console
{
    /// <summary>
    /// Handles user input from the console, maps it to input types, and publishes input events to the event manager.
    /// </summary>
    /// <param name="console">The console provider.</param>
    /// <param name="eventManager">The event manager.</param>
    /// <param name="inputMappingProvider">The input mapping provider.</param>
    public class InputEngine(
        IConsoleProvider console,
        IEventManager eventManager,
        IInputMappingProvider inputMappingProvider
    ) : IInputEngine
    {
        private bool _stop = true;
        private Task? _inputLoop;
        private readonly IConsoleProvider _console = console;
        private readonly IEventManager _eventManager = eventManager;
        private readonly IInputMappingProvider _mappingProvider = inputMappingProvider;

        /// <inheritdoc/>
        public void Start()
        {
            if (
                this._inputLoop is not null &&
                this._inputLoop.Status != TaskStatus.RanToCompletion &&
                this._inputLoop.Status != TaskStatus.Canceled &&
                this._inputLoop.Status != TaskStatus.Faulted
            )
                throw new InvalidOperationException($"Start called when Input Handler input loop is already running. Input loop in status {this._inputLoop?.Status}");

            this._stop = false;

            // Start the event loop in a background thread to continuously check for user input without blocking the main thread.
            this._inputLoop = Task.Factory.StartNew(() => this.InputLoop(), TaskCreationOptions.LongRunning);
        }

        /// <inheritdoc/>
        public Task Stop()
        {
            this._stop = true;
            return this._inputLoop ?? Task.CompletedTask;
        }

        /// <summary>
        /// Starts the event loop that continuously checks for user input and handles it.
        /// </summary>
        private void InputLoop()
        {
            while (!this._stop)
            {
                if(this._console.KeyAvailable)
                {
                    this.HandleInput();
                }
            }
        }

        /// <summary>
        /// Handles user input by reading a key from the console, mapping it to an input type, and publishing an input event if the input is valid.
        /// </summary>
        private void HandleInput()
        {
            ConsoleKeyInfo keyInfo = this._console.ReadKey(intercept: true);
            InputType input = this._mappingProvider.GetInputType(keyInfo.Key);
            if (input != InputType.Invalid)
            {
                this._eventManager.Publish(new InputEvent(input));
            }
        }
    }
}
