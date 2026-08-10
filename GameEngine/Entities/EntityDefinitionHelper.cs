using System.Text.Json;

namespace GameEngine.Entities
{
    /// <summary>
    /// Helper for loading entity definitions from a JSON file.
    /// </summary>
    /// <remarks>
    /// TODO: note that IEntityDefinitionRepository and ITileDefinitionRepository are very similar and could potentially be refactored into a single generic interface.
    /// </remarks>
    public static class EntityDefinitionHelper
    {
        /// <summary>
        /// Gets the entity definitions from the specified JSON file.
        /// </summary>
        /// <param name="filePath">The path to the JSON file containing the entity definitions.</param>
        /// <returns>A task which, when completed, returns a dictionary of entity definitions keyed on type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the JSON cannot be deserialized.</exception>
        public static async Task<Dictionary<EntityType, EntityDefinition>> GetDefinitions(string filePath)
		{
			string json = await File.ReadAllTextAsync(filePath);
			return JsonSerializer.Deserialize<Dictionary<EntityType, EntityDefinition>>(json, EntityDefinitionSerializerContext.Default.DictionaryEntityTypeEntityDefinition)
				?? throw new InvalidOperationException($"Failed to deserialize {typeof(Dictionary<EntityType, EntityDefinition>).Name}.");
		}
	}
}
