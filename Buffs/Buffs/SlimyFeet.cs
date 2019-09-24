using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Buffs.Buffs
{
   internal class SlimyFeet : DecimationBuff
    {
        protected override string DisplayName => "Slimy feet!";
        protected override string Description => "You now have the abilities of slimes!";

        protected override void Init()
        {
            save = true;
            clearable = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;
            player.waterWalk2 = true;

            Keys[] pressedKeys = Main.keyState.GetPressedKeys();

            if (player.GetModPlayer<DecimationPlayer>().lastJumpBoost > 5)
                player.GetModPlayer<DecimationPlayer>().lastJumpBoost--;


            for (int j = 0; j < pressedKeys.Length; j++)
            {
                string a = string.Concat(pressedKeys[j]);

                if (a == Main.cJump)
                {
                    if (!player.GetModPlayer<DecimationPlayer>().wasJumping && player.wingTime == player.wingTimeMax)
                    {
                        player.GetModPlayer<DecimationPlayer>().lastJumpBoost++;
                    }
                    player.GetModPlayer<DecimationPlayer>().wasJumping = true;
                    break;
                }
                player.GetModPlayer<DecimationPlayer>().wasJumping = false;
            }
            player.jumpSpeedBoost += player.GetModPlayer<DecimationPlayer>().lastJumpBoost;
        }
    }
}
