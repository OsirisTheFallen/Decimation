using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Items.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Decimation.Items.Amulets
{
    class HealtyAmulet : Amulet
    {
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.HEALER; }
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 30;
        }

        public override void UpdateAmulet(Player player)
        {
            player.statManaMax2 += 10;
            player.statLifeMax2 += (int)(player.statLifeMax * 0.05f);

            if (player.GetModPlayer<DecimationPlayer>().isInCombat && player.GetModPlayer<DecimationPlayer>().enchantedHeartDropTime % 300 == 0)
                Item.NewItem(new Vector2(player.position.X, player.position.Y), mod.ItemType<EnchantedHeart>());
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddIngredient(ItemID.Gel, 5);
            recipe.AddIngredient(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+10 to maximum mana")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+5% to maximum life")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "Drop enchanted hearts each 5 seconds when you are in combat")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "Your lifesteal will be given to your allies (WIP)")
                {
                    overrideColor = Color.ForestGreen
                }
            };
        }
    }
}
