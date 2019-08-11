using System;
using System.Reflection;
using Terraria;
using Terraria.ID;

/**
 * <summary>Class <c>ItemUtils</c> provides several static methods to simplify items </summary>
 */
namespace Decimation.Utils
{
    public class ItemUtils
    {
        private static Decimation mod = Decimation.Instance;

        /**
         * <summary>Returns the identifier of an entity.</summary>
         */
        public static int GetIdFromName(string name, Type entityType, bool isVanilla)
        {
            return isVanilla
                ? GetVanillaEntityIdFromName(name, entityType)
                : GetModdedEntityIdFromName(name, entityType);
        }

        /**
         * <summary>Returns the identifier of a modded entity from its name.</summary>
         */
        public static int GetModdedEntityIdFromName(string name, Type entityType)
        {
            int id = int.MinValue;
            if (entityType == typeof(Item))
            {
                id = mod.ItemType(name);
            }
            else if (entityType == typeof(Projectile))
            {
                id = mod.ProjectileType(name);
            }
            else if (entityType == typeof(NPC))
            {
                id = mod.NPCType(name);
            }

            if (id == int.MinValue)
            {
                throw new ArgumentException($"No entity of type {entityType.Name} found with the name '{name}'");
            }

            return id;
        }

        /**
         * <summary>Returns the identifier of a vanilla entity from its name in an ID class using reflection.</summary>
         */
        public static int GetVanillaEntityIdFromName(string name, Type entityType)
        {
            // Get which ID class to use
            Type idType;
            if (entityType == typeof(Item))
            {
                idType = typeof(ItemID);
            }
            else if (entityType == typeof(Projectile))
            {
                idType = typeof(ProjectileID);
            }
            else if (entityType == typeof(NPCID))
            {
                idType = typeof(NPCID);
            }
            else
            {
                throw new ArgumentException($"There is no entity of type ${entityType.Name}");
            }

            // Gets the field in the ID class and check if it's valid
            FieldInfo correspondingItemField = idType.GetField(name);
            if (correspondingItemField == null || correspondingItemField.FieldType != typeof(short))
            {
                throw new ArgumentException($"No entity of type {entityType.Name} found with the name '{name}'");
            }

            return (short)correspondingItemField.GetValue(null);
        }
    }
}
