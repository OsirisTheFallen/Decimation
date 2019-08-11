using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class LightweightGlove : DecimationAccessory
    {
        protected override string ItemName => "Lightweight Glove";

        protected override string ItemTooltip =>
            "+5% throwing velocity\n+4% throwing damages\n+3% throwing critical strikes chances";

        protected override void InitAccessory()
        {
            width = 22;
            height = 28;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownVelocity *= 1.05f;
            player.thrownDamage *= 1.04f;
            player.thrownCrit += 3;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.Loom }, false);

            recipe.AddIngredient(ItemID.Leather, 6);
            recipe.AddRecipeGroup("AnyThread", 2);
            recipe.AddIngredient(ItemID.Sapphire, 2);

            return new List<ModRecipe>() { recipe };
        }
    }
}
