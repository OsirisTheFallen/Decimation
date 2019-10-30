using Decimation.Core;
using Decimation.Core.Amulets.Synergy;

namespace Decimation.Synergies
{
    internal class FireAmuletSynergy : AmuletSynergyAdapter
    {
        private const int AddedLavaTime = 500;

        public override void Update(DecimationModPlayer modPlayer)
        {
            if (modPlayer.HasLavaCharm) modPlayer.player.lavaMax += AddedLavaTime;
        }
    }
}