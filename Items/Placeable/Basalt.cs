using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class Basalt : DecimationPlaceableItem
    {
        protected override string ItemName => "Basalt";
        protected override string ItemTooltip => "Volcanic stone";
        protected override int Tile => ModContent.TileType<Tiles.Basalt>();

        protected override void InitPlaceable()
        {
        }
    }
}