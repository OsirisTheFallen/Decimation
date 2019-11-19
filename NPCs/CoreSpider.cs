using System;
using Decimation.Tiles.ShrineoftheMoltenOne;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs
{
    // Check  line 43861 of NPC.cs
    internal class CoreSpider : ModNPC
    {
        private int _frame;
        private readonly int shootFrame = 120;

        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("Core Spider");
            Main.npcFrameCount[this.npc.type] = 8;
        }

        public override void SetDefaults()
        {
            this.npc.CloneDefaults(NPCID.BlackRecluse);
            this.npc.width = 84;
            this.npc.height = 24;
            this.npc.lifeMax = 750;
            animationType = NPCID.BlackRecluse;

            this.npc.lavaImmune = true;
            this.npc.buffImmune[BuffID.OnFire] = true;
            this.npc.buffImmune[BuffID.Burning] = true;
        }

        public override void AI()
        {
            int x = (int) this.npc.Center.X / 16;
            int y = (int) this.npc.Center.Y / 16;
            bool onWall = false;
            for (int i = x - 1; i <= x + 1; i++)
            for (int j = y - 1; j <= y + 1; j++)
                if (Main.tile[i, j].wall > 0)
                    onWall = true;

            if (Main.expertMode)
            {
                if (_frame >= shootFrame)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        float mouthX = (float) (this.npc.height / 2f * Math.Cos(this.npc.rotation - Math.PI * 0.5f)) +
                                       this.npc.Center.X;
                        float mouthY = (float) (this.npc.height / 2f * Math.Sin(this.npc.rotation - Math.PI * 0.5f)) +
                                       this.npc.Center.Y;
                        float speedX = (float) (3 * Math.Cos(this.npc.rotation - Math.PI)) * 2;
                        float speedY = (float) (3 * Math.Sin(this.npc.rotation - Math.PI)) * 2;

                        Projectile.NewProjectile(new Vector2(mouthX, mouthY), new Vector2(speedX, speedY),
                            ProjectileID.Fireball, 130, 30);
                    }

                    _frame = 0;
                }
                else
                {
                    _frame++;
                }
            }

            if (onWall)
                this.npc.Transform(ModContent.NPCType<CoreSpiderWall>());
            else
                base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = (int) Main.LocalPlayer.position.X / 16;
            int y = (int) Main.LocalPlayer.position.Y / 16;

            int validBlockCount = 0;
            for (int i = -50 + x; i <= 50 + x; i++)
            for (int j = -50 + y; j <= 50 + y; j++)
                if (i >= 0 && i <= Main.maxTilesX && j >= 0 && j <= Main.maxTilesY)
                    if (Main.tile[i, j].type == ModContent.TileType<ShrineBrick>() ||
                        Main.tile[i, j].type == ModContent.TileType<LockedShrineDoor>() ||
                        Main.tile[i, j].type == ModContent.TileType<ShrineDoorClosed>() ||
                        Main.tile[i, j].type == ModContent.TileType<ShrineDoorOpened>() ||
                        Main.tile[i, j].type == ModContent.TileType<RedHotSpike>())
                        validBlockCount++;


            if (validBlockCount >= 15 && Main.hardMode)
                return SpawnCondition.Underworld.Chance * 2;
            return 0;
        }
    }
}