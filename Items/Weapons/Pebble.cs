using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
	public class Pebble : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pebble");
			Tooltip.SetDefault("For use with slings and slingshots");
		}
		public override void SetDefaults()
		{
			item.damage = 1;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = 0;
			item.shoot = mod.ProjectileType("Pebble");
			item.ammo = item.type; // The first item in an ammo class sets the AmmoID to it's type
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 15);
			recipe.AddRecipe();
		}
	}
}	