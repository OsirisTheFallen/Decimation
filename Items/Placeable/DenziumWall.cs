using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
   internal class DenziumWall : DecimationPlaceableItem
   {
       protected override string ItemName => "Denzium Wall";
       protected override int Tile => -1;

       protected override void InitPlaceable()
        {
            width = 32;
            height = 32;
            item.createWall = mod.WallType("DenziumWall");
        }
    }
}
