using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization.Metadata;

namespace GameEngine.Entities
{
    /// <summary>
    /// Represents the definition of a type of entity in the game engine.
    /// </summary>
    public class EntityDefinition : IJsonSerializable<EntityDefinition>
    {
        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        public EntityType Type { get; init; }

        /// <summary>
        /// Gets the name of the entity type.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the entity type is alive (can take damage and be killed).
        /// </summary>
        public bool Alive { get; init; } = false;

        /// <summary>
        /// Gets the health points of the entity type.
        /// This value represents the amount of damage the entity can take before being defeated.
        /// </summary>
        public int Health { get; init; } = 0;

        /// <summary>
        /// Gets the stamina points of the entity type.
        /// This value represents the entity's ability to perform actions, such as attacking, moving or casting spells.
        /// </summary>
        public int Stamina { get; init; } = 0;

        /// <summary>
        /// Gets the attack power of the entity type.
        /// This value represents the amount of damage the entity can inflict on others.
        /// </summary>
        public int Attack { get; init; } = 0;

        /// <summary>
        /// Gets the defence power of the entity type.
        /// This value represents the entity's ability to resist damage from others.
        /// </summary>
        public int Defence { get; init; } = 0;

        /// <summary>
        /// Gets the arcana power of the entity type.
        /// This value represents the entity's effectiveness with magical abilities.
        /// </summary>
        public int Arcana { get; init; } = 0;

        /// <summary>
        /// Gets the esotericism level of the entity type.
        /// This value represents the entity's ability to resist incoming magical effects.
        /// </summary>
        public int Esotericism { get; init; } = 0;

        /// <summary>
        /// Gets the speed of the entity type.
        /// This value represents the entity's movement capability.
        /// </summary>
        public int Speed { get; init; } = 0;

        /// <summary>
        /// Gets a value indicating whether the entity type is vulnerable (can be damaged).
        /// </summary>
        public bool Vulnerable { get; init; } = false;

        /// <summary>
        /// Gets a value indicating whether the entity type is tangible (can be collided with).
        /// </summary>
        public bool Tangiable { get; init; } = false;

        /// <inheritdoc/>
        public static JsonTypeInfo<EntityDefinition> JsonTypeInfo => 
            EntityDefinitionSerializerContext.Default.EntityDefinition;
    }
}
