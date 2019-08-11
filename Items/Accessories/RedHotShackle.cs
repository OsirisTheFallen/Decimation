using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class RedHotShackle : DecimationAccessory
    {
        protected override string ItemName => "Red Hot Shackle";
        protected override string ItemTooltip => "WIP";

        protected override void InitAccessory()
        {
            width = 24;
            height = 24;
            rarity = Rarity.Green;
            item.value = Item.buyPrice(0, 0, 2);
            item.defense = 1;
        }
        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.Furnaces }, true);

            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddIngredient(ItemID.Gel, 10);

            return new List<ModRecipe>() { recipe };
        }
    }
}
