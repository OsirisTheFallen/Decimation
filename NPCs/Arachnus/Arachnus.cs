using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Decimation.Projectiles;
using Decimation.Tiles.ShrineoftheMoltenOne;

namespace Decimation.NPCs.Arachnus
{
    [AutoloadBossHead]
    class Arachnus : ModNPC
    {
        private int counter = 0;
        private int counterMax = 1320;
        private float speed = 2;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arachnus");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 80000;
            npc.damage = 100;
            npc.defense = 25;
            npc.knockBackResist = 0f;
            npc.width = 200;
            npc.height = 200;
            npc.value = 50000;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false; // When not enraged
            npc.HitSound = SoundID.NPCHit6;
            npc.DeathSound = SoundID.NPCDeath10;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Drums_of_hell");
            bossBag = mod.ItemType("ArachnusBag");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
            npc.defense = (int)(npc.defense + numPlayers * 2);
        }

        private bool CheckDispawn()
        {
            bool playersActive = false;
            bool playersDead = true;

            foreach (Player player in Main.player)
            {
                if (player.active) playersActive = true;
                if (!player.dead) playersDead = false;
            }

            return playersDead || !playersActive;
        }

        private void Move()
        {
            // Rotate to player
            Vector2 moveTo = Main.player[npc.target].Center - npc.Center;
            float angle = (float)Math.Atan2(moveTo.Y, moveTo.X);
            npc.rotation = (float)(angle + Math.PI * 0.5f);

            // Move
            Vector2 move = moveTo;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 50f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = npc.ai[1] != 99 ? move : new Vector2(0, 0);
        }

        private bool CheckForShrine()
        {
            bool tooFarFromShrine = false;
            if (counter % 60 == 0)
            {
                int validBlockCount = 0;
                for (int i = (int)(-50 + npc.Center.X / 16f); i <= (int)(50 + npc.Center.X / 16f); i++)
                {
                    for (int j = (int)(-50 + npc.Center.Y / 16f); j <= (int)(50 + npc.Center.Y / 16f); j++)
                    {
                        if (i >= 0 && i <= Main.maxTilesX && j >= 0 && j <= Main.maxTilesY)
                        {
                            if (Main.tile[i, j].type == mod.TileType<ShrineBrick>() || (Main.tile[i, j].type == mod.TileType<LockedShrineDoor>() || Main.tile[i, j].type == mod.TileType<ShrineDoorClosed>() || Main.tile[i, j].type == mod.TileType<ShrineDoorOpened>()) || Main.tile[i, j].type == mod.TileType<RedHotSpike>() || Main.tile[i, j].type == mod.TileType<ShrineAltar>())
                                validBlockCount++;
                        }
                    }
                }

                if (validBlockCount < 15)
                    tooFarFromShrine = true;
            }

            return tooFarFromShrine;
        }
        private void CheckEnraged()
        {
            npc.ai[2] = !Main.player[npc.target].ZoneUnderworldHeight || !Collision.CanHit(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0) || CheckForShrine() ? 1 : 0;
        }

        public override void AI()
        {
            if (CheckDispawn())
            {
                npc.velocity = new Vector2(0, 10f);
                npc.noTileCollide = true;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
            }

            npc.TargetClosest(true);

            CheckEnraged();

            // Normal ai
            if (npc.ai[0] == 0)
            {

                float mouthX = (float)(((npc.height) / 2) * Math.Cos(npc.rotation - Math.PI * 0.5f)) + npc.Center.X;
                float mouthY = (float)(((npc.height) / 2) * Math.Sin(npc.rotation - Math.PI * 0.5f)) + npc.Center.Y;

                //Counter
                if (npc.life <= npc.lifeMax / 2) counterMax = 1500;
                if (counter >= counterMax) counter = 0;

                // Fireballs
                if (counter <= 600) npc.ai[1] = 0;
                else if (counter > 600 && counter < 800) npc.ai[1] = 98;
                // Blast of Heat
                else if (counter >= 800 && counter <= 1100)
                {
                    npc.ai[1] = 1;
                }
                // Increase speed
                else if (counter > 1320 && counter <= 1500 || (Main.expertMode && counter > 600 && counter < 800 && npc.life <= npc.lifeMax / 4))
                {
                    if (counter == 1321)
                    {
                        Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                        if (Main.netMode == 2)
                            GetPacket(ArachnusMessageType.RoarSound).Send();
                    }
                    npc.ai[1] = 2;
                }
                else npc.ai[1] = 99;

                if (counter % 40 == 0 && npc.ai[1] == 0)
                {
                    float speedX = (float)(6 * Math.Cos(npc.rotation - Math.PI * 0.5f)) * 2;
                    float speedY = (float)(6 * Math.Sin(npc.rotation - Math.PI * 0.5f)) * 2;
                    Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY), mod.ProjectileType<ArachnusFireball>(), 30, 0f);
                }
                else if (counter % 5 == 0 && npc.ai[1] == 1)
                {
                    float speedX = (float)(7 * Math.Cos(npc.rotation - Math.PI * 0.5f));
                    float speedY = (float)(7 * Math.Sin(npc.rotation - Math.PI * 0.5f));
                    Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY), npc.ai[2] == 1 ? mod.ProjectileType<BlastofShadowFlame>() : mod.ProjectileType<BlastofHeat>(), 30, 0f, 255);
                    Main.PlaySound(SoundID.Item34, npc.position);
                    if (Main.netMode == 2)
                        GetPacket(ArachnusMessageType.FlamesSound).Send();
                }
                else if (npc.ai[1] == 2)
                {
                    if (Main.netMode != 1)
                    {
                        speed = 20f;
                        if (Main.expertMode)
                        {
                            speed = (npc.lifeMax - npc.life) / 500;
                            if (npc.ai[2] == 1)
                                speed += 20;
                        }
                        else if (npc.ai[2] == 1)
                            speed = 40f;
                    }
                }

                if (npc.ai[1] != 2)
                {
                    if (Main.netMode != 1)
                        speed = 2f;
                }

                // Enraged
                if (npc.ai[2] == 1)
                {
                    npc.noTileCollide = true;
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);

                    if (npc.ai[1] != 2)
                    {
                        if (Main.netMode != 1)
                            speed = 6f;
                        npc.defense = 300;
                    }
                }

                if (Main.netMode != 1)
                    counter++;
            }

            Move();
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(counter);
            writer.Write(counterMax);
            writer.Write(speed);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            counter = reader.ReadInt32();
            counterMax = reader.ReadInt32();
            speed = reader.ReadSingle();
        }

        private ModPacket GetPacket(ArachnusMessageType type)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)DecimationModMessageType.Arachnus);
            packet.Write(npc.whoAmI);
            packet.Write((byte)type);
            return packet;
        }
        public void HandlePacket(BinaryReader reader)
        {
            ArachnusMessageType type = (ArachnusMessageType)reader.ReadByte();
            switch (type)
            {
                case ArachnusMessageType.RoarSound:
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    break;
                case ArachnusMessageType.FlamesSound:
                    Main.PlaySound(SoundID.Item34, npc.position);
                    break;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[1] == 2 && Main.expertMode)
            {
                target.AddBuff(mod.BuffType("Hyperthermic"), 900);
            }
            base.OnHitPlayer(target, damage, crit);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 3f;
            if (npc.frameCounter >= 40)
                npc.frameCounter = 0;
            npc.frame.Y = ((int)npc.frameCounter / 10) * frameHeight;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.Center, mod.ItemType("SoulofSpite"), Main.rand.Next(15, 31));

            if (!Main.expertMode)
            {
                int rand = Main.rand.Next(3);
                if (rand == 0)
                    Item.NewItem(npc.Center, mod.ItemType("ChainStynger"));
                else if (rand == 1)
                    Item.NewItem(npc.Center, mod.ItemType("GlaiveWeaver"));
                else if (rand == 2)
                    Item.NewItem(npc.Center, mod.ItemType("Infernolizer"));
            }
            else
            {
                npc.DropBossBags();
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Arachnus";
            // Maybe better
            potionType = ItemID.SuperHealingPotion;

            DecimationWorld.downedArachnus = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 frameSize = new Vector2(298, 318);
            Texture2D texture = mod.GetTexture("NPCs/Arachnus/Arachnus");

            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    npc.position.X - Main.screenPosition.X + frameSize.X * 0.34f,
                    npc.position.Y - Main.screenPosition.Y + frameSize.Y * 0.31f
                ),
                npc.frame,
                drawColor,
                npc.rotation,
                frameSize * 0.5f,
                npc.scale,
                SpriteEffects.None,
                0f
            );
            return false;
        }

        public enum ArachnusMessageType
        {
            RoarSound,
            FlamesSound
        }
    }
}
