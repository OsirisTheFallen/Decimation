using System.Collections.Generic;
using Decimation.Core.Amulets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class MinersAmulet : Amulet
    {
        protected override string ItemName => "Miner's Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Miner;

        protected override void InitAmulet()
        {
            height = 32;
        }

        protected override void UpdateAmulet(Player player)
        {
            player.pickSpeed *= 1.03f;
            player.meleeSpeed *= 1.04f;
            player.blockRange += 1;
            Lighting.AddLight((int) (player.position.X + player.width / 2f) / 16,
                (int) (player.position.Y + player.height / 2f) / 16, 1, 1, 1);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.TinkerersWorkbench}, true);

            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.CopperPickaxe);
            recipe.AddIngredient(ItemID.IronOre, 2);

            return new List<ModRecipe> {recipe};
        }

        protected override void GetAmuletTooltip(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+3% mining speed")
                .AddEffect("+4% melee speed")
                .AddEffect("+1 block interaction range")
                .AddEffect("Provides light");
        }
    }
}