using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Accessories
{
    internal class ShinySentinel : DecimationAccessory
    {
        protected override string ItemName => "Shiny Sentinel";
        protected override string ItemTooltip => "Doubles life and mana regeneration \n+5 defense";

        protected override void InitAccessory()
        {
            width = 30;
            height = 28;
            rarity = Rarity.Rainbow;
            this.item.value = Item.buyPrice(0, 45);
            this.item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen *= 2;
            player.manaRegen *= 2;
            player.statDefense += 5;
        }
    }
}