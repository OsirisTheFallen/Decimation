// Coded by Evie, Sprite using HourHand.png
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
	public class HourHand : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hour Hand");
			Tooltip.SetDefault("No tooltip yet. Ping me on Discord when theres a tooltip Oris/Fyloz");
		}

		public override void SetDefaults() 
		{
			item.damage = 45;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 28;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = 27;
			item.crit = 7;
			item.shootSpeed = 10f;
		}

/* 		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		} */ // Add recipe here if needed, couldn't find one in #to-code
	}
}