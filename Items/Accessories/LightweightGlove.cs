using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class LightweightGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+5% throwing velocity\n+4% throwing damages\n+3% throwing critical strikes chances");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownVelocity *= 1.05f;
            player.thrownDamage *= 1.04f;
            player.thrownCrit += 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 6);
            recipe.AddIngredient(ItemID.BlackThread, 2);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 6);
            recipe.AddIngredient(ItemID.GreenThread, 2);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 6);
            recipe.AddIngredient(ItemID.PinkThread, 2);
            recipe.AddIngredient(ItemID.Sapphire, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
