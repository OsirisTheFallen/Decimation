using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    internal class AvianFlu : DecimationBuff
    {
        protected override string DisplayName => "Avian Flu";
        protected override string Description => "Your wings don't feel too hot...";
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
            if (player.lifeRegen > 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 3;

            player.jumpSpeedBoost += -2;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegenExpectedLossPerSecond += 3;
            npc.lifeRegen -= 3;
        }
    }
}