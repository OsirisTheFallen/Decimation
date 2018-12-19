using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    class ArachnusTreasureBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 9;
            item.expert = true;
            bossBagNPC = mod.NPCType("Arachnus");
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
