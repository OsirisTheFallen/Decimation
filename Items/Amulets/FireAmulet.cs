using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    public class FireAmulet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Amulet");
			Tooltip.SetDefault("WIP");
		}
		public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 10;
            item.rare = 2;

            Decimation.amulets.Add(item.type);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.Obsidian, 6);
			recipe.AddIngredient(ItemID.Gel, 20);
			recipe.AddIngredient(ItemID.Chain, 2);
			recipe.AddIngredient(null, "RedHotShackle");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Player player = Main.LocalPlayer;

            player.meleeDamage += 0.03f;
            player.meleeCrit += 3;
            player.meleeSpeed += 0.03f;
            player.lavaMax += 420;
        }
	}
}