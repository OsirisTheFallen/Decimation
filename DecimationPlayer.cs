using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation
{
    public class DecimationPlayer : ModPlayer
    {
        public bool closeToEnchantedAnvil = false;

        public override void ResetEffects()
        {
            //closeToEnchantedAnvil = false;
        }
    }
}
