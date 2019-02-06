using Decimation.Items.Misc;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    class NecroCaster : ModNPC
    {
        public static int numberKilled = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 56;
            npc.aiStyle = -1;
            npc.lifeMax = 100;
            npc.knockBackResist = 0.46f;
            npc.defense = 2;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.value = 10000;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            animationType = NPCID.Necromancer;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 220;
            npc.knockBackResist = 0.51f;
            npc.defDamage = 5;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(10, 16));

            if (Main.rand.NextBool(2))
                Item.NewItem(npc.getRect(), mod.ItemType<BloodiedEssence>(), Main.rand.Next(4, 9));
        }

        public override void AI()
        {
            Color newColor;
            Dust dust3;

            npc.TargetClosest(true);
            npc.velocity.X = npc.velocity.X * 0.93f;
            if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
            {
                npc.velocity.X = 0f;
            }
            if (npc.ai[0] == 0f)
            {
                npc.ai[0] = 500f;
            }
            if (npc.ai[2] != 0f && npc.ai[3] != 0f)
            {
                Main.PlaySound(SoundID.Item8, npc.position);
                int num2;
                for (int num65 = 0; num65 < 50; num65 = num2 + 1)
                {
                    Vector2 position12 = new Vector2(npc.position.X, npc.position.Y);
                    int width11 = npc.width;
                    int height11 = npc.height;
                    newColor = default(Color);
                    int num73 = Dust.NewDust(position12, width11, height11, mod.DustType<Dusts.Blood>(), 0f, 0f, 100, newColor, 2.5f);
                    dust3 = Main.dust[num73];
                    dust3.velocity *= 3f;
                    Main.dust[num73].noGravity = true;
                    num2 = num65;
                }
                npc.position.X = npc.ai[2] * 16f - (float)(npc.width / 2) + 8f;
                npc.position.Y = npc.ai[3] * 16f - (float)npc.height;
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
                npc.ai[2] = 0f;
                npc.ai[3] = 0f;
                Main.PlaySound(SoundID.Item8, npc.position);
                for (int num74 = 0; num74 < 50; num74 = num2 + 1)
                {
                    Vector2 position20 = new Vector2(npc.position.X, npc.position.Y);
                    int width19 = npc.width;
                    int height19 = npc.height;
                    newColor = default(Color);
                    int num82 = Dust.NewDust(position20, width19, height19, mod.DustType<Dusts.Blood>(), 0f, 0f, 100, newColor, 2.5f);
                    dust3 = Main.dust[num82];
                    dust3.velocity *= 3f;
                    Main.dust[num82].noGravity = true;
                    num2 = num74;
                }
            }
            npc.ai[0] += 1f;
            if (npc.ai[0] == 100f || npc.ai[0] == 200f || npc.ai[0] == 300f)
            {
                npc.ai[1] = 30f;
                npc.netUpdate = true;
            }
            if (npc.ai[0] >= 650f && Main.netMode != 1)
            {
                npc.ai[0] = 1f;
                int num83 = (int)Main.player[npc.target].position.X / 16;
                int num84 = (int)Main.player[npc.target].position.Y / 16;
                int num85 = (int)npc.position.X / 16;
                int num86 = (int)npc.position.Y / 16;
                int num87 = 20;
                int num88 = 0;
                bool flag4 = false;
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) + Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    num88 = 100;
                    flag4 = true;
                }
                while (!flag4 && num88 < 100)
                {
                    int num2 = num88;
                    num88 = num2 + 1;
                    int num89 = Main.rand.Next(num83 - num87, num83 + num87);
                    int num90 = Main.rand.Next(num84 - num87, num84 + num87);
                    for (int num91 = num90; num91 < num84 + num87; num2 = num91, num91 = num2 + 1)
                    {
                        bool flag5;
                        if ((num91 < num84 - 4 || num91 > num84 + 4 || num89 < num83 - 4 || num89 > num83 + 4) && (num91 < num86 - 1 || num91 > num86 + 1 || num89 < num85 - 1 || num89 > num85 + 1) && Main.tile[num89, num91].nactive())
                        {
                            flag5 = true;
                            if (Main.tile[num89, num91 - 1].lava())
                            {
                                flag5 = false;
                            }
                            goto IL_3b70;
                        }
                        continue;
                        IL_3b70:
                        if (flag5 && Main.tileSolid[Main.tile[num89, num91].type] && !Collision.SolidTiles(num89 - 1, num89 + 1, num91 - 4, num91 - 1))
                        {
                            npc.ai[1] = 20f;
                            npc.ai[2] = (float)num89;
                            npc.ai[3] = (float)num91;
                            flag4 = true;
                            break;
                        }
                    }
                }
                npc.netUpdate = true;
            }
            if (npc.ai[1] > 0f)
            {
                npc.ai[1] -= 1f;
                if (npc.ai[1] == 25f)
                {
                    if (Main.netMode != 1)
                    {
                        float speedX = Main.rand.NextFloat(-1, 2);
                        float speedY = Main.rand.NextFloat(-1, 2);

                        Projectile.NewProjectile(new Vector2(npc.Center.X + npc.direction * 8, npc.position.Y), new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.Bone>(), 1, 0, npc.target);
                    }
                }
            }

            if (Main.rand.Next(2) == 0)
            {
                Vector2 position27 = new Vector2(npc.position.X, npc.position.Y + 2f);
                int width26 = npc.width;
                int height26 = npc.height;
                float speedX9 = npc.velocity.X * 0.2f;
                float speedY9 = npc.velocity.Y * 0.2f;
                newColor = default(Color);
                int num121 = Dust.NewDust(position27, width26, height26, mod.DustType<Dusts.Blood>(), speedX9, speedY9, 100, newColor, 2f);
                Main.dust[num121].noGravity = true;
                Dust dust13 = Main.dust[num121];
                dust13.velocity.X = dust13.velocity.X * 1f;
                Dust dust14 = Main.dust[num121];
                dust14.velocity.Y = dust14.velocity.Y * 1f;
            }

            Lighting.AddLight(npc.Center, new Vector3(0.8f, 0f, 0.2f));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.bloodMoon) return 0;
            return SpawnCondition.OverworldNightMonster.Chance * 0.02f;
        }

        public override bool CheckDead()
        {
            numberKilled++;
            for (int i = 0; i < 10; i++)
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.Blood>(), npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 2f);

            if(numberKilled >= 3)
            {
                Player player = Main.player[npc.target];

                if (Main.netMode == 0)
                {
                    Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<NPCs.Bloodshot.BloodshotEye>());
                }
                else
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)DecimationModMessageType.SpawnBoss);
                    packet.Write(mod.NPCType<NPCs.Bloodshot.BloodshotEye>());
                    packet.Write(player.whoAmI);
                    packet.Send();
                }

                numberKilled = 0;
            }

            return base.CheckDead();
        }
    }
}
