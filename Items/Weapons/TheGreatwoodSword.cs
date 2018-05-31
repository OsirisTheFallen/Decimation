using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    public class TheGreatwoodSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Greatwood Sword");
			Tooltip.SetDefault("");
		}		
        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 25;
            item.useAnimation = 25;     
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 4000;        
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
			item.expert = false;
		}	
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
			recipe.AddIngredient(ItemID.WoodenSword, 1);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddIngredient(ItemID.ShadewoodSword, 1);
			recipe.AddIngredient(ItemID.PalmWoodSword, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenSword, 1);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddIngredient(ItemID.EbonwoodSword, 1);
			recipe.AddIngredient(ItemID.PalmWoodSword, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}