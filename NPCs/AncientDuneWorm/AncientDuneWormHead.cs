using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Decimation.Buffs;
using Decimation.Projectiles;
using Decimation.Items;
using System.IO;
using Decimation.Items.Vanity.DuneWorm;
using Decimation.Items.Misc.Souls;
using Decimation.Buffs.Debuffs;
using Decimation.Items.Placeable.DuneWorm;

namespace Decimation.NPCs.AncientDuneWorm
{
    [AutoloadBossHead]
    public class AncientDuneWormHead : ModNPC
    {
        private bool spawnedAncientTombCrawler = false;

        private const float initialSpeed = 15f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Dune Worm");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 11000;        //this is the npc health
            npc.damage = 65;    //this is the npc damage
            npc.defense = 15;         //this is the npc defense
            npc.knockBackResist = 0f;
            npc.width = 115; //this is where you put the npc sprite width.     important
            npc.height = 115; //this is where you put the npc sprite height.   important
            npc.boss = true;
            npc.lavaImmune = true;       //this make the npc immune to lava
            npc.noGravity = true;           //this make the npc float
            npc.noTileCollide = true;        //this make the npc go tru walls
            npc.behindTiles = true;
            npc.DeathSound = SoundID.NPCDeath18;
            npc.HitSound = SoundID.NPCHit1;
            Main.npcFrameCount[npc.type] = 1;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.aiStyle = -1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/The_Deserts_Call");
            bossBag = mod.ItemType<DuneWormTreasureBag>();

            // Speed
            npc.localAI[1] = 0;
            // Maximum speed
            if (!Main.expertMode)
                npc.localAI[2] = 20;
            else
                npc.localAI[2] = 40;
        }

        public override void AI()
        {
            if (!CheckDispawn())
            {
                if (npc.life >= npc.lifeMax / 2) ComputeSpeed();
                if (Main.expertMode) SummonSandnado();
                ShootAmmonite();

                if (npc.life <= npc.lifeMax * 0.15f && !NPC.AnyNPCs(mod.NPCType<AncientTombCrawlerHead>()) && !spawnedAncientTombCrawler)
                {
                    NPC.SpawnOnPlayer(npc.target, mod.NPCType<AncientTombCrawlerHead>());
                    spawnedAncientTombCrawler = true;
                }

                // Latest tile
                npc.ai[1] = Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type;
            }
        }

        private bool CheckDispawn()
        {
            // check active
            bool playersActive = false;
            // check death
            bool playersDead = true;
            foreach (Player player in Main.player)
            {
                if (player.active) playersActive = true;
                if (!player.dead) playersDead = false;
            }

            if (!Main.player[npc.target].ZoneDesert || npc.ai[2] == 1 || !playersActive || playersDead)
            {
                npc.ai[2] = 1;
                npc.velocity = new Vector2(0, 15);
                npc.rotation = (int)(Math.PI * 3) / 2;
                npc.netUpdate = true;
                npc.timeLeft = 10;
                return true;
            }

            return false;
        }

        private void ComputeSpeed()
        {
            float currentSpeed = npc.localAI[1];
            float maximumSpeed = npc.localAI[2];

            npc.localAI[1] = initialSpeed * (100 + (maximumSpeed * (npc.lifeMax - npc.life)) / 4500) / 100;
        }

        private void SummonSandnado()
        {
            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type == 0 && npc.ai[1] == TileID.Sand)
                Projectile.NewProjectile(npc.Center, new Vector2(0, 0), ProjectileID.SandnadoHostile, 15, 10f);
        }

        private void ShootAmmonite()
        {
            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type == TileID.Sand && npc.ai[1] == 0)
            {
                Main.PlaySound(SoundID.Item14, npc.Center);

                // Smoke
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }

                // Ammonite
                Vector2 explosionPos = new Vector2(npc.Center.X / 16, npc.Center.Y / 16);
                int ammoniteNbre = Main.rand.Next(5, 9);

                for (int i = 0; i < ammoniteNbre; i++)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-8, 9), Main.rand.Next(8, 15)), mod.ProjectileType<Ammonite>(), 15, 5f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                Item.NewItem(npc.Center, mod.ItemType<SoulofTime>(), Main.rand.Next(15, 26));
                Item.NewItem(npc.Center, ItemID.DesertFossil, Main.rand.Next(3, 7));
                if (Main.rand.NextBool(7))
                    Item.NewItem(npc.Center, mod.ItemType<DuneWormMask>());
                if (Main.rand.NextBool(13))
                    Item.NewItem(npc.Center, mod.ItemType<DuneWormTrophy>());
            }
            else
            {
                npc.DropBossBags();
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Ancient Dune Worm";
            DecimationWorld.downedDuneWorm = true;

            potionType = ItemID.HealingPotion;
            base.BossLoot(ref name, ref potionType);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(5) || (!Main.expertMode && Main.rand.NextBool(2)))
                target.AddBuff(mod.BuffType<Hyperthermic>(), Main.expertMode ? 600 : 300);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(5) || (!Main.expertMode && Main.rand.NextBool(2)))
                target.AddBuff(mod.BuffType<Hyperthermic>(), Main.expertMode ? 600 : 300);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(spawnedAncientTombCrawler);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            spawnedAncientTombCrawler = reader.ReadBoolean();
        }

        private ModPacket GetPacket(DuneWormMessageType type)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)DecimationModMessageType.DuneWorm);
            packet.Write(npc.whoAmI);
            packet.Write((byte)type);
            return packet;
        }
        public void HandlePacket(BinaryReader reader)
        {
            DuneWormMessageType type = (DuneWormMessageType)reader.ReadByte();
            switch (type)
            {
                case DuneWormMessageType.UndergroundSound:
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Earthquake"), npc.Center);
                    break;
            }
        }

        public override bool PreAI()
        {

            if (Main.netMode != 1)
            {
                // So, we start the AI off by checking if npc.ai[0] is 0.
                // This is practically ALWAYS the case with a freshly spawned NPC, so this means this is the first update.
                // Since this is the first update, we can safely assume we need to spawn the rest of the worm (bodies + tail).
                if (npc.ai[0] == 0)
                {
                    // So, here we assing the npc.realLife value.
                    // The npc.realLife value is mainly used to determine which NPC loses life when we hit this NPC.
                    // We don't want every single piece of the worm to have its own HP pool, so this is a neat way to fix that.
                    npc.realLife = npc.whoAmI;
                    // LatestNPC is going to be used later on and I'll explain it there.
                    int latestNPC = npc.whoAmI;

                    // Here we determine the length of the worm.
                    // In this case the worm will have a length of 10 to 14 body parts.
                    int wormLength = 16;
                    for (int i = 0; i < wormLength; ++i)
                    {
                        // We spawn a new NPC, setting latestNPC to the newer NPC, whilst also using that same variable
                        // to set the parent of this new NPC. The parent of the new NPC (may it be a tail or body part)
                        // will determine the movement of this new NPC.
                        // Under there, we also set the realLife value of the new NPC, because of what is explained above.
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AncientDuneWormBody"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }
                    // When we're out of that loop, we want to 'close' the worm with a tail part!
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AncientDuneWormTail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                    // We're setting npc.ai[0] to 1, so that this 'if' is not triggered again.
                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }

            int minTilePosX = (int)(npc.position.X / 16.0) - 1;
            int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            bool collision = false;
            // This is the initial check for collision with tiles.
            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                    {
                        Vector2 vector2;
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
                        {
                            collision = true;
                            if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                                WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
            // If there is no collision with tiles, we check if the distance between this NPC and its target is too large, so that we can still trigger 'collision'.
            if (!collision)
            {
                Rectangle rectangle1 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int maxDistance = 1000;
                bool playerCollision = true;
                for (int index = 0; index < 255; ++index)
                {
                    if (Main.player[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                        if (rectangle1.Intersects(rectangle2))
                        {
                            playerCollision = false;
                            break;
                        }
                    }
                }
                if (playerCollision)
                    collision = true;
            }

            float speed = npc.localAI[1];
            // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
            float acceleration = 0.08f;

            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
            if (!collision)
            {
                npc.TargetClosest(true);
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (npc.velocity.Y > speed)
                    npc.velocity.Y = speed;
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    else
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                }
                else if (npc.velocity.Y == speed)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                else if (npc.velocity.Y > 4.0)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
                    else
                        npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
                }
            }
            // Else we want to play some audio (soundDelay) and move towards our target.
            else
            {
                if (npc.soundDelay == 0)
                {
                    npc.soundDelay = 120;
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Earthquake"), npc.Center);
                    if (Main.netMode == 2)
                        GetPacket(DuneWormMessageType.UndergroundSound).Send();
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 2f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                }
            }
            // Set the correct rotation for this NPC.
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

            // Some netupdate stuff (multiplayer compatibility).
            if (collision)
            {
                if (npc.localAI[0] != 1)
                    npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            else
            {
                if (npc.localAI[0] != 0.0)
                    npc.netUpdate = true;
                npc.localAI[0] = 0.0f;
            }
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                npc.netUpdate = true;

            return true;
        }
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Color color = npc.GetAlpha(drawColor) * ((float)(npc.oldPos.Length) / (float)npc.oldPos.Length);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), color, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.9f;   //this make the NPC Health Bar biger
            return null;
        }

        public enum DuneWormMessageType
        {
            UndergroundSound
        }
    }
}
