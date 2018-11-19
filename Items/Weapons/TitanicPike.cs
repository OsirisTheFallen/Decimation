using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class TitanicPike : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 120;
            item.crit = 14;
            item.knockBack = 12;
            item.useStyle = 5;
            item.value = Item.buyPrice(gold: 45);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
            item.melee = true;
            item.value = Item.buyPrice(gold: 45);
            item.rare = 6;
            item.width = 94;
            item.height = 94;
            item.shoot = mod.ProjectileType("TitanicPikeProjectile");
            item.useAnimation = 18;
            item.useTime = 24;
            item.shootSpeed = 3.7f;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
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
