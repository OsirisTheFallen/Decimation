using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Core.Amulets.Synergy
{
    public class AmuletSynergyAdapter : IAmuletsSynergy
    {
        public virtual void OnHitPlayer(DecimationModPlayer modPlayer, ref int damages)
        {
        }

        public virtual void OnShoot(DecimationModPlayer modPlayer, Item item, ref Vector2 position, ref float speedX, ref float speedY,
            ref int projectileType, ref int damages, ref float knockBack)
        {
        }

        public virtual void Update(DecimationModPlayer modPlayer)
        {
        }
    }
}
