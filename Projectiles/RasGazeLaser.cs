using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class RasGazeLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.damage = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if (projectile.timeLeft < 5 && projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1;
                projectile.timeLeft = 80;
                projectile.damage = 75;
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[projectile.owner];
            Vector2 unit = projectile.velocity;
            float point = 0f;

            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
player.Center + unit * 2000, (projectile.ai[0] == 0 ? 20 : 44), ref point);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int laserHeight = 2000;
            int laserWidth = (projectile.ai[0] == 0 ? 20 : 44);

            float angle = projectile.velocity.ToRotation();
            Vector2 v = new Vector2((float)(Math.Cos(angle) * laserHeight), (float)(Math.Sin(angle) * laserHeight));
            Texture2D texture = projectile.ai[0] == 0 ? ModLoader.GetTexture("Decimation/Projectiles/RasGazeLaser") : ModLoader.GetTexture("Decimation/Projectiles/RasGazeBeam");
            Vector2 position = projectile.position - Main.screenPosition + v;
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, laserWidth, laserHeight), Color.White, (float)(angle + (Math.PI * 1 / 2)), new Vector2(0, 0), 1, SpriteEffects.None, 0);

            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
