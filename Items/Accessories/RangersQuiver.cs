using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class RangersQuiver : DecimationAccessory
    {
        protected override string ItemName => "Ranger's Quiver";

        protected override string ItemTooltip =>
            "25% Chance not to consume ammo\n+10%ranged damage\n+15% arrow velocity\n+5% ranged Crit Chance";

        protected override void InitAccessory()
        {
            width = 32;
            height = 32;
            rarity = Rarity.Green;
            item.value = Item.buyPrice(0, 0, 0, 10);
        }
        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.MythrilAnvil }, true);

            recipe.AddIngredient(ItemID.MagicQuiver, 1);
            recipe.AddIngredient(ItemID.RangerEmblem, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 5);

            return new List<ModRecipe>() { recipe };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.10f;
            player.rangedCrit += 05;
            player.ammoCost75 = true;
        }

    }
}