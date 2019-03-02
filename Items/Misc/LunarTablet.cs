using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    class LunarTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons the full moon.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 40;
            item.consumable = true;
            item.value = 25000;
            item.maxStack = 1;
            item.rare = 2;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
        }

        public override bool CanUseItem(Player player)
        {
            return Main.dayTime;
        }

        public override bool UseItem(Player player)
        {
            Main.moonPhase = 0;
            Main.dayTime = false;

            return true;
        }
    }
}
