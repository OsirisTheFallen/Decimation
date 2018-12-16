using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class Discombobulated : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Discombobulated?");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
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