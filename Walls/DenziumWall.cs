using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Walls
{
    internal class DenziumWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[this.Type] = true;
            dustType = DustID.Stone;
            drop = this.mod.ItemType<Items.Placeable.DenziumWall>();
            AddMapEntry(new Color(5, 7, 22));
        }
    }
}