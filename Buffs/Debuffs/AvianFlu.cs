using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    public class AvianFlu : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Avian Flu");
            Description.SetDefault("Your wings don't feel too hot...");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
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