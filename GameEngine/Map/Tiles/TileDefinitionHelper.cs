using System.Text.Json;

namespace GameEngine.Map.Tiles
{
    /// <summary>
    /// Helper for loading tile definitions from a JSON file.
    /// </summary>
    /// <remarks>
    /// TODO: note that IEntityDefinitionRepository and ITileDefinitionRepository are very similar and could potentially be refactored into a single generic interface.
    /// </remarks>
    public static class TileDefinitionHelper
    {
        /// <summary>
        /// Gets the tile definitions from the specified JSON file.
        /// </summary>
        /// <param name="filePath">The path to the JSON file containing the tile definitions.</param>
        /// <returns>A task which, when completed, returns a dictionary of tile definitions keyed on type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the JSON cannot be deserialized.</exception>
		public static async Task<Dictionary<TileType, TileDefinition>> GetDefinitions(string filePath)
        {
			string json = await File.ReadAllTextAsync(filePath);
			return JsonSerializer.Deserialize<Dictionary<TileType, TileDefinition>>(json, TileDefinitionSerializerContext.Default.DictionaryTileTypeTileDefinition)
				?? throw new InvalidOperationException($"Failed to deserialize {typeof(Dictionary<TileType, TileDefinition>).Name}.");
		}
	}
}
