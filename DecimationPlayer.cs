using Decimation.Buffs.Buffs;
using Decimation.Items.Amulets;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Decimation
{
    public class DecimationPlayer : ModPlayer
    {
        public bool closeToEnchantedAnvil = false;
        public bool jestersQuiverEquiped = false;
        public bool deadeyesQuiverEquipped = false;
        public bool endlessPouchofLifeEquipped = false;
        public bool graniteLinedTunicEquipped = false;
        public bool necrosisStoneEquipped = false;
        public bool tideTurnerEquipped = false;
        public bool vampire = false;
        public bool hasShield = false;
		public bool hasLavaCharm = false;

        // Effects
        public bool hasCursedAccessory = false;

        public bool isInCombat = false;
        public uint combatTime = 0;
        public uint counter = 0;

        // Amulet slot
        public Item amuletSlotItem;

        // Slimy Feet buff
        public bool wasJumping = false;
        public float lastJumpBoost = 0;

        // Scarab Endurance buff
        public byte scarabEnduranceBuffTimeCounter = 0;
        public byte scarabCounter = 0;
        public int[] scarabs = new int[3];
        public int oldStatDefense = 0;
        public byte lastHitCounter = 0;
        public bool wasHurt = false;

        // Scarab shield
        public int solarCounter = 0;

        // amulets
        public int amuletsBuff = 0;
        public byte amuletsBuffChances = 0;
        public int amuletsBuffTime = 0;
        public bool amuletsBuffWhenAttacking = false;
        public uint enchantedHeartDropTime = 0;

        private AmuletsSynergy synergy;

        public override void Initialize()
        {
            amuletSlotItem = new Item();
            amuletSlotItem.SetDefaults(0, true);

            synergy = new AmuletsSynergy((Decimation) mod);
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
            hasShield = false;
			hasLavaCharm = false;

            hasCursedAccessory = false;

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

            if (!player.HasBuff(mod.BuffType<SlimyFeet>())) lastJumpBoost = 0;
            if (!player.HasBuff(mod.BuffType<ScarabEndurance>()))
            {
                scarabEnduranceBuffTimeCounter = 0;
                scarabCounter = 0;
            }

            dash = 0;
            dashDamages = 0;

            if (counter > uint.MaxValue - (uint.MaxValue % 60))
                counter = 0;
        }

        public override TagCompound Save()
        {
            Decimation.amuletSlotState.slot.item = new Item();

            return new TagCompound() {
                {"amuletSlotItem", ItemIO.Save(amuletSlotItem) }
            };
        }

        public override void Load(TagCompound tag)
        {
            amuletSlotItem = ItemIO.Load(tag.GetCompound("amuletSlotItem"));
        }

        // FIND AN ALTERNATIVE! THIS METHOD DOESN'T GET CALLED WITH ALL THE WEAPONS
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
            if (amuletSlotItem.type == mod.ItemType<FrostAmulet>() && toCheck.arrow)
            {
                speedX *= 1.03f;
                speedY *= 1.03f;
            }

            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (deadeyesQuiverEquipped && (ammo.ammo == AmmoID.Arrow || ammo.ammo == AmmoID.Bullet) && Main.rand.Next(20) > 3)
                return false;
            if (endlessPouchofLifeEquipped && ammo.ammo == AmmoID.Bullet)
                return false;
            if (amuletSlotItem.type == mod.ItemType<FrostAmulet>() && (ammo.ammo == AmmoID.Arrow) && Main.rand.NextBool(50))
                return false;
            if (amuletSlotItem.type == mod.ItemType<MarbleAmulet>() && weapon.thrown && Main.rand.NextBool(50) && weapon.thrown)
                return false;

            return base.ConsumeAmmo(weapon, ammo);
        }

        public override void UpdateVanityAccessories()
        {
            Decimation.amuletSlotState.UpdateAmulet(this);
			synergy.Update(amuletSlotItem, this);

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
                player.head = 124;
                player.body = 85;
                player.legs = 72;
            }
        }

        public override void PostUpdate()
        {
            oldStatDefense = player.statDefense;

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
            if (target.HasBuff(mod.BuffType<ScarabEndurance>())) player.AddBuff(BuffID.OnFire, 300);

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking && amuletSlotItem.type != mod.ItemType<MarbleAmulet>())
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);

            if (amuletSlotItem.type == mod.ItemType<CrystalAmulet>())
                CrystalAmuletEffect();
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            isInCombat = true;
            combatTime = 0;

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking && amuletSlotItem.type != mod.ItemType<MarbleAmulet>())
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
        {
            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking && (amuletSlotItem.type != mod.ItemType<MarbleAmulet>() || (amuletSlotItem.type == mod.ItemType<MarbleAmulet>() && proj.thrown)))
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            isInCombat = true;
            combatTime = 0;

            if (amuletsBuffTime != 0 && amuletsBuff != 0 && amuletsBuffChances != 0 && amuletsBuffWhenAttacking && (amuletSlotItem.type != mod.ItemType<MarbleAmulet>() || (amuletSlotItem.type == mod.ItemType<MarbleAmulet>() && proj.thrown)))
                if (Main.rand.Next(amuletsBuffChances, 100) < amuletsBuffChances)
                    target.AddBuff(amuletsBuff, amuletsBuffTime);
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (player.HasBuff(mod.BuffType<ScarabEndurance>()) && scarabCounter > 0 && lastHitCounter == 0 && !wasHurt)
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
                player.statLife += (int)(damage * 0.04f);

                if (Main.rand.Next(3, 100) < 3)
                    npc.AddBuff(BuffID.Confused, 600);
            }

            if (tideTurnerEquipped && Main.rand.NextBool(2))
                player.statLife += damage;

            foreach (Player otherPlayer in Main.player)
            {
                if (otherPlayer.whoAmI != player.whoAmI)
                    if (otherPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem.type == mod.ItemType<GraniteAmulet>() && otherPlayer.team == player.team)
                    {
                        player.statLife += (int)(damage * 0.03f);
                        break;
                    }
            }

            if (amuletSlotItem.type == mod.ItemType<CrystalAmulet>() && Main.rand.NextBool(25))
                CrystalAmuletEffect();
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (player.HasBuff(mod.BuffType<ScarabEndurance>()) && scarabCounter > 0 && lastHitCounter == 0 && !wasHurt)
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
                player.statLife += (int)(damage * 0.04f);

                if (proj.npcProj && Main.rand.Next(3, 100) < 3)
                    Main.npc[proj.owner].AddBuff(BuffID.Confused, 600);
                else if (Main.rand.Next(3, 100) < 3)
                    Main.player[proj.owner].AddBuff(BuffID.Confused, 600);
            }

            foreach (Player otherPlayer in Main.player)
            {
                if (otherPlayer.whoAmI != player.whoAmI)
                    if (otherPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem.type == mod.ItemType<GraniteAmulet>() && otherPlayer.team == player.team)
                    {
                        player.statLife += (int)(damage * 0.03f);
                        break;
                    }
            }

            if (amuletSlotItem.type == mod.ItemType<CrystalAmulet>() && Main.rand.NextBool(25))
                CrystalAmuletEffect();
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            synergy.OnHitPlayer(amuletSlotItem, this, ref damage);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            synergy.OnHitPlayer(amuletSlotItem, this, ref damage);
        }

        public int dash = 0;
        public int dashDamages = 0;
        public int dashTime = 0;
        public int dashDelay = 0;
        public int ttDash = 0;
        public int ttHit = 0;

        public void DashMovement()
        {
            if (dash == 2 && ttDash > 0)
            {
                if (ttHit < 0)
                {
                    Rectangle rectangle = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly)
                        {
                            NPC nPC = Main.npc[i];
                            Rectangle rect = nPC.getRect();
                            if (rectangle.Intersects(rect) && (nPC.noTileCollide || player.CanHit(nPC)))
                            {
                                float num = dashDamages * player.meleeDamage;
                                float num2 = 9f;
                                bool crit = false;
                                if (player.kbGlove)
                                {
                                    num2 *= 2f;
                                }
                                if (player.kbBuff)
                                {
                                    num2 *= 1.5f;
                                }
                                if (Main.rand.Next(100) < player.meleeCrit)
                                {
                                    crit = true;
                                }
                                int num3 = player.direction;
                                if (player.velocity.X < 0f)
                                {
                                    num3 = -1;
                                }
                                if (player.velocity.X > 0f)
                                {
                                    num3 = 1;
                                }
                                if (player.whoAmI == Main.myPlayer)
                                {
                                    player.ApplyDamageToNPC(nPC, (int)num, num2, num3, crit);
                                }
                                ttDash = 10;
                                dashDelay = 30;
                                player.velocity.X = (0f - (float)num3) * 9f;
                                player.velocity.Y = -4f;
                                player.immune = true;
                                player.immuneNoBlink = true;
                                player.immuneTime = 4;
                                ttHit = i;
                            }
                        }
                    }
                }
                else if ((!player.controlLeft || player.velocity.X >= 0f) && (!player.controlRight || player.velocity.X <= 0f))
                {
                    player.velocity.X = player.velocity.X * 0.95f;
                }
            }
            if (dash == 3 && dashDelay < 0 && player.whoAmI == Main.myPlayer)
            {
                Rectangle rectangle2 = new Rectangle((int)((double)player.position.X + (double)player.velocity.X * 0.5 - 4.0), (int)((double)player.position.Y + (double)player.velocity.Y * 0.5 - 4.0), player.width + 8, player.height + 8);
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].active && !Main.npc[j].dontTakeDamage && !Main.npc[j].friendly && Main.npc[j].immune[player.whoAmI] <= 0)
                    {
                        NPC nPC2 = Main.npc[j];
                        Rectangle rect2 = nPC2.getRect();
                        if (rectangle2.Intersects(rect2) && (nPC2.noTileCollide || player.CanHit(nPC2)))
                        {
                            float num4 = 150f * player.meleeDamage;
                            float num5 = 9f;
                            bool crit2 = false;
                            if (player.kbGlove)
                            {
                                num5 *= 2f;
                            }
                            if (player.kbBuff)
                            {
                                num5 *= 1.5f;
                            }
                            if (Main.rand.Next(100) < player.meleeCrit)
                            {
                                crit2 = true;
                            }
                            int direction = player.direction;
                            if (player.velocity.X < 0f)
                            {
                                direction = -1;
                            }
                            if (player.velocity.X > 0f)
                            {
                                direction = 1;
                            }
                            if (player.whoAmI == Main.myPlayer)
                            {
                                player.ApplyDamageToNPC(nPC2, (int)num4, num5, direction, crit2);
                                int num6 = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, 608, 150, 15f, Main.myPlayer, 0f, 0f);
                                Main.projectile[num6].Kill();
                            }
                            nPC2.immune[player.whoAmI] = 6;
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 4;
                        }
                    }
                }
            }
            if (dashDelay > 0)
            {
                if (ttDash > 0)
                {
                    ttDash--;
                }
                if (ttDash == 0)
                {
                    ttHit = -1;
                }
                dashDelay--;
            }
            else if (dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.992f;
                float num9 = Math.Max(player.accRunSpeed, player.maxRunSpeed);
                float num10 = 0.96f;
                int num11 = 20;
                if (dash == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int num12 = (player.velocity.Y != 0f) ? Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(player.height / 2) - 8f), player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f) : Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                        Dust obj = Main.dust[num12];
                        obj.velocity *= 0.1f;
                        Main.dust[num12].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                }
                else if (dash == 2)
                {
                    for (int l = 0; l < 0; l++)
                    {
                        int num13 = (player.velocity.Y != 0f) ? Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)(player.height / 2) - 8f), player.width, 16, 31, 0f, 0f, 100, default(Color), 1.4f) : Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, 31, 0f, 0f, 100, default(Color), 1.4f);
                        Dust obj2 = Main.dust[num13];
                        obj2.velocity *= 0.1f;
                        Main.dust[num13].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num13].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                    }
                    num8 = 0.985f;
                    num10 = 0.94f;
                    num11 = 30;
                }
                else if (dash == 3)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        int num14 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 4f), player.width, player.height - 8, 6, 0f, 0f, 100, default(Color), 1.7f);
                        Dust obj3 = Main.dust[num14];
                        obj3.velocity *= 0.1f;
                        Main.dust[num14].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num14].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        Main.dust[num14].noGravity = true;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num14].fadeIn = 0.5f;
                        }
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
                        int num15 = Dust.NewDust(new Vector2(player.position.X, player.position.Y + 4f), player.width, player.height - 8, 229, 0f, 0f, 100, default(Color), 1.2f);
                        Dust obj4 = Main.dust[num15];
                        obj4.velocity *= 0.1f;
                        Main.dust[num15].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                        Main.dust[num15].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                        Main.dust[num15].noGravity = true;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num15].fadeIn = 0.3f;
                        }
                    }
                    num8 = 0.985f;
                    num10 = 0.94f;
                    num11 = 20;
                }
                if (dash > 0)
                {
                    player.vortexStealthActive = false;
                    if (player.velocity.X > num7 || player.velocity.X < 0f - num7)
                    {
                        player.velocity.X = player.velocity.X * num8;
                    }
                    else if (player.velocity.X > num9 || player.velocity.X < 0f - num9)
                    {
                        player.velocity.X = player.velocity.X * num10;
                    }
                    else
                    {
                        dashDelay = num11;
                        if (player.velocity.X < 0f)
                        {
                            player.velocity.X = 0f - num9;
                        }
                        else if (player.velocity.X > 0f)
                        {
                            player.velocity.X = num9;
                        }
                    }
                }
            }
            else if (dash > 0 && !player.mount.Active)
            {
                if (dash == 1)
                {
                    int num16 = 0;
                    bool flag = false;
                    if (dashTime > 0)
                    {
                        dashTime--;
                    }
                    if (dashTime < 0)
                    {
                        dashTime++;
                    }
                    if (player.controlRight && player.releaseRight)
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
                    else if (player.controlLeft && player.releaseLeft)
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
                        player.velocity.X = 16.9f * (float)num16;
                        Point point = (player.Center + new Vector2((float)(num16 * player.width / 2 + 2), player.gravDir * (0f - (float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (player.Center + new Vector2((float)(num16 * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        dashDelay = -1;
                        for (int num17 = 0; num17 < 20; num17++)
                        {
                            int num18 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 2f);
                            Dust dust = Main.dust[num18];
                            dust.position.X = dust.position.X + (float)Main.rand.Next(-5, 6);
                            Dust dust2 = Main.dust[num18];
                            dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-5, 6);
                            Dust obj5 = Main.dust[num18];
                            obj5.velocity *= 0.2f;
                            Main.dust[num18].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
                        }
                        int num19 = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 34f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num19].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num19].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
                        Gore obj6 = Main.gore[num19];
                        obj6.velocity *= 0.4f;
                        num19 = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 14f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num19].velocity.X = (float)Main.rand.Next(-50, 51) * 0.01f;
                        Main.gore[num19].velocity.Y = (float)Main.rand.Next(-50, 51) * 0.01f;
                        Gore obj7 = Main.gore[num19];
                        obj7.velocity *= 0.4f;
                    }
                }
                else if (dash == 2)
                {
                    int num20 = 0;
                    bool flag2 = false;
                    if (dashTime > 0)
                    {
                        dashTime--;
                    }
                    if (dashTime < 0)
                    {
                        dashTime++;
                    }
                    if (player.controlRight && player.releaseRight)
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
                    else if (player.controlLeft && player.releaseLeft)
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
                        player.velocity.X = 14.5f * (float)num20;
                        Point point3 = (player.Center + new Vector2((float)(num20 * player.width / 2 + 2), player.gravDir * (0f - (float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point4 = (player.Center + new Vector2((float)(num20 * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point3.X, point3.Y) || WorldGen.SolidOrSlopedTile(point4.X, point4.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        dashDelay = -1;
                        ttDash = 15;
                        for (int num21 = 0; num21 < 0; num21++)
                        {
                            int num22 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f, 0f, 100, default(Color), 2f);
                            Dust dust3 = Main.dust[num22];
                            dust3.position.X = dust3.position.X + (float)Main.rand.Next(-5, 6);
                            Dust dust4 = Main.dust[num22];
                            dust4.position.Y = dust4.position.Y + (float)Main.rand.Next(-5, 6);
                            Dust obj8 = Main.dust[num22];
                            obj8.velocity *= 0.2f;
                            Main.dust[num22].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num22].shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
                        }
                    }
                }
                else if (dash == 3)
                {
                    int num23 = 0;
                    bool flag3 = false;
                    if (dashTime > 0)
                    {
                        dashTime--;
                    }
                    if (dashTime < 0)
                    {
                        dashTime++;
                    }
                    if (player.controlRight && player.releaseRight)
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
                    else if (player.controlLeft && player.releaseLeft)
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
                        player.velocity.X = 21.9f * (float)num23;
                        Point point5 = (player.Center + new Vector2((float)(num23 * player.width / 2 + 2), player.gravDir * (0f - (float)player.height) / 2f + player.gravDir * 2f)).ToTileCoordinates();
                        Point point6 = (player.Center + new Vector2((float)(num23 * player.width / 2 + 2), 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point5.X, point5.Y) || WorldGen.SolidOrSlopedTile(point6.X, point6.Y))
                        {
                            player.velocity.X = player.velocity.X / 2f;
                        }
                        dashDelay = -1;
                        for (int num24 = 0; num24 < 20; num24++)
                        {
                            int num25 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0f, 0f, 100, default(Color), 2f);
                            Dust dust5 = Main.dust[num25];
                            dust5.position.X = dust5.position.X + (float)Main.rand.Next(-5, 6);
                            Dust dust6 = Main.dust[num25];
                            dust6.position.Y = dust6.position.Y + (float)Main.rand.Next(-5, 6);
                            Dust obj9 = Main.dust[num25];
                            obj9.velocity *= 0.2f;
                            Main.dust[num25].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                            Main.dust[num25].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
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
            float angleDifference = (float)(Math.PI * 2) / shardNumber;
            float speed = 5f;
            float currentAngle = 0;

            for (int i = 0; i < shardNumber; i++)
            {
                //float angle = currentAngle - 0.5f + (float)Main.rand.NextDouble();
                //float positionX = player.position.X;
                //float positionY = player.position.Y;
                float speedX = (float)Math.Cos(currentAngle) * speed;
                float speedY = (float)Math.Sin(currentAngle) * speed;

                Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), ProjectileID.CrystalShard, 20, 5, player.whoAmI);

                currentAngle += angleDifference;
            }
        }
    }
}
