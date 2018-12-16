﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class Corrosion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Corrosion");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Poisoned, 1);

            player.statDefense -= (int)(player.statDefense * 0.25f);

            // Bot test
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.AddBuff(BuffID.Poisoned, 1);

            npc.defense -= (int)(npc.defense * 0.25f);
        }
    }
}