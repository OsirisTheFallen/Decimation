using Terraria.ModLoader;

namespace Decimation.Core
{
    public abstract class DecimationModPlayer : ModPlayer, IDecimationPlayer
    {

        public abstract bool HasLavaCharm { get; set; }
        public abstract bool HasShield { get; set; }

    }
}
