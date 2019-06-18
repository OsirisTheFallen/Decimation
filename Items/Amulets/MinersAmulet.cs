using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class MinersAmulet : Amulet
    {
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.MINER; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner's Amulet");
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override void UpdateAmulet(Player player)
        {
            player.pickSpeed *= 1.03f;
            player.meleeSpeed *= 1.04f;
            player.blockRange += 1;
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1, 1, 1);
            //Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.CopperPickaxe);
            recipe.AddIngredient(ItemID.IronOre, 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Torch, 10);
            recipe.AddIngredient(ItemID.CopperPickaxe);
            recipe.AddIngredient(ItemID.IronOre, 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("+3% mining speed")
                .addEffect("+4% melee speed")
                .addEffect("+1 block interaction range")
                .addEffect("Provides light");
        }
    }
}
