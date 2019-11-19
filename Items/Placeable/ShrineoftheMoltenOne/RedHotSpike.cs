using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class RedHotSpike : DecimationPlaceableItem
    {
        protected override string ItemName => "Red Hot Spike";
        protected override int Tile => ModContent.TileType<Tiles.ShrineoftheMoltenOne.RedHotSpike>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}