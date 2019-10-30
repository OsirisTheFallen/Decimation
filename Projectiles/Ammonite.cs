using Terraria.ID;

namespace Decimation.Projectiles
{
    internal class Ammonite : DecimationProjectile
    {
        protected override bool IsClone => true;

        protected override void Init()
        {
            this.projectile.CloneDefaults(ProjectileID.SpikyBall);
            this.projectile.damage = 30;
            this.projectile.width = 10;
            this.projectile.height = 16;
            this.projectile.friendly = false;
            this.projectile.hostile = true;
        }
    }
}