using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace Decimation.NPCs
{
    // Check  line 43861 of NPC.cs
    class CoreSpider : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core Spider");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BlackRecluse);
            npc.width = 84;
            npc.height = 24;
            npc.lifeMax = 750;
            animationType = NPCID.BlackRecluse;
        }

        int frame = 0;
        int shootFrame = 120;

        public override void AI()
        {
            int x = (int)npc.Center.X / 16;
            int y = (int)npc.Center.Y / 16;
            bool onWall = false;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (Main.tile[i, j].wall > 0)
                    {
                        onWall = true;
                    }
                }
            }

            if (Main.expertMode)
            {
                if (frame >= shootFrame)
                {
                    if (Main.rand.Next(4) == 0)
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

            if (onWall)
            {
                npc.Transform(mod.NPCType("CoreSpiderWall"));
            }
            else
            {
                base.AI();
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = (int)Main.LocalPlayer.position.X / 16;
            int y = (int)Main.LocalPlayer.position.Y / 16;

            int validBlockCount = 0;
            for (int i = -50 + x; i <= 50 + x; i++)
            {
                for (int j = -50 + y; j <= 50 + y; j++)
                {
                    if (i >= 0 && i <= Main.maxTilesX && j >= 0 && j <= Main.maxTilesY)
                    {
                        if (Main.tile[i, j].type == mod.TileType("ShrineBrick") || (Main.tile[i, j].type == mod.TileType("LockedShrineDoor") || Main.tile[i, j].type == mod.TileType("ShrineDoorClosed") || Main.tile[i, j].type == mod.TileType("ShrineDoorOpened")) || Main.tile[i, j].type == mod.TileType("RedHotSpike"))
                        {
                            validBlockCount++;
                        }
                    }
                }
            }


            if (validBlockCount >= 15)
                return SpawnCondition.Underworld.Chance * 2;
            return 0;
        }
    }
}
