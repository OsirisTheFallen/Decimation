using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    public class Ammonite : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SpikyBall);
            projectile.damage = 30;
            projectile.width = 20;
            projectile.height = 16;
            projectile.friendly = false;
            projectile.hostile = true;
        }
    }
}
