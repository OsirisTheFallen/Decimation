using Decimation.Core.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Decimation.Items.Misc.Souls
{
    internal class SoulofSpite : DecimationItem
    {
        protected override string ItemName => "Soul of Spite";
        protected override string ItemTooltip => "The essence of malice.";
        protected override DrawAnimation Animation => new DrawAnimationVertical(5, 4);

        protected override void Init()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);

            width = refItem.width;
            height = refItem.height;
            this.item.maxStack = 999;
            value = 50000;

            ItemID.Sets.AnimatesAsSoul[this.item.type] = true;
            ItemID.Sets.ItemIconPulse[this.item.type] = true;
            ItemID.Sets.ItemNoGravity[this.item.type] = true;
        }

        // Uncomment when Slime Prince will be done
        /**public class SoulGlobalNPC : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == mod.NPCType<SlimePrince>())
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SoulofSpite>(), Main.rand.Next(12, 25));
                }
            }
        }**/
    }
}