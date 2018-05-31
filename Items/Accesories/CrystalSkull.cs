using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accesories
{
    public class CrystalSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Skull");
			Tooltip.SetDefault("WIP");
		}
		public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
			item.defense = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.ObsidianSkull);
			recipe.AddIngredient(ItemID.CrystalShard, 5);
			recipe.AddRecipeGroup("AnyGem", 4);
            recipe.AddIngredient(ItemID.Glass, 6);			
            recipe.AddTile(TileID.GlassKiln);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.thorns = 1f;
		}
	}
}