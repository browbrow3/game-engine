using FluentAssertions;
using GameEngine.Events;
using GameEngine.Input;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameEngine_Tests.Events
{
    // Note that since the game engine itself doesn't really do anything at the moment, tests in this class are really just stubs for now.
    public class EventManagerTests
    {
        private readonly Mock<ILogger<GameEngine.GameEngine>> mockLogger = new Mock<ILogger<GameEngine.GameEngine>>(MockBehavior.Loose);

        GameEngine.Events.EventManager eventManager = new GameEngine.Events.EventManager();

        #region Subscribe Tests
        [Fact]
        public async Task SubscribeAndPublish_AddsHandlerToEventManager_EventsPublishedToHandler()
        {
            string result = string.Empty;

            Action<IEvent> handler = (e) => { result = $"{((InputEvent)e).InputType}"; };

            // Subscribe the handler to the MoveEvent. Should get an ID and the handler should not be invoked yet.
            int subscriptionId = this.eventManager.Subscribe(EventType.Input, handler);

            subscriptionId.Should().Be(0);
            result.Should().Be(string.Empty);

            // Publish a MoveEvent. The handler should be invoked.
            eventManager.Publish(new InputEvent(InputType.MoveUp));
            result.Should().Be("MoveUp");
        }
        #endregion

        #region Publish Tests
        [Fact]
        public async Task SubscribeAndPublish_CallsAllRegisteredHandlersForType()
        {
            string handlerResult_a = string.Empty;
            string handlerResult_b = string.Empty;
            Action<IEvent> handler_a = (e) => { handlerResult_a = $"{((InputEvent)e).InputType}-A"; };
            Action<IEvent> handler_b = (e) => { handlerResult_b = $"{((InputEvent)e).InputType}-B"; };

            eventManager.Subscribe(EventType.Input, handler_a).Should().Be(0);
            eventManager.Subscribe(EventType.Input, handler_b).Should().Be(1);

            handlerResult_a.Should().Be(string.Empty);
            handlerResult_b.Should().Be(string.Empty);

            eventManager.Publish(new InputEvent(InputType.MoveUp));

            handlerResult_a.Should().Be("MoveUp-A");
            handlerResult_b.Should().Be("MoveUp-B");
        }

        [Fact]
        public async Task SubscribeAndPublish_SeriesOfPublishedEvents_HandlerCalledForEachEvent()
        {
            string handlerResult = string.Empty;
            Action<IEvent> handler = (e) => { handlerResult = $"{((InputEvent)e).InputType}"; };

            eventManager.Subscribe(EventType.Input, handler);
            handlerResult.Should().Be(string.Empty);

            eventManager.Publish(new InputEvent(InputType.MoveUp));
            handlerResult.Should().Be("MoveUp");
            eventManager.Publish(new InputEvent(InputType.MoveDown));
            handlerResult.Should().Be("MoveDown");
            eventManager.Publish(new InputEvent(InputType.Interact));
            handlerResult.Should().Be("Interact");
            eventManager.Publish(new InputEvent(InputType.Attack));
            handlerResult.Should().Be("Attack");
        }

        [Fact]
        public async Task SubscribeAndPublish_NonMatchingEvent_EventNotPublished()
        {
            string result = string.Empty;

            Action<IEvent> handler = (e) => { result = "invoked"; };

            // Subscribe the handler to the MoveEvent. Should get an ID and the handler should not be invoked yet.
            int subscriptionId = this.eventManager.Subscribe(EventType.Input, handler);

            subscriptionId.Should().Be(0);
            result.Should().Be(string.Empty);

            // Publish a CollisionEvent. The handler should not be invoked.
            eventManager.Publish(new Event(EventType.Collision));
            result.Should().Be(string.Empty);
        }
        #endregion

        #region Unsubscribe Tests

        [Fact]
        public async Task Unsubscribe_RemovesHandler_PublishedEventsNotReceived()
        {
            string result = string.Empty;

            Action<IEvent> handler = (e) => { result = $"{((InputEvent)e).InputType}"; };

            // Subscribe the handler to the MoveEvent. Should get an ID and the handler should not be invoked yet.
            int subscriptionId = this.eventManager.Subscribe(EventType.Input, handler);

            subscriptionId.Should().Be(0);
            result.Should().Be(string.Empty);

            // Publish a MoveEvent. The handler should be invoked.
            eventManager.Publish(new InputEvent(InputType.MoveUp));
            result.Should().Be("MoveUp");

            // Unsubscribe the handler. The handler should not be invoked anymore.
            this.eventManager.Unsubscribe(EventType.Input, subscriptionId);

            eventManager.Publish(new InputEvent(InputType.Attack));
            result.Should().Be("MoveUp");
        }

        [Fact]
        public async Task Unsubscribe_SubscriptionIdInvalid_SubscriptionNotRemoved()
        {
            string result = string.Empty;

            Action<IEvent> handler = (e) => { result = $"{((InputEvent)e).InputType}"; };

            // Subscribe the handler to the MoveEvent. Should get an ID and the handler should not be invoked yet.
            int subscriptionId = this.eventManager.Subscribe(EventType.Input, handler);

            subscriptionId.Should().Be(0);
            result.Should().Be(string.Empty);

            // Publish a MoveEvent. The handler should be invoked.
            eventManager.Publish(new InputEvent(InputType.MoveUp));
            result.Should().Be("MoveUp");

            // Unsubscribe the handler. The handler should still be invoked as the subscription ID is invalid.
            this.eventManager.Unsubscribe(EventType.Input, -10);

            eventManager.Publish(new InputEvent(InputType.Attack));
            result.Should().Be("Attack");
        }

        [Fact]
        public async Task Unsubscribe_SubscriptionNotInType_SubscriptionNotRemoved()
        {
            string result = string.Empty;

            Action<IEvent> handler = (e) => { result = $"{((InputEvent)e).InputType}"; };

            // Subscribe the handler to the MoveEvent. Should get an ID and the handler should not be invoked yet.
            int subscriptionId = this.eventManager.Subscribe(EventType.Input, handler);

            subscriptionId.Should().Be(0);
            result.Should().Be(string.Empty);

            // Publish a MoveEvent. The handler should be invoked.
            eventManager.Publish(new InputEvent(InputType.MoveUp));
            result.Should().Be("MoveUp");

            // Unsubscribe the handler. The handler should still be invoked as the subscription ID is invalid.
            this.eventManager.Unsubscribe(EventType.Collision, 1);

            eventManager.Publish(new InputEvent(InputType.Attack));
            result.Should().Be("Attack");
        }

        #endregion
    }
}