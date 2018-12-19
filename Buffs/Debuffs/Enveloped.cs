using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
    class Enveloped : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Enveloped!");
            Description.SetDefault("Is it frostbite? Or are your nerves burnt out?");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = false;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.OnFire, 1);
            player.AddBuff(BuffID.Chilled, 1);

            player.meleeDamage -= (int)(player.meleeDamage * 0.05f);
            player.meleeCrit -= (int)(player.meleeCrit * 0.04f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.AddBuff(BuffID.OnFire, 1);
            npc.AddBuff(BuffID.Chilled, 1);

            npc.damage -= (int)(npc.damage * 0.05f);
        }
    }
}