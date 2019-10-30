using Decimation.Core.Items;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class RedHotSpike : DecimationPlaceableItem
    {
        protected override string ItemName => "Red Hot Spike";
        protected override int Tile => this.mod.TileType<Tiles.ShrineoftheMoltenOne.RedHotSpike>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}