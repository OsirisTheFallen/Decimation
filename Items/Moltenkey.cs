using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
    public class MoltenKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hot enough to open the shrine door.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 450000;
            item.rare = 10;
            item.consumable = true;
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
