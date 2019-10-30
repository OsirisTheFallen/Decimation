using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Misc
{
    internal class LunarTablet : DecimationItem
    {
        protected override string ItemName => "Lunar Tablet";
        protected override string ItemTooltip => "Summons the full moon.";

        protected override void Init()
        {
            width = 30;
            height = 40;
            consumable = true;
            value = 25000;
            rarity = Rarity.Green;
            useStyle = 1;
            useTime = 20;
            useAnimation = 20;

            this.item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return Main.dayTime;
        }

        public override bool UseItem(Player player)
        {
            Main.moonPhase = 0;
            Main.dayTime = false;

            return true;
        }
    }
}