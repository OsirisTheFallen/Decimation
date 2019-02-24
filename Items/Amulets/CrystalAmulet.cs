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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Amulet");
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override AmuletClasses GetAmuletClass()
        {
            return AmuletClasses.MAGE;
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

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+5 maximum mana")
                {
                    overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% magic damages")
                {
                    overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% magic critical strike chances")
                {
                    overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+4% chances to shoot out a burst of crystal shards when taking damages")
                {
                    overrideColor = Color.ForestGreen
                }
            };
        }
    }
}
