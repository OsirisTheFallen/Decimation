using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
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
            item.value = Item.buyPrice(silver: 40);
            rarity = Rarity.Green;
            autoReuse = true;
            item.expert = false;
        }
        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.Anvils, true);

            recipe.AddIngredient(ItemID.WoodenSword, 1);
            recipe.AddIngredient(ItemID.BorealWoodSword, 1);
            recipe.AddIngredient(ItemID.ShadewoodSword, 1);
            recipe.AddIngredient(ItemID.PalmWoodSword, 1);

            return recipe;
        }
    }
}