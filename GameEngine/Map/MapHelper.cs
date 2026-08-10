using System.Text.Json;

namespace GameEngine.Map
{
    /// <summary>
    /// Helper for loading a map definition from a JSON file.
    /// </summary>
    /// <remarks>
    /// TODO: note that IEntityDefinitionRepository and ITileDefinitionRepository are very similar and could potentially be refactored into a single generic interface.
    /// </remarks>
    public static class MapHelper
    {
        /// <summary>
        /// Gets the map definition from the specified JSON file.
        /// </summary>
        /// <param name="filePath">The path to the JSON file containing the map definition.</param>
        /// <returns>A task which, when completed, returns the map definition.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the JSON cannot be deserialized.</exception>
        public static async Task<MapDto> GetMap(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<MapDto>(json, MapDto.JsonTypeInfo)
                ?? throw new InvalidOperationException($"Failed to deserialize {typeof(MapDto).Name}.");
        }
    }
}