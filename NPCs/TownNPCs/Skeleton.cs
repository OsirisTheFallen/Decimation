using System;
using System.Collections.Generic;
using Decimation.Items.Boss.DuneWorm;
using Decimation.Items.Misc;
using Decimation.Items.Misc.Souls;
using Decimation.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Decimation.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Skeleton : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "Skeleton";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = NPCID.Sets.ExtraFramesCount[NPCID.Guide];
            NPCID.Sets.AttackFrameCount[npc.type] = NPCID.Sets.AttackFrameCount[NPCID.Guide];
            NPCID.Sets.DangerDetectRange[npc.type] = 400;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 60;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            NPCID.Sets.AttackType[npc.type] = 0;
            npc.CloneDefaults(NPCID.Guide);
            npc.townNPC = true;
            npc.friendly = true;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.DD2_SkeletonHurt;
            npc.DeathSound = SoundID.DD2_SkeletonDeath;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return NPC.downedAncientCultist ||
                   NPC.downedBoss1 ||
                   NPC.downedBoss2 ||
                   NPC.downedBoss3 ||
                   NPC.downedFishron ||
                   NPC.downedGolemBoss ||
                   NPC.downedMechBossAny ||
                   NPC.downedMoonlord ||
                   NPC.downedPlantBoss ||
                   NPC.downedQueenBee ||
                   NPC.downedSlimeKing;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            int score = 0;
            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    int type = Main.tile[x, y].type;
                    if (type == TileID.Tables || type == TileID.Chairs || type == TileID.WorkBenches || type == TileID.Beds || type == TileID.OpenDoor || type == TileID.ClosedDoor || type == TileID.Torches)
                    {
                        score++;
                    }
                }
            }
            return score >= (right - left) * (bottom - top) / 2;
        }

        private List<String> names = new List<string>()
        {
            "Tommy",
            "Johnny",
            "Comet Sands",
            "Skeletor",
            "Jeff Skullingtin"
        };

        public override string TownNPCName()
        {
            return names[Main.rand.Next(5)];
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add("The skeletons underground are kinda stupid, they use themselves as ammunition!");
            chat.Add("I used to know a girl named Lisa, she tore me apart!");
            chat.Add("I did not hit her it is not true I did not hit her I did not!.....oh, hi " + Main.LocalPlayer.name);

            if (Main.bloodMoon)
            {
                chat.Add("I feel like I'm going to have a bad time");
                chat.Add("I've got a bone to pick with this \"Cthulhu\" guy!");
                chat.Add("Please don't rattle my bones! It took me a long time to arrange them like this");
                chat.Add("Everybody betrayed me! I'm fed up with this world...");
            }

            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton) shop = true;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.Bone);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Skull);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<LunarTablet>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType<BloodyLunarTablet>());
            nextSlot++;

            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SoulofLight);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SoulofNight);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.GuideVoodooDoll);
                nextSlot++;
            }

            if (DecimationWorld.downedWyvern)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SoulofFlight);
                nextSlot++;
            }

            if (DecimationWorld.downedDuneWorm)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<SoulofTime>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<DesertDessert>());
                nextSlot++;
            }

            if (NPC.downedMechBoss1)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SoulofMight);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MechanicalWorm);
                nextSlot++;
            }

            if (NPC.downedMechBoss2)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SoulofSight);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MechanicalEye);
                nextSlot++;
            }

            if (NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SoulofFright);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MechanicalSkull);
                nextSlot++;
            }

            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<SoulofLife>());
                nextSlot++;

                if (Main.bloodMoon)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Ectoplasm);
                    nextSlot++;
                }
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<SkeletonBone>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
        }
    }

    public class DownedWyvern : GlobalNPC
    {
        public override bool CheckDead(NPC npc)
        {
            if (npc.type == NPCID.WyvernHead) DecimationWorld.downedWyvern = true;

            return base.CheckDead(npc);
        }
    }
}