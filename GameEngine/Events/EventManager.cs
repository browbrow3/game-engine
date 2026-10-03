namespace GameEngine.Events
{
    /// <summary>
    /// Manages event subscriptions and publishing within the game engine. This class allows components to subscribe to specific event types and receive notifications when those events are published.
    /// </summary>
    /// <remarks>
    /// TODO: make thread safe. Will need to be able to subscribe/unsubscribe from any thread, and publish from any thread.
    /// </remarks>
    public class EventManager : IEventManager
    {
        private int subscriptionIndex = 0;
        private Dictionary<EventType, List<Subscription>> subscriptions = [];

        /// <inheritdoc/>
        public int Subscribe(EventType type, Action<IEvent> callback)
        {
            if (!this.subscriptions.ContainsKey(type))
            {
                this.subscriptions[type] = [];
            }
            Subscription subscription = new(this.subscriptionIndex++, callback);
            this.subscriptions[type].Add(subscription);
            return subscription.Id;
        }

        /// <inheritdoc/>
        public void Unsubscribe(EventType type, int subscriptionId)
        {
            this.subscriptions
                .GetValueOrDefault(type)?
                .RemoveAll(s => s.Id == subscriptionId);
        }

        /// <inheritdoc/>
        public void Publish(IEvent e)
        {
            if (this.subscriptions.ContainsKey(e.Type))
            {
                foreach (var subscription in this.subscriptions[e.Type])
                {
                    subscription.Callback(e);
                }
            }
        }
    }
}
