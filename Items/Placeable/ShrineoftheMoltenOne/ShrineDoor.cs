using System;
using Decimation.Tiles.ShrineoftheMoltenOne;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineDoor : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Door";
        protected override int Tile => mod.TileType<LockedShrineDoor>();

        protected override void InitPlaceable()
        {
            width = 14;
            height = 28;
            item.maxStack = 99;
            value = Item.buyPrice(0, 0, 1, 50);
        }
    }
}
