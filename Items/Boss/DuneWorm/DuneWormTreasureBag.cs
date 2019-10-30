using Decimation.Items.Misc.Souls;
using Decimation.Items.Placeable.DuneWorm;
using Decimation.Items.Tools;
using Decimation.Items.Vanity.DuneWorm;
using Decimation.NPCs.AncientDuneWorm;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;

namespace Decimation.Items.Boss.DuneWorm
{
    internal class DuneWormTreasureBag : DecimationItem
    {
        protected override string ItemName => "Treasure Bag";
        protected override string ItemTooltip => "Right click to open";
        public override int BossBagNPC => this.mod.NPCType<AncientDuneWormHead>();

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
            player.QuickSpawnItem(this.mod.ItemType<SoulofTime>(), Main.rand.Next(20, 35));
            player.QuickSpawnItem(ItemID.FossilOre, Main.rand.Next(10, 15));
            player.QuickSpawnItem(this.mod.ItemType<TheHourGlass>());
            if (Main.rand.Next(7) == 0)
                player.QuickSpawnItem(this.mod.ItemType<DuneWormMask>());
            if (Main.rand.NextBool(13))
                player.QuickSpawnItem(this.mod.ItemType<DuneWormTrophy>());
        }
    }
}