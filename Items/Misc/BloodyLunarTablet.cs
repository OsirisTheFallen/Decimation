using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    class BloodyLunarTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summon blood moon.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 40;
            item.rare = 2;
            item.value = 50000;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.consumable = true;
            item.maxStack = 1;
        }

        public override bool CanUseItem(Player player)
        {
            return !Main.bloodMoon;
        }

        public override bool UseItem(Player player)
        {
            Main.bloodMoon = true;
            return true;
        }
    }
}
