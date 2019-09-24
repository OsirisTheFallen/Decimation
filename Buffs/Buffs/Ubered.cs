using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
   internal class Ubered : DecimationBuff
    {
        protected override string DisplayName => "Ubered!";
        protected override string Description => "YOU ARE ZE UBERMENSCH!";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += (int)(player.statDefense * 0.5f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense += (int)(npc.defense * 0.5f);
        }
    }
}
