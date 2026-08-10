namespace GameEngine.Events
{
    /// <summary>
    /// Represents a basic event in the game engine, implementing the IEvent interface. This class is used to create events of a specific type.
    /// </summary>
    /// <param name="type"></param>
    public class Event(EventType type) : IEvent
    {
        /// <inheritdoc />
        public EventType Type { get; init; } = type;
    }
}
