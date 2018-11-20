using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Walls
{
    class DenziumWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            dustType = DustID.Stone;
            drop = mod.ItemType("DenziumWall");
            AddMapEntry(new Color(5, 7, 22));
        }
    }
}
