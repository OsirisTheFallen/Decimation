using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
   internal class InfernalGaze : DecimationBuff
    {
        protected override string DisplayName => "Infernal Gaze";
        protected override string Description => "You feel your sins burning inside of you";
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
            player.statDefense -= (int)(player.statDefense * 0.1f);
            player.meleeDamage *= 0.90f;

            player.AddBuff(BuffID.CursedInferno, 1);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= (int)(npc.defense * 0.1f);
            npc.damage -= (int)(npc.damage * 0.1f);

            npc.AddBuff(BuffID.CursedInferno, 1);
        }
    }
}