using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Tools
{
    internal class MultigrainHammer : DecimationTool
    {
        protected override string ItemName => "Multigrain Hamaxe";
        protected override string ItemTooltip => "Smells like honeysuckle";
        protected override int MeleeDamages => 20;
        protected override int HammerPower => 67;
        protected override int AxePower => 13;

        protected override void InitTool()
        {
            width = 32;
            height = 32;
            useTime = 20;
            useAnimation = 20;
            item.knockBack = 5;
            value = Item.buyPrice(0, 0, 10);
            rarity = Rarity.Green;
            item.crit = 3;
            useSound = SoundID.Item1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.Loom);

            recipe.AddIngredient(null, "GreatwoodHammer");
            recipe.AddIngredient(ItemID.CactusPickaxe);
            recipe.AddIngredient(ItemID.Pumpkin, 10);
            recipe.AddIngredient(ItemID.Vine, 2);

            return recipe;
        }
    }
}