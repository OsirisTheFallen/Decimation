using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ores
{
    public class DenziumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Denzium Bar");
            Tooltip.SetDefault("Interstellar!");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 0;
            item.rare = 9;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar);
            recipe.AddIngredient(ItemID.TitaniumBar);
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}