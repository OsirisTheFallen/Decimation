using Decimation.Items;
using Decimation.Items.Boss.Arachnus;
using Decimation.NPCs.Arachnus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Decimation.Tiles.ShrineoftheMoltenOne
{
    class ShrineAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Shrine Altar");
            AddMapEntry(new Color(33, 28, 25), name);
            dustType = DustID.LavaMoss;
            disableSmartCursor = true;
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Item[] inventory = player.inventory;

            bool inventoryContainAmulet = false;

            for (int k = 0; k < inventory.Length; k++)
                if (inventory[k].type == mod.ItemType<MoltenArachnidsAmulet>())
                {
                    inventoryContainAmulet = true;
                    inventory[k].TurnToAir();
                    break;
                }

            if (inventoryContainAmulet)
            {
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<Arachnus>());
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = mod.ItemType<MoltenArachnidsAmulet>();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 80, 32, mod.ItemType<Items.Placeable.ShrineoftheMoltenOne.ShrineAltar>());
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return DecimationWorld.downedArachnus;
        }
    }
}
