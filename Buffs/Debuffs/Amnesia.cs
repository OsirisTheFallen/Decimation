using Terraria;
using Terraria.ID;

namespace Decimation.Buffs.Debuffs
{
   internal class Amnesia : DecimationBuff
    {
        protected override string DisplayName => "Amnesia";
        protected override string Description => "Discombobulate";
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
            player.moveSpeed *= 0.95f;
            player.buffImmune[BuffID.Confused] = false;
            player.AddBuff(BuffID.Confused, 1, false);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0.95f;
            npc.buffImmune[BuffID.Confused] = false;
            npc.AddBuff(BuffID.Confused, 1, false);
        }
    }
}
