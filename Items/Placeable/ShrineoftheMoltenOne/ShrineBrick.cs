using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineBrick : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Brick";
        protected override int Tile => ModContent.TileType<Tiles.ShrineoftheMoltenOne.ShrineBrick>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}