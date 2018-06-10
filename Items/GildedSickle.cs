using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Decimation.Items
{
    public class GildedSickle : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Gilded Sickle");
			Tooltip.SetDefault("Allows the collection of hay from grass");
		}
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Sickle);		
            item.width = 16;
            item.height = 16;
            item.value = 100;
            item.rare = 1;
			item.damage = 10;
            item.melee = true;
            item.knockBack = 5;
            item.useTime = 14;
            item.useAnimation = 14;			
        }
		public override void AddRecipes()
  		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sickle);
			recipe.AddIngredient(ItemID.GoldBar, 5);			
			recipe.AddIngredient(null, "SoulofTime", 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}		
    }
}
