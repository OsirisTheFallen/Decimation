using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    class Amnesia : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Amnesia");
            Description.SetDefault("Discombobulate");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
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
