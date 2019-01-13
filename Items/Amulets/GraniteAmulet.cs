using Decimation.Items.Accessories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class GraniteAmulet : Amulet
    {
        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 30;
        }

        public override AmuletClasses GetAmuletClass()
        {
            return AmuletClasses.TANK;
        }

        public override void UpdateAmulet(Player player)
        {
            player.statDefense += 1;

            if (player.statLife >= player.statLifeMax * 0.75f)
            {
                player.moveSpeed *= 0.95f;
                player.statDefense = (int)(player.statDefense * 1.05f);
                player.noKnockback = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<GraniteLinedTunic>());
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Granite, 6);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+1 to defense")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "-3% of incoming damage to team members (WIP)")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "When above " + (Main.LocalPlayer.statLifeMax * 0.75f) + " hp, your are slowed by 5%, but your defense is increased by 5% and the knockback is cancelled")
                {
                   overrideColor = Color.ForestGreen
                },
            };
        }
    }
}
