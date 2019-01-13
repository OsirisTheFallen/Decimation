using Decimation.Buffs.Debuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class BloodBeamFriendly : ModProjectile
    {
        public override string Texture
        {
            get { return "Terraria/Projectile_" + ProjectileID.CursedFlameFriendly; }
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 40;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.damage = 15;
            projectile.hostile = false;
            projectile.friendly = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += (60 - projectile.timeLeft) * 0.005f;

            Dust.NewDust(projectile.position, 26, 26, DustID.SomethingRed);
        }
    }
}
