using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Decimation.Tiles
{
    class DenziumOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            minPick = 200;
            mineResist = 10;
            dustType = DustID.Stone;
            drop = mod.ItemType("DenziumOre");
            AddMapEntry(new Color(192, 57, 85));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
