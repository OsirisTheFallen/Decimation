using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Decimation.NPCs.Arachnus;
using Decimation.NPCs.AncientDuneWorm;
using Decimation.UI;
using System.Collections.Generic;
using Decimation.Core.Util;
using Terraria.UI;
using Microsoft.Xna.Framework;

namespace Decimation
{
    public class Decimation : Mod
    {
        public static Decimation Instance { set; get; }

        public static AmuletSlotState amuletSlotState;
        private UserInterface amuletSlotInterface;

        internal UserInterface skeletonUserInterface;

        public Decimation()
        {
            Instance = this;

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            References.mod = this;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                amuletSlotState = new AmuletSlotState();
                amuletSlotState.Activate();
                amuletSlotInterface = new UserInterface();
                amuletSlotInterface.SetState(amuletSlotState);

                skeletonUserInterface = new UserInterface();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<DecimationPlayer>().necrosisStoneEquipped && player.respawnTimer != 0)
                player.respawnTimer -= 1;

            amuletSlotInterface?.Update(gameTime);
            skeletonUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex + 1, new LegacyGameInterfaceLayer(
                    "Decimation: Amulet Slot",
                    delegate
                    {
                        if (Main.playerInventory)
                            amuletSlotInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(inventoryIndex + 2, new LegacyGameInterfaceLayer(
                    "Decimation: Skeleton UI",
                    delegate
                    {
                        skeletonUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "The Bloodshot Eye", 2.5f, (Func<bool>)(() => DecimationWorld.downedBloodshotEye), "INSERT LATER");
                bossChecklist.Call("AddBossWithInfo", "The Ancient Dune Worm", 5.7f, (Func<bool>)(() => DecimationWorld.downedDuneWorm), "INSERT LATER");
                bossChecklist.Call("AddBossWithInfo", "Arachnus", 20f, (Func<bool>)(() => DecimationWorld.downedArachnus), "INSERT LATER");
            }
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup gems = new RecipeGroup(() => Lang.misc[37] + " Gem", new int[]
            {
                ItemID.Amethyst,
                ItemID.Topaz,
                ItemID.Emerald,
                ItemID.Sapphire,
                ItemID.Ruby,
                ItemID.Diamond,
            });

            RecipeGroup threads = new RecipeGroup(() => Lang.misc[37] + " Thread", new int[]
            {
                ItemID.BlackThread,
                ItemID.GreenThread,
                ItemID.PinkThread
            });

            RecipeGroup.RegisterGroup("AnyGem", gems);
            RecipeGroup.RegisterGroup("AnyThread", threads);
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            DecimationModMessageType msgType = (DecimationModMessageType)reader.ReadByte();

            switch (msgType)
            {
                case DecimationModMessageType.Arachnus:
                    Arachnus arachnus = (Arachnus)Main.npc[reader.ReadInt32()].modNPC;
                    if (arachnus != null && arachnus.npc.active)
                    {
                        arachnus.HandlePacket(reader);
                    }
                    break;
                case DecimationModMessageType.DuneWorm:
                    AncientDuneWormHead duneWorm = (AncientDuneWormHead)Main.npc[reader.ReadInt32()].modNPC;
                    if (duneWorm != null && duneWorm.npc.active)
                    {
                        duneWorm.HandlePacket(reader);
                    }
                    break;
                case DecimationModMessageType.SpawnBoss:
                    int type = reader.ReadInt32();
                    int player = reader.ReadInt32();
                    Main.PlaySound(15, (int)Main.player[player].position.X, (int)Main.player[player].position.Y, 0);
                    if (Main.netMode != 1)
                        NPC.SpawnOnPlayer(player, type);
                    break;
                default:
                    ErrorLogger.Log("DecimationMod: Unknown Message type: " + msgType);
                    break;
            }
        }
    }

    enum DecimationModMessageType : byte
    {
        Arachnus,
        DuneWorm,
        SpawnBoss
    }
}