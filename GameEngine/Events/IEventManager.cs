namespace GameEngine.Events
{
    /// <summary>
    /// Interface for managing events in the game engine.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Subscribes to an event of type TEvent with the provided callback.
        /// </summary>
        /// <param name="type">The type of event to which to subscribe.</param>
        /// <param name="callback">The callback to invoke when an event of this type is published.</param>
        /// <returns>The subscription ID.</returns>
        public int Subscribe(EventType type, Action<IEvent> callback);

        /// <summary>
        /// Unsubscribes from events using the provided subscription ID.
        /// </summary>
        /// <param name="subscriptionId">The subscription ID.</param>
        public void Unsubscribe(EventType type, int subscriptionId);

        /// <summary>
        /// Publishes an event of type TEvent with the provided event data.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to publish.</typeparam>
        /// <param name="e">The event.</param>
        public void Publish(IEvent e);
    }
}
