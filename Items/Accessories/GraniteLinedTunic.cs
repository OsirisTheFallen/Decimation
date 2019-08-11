using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class GraniteLinedTunic : DecimationAccessory
    {
        protected override string ItemName => "Granite Lined Tunic";

        protected override void InitAccessory()
        {
            width = 30;
            height = 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.Anvils }, false);

            recipe.AddIngredient(ItemID.FamiliarShirt);
            recipe.AddIngredient(ItemID.Granite, 16);
            recipe.AddRecipeGroup("AnyThread", 10);
            recipe.AddIngredient(ItemID.Chain, 6);

            return new List<ModRecipe>() { recipe };
        }
    }
}
