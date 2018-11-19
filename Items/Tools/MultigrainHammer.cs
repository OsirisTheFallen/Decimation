using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    public class MultigrainHammer : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Multigrain Hamaxe");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.hammer = 67;
			item.axe = 13;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 1000;
            item.rare = 2;
			item.crit = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GreatwoodHammer");
			recipe.AddIngredient(ItemID.CactusPickaxe);
			recipe.AddIngredient(ItemID.Pumpkin, 10);
			recipe.AddIngredient(ItemID.Vine, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}		
    }
}