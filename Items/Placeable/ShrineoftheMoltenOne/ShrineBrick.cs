using Decimation.Core.Items;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineBrick : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Brick";
        protected override int Tile => this.mod.TileType<Tiles.ShrineoftheMoltenOne.ShrineBrick>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}