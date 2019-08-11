using Terraria.ModLoader;

namespace Decimation.Items.Vanity.DuneWorm
{
    [AutoloadEquip(EquipType.Head)]
    internal class DuneWormMask : DecimationItem
    {
        protected override string ItemName => "Ancient Dune Worm Mask";
        protected override string ItemTooltip => "A bit dusty";

        protected override void Init()
        {
            width = 36;
            height = 32;
            rarity = Rarity.Orange;
            item.vanity = true;
        }

        public override bool DrawHead()
        {
            return false;
        }
    }
}