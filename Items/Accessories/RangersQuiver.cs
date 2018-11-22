using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class RangersQuiver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ranger's Quiver");
			Tooltip.SetDefault("25% Chance not to consume ammo\n+10%ranged damage\n+15% arrow velocity\n+5% ranged Crit Chance");
		}
		public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
			recipe.AddIngredient(ItemID.MagicQuiver, 1);
			recipe.AddIngredient(ItemID.RangerEmblem, 1);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.rangedDamage += 0.10f;
			player.rangedCrit += 05;
			player.ammoCost75 = true;
        }
    
    }
}