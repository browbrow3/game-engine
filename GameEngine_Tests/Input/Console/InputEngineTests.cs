using FluentAssertions;
using GameEngine.Events;
using GameEngine.Input;
using GameEngine.Input.Console;
using GameEngine_Tests.Helpers;
using Moq;

namespace GameEngine_Tests.Input.Console
{
    public class InputEngineTests : IAsyncLifetime
    {
        private readonly ConsoleProviderStub consoleProviderStub = new();
        private readonly Mock<IEventManager> mockEventManager = new(MockBehavior.Strict);
        private readonly Mock<IInputMappingProvider> mockInputMappingProvider = new(MockBehavior.Strict);

        private readonly InputEngine inputEngine;

        public InputEngineTests()
        {
            this.inputEngine = new InputEngine(this.consoleProviderStub, this.mockEventManager.Object, this.mockInputMappingProvider.Object);
        }

        public async Task InitializeAsync() { }

        public async Task DisposeAsync()
        {
            await this.inputEngine.Stop();
        }

        [Fact]
        public async Task Stop_BeforeStart_ReturnsCompletedTask()
        {
            this.inputEngine.Stop().Should().Be(Task.CompletedTask);
        }

        [Fact]
        public async Task Stop_ShouldStopInputLoop()
        {
            this.inputEngine.Start();

            Thread.Sleep(1000);

            await this.inputEngine.Stop();
            
            this.consoleProviderStub.PushKey(new('d', ConsoleKey.D, false, false, false));

            Thread.Sleep(5000);

            // confirm that key has not been processed.
            this.consoleProviderStub.GetKeyBuffer().Should().HaveCount(1);
        }

        [Fact]
        public void Start_StartedTwice_ThrowsInvalidOperationException()
        {
            this.inputEngine.Start();
            this.inputEngine.Invoking(x => x.Start()).Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void HandleInput_ValidKey_RaisesEvent()
        {
            this.inputEngine.Start();

            // Push an "A".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.A))
                .Returns(InputType.MoveLeft);

            ManualResetEvent publishInvoked = new ManualResetEvent(false);

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.MoveLeft)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('a', ConsoleKey.A, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push a "D".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.D))
                .Returns(InputType.MoveRight);

            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.MoveRight)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('d', ConsoleKey.D, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push a "W".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.W))
                .Returns(InputType.MoveUp);

            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.MoveUp)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('w', ConsoleKey.W, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push an "S".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.S))
                .Returns(InputType.MoveDown);

            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.MoveDown)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('s', ConsoleKey.S, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push an "E".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.E))
                .Returns(InputType.Interact);
            
            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.Interact)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('e', ConsoleKey.E, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push a "Spacebar".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.Spacebar))
                .Returns(InputType.Attack);

            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.Attack)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new(' ', ConsoleKey.Spacebar, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push a "P".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.P))
                .Returns(InputType.Pause);

            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.Pause)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('p', ConsoleKey.P, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push a "Q".
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.Q))
                .Returns(InputType.Quit);
            
            publishInvoked.Reset();

            this.mockEventManager
                .Setup(x => x.Publish(It.Is<InputEvent>(e => e.InputType == InputType.Quit)))
                .Callback(() => publishInvoked.Set());

            this.consoleProviderStub.PushKey(new('q', ConsoleKey.Q, false, false, false));

            if (!publishInvoked.WaitOne(1000)) // Wait for the event to be published
            {
                throw new TimeoutException("Event was not published within the expected time.");
            }

            this.mockInputMappingProvider.VerifyAll();
            this.mockEventManager.VerifyAll();
            this.mockInputMappingProvider.Reset();
            this.mockEventManager.Reset();

            // Push an invalid input:
            this.mockInputMappingProvider.Setup(x => x.GetInputType(ConsoleKey.Z))
                .Returns(InputType.Invalid);
            this.consoleProviderStub.PushKey(new('z', ConsoleKey.Z, false, false, false));

            Thread.Sleep(5000); // Wait for a while to ensure no event is published

            this.mockInputMappingProvider.VerifyAll();
        }
    }
}

