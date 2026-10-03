using FluentAssertions;
using GameEngine.Input;
using GameEngine.Input.Console;

namespace GameEngine_Tests.Input.Console
{
    public class InputMappingProviderTests
    {
        private readonly InputMappingProvider inputMappingProvider = new();

        [Theory]
        [InlineData(ConsoleKey.A, InputType.MoveLeft)]
        [InlineData(ConsoleKey.D, InputType.MoveRight)]
        [InlineData(ConsoleKey.W, InputType.MoveUp)]
        [InlineData(ConsoleKey.S, InputType.MoveDown)]
        [InlineData(ConsoleKey.E, InputType.Interact)]
        [InlineData(ConsoleKey.Spacebar, InputType.Attack)]
        [InlineData(ConsoleKey.P, InputType.Pause)]
        [InlineData(ConsoleKey.Q, InputType.Quit)]
        public void GetInputType_ReturnsCorrectInputType(ConsoleKey key, InputType expected)
        {
            this.inputMappingProvider.GetInputType(key).Should().Be(expected);
        }
    }
}

