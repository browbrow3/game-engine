using GameEngine.Adapters.Console;

namespace GameEngine_Tests.Helpers
{
    /// <summary>
    /// A stub implementation of the IConsoleProvider interface for testing purposes.
    /// </summary>
    public class ConsoleProviderStub : IConsoleProvider
    {
        public static string ClearInstruction = "<CLEAR>";
        private Queue<ConsoleKeyInfo> _keyBuffer = new();
        private List<string> _output = new();

        public bool KeyAvailable => _keyBuffer.Count > 0;

        public ConsoleKeyInfo ReadKey(bool intercept = false)
            => this._keyBuffer.Dequeue();

        public void Clear()
            => this._output.Add(ClearInstruction);

        public void WriteLine(string? value = null)
            => this._output.Add(value ?? "");

        public void Write(string? value = null)
            => this._output.Add(value ?? "");

        // Test helper methods
        public void PushKey(ConsoleKeyInfo keyInfo) => this._keyBuffer.Enqueue(keyInfo);
        public IReadOnlyList<string> GetOutput() => this._output.AsReadOnly();
        public IEnumerable<ConsoleKeyInfo> GetKeyBuffer() => this._keyBuffer.AsEnumerable();
    }
}
