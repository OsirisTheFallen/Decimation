using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    internal class ArachnusTreasureBag : DecimationItem
    {
        protected override string ItemName => "Treasure Bag";
        protected override string ItemTooltip => "{$CommonItemTooltip.RightClickToOpen}";
        public override int BossBagNPC => mod.NPCType<NPCs.Arachnus.Arachnus>();

        protected override void Init()
        {
            consumable = true;
            width = 32;
            height = 32;
            rarity = Rarity.Cyan;

            item.expert = true;
            item.maxStack = 999;
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
                player.QuickSpawnItem(mod.ItemType("ChainStynger"));
            else if (rand == 1)
                player.QuickSpawnItem(mod.ItemType("GlaiveWeaver"));
            else if (rand == 2)
                player.QuickSpawnItem(mod.ItemType("Infernolizer"));

            player.QuickSpawnItem(mod.ItemType("ShinySentinel"));
        }
    }
}
