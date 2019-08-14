using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{

    internal class Stinger : DecimationProjectile
    {
        protected override void Init()
        {
            width = 10;
            height = 18;
            tileCollide = true;
            penetrate = 2;
            timeLeft = 200;
            projectile.extraUpdates = 1;
            ignoreWater = false;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 100);
            }
        }

    }
}