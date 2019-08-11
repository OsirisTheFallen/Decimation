using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class RangersPouch : DecimationAccessory
    {
        protected override string ItemName => "Ranger's Pouch";
        protected override string ItemTooltip =>
            "25% Chance to not consume ammo \n+10% Ranged damage \n+5% Ranged critical chance";

        protected override void InitAccessory()
        {
            width = 30;
            height = 30;
            item.value = 10;
            rarity = Rarity.Green;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.MythrilAnvil }, false);

            recipe.AddIngredient(ItemID.EndlessMusketPouch);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.SoulofSight, 5);

            return new List<ModRecipe>() { recipe };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.10f;
            player.rangedCrit += 5;
            player.ammoCost75 = true;
        }

    }
}