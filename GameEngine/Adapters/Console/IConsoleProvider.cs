namespace GameEngine.Adapters.Console
{
    /// <summary>
    /// Interface that abstracts console input and output operations.
    /// </summary>
    public interface IConsoleProvider
    {
        /// <inheritdoc cref="System.Console.KeyAvailable"/>
        bool KeyAvailable { get; }

        /// <inheritdoc cref="System.Console.ReadKey(bool)"/>
        ConsoleKeyInfo ReadKey(bool intercept = false);

        /// <inheritdoc cref="System.Console.Clear()"/>
        void Clear();

        /// <inheritdoc cref="System.Console.Write(string?)"/>
        void Write(string? value = null);

        /// <inheritdoc cref="System.Console.WriteLine(string?)"/>
        void WriteLine(string? value = null);
    }
}
