using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineAltar : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Altar";
        protected override int Tile => ModContent.TileType<Tiles.ShrineoftheMoltenOne.ShrineAltar>();

        protected override void InitPlaceable()
        {
            width = 66;
            height = 32;
        }
    }
}