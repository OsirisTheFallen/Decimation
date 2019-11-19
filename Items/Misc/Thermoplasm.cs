using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
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
            value = Item.buyPrice(silver: 50);
            rarity = Rarity.Yellow;
        }
    }

    public class ThermoplasmDrop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.LocalPlayer.ZoneUnderworldHeight && Main.rand.NextBool(20))
                Item.NewItem((int) npc.position.X, (int) npc.position.Y, npc.width, npc.height,
                    ModContent.ItemType<Thermoplasm>());
        }
    }
}