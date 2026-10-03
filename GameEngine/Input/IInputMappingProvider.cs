using GameEngine.Events;

namespace GameEngine.Input
{
    /// <summary>
    /// Interface for providing input mappings based on input types.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface to be provided per platform, allowing for platform-specific input handling and mapping.
    /// </remarks>
    public interface IInputMappingProvider
    {
        /// <summary>
        /// Gets the input type for a given input.
        /// </summary>
        /// <typeparam name="T">Type of the input</typeparam>
        /// <param name="input">The input for which to get the input type</param>
        /// <returns>The corresponding input type</returns>
        InputType GetInputType<T>(T input);
    }
}
