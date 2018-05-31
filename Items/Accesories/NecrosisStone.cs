using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Decimation.Items.Accesories
{
    public class NecrosisStone : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Necrosis Stone");
			Tooltip.SetDefault("This stone breathes life into once deceased creatures/nWIP");
		}
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 100;
            item.rare = 1;
            item.maxStack = 999;
        }
    }
}