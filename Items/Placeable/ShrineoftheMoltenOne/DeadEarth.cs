using Decimation.Core.Items;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class DeadEarth : DecimationPlaceableItem
    {
        protected override string ItemName => "Dead Earth";
        protected override int Tile => this.mod.TileType<Tiles.ShrineoftheMoltenOne.DeadEarth>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}