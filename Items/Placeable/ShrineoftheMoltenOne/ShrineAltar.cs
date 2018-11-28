using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Items.Placeable.ShrineoftheMoltenOne
{
    public class ShrineAltar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 32;
            item.useTime = 10;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true;
            item.consumable = true;
            item.createTile = mod.TileType<Tiles.ShrineoftheMoltenOne.ShrineAltar>();
        }
    }
}
