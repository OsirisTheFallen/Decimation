using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
   internal class PiercingGaze : DecimationBuff
    {
        protected override string DisplayName => "Piercing Gaze";
        protected override string Description => "Eyes are peering into your soul...";
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
            player.statDefense -= (int)(player.statDefense * 0.08f);
            player.moveSpeed *= 0.85f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= (int)(npc.defense * 0.08f);
        }
    }
}