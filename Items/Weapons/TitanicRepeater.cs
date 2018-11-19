using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class TitanicRepeater : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 36;
            item.ranged = true;
            item.noMelee = true;
            item.damage = 120;
            item.crit = 20;
            item.useStyle = 5;
            item.useTime = 12;
            item.useAnimation = 12;
            item.knockBack = 7;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.shootSpeed = 25;
            item.autoReuse = true;
            item.value = Item.buyPrice(gold: 45);
            item.rare = 6;
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
