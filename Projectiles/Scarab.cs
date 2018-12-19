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
    class Scarab : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.damage = 0;
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            Player player = Main.player[(int)(projectile.ai[0])];
            if (!player.HasBuff(mod.BuffType<ScarabEndurance>()))
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

            if (x < player.Center.X - 32 || x > player.Center.X + 32)
                projectile.velocity.X *= -1;
            if (y < player.Center.Y - 32 || y > player.Center.Y + 32)
                projectile.velocity.Y *= -1;

            // Follow the player
            Vector2 velocity = projectile.velocity;
            float diffX = projectile.Center.X - player.Center.X;
            float diffY = projectile.Center.Y - player.Center.Y;

            if (diffX > 40)
                projectile.position.X -= diffX / 60;
            else if (diffX < -40)
                projectile.position.X -= diffX / 60;

            if (diffY > 40)
                projectile.position.Y -= diffY / 60;
            else if (diffY < -40)
                projectile.position.Y -= diffY / 60;
        }
    }
}
