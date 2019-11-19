using Decimation.Items.Amulets;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    internal class EnchantedHeart : DecimationItem
    {
        private const int HealAmount = 25;
        private const int ManaAmount = 5;

        protected override string ItemName => "Enchanted Heart";
        protected override bool IsClone => true;
        public override string Texture => "Terraria/Item_" + ItemID.Heart;

        protected override void Init()
        {
            this.item.CloneDefaults(ItemID.Heart);
        }

        public override bool OnPickup(Player player)
        {
            player.statLife += HealAmount;
            player.HealEffect(HealAmount);

            player.statMana += ManaAmount;
            player.ManaEffect(ManaAmount);

            return false;
        }

        public override bool CanPickup(Player player)
        {
            return player.GetModPlayer<DecimationPlayer>().AmuletSlotItem.type != ModContent.ItemType<HealtyAmulet>();
        }
    }
}