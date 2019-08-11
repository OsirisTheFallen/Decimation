using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class RedHotSpike : DecimationPlaceableItem
    {
        protected override string ItemName => "Red Hot Spike";
        protected override int Tile => mod.TileType<Tiles.ShrineoftheMoltenOne.RedHotSpike>();

        protected override void InitPlaceable()
        {
            width = 12;
            height = 12;
        }
    }
}
