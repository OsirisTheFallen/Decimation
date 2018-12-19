using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.Souls
{
    public class SoulofTime : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Time");
			Tooltip.SetDefault("Essence of Existance");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));				
		}
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 100;
            item.rare = 3;
            item.maxStack = 999;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
    }
}