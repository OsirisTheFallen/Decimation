using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Decimation.Projectiles;
using System.IO;
using Decimation.Items.Boss.Bloodshot;
using Decimation.Items.Weapons;
using Decimation.Items.Misc;
using Decimation.Items.Weapons.Bloodshot;

namespace Decimation.NPCs.Bloodshot
{
    [AutoloadBossHead]
    class BloodshotEye : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Bloodshot Eye");
        }

        public override void SetDefaults()
        {
            npc.width = 110;
            npc.height = 110;
            npc.value = 70000;
            npc.aiStyle = -1;
            npc.defense = 18;
            npc.damage = 54;
            npc.lifeMax = 8500;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.knockBackResist = 0;
            npc.dontTakeDamage = true;
            bossBag = mod.ItemType<TreasureBagBloodshotEye>();
        }

        private bool hasSpawnedMinions = false;

        public override void AI()
        {
            // --> EoC phase 1
            if (npc.ai[0] == 0f)
            {
                // --> Custom
                // Blood

                if (Main.rand.NextBool(10) && Main.netMode != 1)
                {
                    Vector2 cloudPosition = npc.position;
                    cloudPosition.X += Main.rand.Next(npc.width / 2) + npc.width / 4;
                    cloudPosition.Y += npc.height / 2;
                    int proj = Projectile.NewProjectile(cloudPosition, new Vector2(0, 10), ProjectileID.BloodRain, Main.expertMode ? 20 : 12, 0);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                }

                // Minions
                if (!hasSpawnedMinions)
                {
                    if (Main.netMode != 1)
                        for (int i = 0; i < 10; i++)
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<MangledServant>(), 0, 0, npc.whoAmI);

                    hasSpawnedMinions = true;
                }

                if (!NPC.AnyNPCs(mod.NPCType<MangledServant>()))
                    npc.dontTakeDamage = false;
                // <-- Custom

                float num11;
                Color newColor;
                Vector2 vector;
                bool flag2 = false;
                if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.12)
                {
                    flag2 = true;
                }
                bool flag3 = false;
                if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.04)
                {
                    flag3 = true;
                }
                float num8 = 20f;
                if (flag3)
                {
                    num8 = 10f;
                }
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    npc.TargetClosest(true);
                }
                bool dead = Main.player[npc.target].dead;
                float num9 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
                float num10 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
                num11 = (float)Math.Atan2((double)num10, (double)num9) + 1.57f;
                if (num11 < 0f)
                {
                    num11 += 6.283f;
                }
                else if ((double)num11 > 6.283)
                {
                    num11 -= 6.283f;
                }
                float num12 = 0f;
                if (npc.ai[0] == 0f && npc.ai[1] == 0f)
                {
                    num12 = 0.02f;
                }
                if (npc.ai[0] == 0f && npc.ai[1] == 2f && npc.ai[2] > 40f)
                {
                    num12 = 0.05f;
                }
                if (npc.ai[0] == 3f && npc.ai[1] == 0f)
                {
                    num12 = 0.05f;
                }
                if (npc.ai[0] == 3f && npc.ai[1] == 2f && npc.ai[2] > 40f)
                {
                    num12 = 0.08f;
                }
                if (npc.ai[0] == 3f && npc.ai[1] == 4f && npc.ai[2] > num8)
                {
                    num12 = 0.15f;
                }
                if (npc.ai[0] == 3f && npc.ai[1] == 5f)
                {
                    num12 = 0.05f;
                }
                if (Main.expertMode)
                {
                    num12 *= 1.5f;
                }
                if (flag3 && Main.expertMode)
                {
                    num12 = 0f;
                }
                if (npc.rotation < num11)
                {
                    if ((double)(num11 - npc.rotation) > 3.1415)
                    {
                        npc.rotation -= num12;
                    }
                    else
                    {
                        npc.rotation += num12;
                    }
                }
                else if (npc.rotation > num11)
                {
                    if ((double)(npc.rotation - num11) > 3.1415)
                    {
                        npc.rotation += num12;
                    }
                    else
                    {
                        npc.rotation -= num12;
                    }
                }
                if (npc.rotation > num11 - num12 && npc.rotation < num11 + num12)
                {
                    npc.rotation = num11;
                }
                if (npc.rotation < 0f)
                {
                    npc.rotation += 6.283f;
                }
                else if ((double)npc.rotation > 6.283)
                {
                    npc.rotation -= 6.283f;
                }
                if (npc.rotation > num11 - num12 && npc.rotation < num11 + num12)
                {
                    npc.rotation = num11;
                }
                if (Main.rand.Next(5) == 0)
                {
                    Vector2 position = new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f);
                    int width = npc.width;
                    int height = (int)((float)npc.height * 0.5f);
                    float x = npc.velocity.X;
                    newColor = default(Color);
                    int num13 = Dust.NewDust(position, width, height, 5, x, 2f, 0, newColor, 1f);
                    Dust dust = Main.dust[num13];
                    dust.velocity.X = dust.velocity.X * 0.5f;
                    Dust dust2 = Main.dust[num13];
                    dust2.velocity.Y = dust2.velocity.Y * 0.1f;
                }
                if (Main.dayTime | dead)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                }
                else
                {
                    if (npc.ai[1] == 0f)
                    {
                        float num14 = 5f;
                        float num15 = 0.04f;
                        if (Main.expertMode)
                        {
                            num15 = 0.15f;
                            num14 = 7f;
                        }
                        vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num16 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector.X;
                        float num17 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 200f - vector.Y;
                        float num18 = (float)Math.Sqrt((double)(num16 * num16 + num17 * num17));
                        float num19 = num18;
                        num18 = num14 / num18;
                        num16 *= num18;
                        num17 *= num18;
                        if (npc.velocity.X < num16)
                        {
                            npc.velocity.X = npc.velocity.X + num15;
                            if (npc.velocity.X < 0f && num16 > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num15;
                            }
                        }
                        else if (npc.velocity.X > num16)
                        {
                            npc.velocity.X = npc.velocity.X - num15;
                            if (npc.velocity.X > 0f && num16 < 0f)
                            {
                                npc.velocity.X = npc.velocity.X - num15;
                            }
                        }
                        if (npc.velocity.Y < num17)
                        {
                            npc.velocity.Y = npc.velocity.Y + num15;
                            if (npc.velocity.Y < 0f && num17 > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num15;
                            }
                        }
                        else if (npc.velocity.Y > num17)
                        {
                            npc.velocity.Y = npc.velocity.Y - num15;
                            if (npc.velocity.Y > 0f && num17 < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y - num15;
                            }
                        }
                        npc.ai[2] += 1f;
                        float num20 = 600f;
                        if (Main.expertMode)
                        {
                            num20 *= 0.35f;
                        }
                        if (npc.ai[2] >= num20)
                        {
                            npc.ai[1] = 1f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.target = 255;
                            npc.netUpdate = true;
                        }
                        else
                        {
                            if (npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && num19 < 500f)
                            {
                                if (!Main.player[npc.target].dead)
                                {
                                    npc.ai[3] += 1f;
                                }
                                float num1449 = 110f;
                                if (Main.expertMode)
                                {
                                    num1449 *= 0.4f;
                                }
                                if (npc.ai[3] >= num1449)
                                {
                                    npc.ai[3] = 0f;
                                    npc.rotation = num11;
                                }
                                float num1461 = 0.5f;
                                if (Main.expertMode)
                                {
                                    num1461 = 0.65f;
                                }
                                if ((float)npc.life < (float)npc.lifeMax * num1461)
                                {
                                    // 2nd phase
                                    npc.ai[0] = 1f;
                                    npc.ai[1] = 0f;
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                    npc.netUpdate = true;
                                    if (npc.netSpam > 10)
                                    {
                                        npc.netSpam = 10;
                                    }
                                }
                            }
                            if (Main.expertMode && num19 < 500f)
                            {
                                if (!Main.player[npc.target].dead)
                                {
                                    npc.ai[3] += 1f;
                                }
                                float num1449 = 110f;
                                if (Main.expertMode)
                                {
                                    num1449 *= 0.4f;
                                }
                                if (npc.ai[3] >= num1449)
                                {
                                    npc.ai[3] = 0f;
                                    npc.rotation = num11;
                                    float num1450 = 5f;
                                    if (Main.expertMode)
                                    {
                                        num1450 = 6f;
                                    }
                                    float num1451 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector.X;
                                    float num1452 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector.Y;
                                    float num1453 = (float)Math.Sqrt((double)(num1451 * num1451 + num1452 * num1452));
                                    num1453 = num1450 / num1453;
                                    Vector2 vector252 = vector;
                                    Vector2 vector253 = default(Vector2);
                                    vector253.X = num1451 * num1453;
                                    vector253.Y = num1452 * num1453;
                                    vector252.X += vector253.X * 10f;
                                    vector252.Y += vector253.Y * 10f;
                                    Main.PlaySound(3, (int)vector252.X, (int)vector252.Y, 1, 1f, 0f);
                                    int num2;
                                    for (int num1455 = 0; num1455 < 10; num1455 = num2 + 1)
                                    {
                                        Vector2 position102 = vector252;
                                        float speedX31 = vector253.X * 0.4f;
                                        float speedY30 = vector253.Y * 0.4f;
                                        newColor = default(Color);
                                        Dust.NewDust(position102, 20, 20, 5, speedX31, speedY30, 0, newColor, 1f);
                                        num2 = num1455;
                                    }
                                }
                                float num1461 = 0.5f;
                                if (Main.expertMode)
                                {
                                    num1461 = 0.65f;
                                }
                                if ((float)npc.life < (float)npc.lifeMax * num1461)
                                {
                                    // 2nd phase
                                    npc.ai[0] = 1f;
                                    npc.ai[1] = 0f;
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                    npc.netUpdate = true;
                                    if (npc.netSpam > 10)
                                    {
                                        npc.netSpam = 10;
                                    }
                                }
                            }
                        }
                    }
                    else if (npc.ai[1] == 1f)
                    {
                        npc.rotation = num11;
                        float num21 = 6f;
                        if (Main.expertMode)
                        {
                            num21 = 7f;
                        }
                        Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num22 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                        float num23 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector2.Y;
                        float num24 = (float)Math.Sqrt((double)(num22 * num22 + num23 * num23));
                        num24 = num21 / num24;
                        npc.velocity.X = num22 * num24;
                        npc.velocity.Y = num23 * num24;
                        npc.ai[1] = 2f;
                        npc.netUpdate = true;
                        if (npc.netSpam > 10)
                        {
                            npc.netSpam = 10;
                        }

                        if (Main.expertMode)
                        {
                            npc.velocity *= (float)(1 + ((npc.lifeMax - npc.life) / (npc.lifeMax * (Main.expertMode ? 0.65f : 0.5f)) * 2f));
                        }
                    }
                    else if (npc.ai[1] == 2f)
                    {
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= 40f)
                        {
                            npc.velocity *= 0.98f;
                            if (Main.expertMode)
                            {
                                npc.velocity *= 0.985f;
                            }
                            if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                            {
                                npc.velocity.X = 0f;
                            }
                            if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                            {
                                npc.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                        }
                        int num25 = 150;
                        if (Main.expertMode)
                        {
                            num25 = 100;
                        }
                        if (npc.ai[2] >= (float)num25)
                        {
                            npc.ai[3] += 1f;
                            npc.ai[2] = 0f;
                            npc.target = 255;
                            npc.rotation = num11;
                            if (npc.ai[3] >= 3f)
                            {
                                npc.ai[1] = 0f;
                                npc.ai[3] = 0f;
                            }
                            else
                            {
                                npc.ai[1] = 1f;
                            }
                        }
                    }
                    float num1460 = 0.5f;
                    if (Main.expertMode)
                    {
                        num1460 = 0.65f;
                    }
                    if ((float)npc.life < (float)npc.lifeMax * num1460)
                    {
                        // 2nd phase
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                        if (npc.netSpam > 10)
                        {
                            npc.netSpam = 10;
                        }
                    }
                }
            } // <-- EoC phase 1
              // --> Spazmatism phase 2
            else if (npc.ai[0] == 2)
            {
                Color newColor;
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    npc.TargetClosest(true);
                }
                bool dead3 = Main.player[npc.target].dead;
                float num389 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
                float num390 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
                float num391 = (float)Math.Atan2((double)num390, (double)num389) + 1.57f;
                if (num391 < 0f)
                {
                    num391 += 6.283f;
                }
                else if ((double)num391 > 6.283)
                {
                    num391 -= 6.283f;
                }
                float num392 = 0.15f;
                if (npc.rotation < num391)
                {
                    if ((double)(num391 - npc.rotation) > 3.1415)
                    {
                        npc.rotation -= num392;
                    }
                    else
                    {
                        npc.rotation += num392;
                    }
                }
                else if (npc.rotation > num391)
                {
                    if ((double)(npc.rotation - num391) > 3.1415)
                    {
                        npc.rotation += num392;
                    }
                    else
                    {
                        npc.rotation -= num392;
                    }
                }
                if (npc.rotation > num391 - num392 && npc.rotation < num391 + num392)
                {
                    npc.rotation = num391;
                }
                if (npc.rotation < 0f)
                {
                    npc.rotation += 6.283f;
                }
                else if ((double)npc.rotation > 6.283)
                {
                    npc.rotation -= 6.283f;
                }
                if (npc.rotation > num391 - num392 && npc.rotation < num391 + num392)
                {
                    npc.rotation = num391;
                }
                if (Main.rand.Next(5) == 0)
                {
                    Vector2 position42 = new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f);
                    int width40 = npc.width;
                    int height38 = (int)((float)npc.height * 0.5f);
                    float x4 = npc.velocity.X;
                    newColor = default(Color);
                    int num393 = Dust.NewDust(position42, width40, height38, 5, x4, 2f, 0, newColor, 1f);
                    Dust dust29 = Main.dust[num393];
                    dust29.velocity.X = dust29.velocity.X * 0.5f;
                    Dust dust30 = Main.dust[num393];
                    dust30.velocity.Y = dust30.velocity.Y * 0.1f;
                }
                if (Main.netMode != 1 && !Main.dayTime && !dead3 && npc.timeLeft < 10)
                {
                    int num2;
                    for (int num394 = 0; num394 < 200; num394 = num2 + 1)
                    {
                        if (num394 != npc.whoAmI && Main.npc[num394].active && (Main.npc[num394].type == 125 || Main.npc[num394].type == 126) && Main.npc[num394].timeLeft - 1 > npc.timeLeft)
                        {
                            npc.timeLeft = Main.npc[num394].timeLeft - 1;
                        }
                        num2 = num394;
                    }
                }
                if (Main.dayTime | dead3)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                }
                npc.damage = (int)((double)npc.defDamage * 1.5);
                npc.defense = npc.defDefense + 18;
                if (npc.ai[1] == 0f)
                {
                    float num410 = 4f;
                    float num411 = 0.1f;
                    int num412 = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        num412 = -1;
                    }
                    Vector2 vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num413 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num412 * 180) - vector41.X;
                    float num414 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
                    float num415 = (float)Math.Sqrt((double)(num413 * num413 + num414 * num414));
                    if (Main.expertMode)
                    {
                        if (num415 > 300f)
                        {
                            num410 += 0.5f;
                        }
                        if (num415 > 400f)
                        {
                            num410 += 0.5f;
                        }
                        if (num415 > 500f)
                        {
                            num410 += 0.55f;
                        }
                        if (num415 > 600f)
                        {
                            num410 += 0.55f;
                        }
                        if (num415 > 700f)
                        {
                            num410 += 0.6f;
                        }
                        if (num415 > 800f)
                        {
                            num410 += 0.6f;
                        }
                    }
                    num415 = num410 / num415;
                    num413 *= num415;
                    num414 *= num415;
                    if (npc.velocity.X < num413)
                    {
                        npc.velocity.X = npc.velocity.X + num411;
                        if (npc.velocity.X < 0f && num413 > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num411;
                        }
                    }
                    else if (npc.velocity.X > num413)
                    {
                        npc.velocity.X = npc.velocity.X - num411;
                        if (npc.velocity.X > 0f && num413 < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num411;
                        }
                    }
                    if (npc.velocity.Y < num414)
                    {
                        npc.velocity.Y = npc.velocity.Y + num411;
                        if (npc.velocity.Y < 0f && num414 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num411;
                        }
                    }
                    else if (npc.velocity.Y > num414)
                    {
                        npc.velocity.Y = npc.velocity.Y - num411;
                        if (npc.velocity.Y > 0f && num414 < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - num411;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 400f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        npc.localAI[2] += 1f;
                        if (npc.localAI[2] > 22f)
                        {
                            npc.localAI[2] = 0f;
                            Main.PlaySound(SoundID.Item34, npc.position);
                        }
                        if (Main.netMode != 1)
                        {
                            npc.localAI[1] += 1f;
                            if ((double)npc.life < (double)npc.lifeMax * 0.75)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.5)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.25)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if ((double)npc.life < (double)npc.lifeMax * 0.1)
                            {
                                npc.localAI[1] += 2f;
                            }
                            if (npc.localAI[1] > 8f)
                            {
                                npc.localAI[1] = 0f;
                                float num416 = 6f;
                                int num417 = 30;
                                if (Main.expertMode)
                                {
                                    num417 = 27;
                                }
                                int num418 = mod.ProjectileType<BloodBeam>();
                                vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                num413 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector41.X;
                                num414 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
                                num415 = (float)Math.Sqrt((double)(num413 * num413 + num414 * num414));
                                num415 = num416 / num415;
                                num413 *= num415;
                                num414 *= num415;
                                num414 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num413 += (float)Main.rand.Next(-40, 41) * 0.01f;
                                num414 += npc.velocity.Y * 0.5f;
                                num413 += npc.velocity.X * 0.5f;
                                vector41.X -= num413 * 2f;
                                vector41.Y -= num414 * 1f;
                                Projectile.NewProjectile(vector41.X, vector41.Y, num413, num414, num418, num417, 0f, Main.myPlayer, npc.whoAmI, 0f);
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    npc.rotation = num391;
                    float num419 = 14f;
                    if (Main.expertMode)
                    {
                        num419 += 2.5f;
                    }
                    Vector2 vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num420 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector42.X;
                    float num421 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector42.Y;
                    float num422 = (float)Math.Sqrt((double)(num420 * num420 + num421 * num421));
                    num422 = num419 / num422;
                    npc.velocity.X = num420 * num422;
                    npc.velocity.Y = num421 * num422;
                    npc.ai[1] = 2f;
                }
                else if (npc.ai[1] == 2f)
                {
                    npc.ai[2] += 1f;
                    if (Main.expertMode)
                    {
                        npc.ai[2] += 0.5f;
                    }
                    if (npc.ai[2] >= 50f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.93f;
                        npc.velocity.Y = npc.velocity.Y * 0.93f;
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) - 1.57f;
                    }
                    if (npc.ai[2] >= 80f)
                    {
                        npc.ai[3] += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = num391;
                        if (npc.ai[3] >= 6f)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
            }
            // <-- Spazmatism phase 2

            // --> EoC between phases
            // Rotation between phases
            if (npc.ai[0] == 1f)
            {
                Color newColor;
                if (npc.ai[0] == 1f)
                {
                    npc.ai[2] += 0.005f;
                    if ((double)npc.ai[2] > 0.5)
                    {
                        npc.ai[2] = 0.5f;
                    }
                }
                else
                {
                    npc.ai[2] -= 0.005f;
                    if (npc.ai[2] < 0f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                npc.rotation += npc.ai[2];
                npc.ai[1] += 1f;
                if (Main.expertMode && npc.ai[1] % 20f == 0f)
                {
                    float num26 = 5f;
                    Vector2 vector3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num27 = (float)Main.rand.Next(-200, 200);
                    float num28 = (float)Main.rand.Next(-200, 200);
                    float num29 = (float)Math.Sqrt((double)(num27 * num27 + num28 * num28));
                    num29 = num26 / num29;
                    Vector2 vector4 = vector3;
                    Vector2 vector5 = default(Vector2);
                    vector5.X = num27 * num29;
                    vector5.Y = num28 * num29;
                    vector4.X += vector5.X * 10f;
                    vector4.Y += vector5.Y * 10f;
                    if (Main.netMode != 1)
                    {
                        int num30 = NPC.NewNPC((int)vector4.X, (int)vector4.Y, 5, 0, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num30].velocity.X = vector5.X;
                        Main.npc[num30].velocity.Y = vector5.Y;
                        if (Main.netMode == 2 && num30 < 200)
                        {
                            NetMessage.SendData(23, -1, -1, null, num30, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    int num2;
                    for (int num31 = 0; num31 < 10; num31 = num2 + 1)
                    {
                        Vector2 position2 = vector4;
                        float speedX = vector5.X * 0.4f;
                        float speedY = vector5.Y * 0.4f;
                        newColor = default(Color);
                        Dust.NewDust(position2, 20, 20, 5, speedX, speedY, 0, newColor, 1f);
                        num2 = num31;
                    }
                }
                if (npc.ai[1] == 100f)
                {
                    npc.ai[0] += 1f;
                    npc.ai[1] = 0f;
                    if (npc.ai[0] == 3f)
                    {
                        npc.ai[2] = 0f;
                    }
                    else
                    {
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    }
                }
                Vector2 position4 = npc.position;
                int width3 = npc.width;
                int height3 = npc.height;
                float speedX3 = (float)Main.rand.Next(-30, 31) * 0.2f;
                float speedY3 = (float)Main.rand.Next(-30, 31) * 0.2f;
                newColor = default(Color);
                Dust.NewDust(position4, width3, height3, 5, speedX3, speedY3, 0, newColor, 1f);
                npc.velocity.X = npc.velocity.X * 0.98f;
                npc.velocity.Y = npc.velocity.Y * 0.98f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                }
                // <-- EoC rotation between phases
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                int random = Main.rand.Next(3);
                int weapon = 0;

                switch (random)
                {
                    case 0:
                        weapon = mod.ItemType<VampiricShiv>();
                        break;
                    case 1:
                        weapon = mod.ItemType<Umbra>();
                        break;
                    case 2:
                        weapon = mod.ItemType<BloodStream>();
                        break;
                    default:
                        Main.NewText("Unexpected error in Bloodshot Eye drops: weapon drop random is out of range (" + random + ").", Color.Red);
                        break;
                }

                Item.NewItem(npc.Center, weapon);

                Item.NewItem(npc.Center, mod.ItemType<BloodiedEssence>(), Main.rand.Next(35, 51));
            }
            else
            {
                npc.DropBossBags();
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            DecimationWorld.downedArachnus = true;

            name = "The Bloodshot Eye";
            potionType = ItemID.HealingPotion;
            base.BossLoot(ref name, ref potionType);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hasSpawnedMinions);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hasSpawnedMinions = reader.ReadBoolean();
        }

        public override void ScaleExpertStats(int numPlayers, float bosslifeScale)
        {
            npc.damage = 73;
            npc.lifeMax = 9600;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 frameSize = new Vector2(110, 110);
            Texture2D texture = mod.GetTexture("NPCs/Bloodshot/BloodshotEye");

            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    npc.position.X - Main.screenPosition.X + frameSize.X / 2,
                    npc.position.Y - Main.screenPosition.Y + frameSize.Y / 2
                ),
                npc.frame,
                drawColor,
                npc.rotation - (float)(Math.PI * 0.5f),
                frameSize * 0.5f,
                npc.scale,
                SpriteEffects.None,
                0f
            );

            return false;
        }
    }
}
