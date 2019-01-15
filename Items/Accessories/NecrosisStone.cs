using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    public class NecrosisStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrosis Stone");
            Tooltip.SetDefault("This stone breathes life into once deceased creatures\nReduces respawn time by 50%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 50000;
            item.rare = 1;
            item.maxStack = 1;
            item.rare = -12;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DecimationPlayer>().necrosisStoneEquipped = true;
        }
    }
}