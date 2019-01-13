using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    class ShinySentinel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shiny Sentinel");
            Tooltip.SetDefault("Double life and mana regeneration \n+5 defense");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.accessory = true;
            item.maxStack = 1;
            item.value = 450000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen *= 2;
            player.manaRegen *= 2;
            player.statDefense += 5;
        }
    }
}
