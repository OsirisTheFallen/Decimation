using Decimation.Core.Items;
using Terraria;

namespace Decimation.Items.Placeable.DuneWorm
{
    internal class DuneWormTrophy : DecimationPlaceableItem
    {
        protected override string ItemName => "Ancient Dune Worm Trophy";
        protected override string ItemTooltip => "Achievement get!";
        protected override int Tile => this.mod.TileType<Tiles.DuneWormTrophy>();

        protected override void InitPlaceable()
        {
            width = 30;
            height = 30;
            this.item.maxStack = 99;
            this.item.value = Item.buyPrice(0, 5);
        }
    }
}