using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Items.Accesories
{
	public class JestersQuiver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jester's Quiver");
			Tooltip.SetDefault("Turn wooden arrows into jesters arrows \n+15% Ranged Damages \n-20% Ammo Cost \n+5% Ranged Critical Chances \nTO FINISH!");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 1;
			item.value = 0;
			item.rare = 1;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.rangedDamage += 0.15f;
			player.ammoCost80 = true;
			player.rangedCrit += 5;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RangersQuiver"));
			recipe.AddIngredient(mod.ItemType("RangersPouch"));
			recipe.AddIngredient(ItemID.SoulofSight, 25);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.FallenStar, 15);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}