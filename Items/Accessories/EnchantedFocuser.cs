using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class EnchantedFocuser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Focuser");
			Tooltip.SetDefault("+2% Crit\n10% Magic & Ranged damage\nIncreaed mana & mana regen.");
		}
		public override void SetDefaults()
        {
            item.width = 62;
            item.height = 46;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddIngredient(ItemID.Wire, 15);
			recipe.AddIngredient(ItemID.CopperBar, 5);
			recipe.AddIngredient(ItemID.WaterCandle, 1);
			recipe.AddIngredient(null, "Focuser", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.rangedDamage += 0.10f;
			player.magicDamage += 0.10f;
			player.rangedCrit += 02;
			player.meleeCrit += 02;
			player.magicCrit += 02;
			player.thrownCrit += 02;
			player.manaRegen += 2;
			player.statManaMax2 += 20;
        }
    
    }
}
