using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    class DenziumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Denzium Ore");
            Tooltip.SetDefault("A substance created from intense pressure and heat.");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("DenziumOre");
        }
    }
}
