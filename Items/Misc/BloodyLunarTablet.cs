using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    internal class BloodyLunarTablet : DecimationItem
    {
        protected override string ItemName => "Bloody Lunar Tablet";
        protected override string ItemTooltip => "Summon blood moon.";

        protected override void Init()
        {
            width = 30;
            height = 40;
            rarity = Rarity.Green;
            value = 50000;
            useStyle = 1;
            useTime = 20;
            useAnimation = 20;
            consumable = true;

            item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.bloodMoon;
        }

        public override bool UseItem(Player player)
        {
            Main.bloodMoon = true;
            return true;
        }
    }
}
