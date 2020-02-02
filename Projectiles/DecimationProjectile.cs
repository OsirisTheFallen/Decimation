using Decimation.Core.Items;
using IL.Terraria;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal abstract class DecimationProjectile : ModProjectile
    {
        protected int aiStyle = -1;
        protected int damages = 0;
        protected DecimationWeapon.DamageType damageType = DecimationWeapon.DamageType.MELEE;
        protected int height = 16;
        protected bool hostile = false;
        protected bool ignoreWater = false;
        protected float light = 0f;
        protected int penetrate = 0;
        protected bool tileCollide = true;
        protected int timeLeft = 180;
        protected int width = 16;

        protected virtual bool IsClone { get; } = false;

        protected abstract void Init();

        public sealed override void SetDefaults()
        {
            Init();

            if (this.IsClone) return;

            this.projectile.width = width;
            this.projectile.height = height;
            this.projectile.damage = damages;
            this.projectile.penetrate = penetrate;
            this.projectile.timeLeft = timeLeft;
            this.projectile.hostile = hostile;
            this.projectile.friendly = !hostile;
            this.projectile.tileCollide = tileCollide;
            this.projectile.ignoreWater = ignoreWater;
            this.projectile.aiStyle = aiStyle;
            this.projectile.light = light;

            switch (damageType)
            {
                case DecimationWeapon.DamageType.MELEE:
                    this.projectile.melee = true;
                    break;
                case DecimationWeapon.DamageType.MAGIC:
                    this.projectile.magic = true;
                    break;
                case DecimationWeapon.DamageType.RANGED:
                    this.projectile.ranged = true;
                    break;
                case DecimationWeapon.DamageType.THROW:
                    this.projectile.thrown = true;
                    break;
                default:
                    this.projectile.melee = true;
                    break;
            }
        }
    }
}