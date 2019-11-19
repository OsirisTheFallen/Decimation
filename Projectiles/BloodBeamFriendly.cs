using Decimation.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class BloodBeamFriendly : DecimationProjectile
    {
        public override string Texture =>"Terraria/Projectile_" + ProjectileID.CursedFlameFriendly;

        protected override void Init()
        {
            width = 26;
            height = 26;
            aiStyle = -1;
            penetrate = -1;
            projectile.alpha = 255;
            timeLeft = 40;
            tileCollide = true;
            ignoreWater = true;
            damages = 15;
            hostile = false;
        }

        public override void AI()
        {
            projectile.velocity.Y += (60 - timeLeft) * 0.005f;

            Dust.NewDust(projectile.position, 26, 26, ModContent.DustType<Blood>());
        }
    }
}
