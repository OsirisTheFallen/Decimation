using System.Collections.Generic;
using Decimation.Items.Misc;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Ammo
{
    internal class SiphonArrow : DecimationAmmo
    {
        protected override string ItemName => "Siphon Arrow";
        protected override string ItemTooltip => "Aspires other's life";
        protected override string Projectile => "SiphonArrow";
        protected override int Ammo => AmmoID.Arrow;

        protected override void InitAmmo()
        {
            damages = 11;
            projKnockBack = 2;
            width = 14;
            height = 32;
            value = Item.buyPrice(0, 0, 0, 55);

            this.item.shootSpeed = 2.5f;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.Anvils});

            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(ModContent.ItemType<BloodiedEssence>());

            return new List<ModRecipe> {recipe};
        }
    }
}