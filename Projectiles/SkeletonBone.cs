using System;
using System.IO;
using Decimation.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Projectiles
{
   internal class SkeletonBone : DecimationProjectile
    {
        private float maxSpeed = 5f;

        protected override void Init()
        {
            width = 14;
            height = 34;
            aiStyle = -1;
            ignoreWater = true;
            tileCollide = false;
            damageType = DecimationWeapon.DamageType.RANGED;
            penetrate = 2;
            timeLeft = 60;

            damages = Main.expertMode ? 57 : 20;
            projectile.localAI[0] = 15;
        }

        // ai 0: target, ai 1: state, localAI 0: rotation

        private int counter = 0;

        public override void AI()
        {
            /*if (projectile.ai[1] == 0)
            {
                projectile.localAI[0] = 15;

                if (timeLeft < 10)
                {
                    projectile.ai[1]++;
                    timeLeft = 600;
                    projectile.velocity *= 0;
                }
            }
            else if (projectile.ai[1] == 1)
            {
                if (Main.netMode != 1)
                    counter++;

                projectile.localAI[0] += 4;

                if (counter >= 60)
                {
                    projectile.ai[1]++;
                    counter = 0;
                }
            }
            else if (projectile.ai[1] == 2)
            {
                NPC target = NPC.get
                Vector2 velocity = target.position - projectile.position;

                float magnitude = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                if (magnitude > maxSpeed)
                {
                    float ratio = maxSpeed / magnitude;
                    velocity.X *= ratio;
                    velocity.Y *= ratio;
                }

                projectile.velocity = velocity;
                projectile.ai[1]++;
            }*/

            projectile.rotation += (float)(Math.PI / projectile.localAI[0]);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(counter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            counter = reader.ReadInt32();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode && Main.rand.Next(100) < 35) target.AddBuff(BuffID.Confused, 600);
        }
    }
}
