using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    internal class MoltenKey : DecimationItem
    {
        protected override string ItemName => "Molten Key";
        protected override string ItemTooltip => "A strange key used to open a forbidden temple deep inside this world";

        protected override void Init()
        {
            width = 20;
            height = 20;
            consumable = true;
            value = Item.buyPrice(0, 45);
            rarity = Rarity.Red;

            item.maxStack = 1;
        }

        public class DropFromMoonLord : GlobalNPC
        {
            public override void NPCLoot(NPC npc)
            {
                if (npc.type == NPCID.MoonLordCore)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MoltenKey"));
                }
            }
        }
    }
}
