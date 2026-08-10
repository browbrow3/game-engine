namespace GameEngine.Events
{
    /// <summary>
    /// Represents a basic event in the game engine. This interface defines the structure for events, including the event type.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets the type of the event, represented by the EventType enum.
        /// </summary>
        EventType Type { get; init; }
    }
}
