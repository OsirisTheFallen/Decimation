using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
   internal class Corrosion : DecimationBuff
    {
        protected override string DisplayName => "Corrosion";
        protected override string Description => "Your armor is getting lighter...";
        public override bool Debuff => true;

        protected override void Init()
        {
            save = false;
            displayTime = true;
            clearable = false;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Poisoned, 1);

            player.statDefense -= (int)(player.statDefense * 0.25f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.AddBuff(BuffID.Poisoned, 1);

            npc.defense -= (int)(npc.defense * 0.25f);
        }
    }
}