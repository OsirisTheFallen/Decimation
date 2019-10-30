using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ores
{
    internal class DenziumBar : DecimationItem
    {
        protected override string ItemName => "Denzium Bar";
        protected override string ItemTooltip => "It pulsates with sheer density";

        protected override void Init()
        {
            width = 20;
            height = 20;
            rarity = Rarity.Cyan;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, this.mod.TileType<TitanForge>());

            recipe.AddIngredient(ItemID.AdamantiteBar);
            recipe.AddIngredient(ItemID.TitaniumBar);

            return recipe;
        }
    }
}