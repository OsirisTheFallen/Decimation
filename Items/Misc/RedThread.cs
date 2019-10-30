using System.Collections.Generic;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    internal class RedThread : DecimationItem
    {
        protected override string ItemName => "Red Thread";

        protected override void Init()
        {
            width = 28;
            height = 20;
            rarity = Rarity.Gray;
            value = Item.buyPrice(0, 2);

            this.item.maxStack = 99;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.Tables, TileID.Chairs});

            recipe.AddIngredient(ItemID.RedDye);
            recipe.AddIngredient(ItemID.PinkThread, 5);

            return recipe;
        }
    }
}