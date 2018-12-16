using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs
{
    class Vampire : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Vampire!");
            Description.SetDefault("You are now a vampire!");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeSteal *= 1.3f;
            player.statDefense += 2;
            player.moveSpeed *= 1.05f;
            player.meleeSpeed *= 1.05f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense += 2;
        }
    }
}