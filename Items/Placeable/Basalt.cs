using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class Basalt : DecimationPlaceableItem
    {
        protected override string ItemName => "Basalt";
        protected override string ItemTooltip => "Volcanic stone";
        protected override int Tile => mod.TileType<Tiles.Basalt>();

        protected override void InitPlaceable()
        {
        }
    }
}