using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Decimation.NPCs.Arachnus;
using Decimation.NPCs.AncientDuneWorm;

namespace Decimation
{
    public class Decimation : Mod
    {

        public static Mod decimation;

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Bloodshot Eye of Cthulhu", 2.5f, (Func<bool>)(() => DecimationWorld.downedBloodshotEye), "INSERT LATER");
                bossChecklist.Call("AddBossWithInfo", "Ancient Dune Worm", 5.7f, (Func<bool>)(() => DecimationWorld.downedDuneWorm), "INSERT LATER");
                bossChecklist.Call("AddBossWithInfo", "Arachnus", 20f, (Func<bool>)(() => DecimationWorld.downedArachnus), "INSERT LATER");
            }
        }

        public Decimation()
        {
            decimation = this;

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Gem", new int[]
            {
                ItemID.Amethyst,
                ItemID.Topaz,
                ItemID.Emerald,
                ItemID.Sapphire,
                ItemID.Ruby,
                ItemID.Diamond,
            });
            RecipeGroup.RegisterGroup("AnyGem", group);
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