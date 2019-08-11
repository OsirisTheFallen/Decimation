using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineAltar : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Altar";
        protected override int Tile => mod.TileType<Tiles.ShrineoftheMoltenOne.ShrineAltar>();

        protected override void InitPlaceable()
        {
            width = 66;
            height = 32;
        }
    }
}
