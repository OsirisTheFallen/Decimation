using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class CrystalAmulet : Amulet
    {
        public override AmuletClasses AmuletClass { get { return AmuletClasses.MAGE; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Amulet");
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override void UpdateAmulet(Player player)
        {
            player.statManaMax2 += 5;
            player.magicDamage *= 1.03f;
            player.magicCrit += 3;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AnyGem", 2);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddIngredient(ItemID.ManaRegenerationBand);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("+5 maximum mana")
                .addEffect("+3% magic damages")
                .addEffect("+3% magic critical strike chances")
                .addEffect("+4% chances to shoot out a burst of crystal shards when taking damages");
        }
    }
}
