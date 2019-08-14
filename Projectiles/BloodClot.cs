using System;
using Decimation.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class BloodClot : DecimationProjectile
    {
        protected override void Init()
        {
            width = 24;
            height = 24;
            damages = 22;
            damageType = DecimationWeapon.DamageType.RANGED;
            tileCollide = true;
            projectile.knockBack = 7f;
            aiStyle = -1;
            penetrate = 1;
            timeLeft = 600;
            hostile = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += (600 - timeLeft) * 0.002f;

            Dust.NewDust(projectile.position, 26, 26, DustID.SomethingRed);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Dust.NewDust(new Vector2(projectile.position.X + i, projectile.position.Y + j), width, height, DustID.Blood);
                }
            }

            Main.PlaySound(SoundID.NPCDeath1, projectile.Center);

            return true;
        }
    }
}
