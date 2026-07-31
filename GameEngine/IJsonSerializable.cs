using System.Text.Json.Serialization.Metadata;

namespace GameEngine
{
    /// <summary>
    /// Defines a contract for types that can provide JSON serialization metadata through the JsonTypeInfo property.
    /// </summary>
    /// <typeparam name="T">The type for which to provide JSON serialization metadata.</typeparam>
    public interface IJsonSerializable<T>
    {
        /// <summary>
        /// Gets the JsonTypeInfo for the type T, which provides metadata for JSON serialization and deserialization.
        /// </summary>
        static abstract JsonTypeInfo<T> JsonTypeInfo { get; }
    }
}