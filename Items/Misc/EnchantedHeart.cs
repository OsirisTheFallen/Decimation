using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Items.Amulets;

namespace Decimation.Items.Misc
{
    class EnchantedHeart : ModItem
    {
        public override string Texture
        {
            get
            {
                return "Terraria/Item_" + ItemID.Heart;
            }
        }

        private int healAmount = 25;
        private int manaAmount = 5;

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Heart);
        }

        public override bool OnPickup(Player player)
        {
            player.statLife += healAmount;
            player.HealEffect(healAmount);

            player.statMana += manaAmount;
            player.ManaEffect(manaAmount);

            return false;
        }

        public override bool CanPickup(Player player)
        {
            return player.GetModPlayer<DecimationPlayer>().amuletSlotItem.type != mod.ItemType<HealtyAmulet>();
        }
    }
}
