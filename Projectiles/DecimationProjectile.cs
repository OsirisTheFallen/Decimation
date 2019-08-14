using Decimation.Items.Weapons;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal abstract class DecimationProjectile : ModProjectile
    {
        protected int width = 16;
        protected int height = 16;
        protected int damages = 0;
        protected int penetrate = 0;
        protected int timeLeft = 180;
        protected bool hostile = false;
        protected bool tileCollide = true;
        protected bool ignoreWater = false;
        protected int aiStyle = -1;
        protected float light = 0f;
        protected DecimationWeapon.DamageType damageType = DecimationWeapon.DamageType.MELEE;

        protected virtual bool IsClone { get; } = false;

        protected abstract void Init();

        public sealed override void SetDefaults()
        {
            Init();

            if (!IsClone)
            {
                projectile.width = width;
                projectile.height = height;
                projectile.damage = damages;
                projectile.penetrate = penetrate;
                projectile.timeLeft = timeLeft;
                projectile.hostile = hostile;
                projectile.friendly = !hostile;
                projectile.tileCollide = tileCollide;
                projectile.ignoreWater = ignoreWater;
                projectile.aiStyle = aiStyle;
                projectile.light = light;

                switch (damageType)
                {
                    case DecimationWeapon.DamageType.MAGIC:
                        projectile.magic = true;
                        break;
                    case DecimationWeapon.DamageType.RANGED:
                        projectile.ranged = true;
                        break;
                    case DecimationWeapon.DamageType.THROW:
                        projectile.thrown = true;
                        break;
                    default:
                        projectile.melee = true;
                        break;
                }
            }
        }
    }
}
