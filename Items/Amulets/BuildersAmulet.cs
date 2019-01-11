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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Builder's Amulet");
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 30;
        }

        public override AmuletClasses GetAmuletClass()
        {
            return AmuletClasses.BUILDER;
        }

        public override void UpdateAmulet(Player player)
        {
            player.blockRange += 2;
            player.tileSpeed *= 1.05f;
            player.wallSpeed *= 1.05f;

            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.CopperHammer);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.IronBar);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.CopperHammer);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.LeadBar);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+2 block placement range")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+5% tile and wall placement speed")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "Provides light")
                {
                   overrideColor = Color.ForestGreen
                }
            };
        }
    }
}
