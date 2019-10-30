using Microsoft.Xna.Framework;
using Terraria;

namespace Decimation.Core.Amulets.Synergy
{
    public interface IAmuletsSynergy
    {
        void OnHitPlayer(DecimationModPlayer modPlayer, ref int damages);

        void OnShoot(DecimationModPlayer modPlayer, Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int projectileType, ref int damages, ref float knockBack);

        void Update(DecimationModPlayer modPlayer);
    }
}
