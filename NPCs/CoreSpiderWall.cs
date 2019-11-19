using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Decimation.Tiles.ShrineoftheMoltenOne;
using Microsoft.Xna.Framework;

namespace Decimation.NPCs
{
    // Check  line 43861 of NPC.cs
    class CoreSpiderWall : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core Spider");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BlackRecluseWall);
            npc.width = 60;
            npc.height = 62;
            npc.lifeMax = 750;
            animationType = NPCID.BlackRecluseWall;

            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Burning] = true;
        }

        int frame = 0;
        int shootFrame = 120;

        public override void AI()
        {
            int x = (int)npc.Center.X / 16;
            int y = (int)npc.Center.Y / 16;
            bool onWall = true;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (Main.tile[i, j].wall <= 0)
                    {
                        onWall = false;
                    }
                }
            }

            if (Main.expertMode)
            {
                if (frame >= shootFrame)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        float mouthX = (float)(((npc.height) / 2) * Math.Cos(npc.rotation - Math.PI * 0.5f)) + npc.Center.X;
                        float mouthY = (float)(((npc.height) / 2) * Math.Sin(npc.rotation - Math.PI * 0.5f)) + npc.Center.Y;
                        float speedX = (float)(3 * Math.Cos(npc.rotation - Math.PI)) * 2;
                        float speedY = (float)(3 * Math.Sin(npc.rotation - Math.PI)) * 2;

                        Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY), ProjectileID.Fireball, 130, 30);
                    }

                    frame = 0;
                }
                else
                {
                    frame++;
                }
            }

            if (!onWall)
            {
                npc.Transform(ModContent.NPCType<CoreSpider>());
            }
            else
            {
                base.AI();
            }
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            int x = (int)Main.LocalPlayer.position.X / 16;
            int y = (int)Main.LocalPlayer.position.Y / 16;

            int validBlockCount = 0;
            for (int i = (int)(-50 + x / 16f); i <= (int)(50 + x / 16f); i++)
            {
                for (int j = (int)(-50 + y / 16f); j <= (int)(50 + y / 16f); j++)
                {
                    if (i >= 0 && i <= Main.maxTilesX && j >= 0 && j <= Main.maxTilesY)
                    {
                        if (Main.tile[i, j].type == ModContent.TileType<ShrineBrick>() || (Main.tile[i, j].type == ModContent.TileType<LockedShrineDoor>() || Main.tile[i, j].type == ModContent.TileType<ShrineDoorClosed>() || Main.tile[i, j].type == ModContent.TileType<ShrineDoorOpened>()) || Main.tile[i, j].type == ModContent.TileType<RedHotSpike>())
                            validBlockCount++;
                    }
                }
            }

            return validBlockCount >= 15 && Main.hardMode;
        }
    }
}
