using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Items.Amulets;

namespace Decimation.Items.Misc
{
    class EnchantedHeart : DecimationItem
    {
        private const int HealAmount = 25;
        private const int ManaAmount = 5;

        protected override string ItemName => "Enchanted Heart";
        protected override bool IsClone => true;
        public override string Texture => "Terraria/Item_" + ItemID.Heart;

        protected override void Init()
        {
            item.CloneDefaults(ItemID.Heart);
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
            return player.GetModPlayer<DecimationPlayer>().amuletSlotItem.type != mod.ItemType<HealtyAmulet>();
        }
    }
}
