using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Decimation.Items.Weapons;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class TitanicStyngerBolt : DecimationProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanic Stynger Bolt");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        protected override void Init()
        {
            width = 40;
            height = 10;
            aiStyle = 1;
            damageType = DecimationWeapon.DamageType.RANGED;
            penetrate = 5;
            timeLeft = 600;
            projectile.alpha = 255;
            light = 0.1f;
            ignoreWater = true;
            tileCollide = true;
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
                Projectile.NewProjectile(projectile.Center.X - projectile.oldVelocity.X, projectile.Center.Y - projectile.oldVelocity.Y, velocityX, velocityY, ProjectileID.StyngerShrapnel, damages, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, height * 0.5f);
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
