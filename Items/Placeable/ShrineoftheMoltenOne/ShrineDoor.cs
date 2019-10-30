using Decimation.Tiles.ShrineoftheMoltenOne;
using Decimation.Core.Items;
using Terraria;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    internal class ShrineDoor : DecimationPlaceableItem
    {
        protected override string ItemName => "Shrine Door";
        protected override int Tile => this.mod.TileType<LockedShrineDoor>();

        protected override void InitPlaceable()
        {
            width = 14;
            height = 28;
            this.item.maxStack = 99;
            value = Item.buyPrice(0, 0, 1, 50);
        }
    }
}