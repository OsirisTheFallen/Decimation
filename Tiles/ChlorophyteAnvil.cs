using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Decimation.Tiles
{
    class ChlorophyteAnvil : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Chlorophyte Anvil");
            AddMapEntry(new Color(108, 239, 64), name);
            dustType = DustID.Iron;
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Anvils, TileID.MythrilAnvil, mod.TileType("EnchantedAnvil") };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("ChlorophyteAnvil"));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.LocalPlayer.AddBuff(mod.BuffType("NaturesAura"), 5);
            }
        }
    }
}
