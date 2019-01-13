using Terraria.ModLoader;
using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Decimation;

namespace Decimation.Items.Accessories
{
    public class DeadeyesQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into ichor arrows\n+16% ranged damage\n+15% arrow and bullet velocity\n15% chance not to consume ammo\nTurns musket balls into chlorophyte bullets\n+2% increased ranged crit chance");
            DisplayName.SetDefault("Deadeye's Quiver");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.rare = 10;
            item.value = 150000;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(mod.ItemType("JestersQuiver"));
            r.AddIngredient(mod.ItemType("SoulofKight"));
            r.AddIngredient(ItemID.SoulofSight, 15);
            r.AddIngredient(ItemID.SoulofFright, 15);
            r.AddIngredient(mod.ItemType("EndlessPouchOfLife"));
            r.AddIngredient(mod.ItemType("RedThread"), 5);
            r.AddIngredient(ItemID.FlaskofIchor, 5);
            r.AddIngredient(ItemID.BlackDye, 3);
            r.AddIngredient(ItemID.RedDye, 3);
            r.SetResult(this);
            r.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.16f;
            player.rangedCrit += 2;
            player.GetModPlayer<DecimationPlayer>().deadeyesQuiverEquipped = true;
        }
    }
}
