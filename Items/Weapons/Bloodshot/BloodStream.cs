using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Decimation.Items.Weapons.Bloodshot
{
    public class BloodStream : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Stream");
			Tooltip.SetDefault("Bathe your ennemies in blood");
		}
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 100;
            item.rare = 1;
            item.maxStack = 999;
        }
		public override void AddRecipes()
  		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "BloodiedEssence", 10);
			recipe.AddIngredient(ItemID.Vilethorn);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}