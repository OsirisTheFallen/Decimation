using System.IO;
using System.Linq;
using Decimation.Buffs.Debuffs;
using Decimation.Core.NPCs;
using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.NPCs.AncientDuneWorm
{
    [AutoloadBossHead]
    internal class AncientDuneWormHead : AncientDuneWorm
    {
        private int _attackCounter;
        private int _previousTile = -1;
        private bool _spawnedAncientTombCrawler;

        public override void SetDefaults()
        {
            this.npc.lifeMax = 11000;
            this.npc.damage = 65;
            this.npc.defense = 15;
            this.npc.knockBackResist = 0f;
            this.npc.width = 160;
            this.npc.height = 154;
            this.npc.boss = true;
            this.npc.lavaImmune = true;
            this.npc.noGravity = true;
            this.npc.noTileCollide = true;
            this.npc.behindTiles = true;
            this.npc.DeathSound = SoundID.NPCDeath18;
            this.npc.HitSound = SoundID.NPCHit1;
            Main.npcFrameCount[this.npc.type] = 1;
            this.npc.value = Item.buyPrice(0, 2);
            this.npc.npcSlots = 1f;
            this.npc.netAlways = true;
            this.npc.aiStyle = -1;
            music = this.mod.GetSoundSlot(SoundType.Music, "Sounds/Music/The_Deserts_Call");
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(5) || !Main.expertMode && Main.rand.NextBool(2))
                target.AddBuff(ModContent.BuffType<Hyperthermic>(), Main.expertMode ? 600 : 300);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(5) || !Main.expertMode && Main.rand.NextBool(2))
                target.AddBuff(ModContent.BuffType<Hyperthermic>(), Main.expertMode ? 600 : 300);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Ancient Dune Worm";
            DecimationWorld.downedDuneWorm = true;

            potionType = ItemID.HealingPotion;
            base.BossLoot(ref name, ref potionType);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(_attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _attackCounter = reader.ReadInt32();
        }

        public override void CustomBehavior()
        {
            if (this.npc.life < this.npc.lifeMax / 2f) ComputeSpeed();
            if (Main.expertMode) SummonSandnado();
            if (Main.tile[(int) this.npc.Center.X / 16, (int) this.npc.Center.Y / 16].type != 0 && _previousTile == 0)
                ShootAmmonite();

            if (this.npc.life <= this.npc.lifeMax * 0.15f &&
                !_spawnedAncientTombCrawler && !NPC.AnyNPCs(ModContent.NPCType<AncientTombCrawlerHead>()))
            {
                if (Main.netMode != 1)
                    NPC.SpawnOnPlayer(this.npc.target, ModContent.NPCType<AncientTombCrawlerHead>());
                _spawnedAncientTombCrawler = true;
            }

            _previousTile = Main.tile[(int) this.npc.Center.X / 16, (int) this.npc.Center.Y / 16].type;
        }

        public override bool ShouldRun()
        {
            bool playersActive = Main.player.Any(p => p.active);
            bool playersDead = Main.player.Any(p => p.dead);

            return !Main.player[this.npc.target].ZoneDesert || !playersActive || playersDead;
        }

        private void ComputeSpeed()
        {
            const float ratio = 0.4545454f;
            float deltaLife = this.npc.lifeMax / 2f - this.npc.life;
            float addedSpeed = deltaLife * ratio / 1000f;
            speed = BaseSpeed + addedSpeed;
        }

        private void SummonSandnado()
        {
            int tile = Main.tile[(int) this.npc.Center.X / 16, (int) this.npc.Center.Y / 16].type;

            if (tile == 0 &&
                _previousTile != 0 && Main.netMode != 1)
                Projectile.NewProjectile(this.npc.Center, new Vector2(0, 0), ProjectileID.SandnadoHostile, 15, 10f);
        }

        private void ShootAmmonite()
        {
            Main.PlaySound(SoundID.Item14, this.npc.Center);

            // Smoke
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(this.npc.position.X, this.npc.position.Y), this.npc.width,
                    this.npc.height, 31, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            float x = this.npc.position.X + this.npc.width / 2f - 24f;
            float y = this.npc.position.Y + this.npc.height / 2f - 24f;

            for (int g = 0; g < 2; g++)
            {
                int goreIndex =
                    Gore.NewGore(
                        new Vector2(x, y), default, Main.rand.Next(61, 64));
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X += 1.5f;
                Main.gore[goreIndex].velocity.Y += 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(x, y), default, Main.rand.Next(61, 64));
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X -= 1.5f;
                Main.gore[goreIndex].velocity.Y += 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(x, y), default, Main.rand.Next(61, 64));
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X += 1.5f;
                Main.gore[goreIndex].velocity.Y -= 1.5f;
                goreIndex = Gore.NewGore(
                    new Vector2(x, y), default, Main.rand.Next(61, 64));
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X -= 1.5f;
                Main.gore[goreIndex].velocity.Y -= 1.5f;
            }

            // Ammonite
            int ammoniteNbr = Main.rand.Next(5, 9);

            if (Main.netMode != 1)
                for (int i = 0; i < ammoniteNbr; i++)
                    Projectile.NewProjectile(this.npc.Center,
                        new Vector2(Main.rand.Next(-8, 9), Main.rand.Next(8, 15)),
                        ModContent.ProjectileType<Ammonite>(), 15, 5f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.9f; //this make the NPC Health Bar bigger
            return null;
        }
    }

    internal class AncientDuneWormBody : AncientDuneWorm
    {
        public override void SetDefaults()
        {
            this.npc.width = 92;
            this.npc.height = 92;
            this.npc.damage = 45;
            this.npc.defense = 5;
            this.npc.lifeMax = 1;
            this.npc.knockBackResist = 0.0f;
            this.npc.behindTiles = true;
            this.npc.noTileCollide = true;
            this.npc.netAlways = true;
            this.npc.noGravity = true;
            this.npc.dontCountMe = true;
            this.npc.DeathSound = SoundID.NPCDeath18;
            this.npc.HitSound = SoundID.NPCHit1;
        }
    }

    internal class AncientDuneWormTail : AncientDuneWorm
    {
        public override void SetDefaults()
        {
            this.npc.width = 136;
            this.npc.height = 128;
            this.npc.damage = 45;
            this.npc.defense = 14;
            this.npc.lifeMax = 1;
            this.npc.knockBackResist = 0.0f;
            this.npc.behindTiles = true;
            this.npc.noTileCollide = true;
            this.npc.netAlways = true;
            this.npc.noGravity = true;
            this.npc.dontCountMe = true;
            this.npc.DeathSound = SoundID.NPCDeath18;
            this.npc.HitSound = SoundID.NPCHit1;
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
    }

    public abstract class AncientDuneWorm : Worm
    {
        protected const float BaseSpeed = 10f;

        public override void SetStaticDefaults()
        {
            this.DisplayName.SetDefault("Ancient Dune Worm");
        }

        public override void Init()
        {
            minLength = 16;
            maxLength = 16;
            tailType = ModContent.NPCType<AncientDuneWormTail>();
            bodyType = ModContent.NPCType<AncientDuneWormBody>();
            headType = ModContent.NPCType<AncientDuneWormHead>();
            speed = BaseSpeed;
            turnSpeed = 0.045f;
        }
    }
}