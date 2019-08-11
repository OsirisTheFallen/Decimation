using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.Souls
{
    internal class SoulofTime : DecimationItem
    {
        protected override string ItemName => "Soul of Time";
        protected override string ItemTooltip => "The essence of fate.";
        protected override DrawAnimation Animation => new DrawAnimationVertical(5, 4);

        protected override void Init()
        {
            width = 22;
            height = 24;
            value = Item.buyPrice(0, 0, 1);
            rarity = Rarity.Orange;

            item.maxStack = 999;

            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
    }
}