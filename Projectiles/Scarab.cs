using Decimation.Buffs.Buffs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class Scarab : DecimationProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        protected override void Init()
        {
            damages = 0;
            width = 22;
            height = 22;
            aiStyle = -1;
            ignoreWater = true;
            tileCollide = false;
            penetrate = -1;
            timeLeft = int.MaxValue;
        }

        public override void AI()
        {
            Player player = Main.player[(int)(projectile.ai[0])];
            if (!player.HasBuff(ModContent.BuffType<ScarabEndurance>()))
                projectile.Kill();

            // Loop through the 2 animation frames, spending 5 ticks on each.
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }

            projectile.velocity.X += (float)Main.rand.Next(-75, 76) * 0.005f;
            projectile.velocity.Y += (float)Main.rand.Next(-75, 76) * 0.005f;

            if (projectile.velocity.X > 0.5f)
                projectile.velocity.X = 0.5f;
            if (projectile.velocity.Y > 0.5f)
                projectile.velocity.Y = 0.5f;

            float x = projectile.position.X;
            float y = projectile.position.Y;

            if (x < player.Center.X - 60 || x > player.Center.X + 60)
                projectile.velocity.X *= -1;
            if (y < player.Center.Y - 60 || y > player.Center.Y + 60)
                projectile.velocity.Y *= -1;

            // Follow the player
            Vector2 velocity = projectile.velocity;
            float diffX = projectile.Center.X - player.Center.X;
            float diffY = projectile.Center.Y - player.Center.Y;

            if (diffX > 70)
                projectile.position.X -= diffX / 60;
            else if (diffX < -70)
                projectile.position.X -= diffX / 60;

            if (diffY > 70)
                projectile.position.Y -= diffY / 60;
            else if (diffY < -70)
                projectile.position.Y -= diffY / 60;
        }
    }
}
