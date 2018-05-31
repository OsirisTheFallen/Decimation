using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
    public class GreatwoodHammer : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greatwood Mallet");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 10;
            item.useAnimation = 25;
            item.hammer = 55;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 1000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenHammer);
			recipe.AddIngredient(ItemID.BorealWoodHammer);
			recipe.AddIngredient(ItemID.ShadewoodHammer);
			recipe.AddIngredient(ItemID.PalmWoodHammer);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenHammer);
			recipe.AddIngredient(ItemID.BorealWoodHammer);
			recipe.AddIngredient(ItemID.EbonwoodHammer);
			recipe.AddIngredient(ItemID.PalmWoodHammer);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}		
    }
}