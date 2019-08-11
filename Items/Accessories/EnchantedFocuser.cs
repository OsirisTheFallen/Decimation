using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class EnchantedFocuser : DecimationAccessory
    {
        protected override string ItemName => "Enchanted Focuser";
        protected override string ItemTooltip => "Focuses one's Ki.";

        protected override void InitAccessory()
        {
            width = 62;
            height = 46;
            rarity = Rarity.Green;
            item.value = Item.buyPrice(0, 0, 0, 10);
        }
        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { TileID.Anvils }, true);

            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(ItemID.Wire, 15);
            recipe.AddIngredient(ItemID.CopperBar, 5);
            recipe.AddIngredient(ItemID.WaterCandle);
            recipe.AddIngredient(mod.ItemType<Focuser>());

            return new List<ModRecipe>() { recipe };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.10f;
            player.magicDamage += 0.10f;
            player.rangedCrit += 02;
            player.meleeCrit += 02;
            player.magicCrit += 02;
            player.thrownCrit += 02;
            player.manaRegen += 2;
            player.statManaMax2 += 20;
        }

    }
}
