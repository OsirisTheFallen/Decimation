using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class MysticFlame : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mystic Flame");
            Description.SetDefault("A warmth envelops you");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
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
