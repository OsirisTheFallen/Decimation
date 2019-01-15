using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
    class BloodClotSmall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.damage = 16;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.knockBack = 5f;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void AI()
        {
            projectile.velocity.Y += (600 - projectile.timeLeft) * 0.002f;

            Dust.NewDust(projectile.position, 26, 26, DustID.SomethingRed);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < projectile.width; i++)
            {
                for (int j = 0; j < projectile.height; j++)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + i, projectile.position.Y + j), projectile.width, projectile.height, DustID.Blood);
                }
            }

            Main.PlaySound(SoundID.NPCDeath1, projectile.Center);

            return true;
        }
    }
}
