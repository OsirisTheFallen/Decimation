using Decimation.Core;
using Decimation.Core.Amulets.Synergy;
using Microsoft.Xna.Framework;
using Terraria;

namespace Decimation.Synergies
{
    internal class GraniteAmuletSynergy : AmuletSynergyAdapter
    {
        public override void OnHitPlayer(DecimationModPlayer modPlayer, ref int damages)
        {
            if (modPlayer.HasShield && Main.rand.NextBool(10))
            {
                damages = 0;
                CombatText.NewText(modPlayer.player.getRect(), Color.MediumPurple, "Blocked");
            }
        }
    }
}
