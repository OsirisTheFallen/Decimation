using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    public class AncientTombCrawlerBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("Ancient Tomb Crawler");
        }

        public override void SetDefaults()
        {
            this.npc.scale = 0.7f;
            this.npc.width = 62;
            this.npc.height = 62;
            this.npc.damage = 20;
            this.npc.defense = 1;
            this.npc.lifeMax = 1;
            this.npc.knockBackResist = 0.0f;
            this.npc.behindTiles = true;
            this.npc.noTileCollide = true;
            this.npc.netAlways = true;
            this.npc.noGravity = true;
            this.npc.dontCountMe = true;
            this.npc.HitSound = SoundID.NPCHit1;
        }

        public override bool PreAI()
        {
            if (this.npc.ai[3] > 0) this.npc.realLife = (int) this.npc.ai[3];
            if (this.npc.target < 0 || this.npc.target == byte.MaxValue || Main.player[this.npc.target].dead)
                this.npc.TargetClosest();
            if (Main.player[this.npc.target].dead && this.npc.timeLeft > 300) this.npc.timeLeft = 300;

            if (Main.netMode != 1)
                if (!Main.npc[(int) this.npc.ai[1]].active)
                {
                    this.npc.life = 0;
                    this.npc.HitEffect();
                    this.npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, this.npc.whoAmI, -1f);
                }

            if (this.npc.ai[1] < (double) Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(this.npc.position.X + this.npc.width * 0.5f,
                    this.npc.position.Y + this.npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int) this.npc.ai[1]].position.X + Main.npc[(int) this.npc.ai[1]].width / 2f -
                             npcCenter.X;
                float dirY = Main.npc[(int) this.npc.ai[1]].position.Y + Main.npc[(int) this.npc.ai[1]].height / 2f -
                             npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                this.npc.rotation = (float) Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float) Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - this.npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                this.npc.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                this.npc.position.X = this.npc.position.X + posX;
                this.npc.position.Y = this.npc.position.Y + posY;
            }

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
            return false; //this make that the npc does not have a health bar
        }
    }
}