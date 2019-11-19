using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Core.NPCs
{
    public abstract class Worm : ModNPC
    {
        public int bodyType;
        public bool directional = false;

        public bool flies = false;

        /* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
        public bool head;
        public int headType;
        public int maxLength;
        public int minLength;
        public float speed;
        public bool tail;
        public int tailType;
        public float turnSpeed;

        public override void AI()
        {
            if (this.npc.localAI[1] == 0f)
            {
                this.npc.localAI[1] = 1f;
                Init();
            }

            if (this.npc.ai[3] > 0f) this.npc.realLife = (int)this.npc.ai[3];
            if (!head && this.npc.timeLeft < 300) this.npc.timeLeft = 300;
            if (this.npc.target < 0 || this.npc.target == 255 || Main.player[this.npc.target].dead)
                this.npc.TargetClosest();
            if (Main.player[this.npc.target].dead && this.npc.timeLeft > 300) this.npc.timeLeft = 300;
            if (Main.netMode != 1)
            {
                if (!tail && this.npc.ai[0] == 0f)
                {
                    if (head)
                    {
                        this.npc.ai[3] = this.npc.whoAmI;
                        this.npc.realLife = this.npc.whoAmI;
                        this.npc.ai[2] = Main.rand.Next(minLength, maxLength + 1);
                        this.npc.ai[0] = NPC.NewNPC((int)(this.npc.position.X + this.npc.width / 2),
                            (int)(this.npc.position.Y + this.npc.height), bodyType, this.npc.whoAmI);
                    }
                    else if (this.npc.ai[2] > 0f)
                    {
                        this.npc.ai[0] = NPC.NewNPC((int)(this.npc.position.X + this.npc.width / 2),
                            (int)(this.npc.position.Y + this.npc.height), this.npc.type, this.npc.whoAmI);
                    }
                    else
                    {
                        this.npc.ai[0] = NPC.NewNPC((int)(this.npc.position.X + this.npc.width / 2),
                            (int)(this.npc.position.Y + this.npc.height), tailType, this.npc.whoAmI);
                    }

                    Main.npc[(int)this.npc.ai[0]].ai[3] = this.npc.ai[3];
                    Main.npc[(int)this.npc.ai[0]].realLife = this.npc.realLife;
                    Main.npc[(int)this.npc.ai[0]].ai[1] = this.npc.whoAmI;
                    Main.npc[(int)this.npc.ai[0]].ai[2] = this.npc.ai[2] - 1f;
                    this.npc.netUpdate = true;
                }

                if (!head && (!Main.npc[(int)this.npc.ai[1]].active ||
                              Main.npc[(int)this.npc.ai[1]].type != headType &&
                              Main.npc[(int)this.npc.ai[1]].type != bodyType))
                {
                    this.npc.life = 0;
                    this.npc.HitEffect();
                    this.npc.active = false;
                }

                if (!tail && (!Main.npc[(int)this.npc.ai[0]].active ||
                              Main.npc[(int)this.npc.ai[0]].type != bodyType &&
                              Main.npc[(int)this.npc.ai[0]].type != tailType))
                {
                    this.npc.life = 0;
                    this.npc.HitEffect();
                    this.npc.active = false;
                }

                if (!this.npc.active && Main.netMode == 2) NetMessage.SendData(28, -1, -1, null, this.npc.whoAmI, -1f);
            }

            int num180 = (int)(this.npc.position.X / 16f) - 1;
            int num181 = (int)((this.npc.position.X + this.npc.width) / 16f) + 2;
            int num182 = (int)(this.npc.position.Y / 16f) - 1;
            int num183 = (int)((this.npc.position.Y + this.npc.height) / 16f) + 2;
            if (num180 < 0) num180 = 0;
            if (num181 > Main.maxTilesX) num181 = Main.maxTilesX;
            if (num182 < 0) num182 = 0;
            if (num183 > Main.maxTilesY) num183 = Main.maxTilesY;
            bool flag18 = flies;
            if (!flag18)
                for (int num184 = num180; num184 < num181; num184++)
                    for (int num185 = num182; num185 < num183; num185++)
                        if (Main.tile[num184, num185] != null &&
                            (Main.tile[num184, num185].nactive() &&
                             (Main.tileSolid[Main.tile[num184, num185].type] ||
                              Main.tileSolidTop[Main.tile[num184, num185].type] && Main.tile[num184, num185].frameY == 0) ||
                             Main.tile[num184, num185].liquid > 64))
                        {
                            Vector2 vector17;
                            vector17.X = num184 * 16;
                            vector17.Y = num185 * 16;
                            if (this.npc.position.X + this.npc.width > vector17.X &&
                                this.npc.position.X < vector17.X + 16f &&
                                this.npc.position.Y + this.npc.height > vector17.Y &&
                                this.npc.position.Y < vector17.Y + 16f)
                            {
                                flag18 = true;
                                if (Main.rand.NextBool(100) && this.npc.behindTiles && Main.tile[num184, num185].nactive())
                                    WorldGen.KillTile(num184, num185, true, true);
                                if (Main.netMode != 1 && Main.tile[num184, num185].type == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].type;
                                }
                            }
                        }

            if (!flag18 && head)
            {
                Rectangle rectangle = new Rectangle((int)this.npc.position.X, (int)this.npc.position.Y,
                    this.npc.width, this.npc.height);
                int num186 = 1000;
                bool flag19 = true;
                for (int num187 = 0; num187 < 255; num187++)
                    if (Main.player[num187].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186,
                            (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag19 = false;
                            break;
                        }
                    }

                if (flag19) flag18 = true;
            }

            if (directional)
            {
                if (this.npc.velocity.X < 0f)
                    this.npc.spriteDirection = 1;
                else if (this.npc.velocity.X > 0f) this.npc.spriteDirection = -1;
            }

            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                this.npc.position.Y + this.npc.height * 0.5f);
            float num191 = Main.player[this.npc.target].position.X + Main.player[this.npc.target].width / 2;
            float num192 = Main.player[this.npc.target].position.Y + Main.player[this.npc.target].height / 2;
            num191 = (int)(num191 / 16f) * 16;
            num192 = (int)(num192 / 16f) * 16;
            vector18.X = (int)(vector18.X / 16f) * 16;
            vector18.Y = (int)(vector18.Y / 16f) * 16;
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
            if (this.npc.ai[1] > 0f && this.npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                        this.npc.position.Y + this.npc.height * 0.5f);
                    num191 = Main.npc[(int)this.npc.ai[1]].position.X + Main.npc[(int)this.npc.ai[1]].width / 2 -
                             vector18.X;
                    num192 = Main.npc[(int)this.npc.ai[1]].position.Y + Main.npc[(int)this.npc.ai[1]].height / 2 -
                             vector18.Y;
                }
                catch
                {
                }

                this.npc.rotation = (float)Math.Atan2(num192, num191) + 1.57f;
                num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
                int num194 = this.npc.width;
                num193 = (num193 - num194) / num193;
                num191 *= num193;
                num192 *= num193;
                this.npc.velocity = Vector2.Zero;
                this.npc.position.X = this.npc.position.X + num191;
                this.npc.position.Y = this.npc.position.Y + num192;
                if (directional)
                {
                    if (num191 < 0f) this.npc.spriteDirection = 1;
                    if (num191 > 0f) this.npc.spriteDirection = -1;
                }
            }
            else
            {
                if (!flag18)
                {
                    this.npc.TargetClosest();
                    this.npc.velocity.Y = this.npc.velocity.Y + 0.11f;
                    if (this.npc.velocity.Y > num188) this.npc.velocity.Y = num188;
                    if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < num188 * 0.4)
                    {
                        if (this.npc.velocity.X < 0f)
                            this.npc.velocity.X = this.npc.velocity.X - num189 * 1.1f;
                        else
                            this.npc.velocity.X = this.npc.velocity.X + num189 * 1.1f;
                    }
                    else if (this.npc.velocity.Y == num188)
                    {
                        if (this.npc.velocity.X < num191)
                            this.npc.velocity.X = this.npc.velocity.X + num189;
                        else if (this.npc.velocity.X > num191) this.npc.velocity.X = this.npc.velocity.X - num189;
                    }
                    else if (this.npc.velocity.Y > 4f)
                    {
                        if (this.npc.velocity.X < 0f)
                            this.npc.velocity.X = this.npc.velocity.X + num189 * 0.9f;
                        else
                            this.npc.velocity.X = this.npc.velocity.X - num189 * 0.9f;
                    }
                }
                else
                {
                    if (!flies && this.npc.behindTiles && this.npc.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f) num195 = 10f;
                        if (num195 > 20f) num195 = 20f;
                        this.npc.soundDelay = (int)num195;
                        Main.PlaySound(SoundID.Roar, this.npc.position);
                    }

                    num193 = (float)Math.Sqrt(num191 * num191 + num192 * num192);
                    float num196 = Math.Abs(num191);
                    float num197 = Math.Abs(num192);
                    float num198 = num188 / num193;
                    num191 *= num198;
                    num192 *= num198;
                    if (ShouldRun())
                    {
                        if (Main.netMode != 1 &&
                            this.npc.position.Y / 16f > (Main.rockLayer + Main.maxTilesY) / 2.0)
                        {
                            this.npc.active = false;
                            int num200 = (int)this.npc.ai[0];
                            while (num200 > 0 && num200 < 200 && Main.npc[num200].active &&
                                   Main.npc[num200].aiStyle == this.npc.aiStyle)
                            {
                                int num201 = (int)Main.npc[num200].ai[0];
                                Main.npc[num200].active = false;
                                this.npc.life = 0;
                                if (Main.netMode == 2) NetMessage.SendData(23, -1, -1, null, num200);
                                num200 = num201;
                            }

                            if (Main.netMode == 2) NetMessage.SendData(23, -1, -1, null, this.npc.whoAmI);
                        }

                        num191 = 0f;
                        num192 = num188;
                    }

                    bool flag21 = false;
                    if (this.npc.type == 87)
                    {
                        if ((this.npc.velocity.X > 0f && num191 < 0f || this.npc.velocity.X < 0f && num191 > 0f ||
                             this.npc.velocity.Y > 0f && num192 < 0f || this.npc.velocity.Y < 0f && num192 > 0f) &&
                            Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) > num189 / 2f &&
                            num193 < 300f)
                        {
                            flag21 = true;
                            if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < num188)
                                this.npc.velocity *= 1.1f;
                        }

                        if (this.npc.position.Y > Main.player[this.npc.target].position.Y ||
                            Main.player[this.npc.target].position.Y / 16f > Main.worldSurface ||
                            Main.player[this.npc.target].dead)
                        {
                            flag21 = true;
                            if (Math.Abs(this.npc.velocity.X) < num188 / 2f)
                            {
                                if (this.npc.velocity.X == 0f)
                                    this.npc.velocity.X = this.npc.velocity.X - this.npc.direction;
                                this.npc.velocity.X = this.npc.velocity.X * 1.1f;
                            }
                            else
                            {
                                if (this.npc.velocity.Y > -num188) this.npc.velocity.Y = this.npc.velocity.Y - num189;
                            }
                        }
                    }

                    if (!flag21)
                    {
                        if (this.npc.velocity.X > 0f && num191 > 0f || this.npc.velocity.X < 0f && num191 < 0f ||
                            this.npc.velocity.Y > 0f && num192 > 0f || this.npc.velocity.Y < 0f && num192 < 0f)
                        {
                            if (this.npc.velocity.X < num191)
                            {
                                this.npc.velocity.X = this.npc.velocity.X + num189;
                            }
                            else
                            {
                                if (this.npc.velocity.X > num191) this.npc.velocity.X = this.npc.velocity.X - num189;
                            }

                            if (this.npc.velocity.Y < num192)
                            {
                                this.npc.velocity.Y = this.npc.velocity.Y + num189;
                            }
                            else
                            {
                                if (this.npc.velocity.Y > num192) this.npc.velocity.Y = this.npc.velocity.Y - num189;
                            }

                            if (Math.Abs(num192) < num188 * 0.2 &&
                                (this.npc.velocity.X > 0f && num191 < 0f || this.npc.velocity.X < 0f && num191 > 0f))
                            {
                                if (this.npc.velocity.Y > 0f)
                                    this.npc.velocity.Y = this.npc.velocity.Y + num189 * 2f;
                                else
                                    this.npc.velocity.Y = this.npc.velocity.Y - num189 * 2f;
                            }

                            if (Math.Abs(num191) < num188 * 0.2 &&
                                (this.npc.velocity.Y > 0f && num192 < 0f || this.npc.velocity.Y < 0f && num192 > 0f))
                            {
                                if (this.npc.velocity.X > 0f)
                                    this.npc.velocity.X = this.npc.velocity.X + num189 * 2f;
                                else
                                    this.npc.velocity.X = this.npc.velocity.X - num189 * 2f;
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (this.npc.velocity.X < num191)
                                    this.npc.velocity.X = this.npc.velocity.X + num189 * 1.1f;
                                else if (this.npc.velocity.X > num191)
                                    this.npc.velocity.X = this.npc.velocity.X - num189 * 1.1f;
                                if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < num188 * 0.5)
                                {
                                    if (this.npc.velocity.Y > 0f)
                                        this.npc.velocity.Y = this.npc.velocity.Y + num189;
                                    else
                                        this.npc.velocity.Y = this.npc.velocity.Y - num189;
                                }
                            }
                            else
                            {
                                if (this.npc.velocity.Y < num192)
                                    this.npc.velocity.Y = this.npc.velocity.Y + num189 * 1.1f;
                                else if (this.npc.velocity.Y > num192)
                                    this.npc.velocity.Y = this.npc.velocity.Y - num189 * 1.1f;
                                if (Math.Abs(this.npc.velocity.X) + Math.Abs(this.npc.velocity.Y) < num188 * 0.5)
                                {
                                    if (this.npc.velocity.X > 0f)
                                        this.npc.velocity.X = this.npc.velocity.X + num189;
                                    else
                                        this.npc.velocity.X = this.npc.velocity.X - num189;
                                }
                            }
                        }
                    }
                }

                this.npc.rotation = (float)Math.Atan2(this.npc.velocity.Y, this.npc.velocity.X) + 1.57f;
                if (head)
                {
                    if (flag18)
                    {
                        if (this.npc.localAI[0] != 1f) this.npc.netUpdate = true;
                        this.npc.localAI[0] = 1f;
                    }
                    else
                    {
                        if (this.npc.localAI[0] != 0f) this.npc.netUpdate = true;
                        this.npc.localAI[0] = 0f;
                    }

                    if ((this.npc.velocity.X > 0f && this.npc.oldVelocity.X < 0f ||
                         this.npc.velocity.X < 0f && this.npc.oldVelocity.X > 0f ||
                         this.npc.velocity.Y > 0f && this.npc.oldVelocity.Y < 0f ||
                         this.npc.velocity.Y < 0f && this.npc.oldVelocity.Y > 0f) && !this.npc.justHit)
                    {
                        this.npc.netUpdate = true;
                        return;
                    }
                }
            }

            CustomBehavior();
        }

        public virtual void Init()
        {
        }

        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return head ? (bool?)null : false;
        }
    }
}