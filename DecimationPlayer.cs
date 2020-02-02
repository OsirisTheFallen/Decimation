using System;
using Decimation.Buffs.Buffs;
using Decimation.Core;
using Decimation.Core.Amulets;
using Decimation.Core.Collections;
using Decimation.Core.Util;
using Decimation.Items.Amulets;
using Decimation.Items.Misc;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Decimation
{
    public class DecimationPlayer : DecimationModPlayer
    {
        // Amulet slot
        private Amulet _amuletSlotAmulet;
        private Item _amuletSlotItem;

        // amulets
        public int amuletsBuff;
        public byte amuletsBuffChances;
        public int amuletsBuffTime;
        public bool amuletsBuffWhenAttacking;
        public bool closeToEnchantedAnvil;
        public uint combatTime;
        public uint counter;

        public int dash;
        public int dashDamages;
        public int dashDelay;
        public int dashTime;
        public bool deadeyesQuiverEquipped;
        public uint enchantedHeartDropTime;
        public bool endlessPouchofLifeEquipped;
        public bool graniteLinedTunicEquipped;

        // Effects
        public bool hasCursedAccessory;
        public byte hyperStars;
        public byte soulFruits;

        public bool isInCombat;
        public bool jestersQuiverEquiped;
        public byte lastHitCounter;
        public float lastJumpBoost;
        public bool necrosisStoneEquipped;
        public int oldStatDefense;
        public byte scarabCounter;

        // Scarab Endurance buff
        public byte scarabEnduranceBuffTimeCounter;
        public int[] scarabs = new int[3];

        // Scarab shield
        public int solarCounter = 0;
        public bool tideTurnerEquipped;
        public int ttDash;
        public int ttHit;
        public bool vampire;
        public bool wasHurt;

        // Slimy Feet buff
        public bool wasJumping = false;

        public override bool HasShield { get; set; }
        public override bool HasLavaCharm { get; set; }

        public Item AmuletSlotItem
        {
            get => _amuletSlotItem;
            set
            {
                _amuletSlotAmulet = AmuletList.Instance.GetAmuletForItem(value);
                _amuletSlotItem = value;
            }
        }

        public override void Initialize()
        {
            this.AmuletSlotItem = new Item();
            this.AmuletSlotItem.SetDefaults(0, true);
        }

        public override void ResetEffects()
        {
            closeToEnchantedAnvil = false;
            jestersQuiverEquiped = false;
            deadeyesQuiverEquipped = false;
            endlessPouchofLifeEquipped = false;
            graniteLinedTunicEquipped = false;
            necrosisStoneEquipped = false;
            tideTurnerEquipped = false;
            vampire = false;

            this.HasLavaCharm = false;
            this.HasShield = false;

            hasCursedAccessory = false;

            this.player.statManaMax2 += hyperStars * HyperStar.ManaHealAmount;
            this.player.statLifeMax2 += soulFruits * SoulFruit.LifeHealAmount;

            if (combatTime > 360)
            {
                combatTime = 0;
                enchantedHeartDropTime = 0;
                isInCombat = false;
            }

            amuletsBuff = 0;
            amuletsBuffChances = 0;
            amuletsBuffTime = 0;
            amuletsBuffWhenAttacking = false;

            if (!this.player.HasBuff(ModContent.BuffType<SlimyFeet>())) lastJumpBoost = 0;
            if (!this.player.HasBuff(ModContent.BuffType<ScarabEndurance>()))
            {
                scarabEnduranceBuffTimeCounter = 0;
                scarabCounter = 0;
            }

            dash = 0;
            dashDamages = 0;

            if (counter > uint.MaxValue - uint.MaxValue % 60)
                counter = 0;
        }

        public override TagCompound Save()
        {
            Decimation.amuletSlotState.slot.item = new Item();

            return new TagCompound
            {
                {"amuletSlotItem", ItemIO.Save(this.AmuletSlotItem)},
                {"hyperStars", this.hyperStars},
                {"soulFruits", soulFruits }
            };
        }

        public override void Load(TagCompound tag)
        {
            this.AmuletSlotItem = ItemIO.Load(tag.GetCompound("amuletSlotItem"));
            this.hyperStars = tag.GetByte("hyperStars");
            this.soulFruits = tag.GetByte("soulFruits");
        }

        // FIND AN ALTERNATIVE! THIS METHOD DOESN'T GET CALLED WITH EVERY WEAPONS
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type,
            ref int damage, ref float knockBack)
        {
            Projectile toCheck = Main.projectile[type];

            // Jester's Quiver
            if (jestersQuiverEquiped && toCheck.arrow)
                type = ProjectileID.JestersArrow;

            // Endless Pouch of Life
            if (endlessPouchofLifeEquipped && References.bullets.Contains(type))
                type = ProjectileID.ChlorophyteBullet;

            // Deadeye's Quiver
            if (deadeyesQuiverEquipped && (toCheck.arrow || References.bullets.Contains(type)))
            {
                if (toCheck.arrow)
                    type = ProjectileID.IchorArrow;
                else
                    type = ProjectileID.ChlorophyteBullet;

                speedX *= 1.15f;
                speedY *= 1.15f;
            }

            // Frost Amulet
            if (this.AmuletSlotItem.type == ModContent.ItemType<FrostAmulet>() && toCheck.arrow)
            {
                speedX *= 1.03f;
                speedY *= 1.03f;
            }

            _amuletSlotAmulet?.Synergy.OnShoot(this, item, ref position, ref speedX, ref speedY, ref type, ref damage,
                ref knockBack);

            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (deadeyesQuiverEquipped && (ammo.ammo == AmmoID.Arrow || ammo.ammo == AmmoID.Bullet) &&
                Main.rand.Next(20) > 3)
                return false;
            if (endlessPouchofLifeEquipped && ammo.ammo == AmmoID.Bullet)
                return false;
            if (this.AmuletSlotItem.type == ModContent.ItemType<FrostAmulet>() && ammo.ammo == AmmoID.Arrow &&
                Main.rand.NextBool(50))
                return false;
            if (this.AmuletSlotItem.type == ModContent.ItemType<MarbleAmulet>() && weapon.thrown &&
                Main.rand.NextBool(50) && weapon.thrown)
                return false;

            return base.ConsumeAmmo(weapon, ammo);
        }

        public override void UpdateVanityAccessories()
        {
            Decimation.amuletSlotState.UpdateAmulet(this);
            _amuletSlotAmulet?.Synergy.Update(this);

            base.UpdateVanityAccessories();
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            DashMovement();

            base.UpdateEquips(ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
        }

        public override void FrameEffects()
        {
            if (vampire)
            {
                this.player.head = 124;
                this.player.body = 85;
                this.player.legs = 72;
            }
        }

        public override void PostUpdate()
        {
            oldStatDefense = this.player.statDefense;

            if (lastHitCounter >= 60)
            {
                lastHitCounter = 0;
                wasHurt = false;
            }

            if (wasHurt)
                lastHitCounter++;

            if (isInCombat)
            {
                combatTime++;
                enchantedHeartDropTime++;
            }

            counter++;

            base.PostUpdate();
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (target.HasBuff(ModContent.BuffType<ScarabEndurance>())) this.player.AddBuff(BuffID.OnFire, 300);

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking &&
                this.AmuletSlotItem.type != ModContent.ItemType<MarbleAmulet>())
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);

            if (this.AmuletSlotItem.type == ModContent.ItemType<CrystalAmulet>())
                CrystalAmuletEffect();
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            isInCombat = true;
            combatTime = 0;

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking &&
                this.AmuletSlotItem.type != ModContent.ItemType<MarbleAmulet>())
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking &&
                (this.AmuletSlotItem.type != ModContent.ItemType<MarbleAmulet>() ||
                 this.AmuletSlotItem.type == ModContent.ItemType<MarbleAmulet>() && proj.thrown))
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            isInCombat = true;
            combatTime = 0;

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking &&
                (this.AmuletSlotItem.type != ModContent.ItemType<MarbleAmulet>() ||
                 this.AmuletSlotItem.type == ModContent.ItemType<MarbleAmulet>() && proj.thrown))
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (this.player.HasBuff(ModContent.BuffType<ScarabEndurance>()) && scarabCounter > 0 &&
                lastHitCounter == 0 &&
                !wasHurt)
            {
                Main.projectile[scarabs[scarabCounter - 1]].Kill();
                scarabCounter--;
                wasHurt = true;
            }

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && !amuletsBuffWhenAttacking)
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    npc.AddBuff(amuletsBuff, amuletsBuffTime);

            if (graniteLinedTunicEquipped)
            {
                this.player.statLife += (int) (damage * 0.04f);

                if (Main.rand.Next(3, 100) < 3)
                    npc.AddBuff(BuffID.Confused, 600);
            }

            if (tideTurnerEquipped && Main.rand.NextBool(2)) this.player.statLife += damage;

            foreach (Player otherPlayer in Main.player)
                if (otherPlayer.whoAmI != this.player.whoAmI)
                    if (otherPlayer.GetModPlayer<DecimationPlayer>().AmuletSlotItem.type ==
                        ModContent.ItemType<GraniteAmulet>() && otherPlayer.team == this.player.team)
                    {
                        this.player.statLife += (int) (damage * 0.03f);
                        break;
                    }

            if (this.AmuletSlotItem.type == ModContent.ItemType<CrystalAmulet>() && Main.rand.NextBool(25))
                CrystalAmuletEffect();
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (this.player.HasBuff(ModContent.BuffType<ScarabEndurance>()) && scarabCounter > 0 &&
                lastHitCounter == 0 &&
                !wasHurt)
            {
                Main.projectile[scarabs[scarabCounter - 1]].Kill();
                scarabCounter--;
                wasHurt = true;
            }

            if (amuletsBuff != 0 && amuletsBuffTime != 0 && amuletsBuffChances != 0 && !amuletsBuffWhenAttacking)
            {
                if (proj.npcProj && Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    Main.npc[proj.owner].AddBuff(amuletsBuff, amuletsBuffTime);
                else if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    Main.player[proj.owner].AddBuff(amuletsBuff, amuletsBuffTime);
            }

            if (graniteLinedTunicEquipped)
            {
                this.player.statLife += (int) (damage * 0.04f);

                if (proj.npcProj && Main.rand.Next(3, 100) < 3)
                    Main.npc[proj.owner].AddBuff(BuffID.Confused, 600);
                else if (Main.rand.Next(3, 100) < 3)
                    Main.player[proj.owner].AddBuff(BuffID.Confused, 600);
            }

            foreach (Player otherPlayer in Main.player)
                if (otherPlayer.whoAmI != this.player.whoAmI)
                    if (otherPlayer.GetModPlayer<DecimationPlayer>().AmuletSlotItem.type ==
                        ModContent.ItemType<GraniteAmulet>() && otherPlayer.team == this.player.team)
                    {
                        this.player.statLife += (int) (damage * 0.03f);
                        break;
                    }

            if (this.AmuletSlotItem.type == ModContent.ItemType<CrystalAmulet>() && Main.rand.NextBool(25))
                CrystalAmuletEffect();
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            _amuletSlotAmulet?.Synergy.OnHitPlayer(this, ref damage);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            _amuletSlotAmulet?.Synergy.OnHitPlayer(this, ref damage);
        }

        public void DashMovement()
        {
            if (dash == 2 && ttDash > 0)
            {
                if (ttHit < 0)
                {
                    Rectangle rectangle =
                        new Rectangle((int) (this.player.position.X + this.player.velocity.X * 0.5 - 4.0),
                            (int) (this.player.position.Y + this.player.velocity.Y * 0.5 - 4.0), this.player.width + 8,
                            this.player.height + 8);
                    for (int i = 0; i < 200; i++)
                        if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly)
                        {
                            NPC nPC = Main.npc[i];
                            Rectangle rect = nPC.getRect();
                            if (rectangle.Intersects(rect) && (nPC.noTileCollide || this.player.CanHit(nPC)))
                            {
                                float num = dashDamages * this.player.meleeDamage;
                                float num2 = 9f;
                                bool crit = false;
                                if (this.player.kbGlove) num2 *= 2f;
                                if (this.player.kbBuff) num2 *= 1.5f;
                                if (Main.rand.Next(100) < this.player.meleeCrit) crit = true;
                                int num3 = this.player.direction;
                                if (this.player.velocity.X < 0f) num3 = -1;
                                if (this.player.velocity.X > 0f) num3 = 1;
                                if (this.player.whoAmI == Main.myPlayer)
                                    this.player.ApplyDamageToNPC(nPC, (int) num, num2, num3, crit);
                                ttDash = 10;
                                dashDelay = 30;
                                this.player.velocity.X = (0f - num3) * 9f;
                                this.player.velocity.Y = -4f;
                                this.player.immune = true;
                                this.player.immuneNoBlink = true;
                                this.player.immuneTime = 4;
                                ttHit = i;
                            }
                        }
                }
                else if ((!this.player.controlLeft || this.player.velocity.X >= 0f) &&
                         (!this.player.controlRight || this.player.velocity.X <= 0f))
                {
                    this.player.velocity.X = this.player.velocity.X * 0.95f;
                }
            }

            if (dash == 3 && dashDelay < 0 && this.player.whoAmI == Main.myPlayer)
            {
                Rectangle rectangle2 =
                    new Rectangle((int) (this.player.position.X + this.player.velocity.X * 0.5 - 4.0),
                        (int) (this.player.position.Y + this.player.velocity.Y * 0.5 - 4.0), this.player.width + 8,
                        this.player.height + 8);
                for (int j = 0; j < 200; j++)
                    if (Main.npc[j].active && !Main.npc[j].dontTakeDamage && !Main.npc[j].friendly &&
                        Main.npc[j].immune[this.player.whoAmI] <= 0)
                    {
                        NPC nPC2 = Main.npc[j];
                        Rectangle rect2 = nPC2.getRect();
                        if (rectangle2.Intersects(rect2) && (nPC2.noTileCollide || this.player.CanHit(nPC2)))
                        {
                            float num4 = 150f * this.player.meleeDamage;
                            float num5 = 9f;
                            bool crit2 = false;
                            if (this.player.kbGlove) num5 *= 2f;
                            if (this.player.kbBuff) num5 *= 1.5f;
                            if (Main.rand.Next(100) < this.player.meleeCrit) crit2 = true;
                            int direction = this.player.direction;
                            if (this.player.velocity.X < 0f) direction = -1;
                            if (this.player.velocity.X > 0f) direction = 1;
                            if (this.player.whoAmI == Main.myPlayer)
                            {
                                this.player.ApplyDamageToNPC(nPC2, (int) num4, num5, direction, crit2);
                                int num6 = Projectile.NewProjectile(this.player.Center.X, this.player.Center.Y, 0f, 0f,
                                    608, 150, 15f, Main.myPlayer);
                                Main.projectile[num6].Kill();
                            }

                            nPC2.immune[this.player.whoAmI] = 6;
                            this.player.immune = true;
                            this.player.immuneNoBlink = true;
                            this.player.immuneTime = 4;
                        }
                    }
            }

            if (dashDelay > 0)
            {
                if (ttDash > 0) ttDash--;
                if (ttDash == 0) ttHit = -1;
                dashDelay--;
            }
            else if (dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.992f;
                float num9 = Math.Max(this.player.accRunSpeed, this.player.maxRunSpeed);
                float num10 = 0.96f;
                int num11 = 20;
                if (dash == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int num12 = this.player.velocity.Y != 0f
                            ? Dust.NewDust(
                                new Vector2(this.player.position.X,
                                    this.player.position.Y + this.player.height / 2 - 8f), this.player.width, 16, 31,
                                0f, 0f, 100, default, 1.4f)
                            : Dust.NewDust(
                                new Vector2(this.player.position.X, this.player.position.Y + this.player.height - 4f),
                                this.player.width, 8, 31, 0f, 0f, 100, default, 1.4f);
                        Dust obj = Main.dust[num12];
                        obj.velocity *= 0.1f;
                        Main.dust[num12].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(this.player.cShoe, this.player);
                    }
                }
                else if (dash == 2)
                {
                    for (int l = 0; l < 0; l++)
                    {
                        int num13 = this.player.velocity.Y != 0f
                            ? Dust.NewDust(
                                new Vector2(this.player.position.X,
                                    this.player.position.Y + this.player.height / 2 - 8f), this.player.width, 16, 31,
                                0f, 0f, 100, default, 1.4f)
                            : Dust.NewDust(
                                new Vector2(this.player.position.X, this.player.position.Y + this.player.height - 4f),
                                this.player.width, 8, 31, 0f, 0f, 100, default, 1.4f);
                        Dust obj2 = Main.dust[num13];
                        obj2.velocity *= 0.1f;
                        Main.dust[num13].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num13].shader = GameShaders.Armor.GetSecondaryShader(this.player.cShoe, this.player);
                    }

                    num8 = 0.985f;
                    num10 = 0.94f;
                    num11 = 30;
                }
                else if (dash == 3)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        int num14 = Dust.NewDust(new Vector2(this.player.position.X, this.player.position.Y + 4f),
                            this.player.width, this.player.height - 8, 6, 0f, 0f, 100, default, 1.7f);
                        Dust obj3 = Main.dust[num14];
                        obj3.velocity *= 0.1f;
                        Main.dust[num14].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num14].shader =
                            GameShaders.Armor.GetSecondaryShader(this.player.ArmorSetDye(), this.player);
                        Main.dust[num14].noGravity = true;
                        if (Main.rand.Next(2) == 0) Main.dust[num14].fadeIn = 0.5f;
                    }

                    num7 = 14f;
                    num8 = 0.985f;
                    num10 = 0.94f;
                    num11 = 20;
                }
                else if (dash == 4)
                {
                    for (int n = 0; n < 2; n++)
                    {
                        int num15 = Dust.NewDust(new Vector2(this.player.position.X, this.player.position.Y + 4f),
                            this.player.width, this.player.height - 8, 229, 0f, 0f, 100, default, 1.2f);
                        Dust obj4 = Main.dust[num15];
                        obj4.velocity *= 0.1f;
                        Main.dust[num15].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num15].shader = GameShaders.Armor.GetSecondaryShader(this.player.cWings, this.player);
                        Main.dust[num15].noGravity = true;
                        if (Main.rand.Next(2) == 0) Main.dust[num15].fadeIn = 0.3f;
                    }

                    num8 = 0.985f;
                    num10 = 0.94f;
                    num11 = 20;
                }

                if (dash > 0)
                {
                    this.player.vortexStealthActive = false;
                    if (this.player.velocity.X > num7 || this.player.velocity.X < 0f - num7)
                    {
                        this.player.velocity.X = this.player.velocity.X * num8;
                    }
                    else if (this.player.velocity.X > num9 || this.player.velocity.X < 0f - num9)
                    {
                        this.player.velocity.X = this.player.velocity.X * num10;
                    }
                    else
                    {
                        dashDelay = num11;
                        if (this.player.velocity.X < 0f)
                            this.player.velocity.X = 0f - num9;
                        else if (this.player.velocity.X > 0f) this.player.velocity.X = num9;
                    }
                }
            }
            else if (dash > 0 && !this.player.mount.Active)
            {
                if (dash == 1)
                {
                    int num16 = 0;
                    bool flag = false;
                    if (dashTime > 0) dashTime--;
                    if (dashTime < 0) dashTime++;
                    if (this.player.controlRight && this.player.releaseRight)
                    {
                        if (dashTime > 0)
                        {
                            num16 = 1;
                            flag = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = 15;
                        }
                    }
                    else if (this.player.controlLeft && this.player.releaseLeft)
                    {
                        if (dashTime < 0)
                        {
                            num16 = -1;
                            flag = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = -15;
                        }
                    }

                    if (flag)
                    {
                        this.player.velocity.X = 16.9f * num16;
                        Point point = (this.player.Center + new Vector2(num16 * this.player.width / 2 + 2,
                                           this.player.gravDir * (0f - this.player.height) / 2f +
                                           this.player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (this.player.Center + new Vector2(num16 * this.player.width / 2 + 2, 0f))
                            .ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) ||
                            WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                            this.player.velocity.X = this.player.velocity.X / 2f;
                        dashDelay = -1;
                        for (int num17 = 0; num17 < 20; num17++)
                        {
                            int num18 = Dust.NewDust(new Vector2(this.player.position.X, this.player.position.Y),
                                this.player.width, this.player.height, 31, 0f, 0f, 100, default, 2f);
                            Dust dust = Main.dust[num18];
                            dust.position.X = dust.position.X + Main.rand.Next(-5, 6);
                            Dust dust2 = Main.dust[num18];
                            dust2.position.Y = dust2.position.Y + Main.rand.Next(-5, 6);
                            Dust obj5 = Main.dust[num18];
                            obj5.velocity *= 0.2f;
                            Main.dust[num18].scale *= 1f + Main.rand.Next(20) * 0.01f;
                            Main.dust[num18].shader =
                                GameShaders.Armor.GetSecondaryShader(this.player.cShoe, this.player);
                        }

                        int num19 = Gore.NewGore(
                            new Vector2(this.player.position.X + this.player.width / 2 - 24f,
                                this.player.position.Y + this.player.height / 2 - 34f), default,
                            Main.rand.Next(61, 64));
                        Main.gore[num19].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num19].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Gore obj6 = Main.gore[num19];
                        obj6.velocity *= 0.4f;
                        num19 = Gore.NewGore(
                            new Vector2(this.player.position.X + this.player.width / 2 - 24f,
                                this.player.position.Y + this.player.height / 2 - 14f), default,
                            Main.rand.Next(61, 64));
                        Main.gore[num19].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num19].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                        Gore obj7 = Main.gore[num19];
                        obj7.velocity *= 0.4f;
                    }
                }
                else if (dash == 2)
                {
                    int num20 = 0;
                    bool flag2 = false;
                    if (dashTime > 0) dashTime--;
                    if (dashTime < 0) dashTime++;
                    if (this.player.controlRight && this.player.releaseRight)
                    {
                        if (dashTime > 0)
                        {
                            num20 = 1;
                            flag2 = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = 15;
                        }
                    }
                    else if (this.player.controlLeft && this.player.releaseLeft)
                    {
                        if (dashTime < 0)
                        {
                            num20 = -1;
                            flag2 = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = -15;
                        }
                    }

                    if (flag2)
                    {
                        this.player.velocity.X = 14.5f * num20;
                        Point point3 = (this.player.Center + new Vector2(num20 * this.player.width / 2 + 2,
                                            this.player.gravDir * (0f - this.player.height) / 2f +
                                            this.player.gravDir * 2f)).ToTileCoordinates();
                        Point point4 = (this.player.Center + new Vector2(num20 * this.player.width / 2 + 2, 0f))
                            .ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point3.X, point3.Y) ||
                            WorldGen.SolidOrSlopedTile(point4.X, point4.Y))
                            this.player.velocity.X = this.player.velocity.X / 2f;
                        dashDelay = -1;
                        ttDash = 15;
                        for (int num21 = 0; num21 < 0; num21++)
                        {
                            int num22 = Dust.NewDust(new Vector2(this.player.position.X, this.player.position.Y),
                                this.player.width, this.player.height, 31, 0f, 0f, 100, default, 2f);
                            Dust dust3 = Main.dust[num22];
                            dust3.position.X = dust3.position.X + Main.rand.Next(-5, 6);
                            Dust dust4 = Main.dust[num22];
                            dust4.position.Y = dust4.position.Y + Main.rand.Next(-5, 6);
                            Dust obj8 = Main.dust[num22];
                            obj8.velocity *= 0.2f;
                            Main.dust[num22].scale *= 1f + Main.rand.Next(20) * 0.01f;
                            Main.dust[num22].shader =
                                GameShaders.Armor.GetSecondaryShader(this.player.cShield, this.player);
                        }
                    }
                }
                else if (dash == 3)
                {
                    int num23 = 0;
                    bool flag3 = false;
                    if (dashTime > 0) dashTime--;
                    if (dashTime < 0) dashTime++;
                    if (this.player.controlRight && this.player.releaseRight)
                    {
                        if (dashTime > 0)
                        {
                            num23 = 1;
                            flag3 = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = 15;
                        }
                    }
                    else if (this.player.controlLeft && this.player.releaseLeft)
                    {
                        if (dashTime < 0)
                        {
                            num23 = -1;
                            flag3 = true;
                            dashTime = 0;
                        }
                        else
                        {
                            dashTime = -15;
                        }
                    }

                    if (flag3)
                    {
                        this.player.velocity.X = 21.9f * num23;
                        Point point5 = (this.player.Center + new Vector2(num23 * this.player.width / 2 + 2,
                                            this.player.gravDir * (0f - this.player.height) / 2f +
                                            this.player.gravDir * 2f)).ToTileCoordinates();
                        Point point6 = (this.player.Center + new Vector2(num23 * this.player.width / 2 + 2, 0f))
                            .ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) ||
                            WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
                            this.player.velocity.X = this.player.velocity.X / 2f;
                        dashDelay = -1;
                        for (int num24 = 0; num24 < 20; num24++)
                        {
                            int num25 = Dust.NewDust(new Vector2(this.player.position.X, this.player.position.Y),
                                this.player.width, this.player.height, 6, 0f, 0f, 100, default, 2f);
                            Dust dust5 = Main.dust[num25];
                            dust5.position.X = dust5.position.X + Main.rand.Next(-5, 6);
                            Dust dust6 = Main.dust[num25];
                            dust6.position.Y = dust6.position.Y + Main.rand.Next(-5, 6);
                            Dust obj9 = Main.dust[num25];
                            obj9.velocity *= 0.2f;
                            Main.dust[num25].scale *= 1f + Main.rand.Next(20) * 0.01f;
                            Main.dust[num25].shader =
                                GameShaders.Armor.GetSecondaryShader(this.player.ArmorSetDye(), this.player);
                            Main.dust[num25].noGravity = true;
                            Main.dust[num25].fadeIn = 0.5f;
                        }
                    }
                }
            }
        }

        private void CrystalAmuletEffect()
        {
            int shardNumber = Main.rand.Next(5, 11);
            float angleDifference = (float) (Math.PI * 2) / shardNumber;
            float speed = 5f;
            float currentAngle = 0;

            for (int i = 0; i < shardNumber; i++)
            {
                float speedX = (float) Math.Cos(currentAngle) * speed;
                float speedY = (float) Math.Sin(currentAngle) * speed;

                Projectile.NewProjectile(this.player.Center, new Vector2(speedX, speedY), ProjectileID.CrystalShard, 20,
                    5, this.player.whoAmI);

                currentAngle += angleDifference;
            }
        }
    }

    public class PlayerPropertiesUpdater : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            if (item.type == ItemID.CobaltShield || item.type == ItemID.AnkhShield ||
                item.type == ItemID.PaladinsShield || item.type == ItemID.ObsidianShield) modPlayer.HasShield = true;
            if (item.type == ItemID.LavaCharm) modPlayer.HasLavaCharm = true;
        }
    }
}