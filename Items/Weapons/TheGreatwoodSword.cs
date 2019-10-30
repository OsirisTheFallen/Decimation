using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class TheGreatwoodSword : DecimationWeapon
    {
        protected override string ItemName => "The Greatwood Sword";
        protected override string ItemTooltip => "Who needs metal?";
        protected override int Damages => 20;

        protected override void InitWeapon()
        {
            width = 44;
            height = 44;
            useTime = 25;
            useAnimation = 25;
            knockBack = 5;
            this.item.value = Item.buyPrice(silver: 40);
            rarity = Rarity.Green;
            autoReuse = true;
            this.item.expert = false;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.Anvils, true);

            recipe.AddIngredient(ItemID.WoodenSword);
            recipe.AddIngredient(ItemID.BorealWoodSword);
            recipe.AddIngredient(ItemID.ShadewoodSword);
            recipe.AddIngredient(ItemID.PalmWoodSword);

            return recipe;
        }
    }
}