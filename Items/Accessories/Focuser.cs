using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class Focuser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Focuser");
			Tooltip.SetDefault("+5% Crit & Ranged damage");
		}
		public override void SetDefaults()
        {
            item.width = 54;
            item.height = 46;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Chain, 3);
			recipe.AddIngredient(ItemID.CopperBar, 10);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.AddIngredient(ItemID.Ruby, 1);
			recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.rangedDamage += 0.05f;
			player.rangedCrit += 05;
			player.meleeCrit += 05;
			player.magicCrit += 05;
			player.thrownCrit += 05;
        }
    
    }
}
