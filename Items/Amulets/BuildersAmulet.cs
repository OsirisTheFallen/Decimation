using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class BuildersAmulet : Amulet
    {
        protected override string ItemName => "Builder's Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Builder;

        protected override void InitAmulet()
        {
            height = 34;
        }

        protected override void UpdateAmulet(Player player)
        {
            player.blockRange += 2;
            player.tileSpeed *= 1.05f;
            player.wallSpeed *= 1.05f;

            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, true);

            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.CopperHammer);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.IronBar);

            return new List<ModRecipe> { recipe };
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+2 block interaction range")
                .AddEffect("+5% tile and wall placement speed")
                .AddEffect("Provides light");
        }
    }
}
