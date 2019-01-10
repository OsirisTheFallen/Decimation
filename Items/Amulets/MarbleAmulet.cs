using Decimation.Items.Accessories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class MarbleAmulet : Amulet
    {
        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 30;
        }

        public override AmuletClasses GetAmuletClass()
        {
            return AmuletClasses.THROWING;
        }

        public override void UpdateAmulet(Player player)
        {
            player.thrownDamage *= 1.03f;
            player.thrownVelocity *= 1.03f;
            player.thrownCrit += 3;

            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            modPlayer.amuletsBuff = BuffID.Confused;
            modPlayer.amuletsBuffTime = 300;
            modPlayer.amuletsBuffChances = 4;
            modPlayer.amuletsBuffWhenAttacking = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<LightweightGlove>());
            recipe.AddIngredient(ItemID.Marble, 6);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Shuriken, 2);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+3% throwing damages")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% throwing speed")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% throwing critical strikes chances")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+2% chances to not consume ammo on throwing attacks")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+4% chances to inflict confusion the ennemis on throwing attacks")
                {
                   overrideColor = Color.ForestGreen
                }
            };
        }
    }
}
