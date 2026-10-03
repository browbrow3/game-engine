namespace GameEngine.Adapters.Console
{
    /// <summary>
    /// Provides an implementation of the IConsoleProvider interface that wraps the System.Console class.
    /// </summary>
    public class ConsoleProvider : IConsoleProvider
    {
        /// <inheritdoc/>
        public bool KeyAvailable => System.Console.KeyAvailable;

        /// <inheritdoc/>
        public ConsoleKeyInfo ReadKey(bool intercept = false)
            => System.Console.ReadKey(intercept);

        /// <inheritdoc/>
        public void Clear()
            => System.Console.Clear();

        /// <inheritdoc/>
        public void Write(string? value = null)
            => System.Console.Write(value);

        /// <inheritdoc/>
        public void WriteLine(string? value = null)
            => System.Console.WriteLine(value);
    }
}
