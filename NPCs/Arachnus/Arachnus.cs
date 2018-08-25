using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Decimation.NPCs.Arachnus
{
    [AutoloadBossHead]
    class Arachnus : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arachnus");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1; // Not a vanilla AI
            npc.lifeMax = 25000; // Not the real HP
            npc.damage = 100; // Not real damages
            npc.defense = 25; // Not real defenses
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
            music = MusicID.Boss4;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
            npc.defense = (int)(npc.defense + numPlayers * 2);
        }

        private Player player;
        private float speed = 2f;
        private Vector2 moveTo;
        private float turnResistance;
        private float counter = 0;
        private float counterMax = 1200;
        private float mouthX;
        private float mouthY;

        public override void AI()
        {

            player = Main.player[npc.target]; // player target

            // Check for possibilities to despawn
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0, 10f);
                    npc.noTileCollide = true;
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                }
            }

            // Rotate to player
            moveTo = player.Center - npc.Center;
            float angle = (float)Math.Atan2(moveTo.Y, moveTo.X);
            npc.rotation = (float)(angle + Math.PI * 0.5f);

            // Check if enraged
            if (!player.ZoneUnderworldHeight || !Collision.CanHit(npc.Center, 0, 0, player.Center, 0, 0))
                npc.ai[2] = 1;
            else
                npc.ai[2] = 0;

            // Normal ai
            if (npc.ai[0] == 0)
            {

                mouthX = (float)(((npc.height) / 2) * Math.Cos(npc.rotation - Math.PI * 0.5f)) + npc.Center.X;
                mouthY = (float)(((npc.height) / 2) * Math.Sin(npc.rotation - Math.PI * 0.5f)) + npc.Center.Y;

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
                        Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    npc.ai[1] = 2;
                }
                else npc.ai[1] = 99;

                if (Math.Abs((counter) / 40 % 1) <= (Double.Epsilon * 100) && npc.ai[1] == 0)
                {
                    float speedX = (float)(6 * Math.Cos(npc.rotation - Math.PI * 0.5f)) * 2;
                    float speedY = (float)(6 * Math.Sin(npc.rotation - Math.PI * 0.5f)) * 2;
                    Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY), ProjectileID.Fireball, 30, 0f);
                }
                else if (Math.Abs((counter) / 5 % 1) <= (Double.Epsilon * 100) && npc.ai[1] == 1)
                {
                    float speedX = (float)(7 * Math.Cos(npc.rotation - Math.PI * 0.5f));
                    float speedY = (float)(7 * Math.Sin(npc.rotation - Math.PI * 0.5f));
                    Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY), mod.ProjectileType("BlastofHeat"), 30, 0f);
                    Main.PlaySound(SoundID.Item34, npc.position);
                }
                else if (npc.ai[1] == 2)
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

                if (npc.ai[1] != 2)
                {
                    speed = 2f;
                }

                // Enraged
                if (npc.ai[2] == 1)
                {

                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);

                    if (npc.ai[1] != 2)
                    {
                        speed = 6f;
                        npc.defense = 300;
                    }
                }

                counter++;
            }

            //Move
            Vector2 move = moveTo;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            turnResistance = 50f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = npc.ai[1] != 99 ? move : new Vector2(0, 0);
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
            // Loots
            // Glaive Weaver
            // Infernolizer
            // Chain Stynger
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
    }

    public class FireBallsEffect : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileID.Fireball)
                target.AddBuff(BuffID.OnFire, 120);
            base.OnHitNPC(projectile, target, damage, knockback, crit);
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (projectile.type == ProjectileID.Fireball)
                target.AddBuff(BuffID.OnFire, 120);
            base.OnHitPlayer(projectile, target, damage, crit);
        }
    }
}
