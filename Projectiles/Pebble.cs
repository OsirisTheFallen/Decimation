using Decimation.Items.Weapons;
using Decimation.Core.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    internal class Pebble : DecimationProjectile
    {
        public override string Texture => "Decimation/Items/Ammo/Pebble";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pebble");     //The English name of the projectile
		}		
        protected override void Init()
        {
            width = 10;
            height = 10;
            projectile.scale = 0.625f;
            aiStyle = 1;
            damageType = DecimationWeapon.DamageType.RANGED;
            penetrate = 5;
            timeLeft = 600;
            projectile.alpha = 0;
            light = 0.5f;
            projectile.extraUpdates = 1;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            aiType = ProjectileID.WoodenArrowFriendly;
        }       
    }
}