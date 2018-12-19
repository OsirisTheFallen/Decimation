using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    class InfernalGaze : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infernal Gaze");
            Description.SetDefault("You feel your sins burning inside of you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
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