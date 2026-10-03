using GameEngine.Events;

namespace GameEngine.Input.Console
{
    /// <summary>
    /// Provides a mapping between console input characters and game input types.
    /// </summary>
    /// <remarks>
    /// Currently defines inputs statically. Should be extended to support loading mappings from a configuration file or other sources in the future.
    /// </remarks>
    public class InputMappingProvider : IInputMappingProvider
    {
        /// <summary>
        /// A dictionary that maps console input characters to their corresponding game input types.
        /// </summary>
        private readonly Dictionary<ConsoleKey, InputType> _inputMappings = new()
        {
            { ConsoleKey.A, InputType.MoveLeft },
            { ConsoleKey.D, InputType.MoveRight },
            { ConsoleKey.W, InputType.MoveUp },
            { ConsoleKey.S, InputType.MoveDown },
            { ConsoleKey.E, InputType.Interact },
            { ConsoleKey.Spacebar, InputType.Attack },
            { ConsoleKey.P, InputType.Pause },
            { ConsoleKey.Q, InputType.Quit },
        };

        /// <inheritdoc/>
        public InputType GetInputType<T>(T input)
        {
            if (input is ConsoleKey consoleKey)
            {
                return this._inputMappings.GetValueOrDefault(consoleKey, InputType.Invalid);
            }
            throw new ArgumentException($"Unsupported input type: {input}");
        }
    }
}
