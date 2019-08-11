using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    internal class GreatwoodHammer : DecimationTool
    {
        protected override string ItemName => "Greatwood Mallet";
        protected override string ItemTooltip => "Who needs metal?";
        protected override int MeleeDamages => 20;
        protected override int HammerPower => 55;

        protected override void InitTool()
        {
            width = 40;
            height = 40;
            useTime = 10;
            useAnimation = 25;
            item.knockBack = 5;
            value = Item.buyPrice(0, 0, 10);
            rarity = Rarity.Green;
            useSound = SoundID.Item1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.WorkBenches, true);

            recipe.AddIngredient(ItemID.WoodenHammer);
            recipe.AddIngredient(ItemID.BorealWoodHammer);
            recipe.AddIngredient(ItemID.ShadewoodHammer);
            recipe.AddIngredient(ItemID.PalmWoodHammer);

            return recipe;
        }
    }
}