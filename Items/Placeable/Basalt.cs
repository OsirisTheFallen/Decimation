using Decimation.Core.Items;

namespace Decimation.Items.Placeable
{
    internal class Basalt : DecimationPlaceableItem
    {
        protected override string ItemName => "Basalt";
        protected override string ItemTooltip => "Volcanic stone";
        protected override int Tile => this.mod.TileType<Tiles.Basalt>();

        protected override void InitPlaceable()
        {
        }
    }
}