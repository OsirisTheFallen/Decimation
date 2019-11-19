using Decimation.Core.Items;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class DeadEarth : DecimationPlaceableItem
    {
        protected override string ItemName => "Dead Earth";
        protected override int Tile => ModContent.TileType<Tiles.ShrineoftheMoltenOne.DeadEarth>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}