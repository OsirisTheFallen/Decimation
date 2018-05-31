using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Decimation.Items
{
	
    public class DesertDessert : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Dessert");
			Tooltip.SetDefault("Summons the Ancient Dune Worm");
		}
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 1300;
            item.rare = 1;
            item.maxStack = 20;
			item.useStyle = 4;
			item.useAnimation = 30;
            item.useTime = 30;
            item.consumable = true;
        }
		
		
 		public override bool UseItem(Player player)
		{
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("AncientDuneWormHead"));
			return true;
		}

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 10);
 //           recipe.AddIngredient(ItemID.Wormtooth, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
