using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ores
{
    internal class TitaniteBar : DecimationItem
    {
        protected override string ItemName => "Titanite Bar";
        protected override string ItemTooltip => "Pulsating with titan-like strength.";

        protected override void Init()
        {
            width = 20;
            height = 20;
            rarity = Rarity.Orange;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, ModContent.TileType<TitanForge>());

            recipe.AddIngredient(ItemID.AdamantiteBar);
            recipe.AddIngredient(ItemID.TitaniumBar);

            return recipe;
        }
    }
}