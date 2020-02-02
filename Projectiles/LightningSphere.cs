using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal class LightningSphere : DecimationProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        protected override void Init()
        {
            this.projectile.CloneDefaults(ProjectileID.MagnetSphereBall);
            this.timeLeft = 600;
        }
    }
}
