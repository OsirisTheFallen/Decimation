using System.Collections.Generic;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class SlimeBracelet : DecimationAccessory
    {
        protected override string ItemName => "Slime Bracelet";
        protected override string ItemTooltip => "WIP";

        protected override void InitAccessory()
        {
            width = 24;
            height = 24;
            rarity = Rarity.Green;

            this.item.value = Item.buyPrice(0, 0, 0, 10);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.WorkBenches}, true);

            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddIngredient(ItemID.Aglet);

            return new List<ModRecipe> {recipe};
        }
    }
}