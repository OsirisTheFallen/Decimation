using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
   internal class MysticFlame : DecimationBuff
    {
        protected override string DisplayName => "Mystic Flame";
        protected override string Description => "A warmth envelops you";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 2;
            player.manaRegen += 1;
            player.moveSpeed *= 1.05f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen += 2;
        }
    }
}
