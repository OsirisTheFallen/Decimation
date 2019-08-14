using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal class Ammonite : DecimationProjectile
    {
        protected override bool IsClone => true;

        protected override void Init()
        {
            projectile.CloneDefaults(ProjectileID.SpikyBall);
            projectile.damage = 30;
            projectile.width = 10;
            projectile.height = 16;
            projectile.friendly = false;
            projectile.hostile = true;
        }
    }
}
