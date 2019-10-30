using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Accessories
{
    internal class NecrosisStone : DecimationAccessory
    {
        protected override string ItemName => "Necrosis Stone";

        protected override string ItemTooltip =>
            "This stone breathes life into once deceased creatures\nReduces respawn time by 50%";

        protected override void InitAccessory()
        {
            width = 20;
            height = 20;
            rarity = Rarity.Rainbow;
            this.item.value = Item.buyPrice(0, 5);
            this.item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DecimationPlayer>().necrosisStoneEquipped = true;
        }
    }
}