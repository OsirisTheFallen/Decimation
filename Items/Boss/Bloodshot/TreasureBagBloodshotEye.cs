using Decimation.Items.Accessories;
using Decimation.Items.Misc;
using Decimation.Items.Weapons.Bloodshot;
using Decimation.NPCs.Bloodshot;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Bloodshot
{
    internal class TreasureBagBloodshotEye : DecimationItem
    {
        protected override string ItemName => "Treasure Bag";
        protected override string ItemTooltip => "{$CommonItemTooltip.RightClickToOpen}";
        public override int BossBagNPC => ModContent.NPCType<BloodshotEye>();

        protected override void Init()
        {
            consumable = true;
            width = 24;
            height = 24;
            rarity = Rarity.Rainbow;

            this.item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<BloodiedEssence>(), Main.rand.Next(35, 51));
            player.QuickSpawnItem(ModContent.ItemType<NecrosisStone>());

            int random = Main.rand.Next(3);
            int weapon = 0;

            switch (random)
            {
                case 0:
                    weapon = ModContent.ItemType<VampiricShiv>();
                    break;
                case 1:
                    weapon = ModContent.ItemType<Umbra>();
                    break;
                case 2:
                    weapon = ModContent.ItemType<BloodStream>();
                    break;
                default:
                    Main.NewText(
                        "Unexpected error in Bloodshot Eye drops: weapon drop random is out of range (" + random + ").",
                        Color.Red);
                    break;
            }

            player.QuickSpawnItem(weapon);
        }
    }
}