using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Decimation.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class MoltenStyngerBolt : DecimationProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Stynger Bolt");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        protected override void Init()
        {
            width = 10;
            height = 10;
            aiStyle = 1;
            damageType = DecimationWeapon.DamageType.RANGED;
            penetrate = 1;
            timeLeft = 600;
            projectile.alpha = 255;
            light = 0.5f;
            ignoreWater = false;
            tileCollide = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (timeLeft > 3)
                timeLeft = 3;

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (timeLeft > 3)
                timeLeft = 3;
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (timeLeft > 3)
                timeLeft = 3;
        }

        public override void Kill(int timeLeft)
        {
            penetrate = -1;
            tileCollide = false;
            projectile.alpha = 255;
            projectile.position.X = projectile.position.X + (float)(width / 2);
            projectile.position.Y = projectile.position.Y + (float)(height / 2);
            width = 100;
            height = 100;
            projectile.position.X = projectile.position.X - (float)(width / 2);
            projectile.position.Y = projectile.position.Y - (float)(height / 2);
            damages = 250;
            projectile.knockBack = 10f;

            SpawnDust();
            int fragNbre = Main.rand.Next(8, 12);
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

        private void SpawnDust()
        {

            // Play explosion sound
            Main.PlaySound(SoundID.Item14, projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), width, height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), width, height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), width, height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
            // Large Smoke Gore spawn
            for (int g = 0; g < 2; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(width / 2) - 24f, projectile.position.Y + (float)(height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(width / 2) - 24f, projectile.position.Y + (float)(height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(width / 2) - 24f, projectile.position.Y + (float)(height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(width / 2) - 24f, projectile.position.Y + (float)(height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
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
