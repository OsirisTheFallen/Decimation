using System;
using System.IO;
using Decimation.Items.Boss.Bloodshot;
using Decimation.Items.Misc;
using Decimation.Items.Weapons.Bloodshot;
using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs.Bloodshot
{
    [AutoloadBossHead]
    internal class BloodshotEye : ModNPC
    {
        private bool _hasSpawnedMinions;

        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("The Bloodshot Eye");
            Main.npcFrameCount[this.npc.type] = 3;
        }

        public override void SetDefaults()
        {
            this.npc.width = 110;
            this.npc.height = 110;
            this.npc.value = 70000;
            this.npc.aiStyle = -1;
            this.npc.defense = 18;
            this.npc.damage = 54;
            this.npc.lifeMax = 8500;
            this.npc.noGravity = true;
            this.npc.noTileCollide = true;
            this.npc.boss = true;
            this.npc.aiStyle = -1;
            this.npc.knockBackResist = 0;
            this.npc.dontTakeDamage = true;
            bossBag = this.mod.ItemType<TreasureBagBloodshotEye>();
        }

        public override void AI()
        {
            // Minions
            if (!_hasSpawnedMinions)
            {
                if (Main.netMode != 1)
                    for (int i = 0; i < (Main.expertMode ? 14 : 10); i++)
                        NPC.NewNPC((int) this.npc.Center.X, (int) this.npc.Center.Y, this.mod.NPCType<MangledServant>(),
                            0, 0, this.npc.whoAmI);

                _hasSpawnedMinions = true;
                this.npc.dontTakeDamage = true;
            }

            if (!NPC.AnyNPCs(this.mod.NPCType<MangledServant>())) this.npc.dontTakeDamage = false;

            // --> EoC phase 1
            if (this.npc.ai[0] == 0f)
            {
                // --> Custom
                // Blood

                if (Main.rand.NextBool(10) && Main.netMode != 1)
                {
                    Vector2 cloudPosition = this.npc.position;
                    cloudPosition.X += Main.rand.Next(this.npc.width / 2) + this.npc.width / 4;
                    cloudPosition.Y += this.npc.height / 2;
                    int proj = Projectile.NewProjectile(cloudPosition, new Vector2(0, 10), ProjectileID.BloodRain,
                        Main.expertMode ? 20 : 12, 0);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                }
                // <-- Custom

                float num11;
                Color newColor;
                Vector2 vector;
                bool flag2 = false;
                if (Main.expertMode && this.npc.life < this.npc.lifeMax * 0.12) flag2 = true;
                bool flag3 = false;
                if (Main.expertMode && this.npc.life < this.npc.lifeMax * 0.04) flag3 = true;
                float num8 = 20f;
                if (flag3) num8 = 10f;
                if (this.npc.target < 0 || this.npc.target == 255 || Main.player[this.npc.target].dead ||
                    !Main.player[this.npc.target].active) this.npc.TargetClosest();
                float num9 = this.npc.position.X + this.npc.width / 2 - Main.player[this.npc.target].position.X -
                             Main.player[this.npc.target].width / 2;
                float num10 = this.npc.position.Y + this.npc.height - 59f - Main.player[this.npc.target].position.Y -
                              Main.player[this.npc.target].height / 2;
                num11 = (float) Math.Atan2(num10, num9) + 1.57f;
                if (num11 < 0f)
                    num11 += 6.283f;
                else if (num11 > 6.283) num11 -= 6.283f;
                float num12 = 0f;
                if (this.npc.ai[0] == 0f && this.npc.ai[1] == 0f) num12 = 0.02f;
                if (this.npc.ai[0] == 0f && this.npc.ai[1] == 2f && this.npc.ai[2] > 40f) num12 = 0.05f;
                if (this.npc.ai[0] == 3f && this.npc.ai[1] == 0f) num12 = 0.05f;
                if (this.npc.ai[0] == 3f && this.npc.ai[1] == 2f && this.npc.ai[2] > 40f) num12 = 0.08f;
                if (this.npc.ai[0] == 3f && this.npc.ai[1] == 4f && this.npc.ai[2] > num8) num12 = 0.15f;
                if (this.npc.ai[0] == 3f && this.npc.ai[1] == 5f) num12 = 0.05f;
                if (Main.expertMode) num12 *= 1.5f;
                if (flag3 && Main.expertMode) num12 = 0f;
                if (this.npc.rotation < num11)
                {
                    if (num11 - this.npc.rotation > 3.1415)
                        this.npc.rotation -= num12;
                    else
                        this.npc.rotation += num12;
                }
                else if (this.npc.rotation > num11)
                {
                    if (this.npc.rotation - num11 > 3.1415)
                        this.npc.rotation += num12;
                    else
                        this.npc.rotation -= num12;
                }

                if (this.npc.rotation > num11 - num12 && this.npc.rotation < num11 + num12) this.npc.rotation = num11;
                if (this.npc.rotation < 0f)
                    this.npc.rotation += 6.283f;
                else if (this.npc.rotation > 6.283) this.npc.rotation -= 6.283f;
                if (this.npc.rotation > num11 - num12 && this.npc.rotation < num11 + num12) this.npc.rotation = num11;
                if (Main.rand.Next(5) == 0)
                {
                    Vector2 position = new Vector2(this.npc.position.X, this.npc.position.Y + this.npc.height * 0.25f);
                    int width = this.npc.width;
                    int height = (int) (this.npc.height * 0.5f);
                    float x = this.npc.velocity.X;
                    newColor = default;
                    int num13 = Dust.NewDust(position, width, height, 5, x, 2f, 0, newColor);
                    Dust dust = Main.dust[num13];
                    dust.velocity.X = dust.velocity.X * 0.5f;
                    Dust dust2 = Main.dust[num13];
                    dust2.velocity.Y = dust2.velocity.Y * 0.1f;
                }

                bool dead = Main.player[this.npc.target].dead;
                if (Main.dayTime | dead)
                {
                    this.npc.velocity.Y = this.npc.velocity.Y - 0.04f;
                    if (this.npc.timeLeft > 10) this.npc.timeLeft = 10;
                }
                else
                {
                    if (this.npc.ai[1] == 0f)
                    {
                        float num14 = 5f;
                        float num15 = 0.04f;
                        if (Main.expertMode)
                        {
                            num15 = 0.15f;
                            num14 = 7f;
                        }

                        vector = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                            this.npc.position.Y + this.npc.height * 0.5f);
                        float num16 = Main.player[this.npc.target].position.X + Main.player[this.npc.target].width / 2 -
                                      vector.X;
                        float num17 = Main.player[this.npc.target].position.Y +
                                      Main.player[this.npc.target].height / 2 - 200f - vector.Y;
                        float num18 = (float) Math.Sqrt(num16 * num16 + num17 * num17);
                        float num19 = num18;
                        num18 = num14 / num18;
                        num16 *= num18;
                        num17 *= num18;
                        if (this.npc.velocity.X < num16)
                        {
                            this.npc.velocity.X = this.npc.velocity.X + num15;
                            if (this.npc.velocity.X < 0f && num16 > 0f)
                                this.npc.velocity.X = this.npc.velocity.X + num15;
                        }
                        else if (this.npc.velocity.X > num16)
                        {
                            this.npc.velocity.X = this.npc.velocity.X - num15;
                            if (this.npc.velocity.X > 0f && num16 < 0f)
                                this.npc.velocity.X = this.npc.velocity.X - num15;
                        }

                        if (this.npc.velocity.Y < num17)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y + num15;
                            if (this.npc.velocity.Y < 0f && num17 > 0f)
                                this.npc.velocity.Y = this.npc.velocity.Y + num15;
                        }
                        else if (this.npc.velocity.Y > num17)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y - num15;
                            if (this.npc.velocity.Y > 0f && num17 < 0f)
                                this.npc.velocity.Y = this.npc.velocity.Y - num15;
                        }

                        if (this.npc.ai[2] % 60 == 0)
                        {
                            float num416 = 6f;
                            int num417 = 30;
                            if (Main.expertMode) num417 = 27;
                            int num418 = this.mod.ProjectileType<BloodClot>();
                            Vector2 vector41 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                                this.npc.position.Y + this.npc.height * 0.5f);
                            float num413 = Main.player[this.npc.target].position.X +
                                           Main.player[this.npc.target].width / 2 - vector41.X;
                            float num414 = Main.player[this.npc.target].position.Y +
                                           Main.player[this.npc.target].height / 2 - vector41.Y;
                            float num415 = (float) Math.Sqrt(num413 * num413 + num414 * num414);
                            num415 = num416 / num415;
                            num413 *= num415;
                            num414 *= num415;
                            num414 += Main.rand.Next(-40, 41) * 0.01f;
                            num413 += Main.rand.Next(-40, 41) * 0.01f;
                            num414 += this.npc.velocity.Y * 0.5f;
                            num413 += this.npc.velocity.X * 0.5f;
                            vector41.X -= num413 * 2f;
                            vector41.Y -= num414 * 1f;
                            Projectile.NewProjectile(vector41.X, vector41.Y, num413, num414, num418, num417, 0f);

                            if (Main.expertMode)
                            {
                                num418 = this.mod.ProjectileType<BloodClotSmall>();
                                num414 += (float) (Math.PI * (1 / 6f));
                                Projectile.NewProjectile(vector41.X, vector41.Y, num413, num414, num418, num417, 1f);
                            }
                        }

                        this.npc.ai[2] += 1f;
                        float num20 = 600f;
                        if (Main.expertMode) num20 *= 0.35f;
                        if (this.npc.ai[2] >= num20)
                        {
                            this.npc.ai[1] = 1f;
                            this.npc.ai[2] = 0f;
                            this.npc.ai[3] = 0f;
                            this.npc.target = 255;
                            this.npc.netUpdate = true;
                        }
                        else
                        {
                            if (this.npc.position.Y + this.npc.height < Main.player[this.npc.target].position.Y &&
                                num19 < 500f)
                            {
                                if (!Main.player[this.npc.target].dead) this.npc.ai[3] += 1f;
                                float num1449 = 110f;
                                if (Main.expertMode) num1449 *= 0.4f;
                                if (this.npc.ai[3] >= num1449)
                                {
                                    this.npc.ai[3] = 0f;
                                    this.npc.rotation = num11;
                                }

                                float num1461 = 0.5f;
                                if (Main.expertMode) num1461 = 0.65f;
                                if (this.npc.life < this.npc.lifeMax * num1461)
                                {
                                    // 2nd phase
                                    this.npc.ai[0] = 1f;
                                    this.npc.ai[1] = 0f;
                                    this.npc.ai[2] = 0f;
                                    this.npc.ai[3] = 0f;
                                    this.npc.netUpdate = true;
                                    if (this.npc.netSpam > 10) this.npc.netSpam = 10;
                                }
                            }

                            if (Main.expertMode && num19 < 500f)
                            {
                                if (!Main.player[this.npc.target].dead) this.npc.ai[3] += 1f;
                                float num1449 = 110f;
                                if (Main.expertMode) num1449 *= 0.4f;
                                if (this.npc.ai[3] >= num1449)
                                {
                                    this.npc.ai[3] = 0f;
                                    this.npc.rotation = num11;
                                    float num1450 = 5f;
                                    if (Main.expertMode) num1450 = 6f;
                                    float num1451 = Main.player[this.npc.target].position.X +
                                                    Main.player[this.npc.target].width / 2 - vector.X;
                                    float num1452 = Main.player[this.npc.target].position.Y +
                                                    Main.player[this.npc.target].height / 2 - vector.Y;
                                    float num1453 = (float) Math.Sqrt(num1451 * num1451 + num1452 * num1452);
                                    num1453 = num1450 / num1453;
                                    Vector2 vector252 = vector;
                                    Vector2 vector253 = default;
                                    vector253.X = num1451 * num1453;
                                    vector253.Y = num1452 * num1453;
                                    vector252.X += vector253.X * 10f;
                                    vector252.Y += vector253.Y * 10f;
                                    Main.PlaySound(3, (int) vector252.X, (int) vector252.Y);
                                    int num2;
                                    for (int num1455 = 0; num1455 < 10; num1455 = num2 + 1)
                                    {
                                        Vector2 position102 = vector252;
                                        float speedX31 = vector253.X * 0.4f;
                                        float speedY30 = vector253.Y * 0.4f;
                                        newColor = default;
                                        Dust.NewDust(position102, 20, 20, 5, speedX31, speedY30, 0, newColor);
                                        num2 = num1455;
                                    }
                                }

                                float num1461 = 0.5f;
                                if (Main.expertMode) num1461 = 0.65f;
                                if (this.npc.life < this.npc.lifeMax * num1461)
                                {
                                    // 2nd phase
                                    this.npc.ai[0] = 1f;
                                    this.npc.ai[1] = 0f;
                                    this.npc.ai[2] = 0f;
                                    this.npc.ai[3] = 0f;
                                    this.npc.netUpdate = true;
                                    if (this.npc.netSpam > 10) this.npc.netSpam = 10;
                                }
                            }
                        }
                    }
                    else if (this.npc.ai[1] == 1f)
                    {
                        this.npc.rotation = num11;
                        float num21 = 6f;
                        if (Main.expertMode) num21 = 7f;
                        Vector2 vector2 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                            this.npc.position.Y + this.npc.height * 0.5f);
                        float num22 = Main.player[this.npc.target].position.X + Main.player[this.npc.target].width / 2 -
                                      vector2.X;
                        float num23 = Main.player[this.npc.target].position.Y +
                                      Main.player[this.npc.target].height / 2 - vector2.Y;
                        float num24 = (float) Math.Sqrt(num22 * num22 + num23 * num23);
                        num24 = num21 / num24;
                        this.npc.velocity.X = num22 * num24;
                        this.npc.velocity.Y = num23 * num24;
                        this.npc.ai[1] = 2f;
                        this.npc.netUpdate = true;
                        if (this.npc.netSpam > 10) this.npc.netSpam = 10;

                        if (Main.expertMode)
                            this.npc.velocity *= 1 + (this.npc.lifeMax - this.npc.life) /
                                                 (this.npc.lifeMax * (Main.expertMode ? 0.65f : 0.5f)) * 2f;
                    }
                    else if (this.npc.ai[1] == 2f)
                    {
                        this.npc.ai[2] += 1f;
                        if (this.npc.ai[2] >= 40f)
                        {
                            this.npc.velocity *= 0.98f;
                            if (Main.expertMode) this.npc.velocity *= 0.985f;
                            if (this.npc.velocity.X > -0.1 && this.npc.velocity.X < 0.1) this.npc.velocity.X = 0f;
                            if (this.npc.velocity.Y > -0.1 && this.npc.velocity.Y < 0.1) this.npc.velocity.Y = 0f;
                        }
                        else
                        {
                            this.npc.rotation = (float) Math.Atan2(this.npc.velocity.Y, this.npc.velocity.X) - 1.57f;
                        }

                        int num25 = 150;
                        if (Main.expertMode) num25 = 100;
                        if (this.npc.ai[2] >= num25)
                        {
                            this.npc.ai[3] += 1f;
                            this.npc.ai[2] = 0f;
                            this.npc.target = 255;
                            this.npc.rotation = num11;
                            if (this.npc.ai[3] >= 3f)
                            {
                                this.npc.ai[1] = 0f;
                                this.npc.ai[3] = 0f;
                            }
                            else
                            {
                                this.npc.ai[1] = 1f;
                            }
                        }
                    }

                    float num1460 = 0.5f;
                    if (Main.expertMode) num1460 = 0.65f;
                    if (this.npc.life < this.npc.lifeMax * num1460)
                    {
                        // 2nd phase
                        this.npc.ai[0] = 1f;
                        this.npc.ai[1] = 0f;
                        this.npc.ai[2] = 0f;
                        this.npc.ai[3] = 0f;
                        this.npc.netUpdate = true;
                        if (this.npc.netSpam > 10) this.npc.netSpam = 10;
                    }
                }
            } // <-- EoC phase 1
            // --> Spazmatism phase 2
            else if (this.npc.ai[0] == 2)
            {
                if (Main.expertMode)
                    this.npc.damage = 57;
                else
                    this.npc.damage = 36;

                Color newColor;
                if (this.npc.target < 0 || this.npc.target == 255 || Main.player[this.npc.target].dead ||
                    !Main.player[this.npc.target].active) this.npc.TargetClosest();
                bool dead3 = Main.player[this.npc.target].dead;
                float num389 = this.npc.position.X + this.npc.width / 2 - Main.player[this.npc.target].position.X -
                               Main.player[this.npc.target].width / 2;
                float num390 = this.npc.position.Y + this.npc.height - 59f - Main.player[this.npc.target].position.Y -
                               Main.player[this.npc.target].height / 2;
                float num391 = (float) Math.Atan2(num390, num389) + 1.57f;
                if (num391 < 0f)
                    num391 += 6.283f;
                else if (num391 > 6.283) num391 -= 6.283f;
                float num392 = 0.15f;
                if (this.npc.rotation < num391)
                {
                    if (num391 - this.npc.rotation > 3.1415)
                        this.npc.rotation -= num392;
                    else
                        this.npc.rotation += num392;
                }
                else if (this.npc.rotation > num391)
                {
                    if (this.npc.rotation - num391 > 3.1415)
                        this.npc.rotation += num392;
                    else
                        this.npc.rotation -= num392;
                }

                if (this.npc.rotation > num391 - num392 && this.npc.rotation < num391 + num392)
                    this.npc.rotation = num391;
                if (this.npc.rotation < 0f)
                    this.npc.rotation += 6.283f;
                else if (this.npc.rotation > 6.283) this.npc.rotation -= 6.283f;
                if (this.npc.rotation > num391 - num392 && this.npc.rotation < num391 + num392)
                    this.npc.rotation = num391;
                if (Main.rand.Next(5) == 0)
                {
                    Vector2 position42 =
                        new Vector2(this.npc.position.X, this.npc.position.Y + this.npc.height * 0.25f);
                    int width40 = this.npc.width;
                    int height38 = (int) (this.npc.height * 0.5f);
                    float x4 = this.npc.velocity.X;
                    newColor = default;
                    int num393 = Dust.NewDust(position42, width40, height38, 5, x4, 2f, 0, newColor);
                    Dust dust29 = Main.dust[num393];
                    dust29.velocity.X = dust29.velocity.X * 0.5f;
                    Dust dust30 = Main.dust[num393];
                    dust30.velocity.Y = dust30.velocity.Y * 0.1f;
                }

                if (Main.netMode != 1 && !Main.dayTime && !dead3 && this.npc.timeLeft < 10)
                {
                    int num2;
                    for (int num394 = 0; num394 < 200; num394 = num2 + 1)
                    {
                        if (num394 != this.npc.whoAmI && Main.npc[num394].active &&
                            (Main.npc[num394].type == 125 || Main.npc[num394].type == 126) &&
                            Main.npc[num394].timeLeft - 1 > this.npc.timeLeft)
                            this.npc.timeLeft = Main.npc[num394].timeLeft - 1;
                        num2 = num394;
                    }
                }

                if (Main.dayTime | dead3)
                {
                    this.npc.velocity.Y = this.npc.velocity.Y - 0.04f;
                    if (this.npc.timeLeft > 10) this.npc.timeLeft = 10;
                }
                else
                {
                    this.npc.defense = this.npc.defDefense + 6;
                    if (this.npc.ai[1] == 0f)
                    {
                        float num410 = 4f;
                        float num411 = 0.1f;
                        int num412 = 1;
                        if (this.npc.position.X + this.npc.width / 2 < Main.player[this.npc.target].position.X +
                            Main.player[this.npc.target].width) num412 = -1;
                        Vector2 vector41 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                            this.npc.position.Y + this.npc.height * 0.5f);
                        float num413 = Main.player[this.npc.target].position.X +
                                       Main.player[this.npc.target].width / 2 + num412 * 180 - vector41.X;
                        float num414 = Main.player[this.npc.target].position.Y +
                                       Main.player[this.npc.target].height / 2 - vector41.Y;
                        float num415 = (float) Math.Sqrt(num413 * num413 + num414 * num414);
                        if (Main.expertMode)
                        {
                            if (num415 > 300f) num410 += 0.5f;
                            if (num415 > 400f) num410 += 0.5f;
                            if (num415 > 500f) num410 += 0.55f;
                            if (num415 > 600f) num410 += 0.55f;
                            if (num415 > 700f) num410 += 0.6f;
                            if (num415 > 800f) num410 += 0.6f;
                        }

                        num415 = num410 / num415;
                        num413 *= num415;
                        num414 *= num415;
                        if (this.npc.velocity.X < num413)
                        {
                            this.npc.velocity.X = this.npc.velocity.X + num411;
                            if (this.npc.velocity.X < 0f && num413 > 0f)
                                this.npc.velocity.X = this.npc.velocity.X + num411;
                        }
                        else if (this.npc.velocity.X > num413)
                        {
                            this.npc.velocity.X = this.npc.velocity.X - num411;
                            if (this.npc.velocity.X > 0f && num413 < 0f)
                                this.npc.velocity.X = this.npc.velocity.X - num411;
                        }

                        if (this.npc.velocity.Y < num414)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y + num411;
                            if (this.npc.velocity.Y < 0f && num414 > 0f)
                                this.npc.velocity.Y = this.npc.velocity.Y + num411;
                        }
                        else if (this.npc.velocity.Y > num414)
                        {
                            this.npc.velocity.Y = this.npc.velocity.Y - num411;
                            if (this.npc.velocity.Y > 0f && num414 < 0f)
                                this.npc.velocity.Y = this.npc.velocity.Y - num411;
                        }

                        this.npc.ai[2] += 1f;
                        if (this.npc.ai[2] >= 400f)
                        {
                            this.npc.ai[1] = 1f;
                            this.npc.ai[2] = 0f;
                            this.npc.ai[3] = 0f;
                            this.npc.target = 255;
                            this.npc.netUpdate = true;
                        }

                        if (Collision.CanHit(this.npc.position, this.npc.width, this.npc.height,
                            Main.player[this.npc.target].position, Main.player[this.npc.target].width,
                            Main.player[this.npc.target].height))
                        {
                            this.npc.localAI[2] += 1f;
                            if (this.npc.localAI[2] > 22f)
                            {
                                this.npc.localAI[2] = 0f;
                                Main.PlaySound(SoundID.Item34, this.npc.position);
                            }

                            if (Main.netMode != 1)
                            {
                                this.npc.localAI[1] += 1f;
                                if (this.npc.life < this.npc.lifeMax * 0.75) this.npc.localAI[1] += 1f;
                                if (this.npc.life < this.npc.lifeMax * 0.5) this.npc.localAI[1] += 1f;
                                if (this.npc.life < this.npc.lifeMax * 0.25) this.npc.localAI[1] += 1f;
                                if (this.npc.life < this.npc.lifeMax * 0.1) this.npc.localAI[1] += 2f;
                                if (this.npc.localAI[1] > 8f)
                                {
                                    this.npc.localAI[1] = 0f;
                                    float num416 = 6f;
                                    int num417 = 30;
                                    if (Main.expertMode) num417 = 27;
                                    int num418 = this.mod.ProjectileType<BloodBeam>();
                                    vector41 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                                        this.npc.position.Y + this.npc.height * 0.5f);
                                    num413 = Main.player[this.npc.target].position.X +
                                             Main.player[this.npc.target].width / 2 - vector41.X;
                                    num414 = Main.player[this.npc.target].position.Y +
                                             Main.player[this.npc.target].height / 2 - vector41.Y;
                                    num415 = (float) Math.Sqrt(num413 * num413 + num414 * num414);
                                    num415 = num416 / num415;
                                    num413 *= num415;
                                    num414 *= num415;
                                    num414 += Main.rand.Next(-40, 41) * 0.01f;
                                    num413 += Main.rand.Next(-40, 41) * 0.01f;
                                    num414 += this.npc.velocity.Y * 0.5f;
                                    num413 += this.npc.velocity.X * 0.5f;
                                    vector41.X -= num413 * 2f;
                                    vector41.Y -= num414 * 1f;
                                    Projectile.NewProjectile(vector41.X, vector41.Y, num413, num414, num418, num417, 0f,
                                        Main.myPlayer, this.npc.whoAmI);
                                }
                            }
                        }
                    }
                    else if (this.npc.ai[1] == 1f)
                    {
                        Main.PlaySound(15, (int) this.npc.position.X, (int) this.npc.position.Y, 0);
                        this.npc.rotation = num391;
                        float num419 = 14f;
                        if (Main.expertMode) num419 += 2.5f;
                        Vector2 vector42 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                            this.npc.position.Y + this.npc.height * 0.5f);
                        float num420 = Main.player[this.npc.target].position.X +
                                       Main.player[this.npc.target].width / 2 - vector42.X;
                        float num421 = Main.player[this.npc.target].position.Y +
                                       Main.player[this.npc.target].height / 2 - vector42.Y;
                        float num422 = (float) Math.Sqrt(num420 * num420 + num421 * num421);
                        num422 = num419 / num422;
                        this.npc.velocity.X = num420 * num422;
                        this.npc.velocity.Y = num421 * num422;
                        this.npc.ai[1] = 2f;
                    }
                    else if (this.npc.ai[1] == 2f)
                    {
                        this.npc.ai[2] += 1f;
                        if (Main.expertMode) this.npc.ai[2] += 0.5f;
                        if (this.npc.ai[2] >= 50f || this.npc.life <= this.npc.lifeMax * 0.25f && this.npc.ai[2] >= 20f)
                        {
                            this.npc.velocity.X = this.npc.velocity.X * 0.93f;
                            this.npc.velocity.Y = this.npc.velocity.Y * 0.93f;
                            if (this.npc.velocity.X > -0.1 && this.npc.velocity.X < 0.1) this.npc.velocity.X = 0f;
                            if (this.npc.velocity.Y > -0.1 && this.npc.velocity.Y < 0.1) this.npc.velocity.Y = 0f;
                        }
                        else
                        {
                            this.npc.rotation = (float) Math.Atan2(this.npc.velocity.Y, this.npc.velocity.X) - 1.57f;
                        }

                        if (this.npc.ai[2] >= 80f || this.npc.life <= this.npc.lifeMax * 0.25f && this.npc.ai[2] >= 50f)
                        {
                            this.npc.ai[3] += 1f;
                            this.npc.ai[2] = 0f;
                            this.npc.target = 255;
                            this.npc.rotation = num391;
                            if (this.npc.ai[3] >= 6f)
                            {
                                this.npc.ai[1] = 0f;
                                this.npc.ai[3] = 0f;
                            }
                            else
                            {
                                this.npc.ai[1] = 1f;
                            }
                        }
                    }
                }
            }
            // <-- Spazmatism phase 2

            // --> EoC between phases
            // Rotation between phases
            if (this.npc.ai[0] == 1f)
            {
                Color newColor;
                if (this.npc.ai[0] == 1f)
                {
                    this.npc.ai[2] += 0.005f;
                    if (this.npc.ai[2] > 0.5) this.npc.ai[2] = 0.5f;
                }
                else
                {
                    this.npc.ai[2] -= 0.005f;
                    if (this.npc.ai[2] < 0f) this.npc.ai[2] = 0f;
                }

                this.npc.rotation += this.npc.ai[2];
                this.npc.ai[1] += 1f;
                if (Main.expertMode && this.npc.ai[1] % 20f == 0f)
                {
                    float num26 = 5f;
                    Vector2 vector3 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                        this.npc.position.Y + this.npc.height * 0.5f);
                    float num27 = Main.rand.Next(-200, 200);
                    float num28 = Main.rand.Next(-200, 200);
                    float num29 = (float) Math.Sqrt(num27 * num27 + num28 * num28);
                    num29 = num26 / num29;
                    Vector2 vector4 = vector3;
                    Vector2 vector5 = default;
                    vector5.X = num27 * num29;
                    vector5.Y = num28 * num29;
                    vector4.X += vector5.X * 10f;
                    vector4.Y += vector5.Y * 10f;
                    int num2;
                    for (int num31 = 0; num31 < 10; num31 = num2 + 1)
                    {
                        Vector2 position2 = vector4;
                        float speedX = vector5.X * 0.4f;
                        float speedY = vector5.Y * 0.4f;
                        newColor = default;
                        Dust.NewDust(position2, 20, 20, 5, speedX, speedY, 0, newColor);
                        num2 = num31;
                    }
                }

                if (this.npc.ai[1] == 100f)
                {
                    if (Main.expertMode)
                        _hasSpawnedMinions = false;
                    this.npc.ai[0] += 1f;
                    this.npc.ai[1] = 0f;
                    if (this.npc.ai[0] == 3f)
                        this.npc.ai[2] = 0f;
                    else
                        Main.PlaySound(15, (int) this.npc.position.X, (int) this.npc.position.Y, 0);
                }

                Vector2 position4 = this.npc.position;
                int width3 = this.npc.width;
                int height3 = this.npc.height;
                float speedX3 = Main.rand.Next(-30, 31) * 0.2f;
                float speedY3 = Main.rand.Next(-30, 31) * 0.2f;
                newColor = default;
                Dust.NewDust(position4, width3, height3, 5, speedX3, speedY3, 0, newColor);
                this.npc.velocity.X = this.npc.velocity.X * 0.98f;
                this.npc.velocity.Y = this.npc.velocity.Y * 0.98f;
                if (this.npc.velocity.X > -0.1 && this.npc.velocity.X < 0.1) this.npc.velocity.X = 0f;
                if (this.npc.velocity.Y > -0.1 && this.npc.velocity.Y < 0.1) this.npc.velocity.Y = 0f;
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
                        //weapon = mod.ItemType<VampiricShiv>();
                        break;
                    case 1:
                        //weapon = mod.ItemType<Umbra>();
                        break;
                    case 2:
                        weapon = this.mod.ItemType<BloodStream>();
                        break;
                    default:
                        Main.NewText(
                            "Unexpected error in Bloodshot Eye drops: weapon drop random is out of range (" + random +
                            ").", Color.Red);
                        break;
                }

                Item.NewItem(this.npc.Center, weapon);

                Item.NewItem(this.npc.Center, this.mod.ItemType<BloodiedEssence>(), Main.rand.Next(35, 51));
            }
            else
            {
                this.npc.DropBossBags();
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
            writer.Write(_hasSpawnedMinions);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _hasSpawnedMinions = reader.ReadBoolean();
        }

        public override void ScaleExpertStats(int numPlayers, float bosslifeScale)
        {
            this.npc.damage = 73;
            this.npc.lifeMax = 9600;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 frameSize = new Vector2(110, 110);
            Texture2D texture = this.mod.GetTexture("NPCs/Bloodshot/BloodshotEye");

            spriteBatch.Draw
            (
                texture,
                new Vector2
                (this.npc.position.X - Main.screenPosition.X + frameSize.X / 2,
                    this.npc.position.Y - Main.screenPosition.Y + frameSize.Y / 2
                ), this.npc.frame,
                drawColor, this.npc.rotation - (float) (Math.PI * 0.5f),
                frameSize * 0.5f, this.npc.scale,
                SpriteEffects.None,
                0f
            );

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            this.npc.frameCounter += 1.5f;
            if (this.npc.frameCounter >= 30) this.npc.frameCounter = 0;
            this.npc.frame.Y = (int) this.npc.frameCounter / 10 * frameHeight;
        }
    }
}