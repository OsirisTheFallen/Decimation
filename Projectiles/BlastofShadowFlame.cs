using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Decimation.Projectiles
{
    class BlastofShadowFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast of Shadowflame");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.light = 0.8f;
        }

        public override void AI()
        {
            int num4;

            if (projectile.timeLeft > 60)
            {
                projectile.timeLeft = 60;
            }
            if (projectile.ai[0] > 7f)
            {
                float scale = 0.5f;
                if (projectile.ai[0] == 8f)
                {
                    scale = 0.175f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    scale = 0.25f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    scale = 0.375f;
                }
                projectile.ai[0] += 1f;
                int dustType = 27;
                if (dustType == 6 || Main.rand.Next(2) == 0)
                {
                    for (int i = 0; i < 2; i = num4 + 1)
                    {
                        Vector2 position = new Vector2(projectile.position.X, projectile.position.Y);
                        int width = projectile.width;
                        int height = projectile.height;
                        //int num300 = dustType;
                        float speedX = projectile.velocity.X * 0.2f;
                        float speedY = projectile.velocity.Y * 0.2f;
                        Color newColor = default(Color);
                        int dustID = Dust.NewDust(position, width, height, dustType, speedX, speedY, 100, newColor, 1f);
                        Dust dust = Main.dust[dustID];
                        if (Main.rand.Next(3) != 0 || (dustType == 75 && Main.rand.Next(3) == 0))
                        {
                            dust.noGravity = true;
                            dust.scale *= 3f;
                            dust.velocity.X = dust.velocity.X * 2f;
                            dust.velocity.Y = dust.velocity.Y * 2f;
                        }
                        dust.scale *= 1.5f;
                        dust.velocity.X = dust.velocity.X * 1.2f;
                        dust.velocity.Y = dust.velocity.Y * 1.2f;
                        dust.scale *= scale;
                        if (dustType == 75)
                        {
                            dust.velocity += projectile.velocity;
                            if (!dust.noGravity)
                            {
                                dust.velocity *= 0.5f;
                            }
                        }
                        num4 = i;
                    }
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            projectile.rotation += 0.3f * (float)projectile.direction;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, Main.expertMode ? 360 : 600);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, Main.expertMode ? 360 : 600);
        }
    }
}
