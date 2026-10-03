using GameEngine.Input;
using GameEngine.Map;

namespace GameEngine.Events
{
    /// <summary>
    /// Event representing user input.
    /// </summary>
    /// <param name="inputType">Type of the input event.</param>
    public class InputEvent(InputType inputType) : IEvent
    {
        /// <inheritdoc />
        public EventType Type { get; } = EventType.Input;

        /// <summary>
        /// Type of the input event.
        /// </summary>
        public InputType InputType { get; init; } = inputType;
    }
}
