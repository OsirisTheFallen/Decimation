using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class TitanicStyngerBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanic Stynger Bolt");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 10;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
            projectile.light = 0.1f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void Kill(int timeLeft)
        {
            int fragNbre = Main.rand.Next(2, 6);
            for (int i = 0; i < fragNbre; i++)
            {
                float velocityX = (float)Main.rand.Next(-100, 101);
                velocityX += 0.01f;
                float velocityY = (float)Main.rand.Next(-100, 101);
                velocityX -= 0.01f;
                float sqrt = (float)Math.Sqrt((double)(velocityX * velocityX + velocityY * velocityY));
                sqrt = 8f / sqrt;
                velocityX *= sqrt;
                velocityY *= sqrt;
                Projectile.NewProjectile(projectile.Center.X - projectile.oldVelocity.X, projectile.Center.Y - projectile.oldVelocity.Y, velocityX, velocityY, ProjectileID.StyngerShrapnel, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
