using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class DenziumOre : DecimationPlaceableItem
    {
        protected override string ItemName => "Denzium Ore";
        protected override string ItemTooltip => "A substance created from intense pressure and heat.";
        protected override int Tile => ModContent.TileType<Tiles.DenziumOre>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}