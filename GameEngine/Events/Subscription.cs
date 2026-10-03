namespace GameEngine.Events
{
    public class Subscription(int id, Action<IEvent> callback)
    {
        public int Id { get; } = id;
        public Action<IEvent> Callback { get; } = callback;
    }
}
