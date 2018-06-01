using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Decimation.Items;

namespace Decimation
{
    public class Decimation : Mod
    {
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if(bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Bloodshot Eye of Cthulhu", 2.5f, (Func<bool>)(() => DecimationWorld.downedBloodshotEye), "INSERT LATER");
				bossChecklist.Call("AddBossWithInfo", "Ancient Dune Worm", 5.7f, (Func<bool>)(() => DecimationWorld.downedDuneWorm), "INSERT LATER");
            }
        }		
        public Decimation()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
		public override void AddRecipeGroups()
        {
			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Gem", new int[]
			{
				ItemID.Amethyst,
				ItemID.Topaz,
				ItemID.Emerald,
				ItemID.Sapphire,
				ItemID.Ruby,
				ItemID.Diamond,
			});
			RecipeGroup.RegisterGroup("AnyGem", group);		
		}
	}
}