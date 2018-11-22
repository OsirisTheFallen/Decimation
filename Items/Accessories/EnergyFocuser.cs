using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class EnergyFocuser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Focuser");
			Tooltip.SetDefault("+5% Crit\n10% Magic & Ranged damage & 4% melee damage\nIncreaed max mana & health\nIcreased mana & health regen.");
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
            recipe.AddIngredient(null, "EnchantedFocuser", 1);
			recipe.AddIngredient(ItemID.PixieDust, 40);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddIngredient(ItemID.SoulofLight, 15);
			recipe.AddIngredient(null, "Focuser", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.rangedDamage += 0.10f;
			player.magicDamage += 0.10f;
			player.rangedCrit += 05;
			player.meleeCrit += 05;
			player.magicCrit += 05;
			player.thrownCrit += 05;
			player.manaRegen += 2;
			player.statManaMax2 += 20;
			player.statLifeMax2 += 20;
			player.lifeRegen += 2;
			player.meleeDamage += 0.04f;
        }
    
    }
}