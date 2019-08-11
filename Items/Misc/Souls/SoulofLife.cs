using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.Souls
{
    internal class SoulofLife : DecimationItem
    {
        protected override string ItemName => "Soul of Life";
        protected override string ItemTooltip => "The essence of living cells.";
        protected override DrawAnimation Animation => new DrawAnimationVertical(5, 4);

        protected override void Init()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);

            width = refItem.width;
            height = refItem.height;
            width = 20;
            height = 20;

            item.maxStack = 999;

            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public class SoulGlobalNPC : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == NPCID.Plantera)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulofLife"), Main.rand.Next(12, 25));
                }
            }
        }
    }
}
