using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace Decimation.Items.Weapons
{
    public class SlingshotWood : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slingshot");
			Tooltip.SetDefault("Uses pebbles and marbles as ammo");
		}
        public override void SetDefaults()
        {
            item.damage = 7; 
            item.noMelee = true; 
            item.thrown = true; 
            item.width = 32; 
            item.height = 32; 
            item.useTime = 16; 
            item.useAnimation = 16;  
            item.useStyle = 5; 
            item.shoot = 1; 
			item.useAmmo = mod.ItemType("Pebble");
            item.knockBack = 6; 
            item.rare = 3; 
            item.UseSound = SoundID.Item5;
            item.autoReuse = false; 
            item.shootSpeed = 10f;
            item.crit = 10; 
		}	
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 12); 
//			recipe.AddIngredient(null,"CrimsoniteBar", 10);
			recipe.AddTile(TileID.WorkBenches); 
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}