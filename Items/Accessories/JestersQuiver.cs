using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class JestersQuiver : DecimationAccessory
    {
        protected override string ItemName => "Jester's Quiver";

        protected override string ItemTooltip =>
            "Turns wooden arrows into jesters arrows \n+15% Ranged Damage \n-20% Ammo Cost \n+5% Ranged Critical Chance";

        protected override void InitAccessory()
        {
            width = 20;
            height = 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.15f;
            player.ammoCost80 = true;
            player.rangedCrit += 5;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.TinkerersWorkbench }, false);

            recipe.AddIngredient(mod.ItemType<RangersQuiver>());
            recipe.AddIngredient(mod.ItemType<RangersPouch>());
            recipe.AddIngredient(ItemID.SoulofSight, 25);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.FallenStar, 15);

            return new List<ModRecipe>() { recipe };
        }
    }
}