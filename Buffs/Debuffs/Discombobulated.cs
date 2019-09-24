using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
   internal class Discombobulated : DecimationBuff
    {
        protected override string DisplayName => "Discombobulated?";
        protected override string Description => "";
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
            player.buffImmune[BuffID.Confused] = false;

            player.moveSpeed *= 0.95f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.buffImmune[BuffID.Confused] = false;
        }
    }
}