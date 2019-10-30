using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Tools
{
    internal class TheHourGlass : DecimationTool
    {
        protected override string ItemName => "The Hour Glass";
        protected override string ItemTooltip => "Costs 50 mana.";

        protected override void InitTool()
        {
            this.item.mana = 50;
            width = 22;
            height = 36;
            useTime = 16;
            useAnimation = 16;
            useStyle = 4;
            rarity = Rarity.Purple;
            this.item.expert = true;
        }

        public override bool UseItem(Player player)
        {
            if (Main.dayTime)
                Main.dayTime = false;
            else if (!Main.dayTime) Main.dayTime = true;
            return true;
        }
    }
}