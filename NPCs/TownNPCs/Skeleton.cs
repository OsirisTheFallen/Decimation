using System.Collections.Generic;
using Decimation.Items.Boss.DuneWorm;
using Decimation.Items.Misc;
using Decimation.Items.Misc.Souls;
using Decimation.Projectiles;
using Decimation.UI;
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
        private readonly List<string> names = new List<string>
        {
            "Tommy",
            "Johnny",
            "Comet Sands",
            "Skeletor",
            "Jeff Skullingtin"
        };

        public override bool Autoload(ref string name)
        {
            name = "Skeleton";
            return this.mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[this.npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[this.npc.type] = NPCID.Sets.ExtraFramesCount[NPCID.Guide];
            NPCID.Sets.AttackFrameCount[this.npc.type] = NPCID.Sets.AttackFrameCount[NPCID.Guide];
            NPCID.Sets.DangerDetectRange[this.npc.type] = 400;
            NPCID.Sets.AttackType[this.npc.type] = 0;
            NPCID.Sets.AttackTime[this.npc.type] = 60;
            NPCID.Sets.AttackAverageChance[this.npc.type] = 30;
            NPCID.Sets.HatOffsetY[this.npc.type] = 4;
        }

        public override void SetDefaults()
        {
            NPCID.Sets.AttackType[this.npc.type] = 0;
            this.npc.CloneDefaults(NPCID.Guide);
            this.npc.townNPC = true;
            this.npc.friendly = true;
            this.npc.aiStyle = 7;
            this.npc.damage = 10;
            this.npc.defense = 15;
            this.npc.lifeMax = 250;
            this.npc.HitSound = SoundID.DD2_SkeletonHurt;
            this.npc.DeathSound = SoundID.DD2_SkeletonDeath;
            this.npc.knockBackResist = 0.5f;
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
            for (int y = top; y <= bottom; y++)
            {
                int type = Main.tile[x, y].type;
                if (type == TileID.Tables || type == TileID.Chairs || type == TileID.WorkBenches ||
                    type == TileID.Beds || type == TileID.OpenDoor || type == TileID.ClosedDoor ||
                    type == TileID.Torches) score++;
            }

            return score >= (right - left) * (bottom - top) / 2;
        }

        public override string TownNPCName()
        {
            return names[Main.rand.Next(5)];
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            chat.Add("The skeletons underground are kinda stupid, they use themselves as ammunition!");
            chat.Add("I used to know a girl named Lisa, she tore me apart!");
            chat.Add(
                "I did not hit her it is not true I did not hit her I did not!.....oh, hi " + Main.LocalPlayer.name);

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
            button2 = "Curse";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                Main.playerInventory = true;
                Main.npcChatText = "";
                Decimation.Instance.skeletonUserInterface.SetState(new SkeletonUI());
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.Bone);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Skull);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<LunarTablet>());
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<BloodyLunarTablet>());
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
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulofTime>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<DesertDessert>());
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
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulofLife>());
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
            projType = ModContent.ProjectileType<SkeletonBone>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection,
            ref float randomOffset)
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