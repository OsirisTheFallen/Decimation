using Decimation.Items.Accessories;
using Decimation.Items.Weapons.Arachnus;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;

namespace Decimation.Items.Boss.Arachnus
{
    internal class ArachnusTreasureBag : DecimationItem
    {
        protected override string ItemName => "Treasure Bag";
        protected override string ItemTooltip => "{$CommonItemTooltip.RightClickToOpen}";
        public override int BossBagNPC => this.mod.NPCType<NPCs.Arachnus.Arachnus>();

        protected override void Init()
        {
            consumable = true;
            width = 32;
            height = 32;
            rarity = Rarity.Cyan;

            this.item.expert = true;
            this.item.maxStack = 999;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();

            int rand = Main.rand.Next(3);
            if (rand == 0)
                player.QuickSpawnItem(this.mod.ItemType<ChainStynger>());
            else if (rand == 1)
                player.QuickSpawnItem(this.mod.ItemType<GlaiveWeaver>());
            else if (rand == 2)
                player.QuickSpawnItem(this.mod.ItemType<Infernolizer>());

            player.QuickSpawnItem(this.mod.ItemType<ShinySentinel>());
        }
    }
}