using System.Collections.Generic;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class Focuser : DecimationAccessory
    {
        protected override string ItemName => "Focuser";
        protected override string ItemTooltip => "Focuses one's inner strength.";

        protected override void InitAccessory()
        {
            width = 54;
            height = 46;
            rarity = Rarity.Green;
            this.item.value = Item.buyPrice(0, 0, 0, 10);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.Anvils}, true);

            recipe.AddIngredient(ItemID.Chain, 3);
            recipe.AddIngredient(ItemID.CopperBar, 10);
            recipe.AddIngredient(ItemID.GoldBar);
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.IronBar, 3);

            return new List<ModRecipe> {recipe};
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.05f;
            player.rangedCrit += 05;
            player.meleeCrit += 05;
            player.magicCrit += 05;
            player.thrownCrit += 05;
        }
    }
}