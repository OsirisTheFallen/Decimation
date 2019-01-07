using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs.Bloodshot
{
    class MangledServant : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Creeper);
            npc.width = 52;
            npc.height = 64;
            npc.damage = 24;
            npc.defense = 1;
            npc.lifeMax = 35;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = -1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 55;
            npc.knockBackResist = 0.35f;
            npc.damage = 36;
        }

        public override void AI()
        {
            int bloodshotEye = (int)npc.ai[1];

            if (Main.GameUpdateCount % 60 == 0 && Main.expertMode)
            {
                if (Main.rand.NextBool(21))
                {
                    npc.ai[3] = npc.ai[0];
                    npc.ai[2] = 0;
                    npc.ai[0] = 2f;
                }
            }

            if (bloodshotEye < 0)
            {
                npc.active = false;
                npc.netUpdate = true;
            }
            else if (npc.ai[0] == 0f)
            {
                Vector2 npcCenter = npc.Center;
                float diffX = Main.npc[bloodshotEye].Center.X - npcCenter.X;
                float diffY = Main.npc[bloodshotEye].Center.Y - npcCenter.Y;
                float magnitude = (float)Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                if (magnitude > 90f)
                {
                    magnitude = 8f / magnitude;
                    diffX *= magnitude;
                    diffY *= magnitude;
                    npc.velocity.X = (npc.velocity.X * 15f + diffX) / 16f;
                    npc.velocity.Y = (npc.velocity.Y * 15f + diffY) / 16f;
                }
                else
                {
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 8f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 1.05f;
                        npc.velocity.X = npc.velocity.X * 1.05f;
                    }
                    if (Main.netMode != 1)
                    {
                        if ((!Main.expertMode || Main.rand.Next(100) != 0) && Main.rand.Next(200) != 0)
                        {
                            return;
                        }
                        npc.TargetClosest(true);
                        npcCenter = new Vector2(npc.Center.X, npc.Center.Y);
                        diffX = Main.player[npc.target].Center.X - npcCenter.X;
                        diffY = Main.player[npc.target].Center.Y - npcCenter.Y;
                        magnitude = (float)Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                        magnitude = 8f / magnitude;
                        npc.velocity.X = diffX * magnitude;
                        npc.velocity.Y = diffY * magnitude;
                        npc.ai[0] = 1f;
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (Main.expertMode)
                {
                    Vector2 diff = Main.player[npc.target].Center - npc.Center;
                    diff.Normalize();
                    diff *= 9f;
                    npc.velocity = (npc.velocity * 99f + diff) / 100f;
                }
                Vector2 npcCenter = npc.Center;
                float diffX = Main.npc[bloodshotEye].Center.X - npcCenter.X;
                float diffY = Main.npc[bloodshotEye].Center.Y - npcCenter.Y;
                float magnitude = (float)Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                if (!(magnitude > 700f) && !npc.justHit)
                {
                    return;
                }
                npc.ai[0] = 0f;
            }
            else
            {
                if (npc.ai[2] >= 300)
                {
                    npc.ai[0] = npc.ai[3];
                    npc.ai[3] = 0f;
                    return;
                }

                npc.velocity *= 0;

                // Blood
                Vector2 cloudPosition = npc.position;
                cloudPosition.X += Main.rand.Next(npc.width / 2) + npc.width / 4;
                cloudPosition.Y += npc.height / 2;

                if (Main.rand.NextBool(10))
                {
                    int proj = Projectile.NewProjectile(cloudPosition, new Vector2(0, 10), ProjectileID.BloodRain, Main.expertMode ? 14 : 7, 0);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                }

                npc.ai[2]++;
            }
        }
    }
}
