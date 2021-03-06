using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Decimation.Items.Misc
{
    public class BloodiedEssence : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodied Essence");
			Tooltip.SetDefault("Congealed to perfection");
		}
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = 100;
            item.rare = 1;
            item.maxStack = 999;
        }
    }
}
