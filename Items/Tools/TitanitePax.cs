using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    class TitanitePax : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 52;
            item.melee = true;
            item.damage = 64;
            item.crit = 14;
            item.pick = 250;
            item.axe = 27;
            item.useStyle = 1;
            item.useTime = 13;
            item.useAnimation = 13;
            item.knockBack = 7;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TitaniteBar"), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
