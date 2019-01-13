using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class RedHotShackle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Hot Shackle");
			Tooltip.SetDefault("WIP");
		}
		public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 300;
            item.rare = 2;
            item.accessory = true;
			item.defense = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Shackle);
			recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
/*		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefence += 1;
		}*/
    
    }
}
