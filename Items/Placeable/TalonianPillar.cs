using Decimation.Core.Items;

namespace Decimation.Items.Placeable
{
    internal class TalonianPillar : DecimationPlaceableItem
    {
        protected override string ItemName => "Talonian Pillar";
        protected override string ItemTooltip => "A heavenly pillar created by powerful Talonian warlocks.";
        protected override int Tile => this.mod.TileType("TalonianPillar"); // The tile doesn't exist yet

        protected override void InitPlaceable()
        {
        }
    }
}