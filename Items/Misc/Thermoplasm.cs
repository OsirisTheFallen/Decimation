using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    class Thermoplasm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It resonates with the heat of the sun");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 36;
            item.value = 5000;
            item.rare = 8;
            item.maxStack = 999;
        }
    }

    public class ThermoplasmDrop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if(Main.LocalPlayer.ZoneUnderworldHeight && Main.rand.Next(1, 21) == 1)
                Item.NewItem((int) npc.position.X, (int) npc.position.Y, npc.width, npc.height, mod.ItemType("Thermoplasm"));
        }
    }
}
