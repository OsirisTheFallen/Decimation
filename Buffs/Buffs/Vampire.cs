using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
   internal class Vampire : DecimationBuff
    {
        protected override string DisplayName => "Vampire!";
        protected override string Description => "You are now a vampire!";

        protected override void Init()
        {
            save = true;
            displayTime = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeSteal *= 1.3f;
            player.statDefense += 2;
            player.moveSpeed *= 1.05f;
            player.meleeSpeed *= 1.05f;

            player.GetModPlayer<DecimationPlayer>().vampire = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense += 2;
        }
    }
}