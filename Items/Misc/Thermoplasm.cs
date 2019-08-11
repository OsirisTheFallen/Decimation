using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
   internal class Thermoplasm : DecimationItem
   {
       protected override string ItemName => "Thermoplasm";
        protected override string ItemTooltip => "It resonates with the heat of the planet's core";

        protected override void Init()
        {
            width = 26;
            height = 36;
            value = 5000;
            rarity = Rarity.Yellow;
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
