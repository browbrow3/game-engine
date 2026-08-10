using GameEngine.Entities;
using GameEngine.Events;

namespace GameEngine.Map.Tiles
{
    /// <summary>
    /// Represents a tile in the game engine.
    /// </summary>
    public class Tile : ITile
    {
        /// <inheritdoc/>
        public TileType Type => this.definition.Type;

        /// <inheritdoc/>
        public string Name => this.definition.Name;

        /// <inheritdoc/>
        public bool Tangiable => this.definition.Tangiable;

        /// <inheritdoc/>
        public float SpeedMultiplier => this.definition.SpeedMultiplier;

        /// <inheritdoc/>
        public int StatusEffects => this.definition.StatusEffects;

        /// <inheritdoc/>
        public char PrintChar => this.definition.PrintChar;

        /// <inheritdoc/>
        public bool Passable
        {
            get
            {
                if (this.definition.Tangiable)
                {
                    return false;
                }
                // entities don't support tangiability yet, so for now just check if any entity exists on the tile.
                if (this.entities.Any())
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Tile class based on the provided TileDefinition.
        /// </summary>
        /// <param name="definition">Tile definition pertaining to the tile.</param>
        /// <param name="entities">A dictionary of entities present on the tile.</param>
        public Tile(TileDefinition definition, Dictionary<int, IEntity>? entities = null)
        {
            this.definition = definition;
            this.entities = entities ?? [];
        }

        /// <inheritdoc/>
        public short AddEntity(IEntity entity)
        {
            if (!entities.ContainsKey(entity.Id))
            {
                entities[entity.Id] = entity;
                return 0;
            }
            return -1;
        }

        /// <inheritdoc/>
        public short RemoveEntity(IEntity entity)
        {
            if (entities.ContainsKey(entity.Id))
            {
                entities.Remove(entity.Id);
                return 0;
            }
            return -1;
        }

        /// <inheritdoc/>
        public IEnumerable<IEntity> GetEntities()
        {
            return entities.Values;
        }

        /// <inheritdoc/>
        public short PublishEvent(IEvent e)
        {
            foreach (IEntity entity in entities.Values)
            {
                entity.HandleEvent(e);
            }
            return 0;
        }

        /// <summary>
        /// The definition of the tile, which contains its properties and characteristics.
        /// </summary>
        private TileDefinition definition;

        /// <summary>
        /// A dictionary to store entities present on the tile, keyed by their unique identifier.
        /// </summary>
        private Dictionary<int, IEntity> entities;
    }
}
