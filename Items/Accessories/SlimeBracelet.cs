using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class SlimeBracelet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slime Bracelet");
			Tooltip.SetDefault("WIP");
		}
		public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 10;
            item.rare = 2;
            item.accessory = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Shackle);
			recipe.AddIngredient(ItemID.Gel, 5);
			recipe.AddIngredient(ItemID.Aglet);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

/*		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Main.rand.Next(20) == 0)
			{
				target.AddBuff(BuffID.Slimed, 360, false);
			}
		}*/
	}
}