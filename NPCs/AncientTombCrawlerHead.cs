using System;
using Decimation.Items.Misc.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    public class AncientTombCrawlerHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("Ancient Tomb Crawler");
        }

        public override void SetDefaults()
        {
            this.npc.lifeMax = 2500; //this is the npc health
            this.npc.damage = 50; //this is the npc damage
            this.npc.defense = 1; //this is the npc defense
            this.npc.knockBackResist = 0f;
            this.npc.scale = 0.7f;
            this.npc.width = 83; //this is where you put the npc sprite width.     important
            this.npc.height = 83; //this is where you put the npc sprite height.   important
            this.npc.lavaImmune = true; //this make the npc immune to lava
            this.npc.noGravity = true; //this make the npc float
            this.npc.noTileCollide = true; //this make the npc go tru walls
            this.npc.HitSound = SoundID.NPCHit1;
            this.npc.behindTiles = true;
            this.npc.DeathSound = SoundID.NPCDeath1;
            Main.npcFrameCount[this.npc.type] = 1;
            this.npc.value = Item.buyPrice(0, 0, 1);
            this.npc.npcSlots = 1f;
            this.npc.netAlways = true;
            this.npc.boss = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(20)) target.AddBuff(BuffID.Darkness, 600);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(20)) target.AddBuff(BuffID.Darkness, 600);
        }

        public override void NPCLoot()
        {
            Item.NewItem(this.npc.Center, this.mod.ItemType<SoulofTime>(), Main.rand.Next(5, 11));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "An Ancient Tomb Crawler";
            potionType = ItemID.HealingPotion;

            base.BossLoot(ref name, ref potionType);
        }

        public override bool PreAI()
        {
            if (Main.netMode != 1)
                // So, we start the AI off by checking if npc.ai[0] is 0.
                // This is practically ALWAYS the case with a freshly spawned NPC, so this means this is the first update.
                // Since this is the first update, we can safely assume we need to spawn the rest of the worm (bodies + tail).
                if (this.npc.ai[0] == 0)
                {
                    // So, here we assing the npc.realLife value.
                    // The npc.realLife value is mainly used to determine which NPC loses life when we hit this NPC.
                    // We don't want every single piece of the worm to have its own HP pool, so this is a neat way to fix that.
                    this.npc.realLife = this.npc.whoAmI;
                    // LatestNPC is going to be used later on and I'll explain it there.
                    int latestNPC = this.npc.whoAmI;

                    // Here we determine the length of the worm.
                    // In this case the worm will have a length of 10 to 14 body parts.
                    int wormLength = 20;
                    for (int i = 0; i < wormLength; ++i)
                    {
                        // We spawn a new NPC, setting latestNPC to the newer NPC, whilst also using that same variable
                        // to set the parent of this new NPC. The parent of the new NPC (may it be a tail or body part)
                        // will determine the movement of this new NPC.
                        // Under there, we also set the realLife value of the new NPC, because of what is explained above.
                        latestNPC = NPC.NewNPC((int) this.npc.Center.X, (int) this.npc.Center.Y,
                            this.mod.NPCType<AncientTombCrawlerBody>(), this.npc.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = this.npc.whoAmI;
                        Main.npc[latestNPC].ai[3] = this.npc.whoAmI;
                    }

                    // When we're out of that loop, we want to 'close' the worm with a tail part!
                    latestNPC = NPC.NewNPC((int) this.npc.Center.X, (int) this.npc.Center.Y,
                        this.mod.NPCType<AncientTombCrawlerTail>(), this.npc.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = this.npc.whoAmI;
                    Main.npc[latestNPC].ai[3] = this.npc.whoAmI;

                    // We're setting npc.ai[0] to 1, so that this 'if' is not triggered again.
                    this.npc.ai[0] = 1;
                    this.npc.netUpdate = true;
                }

            int minTilePosX = (int) (this.npc.position.X / 16.0) - 1;
            int maxTilePosX = (int) ((this.npc.position.X + this.npc.width) / 16.0) + 2;
            int minTilePosY = (int) (this.npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int) ((this.npc.position.Y + this.npc.height) / 16.0) + 2;
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
            for (int j = minTilePosY; j < maxTilePosY; ++j)
                if (Main.tile[i, j] != null &&
                    (Main.tile[i, j].nactive() && (Main.tileSolid[Main.tile[i, j].type] ||
                                                   Main.tileSolidTop[Main.tile[i, j].type] &&
                                                   Main.tile[i, j].frameY == 0) || Main.tile[i, j].liquid > 64))
                {
                    Vector2 vector2;
                    vector2.X = i * 16;
                    vector2.Y = j * 16;
                    if (this.npc.position.X + this.npc.width > vector2.X && this.npc.position.X < vector2.X + 16.0 &&
                        this.npc.position.Y + this.npc.height > (double) vector2.Y &&
                        this.npc.position.Y < vector2.Y + 16.0)
                    {
                        collision = true;
                        if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                            WorldGen.KillTile(i, j, true, true);
                    }
                }

            // If there is no collision with tiles, we check if the distance between this NPC and its target is too large, so that we can still trigger 'collision'.
            if (!collision)
            {
                Rectangle rectangle1 = new Rectangle((int) this.npc.position.X, (int) this.npc.position.Y,
                    this.npc.width, this.npc.height);
                int maxDistance = 1000;
                bool playerCollision = true;
                for (int index = 0; index < 255; ++index)
                    if (Main.player[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int) Main.player[index].position.X - maxDistance,
                            (int) Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                        if (rectangle1.Intersects(rectangle2))
                        {
                            playerCollision = false;
                            break;
                        }
                    }

                if (playerCollision)
                    collision = true;
            }

            // speed determines the max speed at which this NPC can move.
            // Higher value = faster speed.
            float speed = 15f;
            // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
            float acceleration = 0.12f;

            Vector2 npcCenter = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                this.npc.position.Y + this.npc.height * 0.5f);
            float targetXPos = Main.player[this.npc.target].position.X + Main.player[this.npc.target].width / 2f;
            float targetYPos = Main.player[this.npc.target].position.Y + Main.player[this.npc.target].height / 2f;

            float targetRoundedPosX = (int) (targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int) (targetYPos / 16.0) * 16;
            npcCenter.X = (int) (npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int) (npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float) Math.Sqrt(dirX * dirX + dirY * dirY);
            // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
            if (!collision)
            {
                this.npc.TargetClosest();
                this.npc.velocity.Y = this.npc.velocity.Y + 0.11f;
                if (this.npc.velocity.Y > speed) this.npc.velocity.Y = speed;
                if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < speed * 0.4)
                {
                    if (this.npc.velocity.X < 0.0)
                        this.npc.velocity.X = this.npc.velocity.X - acceleration * 1.1f;
                    else
                        this.npc.velocity.X = this.npc.velocity.X + acceleration * 1.1f;
                }
                else if (this.npc.velocity.Y == speed)
                {
                    if (this.npc.velocity.X < dirX)
                        this.npc.velocity.X = this.npc.velocity.X + acceleration;
                    else if (this.npc.velocity.X > dirX) this.npc.velocity.X = this.npc.velocity.X - acceleration;
                }
                else if (this.npc.velocity.Y > 4.0)
                {
                    if (this.npc.velocity.X < 0.0)
                        this.npc.velocity.X = this.npc.velocity.X + acceleration * 0.9f;
                    else
                        this.npc.velocity.X = this.npc.velocity.X - acceleration * 0.9f;
                }
            }
            // Else we want to play some audio (soundDelay) and move towards our target.
            else
            {
                if (this.npc.soundDelay == 0)
                {
                    this.npc.soundDelay = 120;
                    Main.PlaySound(this.mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Earthquake"),
                        this.npc.Center);
                }

                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (this.npc.velocity.X > 0.0 && dirX > 0.0 || this.npc.velocity.X < 0.0 && dirX < 0.0 ||
                    this.npc.velocity.Y > 0.0 && dirY > 0.0 || this.npc.velocity.Y < 0.0 && dirY < 0.0)
                {
                    if (this.npc.velocity.X < dirX)
                        this.npc.velocity.X = this.npc.velocity.X + acceleration;
                    else if (this.npc.velocity.X > dirX) this.npc.velocity.X = this.npc.velocity.X - acceleration;
                    if (this.npc.velocity.Y < dirY)
                        this.npc.velocity.Y = this.npc.velocity.Y + acceleration;
                    else if (this.npc.velocity.Y > dirY) this.npc.velocity.Y = this.npc.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 &&
                        (this.npc.velocity.X > 0.0 && dirX < 0.0 || this.npc.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (this.npc.velocity.Y > 0.0)
                            this.npc.velocity.Y = this.npc.velocity.Y + acceleration * 2f;
                        else
                            this.npc.velocity.Y = this.npc.velocity.Y - acceleration * 2f;
                    }

                    if (Math.Abs(dirX) < speed * 0.2 &&
                        (this.npc.velocity.Y > 0.0 && dirY < 0.0 || this.npc.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (this.npc.velocity.X > 0.0)
                            this.npc.velocity.X = this.npc.velocity.X + acceleration * 2f;
                        else
                            this.npc.velocity.X = this.npc.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (this.npc.velocity.X < dirX)
                        this.npc.velocity.X = this.npc.velocity.X + acceleration * 1.1f;
                    else if (this.npc.velocity.X > dirX)
                        this.npc.velocity.X = this.npc.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < speed * 0.5)
                    {
                        if (this.npc.velocity.Y > 0.0)
                            this.npc.velocity.Y = this.npc.velocity.Y + acceleration;
                        else
                            this.npc.velocity.Y = this.npc.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (this.npc.velocity.Y < dirY)
                        this.npc.velocity.Y = this.npc.velocity.Y + acceleration * 1.1f;
                    else if (this.npc.velocity.Y > dirY)
                        this.npc.velocity.Y = this.npc.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < speed * 0.5)
                    {
                        if (this.npc.velocity.X > 0.0)
                            this.npc.velocity.X = this.npc.velocity.X + acceleration;
                        else
                            this.npc.velocity.X = this.npc.velocity.X - acceleration;
                    }
                }
            }

            // Set the correct rotation for this NPC.
            this.npc.rotation = (float) Math.Atan2(this.npc.velocity.Y, this.npc.velocity.X) + 1.57f;

            // Some netupdate stuff (multiplayer compatibility).
            if (collision)
            {
                if (this.npc.localAI[0] != 1) this.npc.netUpdate = true;
                this.npc.localAI[0] = 1f;
            }
            else
            {
                if (this.npc.localAI[0] != 0.0) this.npc.netUpdate = true;
                this.npc.localAI[0] = 0.0f;
            }

            if ((this.npc.velocity.X > 0.0 && this.npc.oldVelocity.X < 0.0 ||
                 this.npc.velocity.X < 0.0 && this.npc.oldVelocity.X > 0.0 ||
                 this.npc.velocity.Y > 0.0 && this.npc.oldVelocity.Y < 0.0 ||
                 this.npc.velocity.Y < 0.0 && this.npc.oldVelocity.Y > 0.0) &&
                !this.npc.justHit) this.npc.netUpdate = true;

            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[this.npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, this.npc.Center - Main.screenPosition, new Rectangle?(), drawColor,
                this.npc.rotation, origin, this.npc.scale, SpriteEffects.None, 0);
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.9f; //this make the NPC Health Bar biger
            return null;
        }
    }
}