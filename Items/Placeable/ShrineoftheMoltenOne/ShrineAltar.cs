using Decimation.Core.Items;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineAltar : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Altar";
        protected override int Tile => this.mod.TileType<Tiles.ShrineoftheMoltenOne.ShrineAltar>();

        protected override void InitPlaceable()
        {
            width = 66;
            height = 32;
        }
    }
}