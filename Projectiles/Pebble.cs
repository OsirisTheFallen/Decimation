using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    public class Pebble : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pebble");     //The English name of the projectile
		}		
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 10;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            aiType = ProjectileID.WoodenArrowFriendly;
        }       
    }
}