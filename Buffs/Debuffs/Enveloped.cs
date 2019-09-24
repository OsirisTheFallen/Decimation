using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Debuffs
{
   internal class Enveloped : DecimationBuff
    {
        protected override string DisplayName => "Enveloped!";
        protected override string Description => "Is it frostbite? Or are your nerves burnt out?";
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