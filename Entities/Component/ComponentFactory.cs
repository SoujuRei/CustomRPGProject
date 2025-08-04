using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomProjectRPG.Entities.Enum;
using Enum = CustomProjectRPG.Entities.Enum.Enum;


namespace CustomProjectRPG.Entities.Component
{
    

    public static class ComponentFactory
    {
        public static object CreateComponent(Enum.Enum.EntityType entityType)
        {
            return entityType switch
            {
               Enum.Enum.EntityType.Player => new CorruptionComponent(entityType),
               Enum.Enum.EntityType.NPC => new CorruptionComponent(entityType),
               Enum.Enum.EntityType.Beast => new CorruptionComponent(entityType),
                _ => throw new ArgumentException("Unsupported entity type.", nameof(entityType))
            };
        }

        // Overload for specific component types if needed
        public static T CreateSpecificComponent<T>(Enum.Enum.EntityType entityType) where T : class
        {
            if (typeof(T) == typeof(CorruptionComponent))
            {
                return new CorruptionComponent(entityType) as T ?? throw new InvalidOperationException("Failed to create component.");
            }
            if (typeof(T) == typeof(StatsComponent))
            {
                return new StatsComponent(entityType) as T ?? throw new InvalidOperationException("Failed to create component.");
            }
            throw new ArgumentException("Unsupported component type.", nameof(T));
        }
    }
}
