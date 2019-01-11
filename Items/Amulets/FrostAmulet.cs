using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class FrostAmulet : Amulet
    {
        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override AmuletClasses GetAmuletClass()
        {
            return AmuletClasses.RANGER;
        }

        public override void UpdateAmulet(Player player)
        {
            player.rangedDamage *= 1.03f;
            player.rangedCrit += 3;
        }

        public override List<TooltipLine> GetTooltipLines()
        {
            return new List<TooltipLine>()
            {
                new TooltipLine(mod, "Effect", "+3% ranged damages")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% ranged velocity")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+3% ranged critical strike chances")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+2% chance to not consume ammo")
                {
                   overrideColor = Color.ForestGreen
                },
                new TooltipLine(mod, "Effect", "+4% chance that arrows will inflict \"Frostburn\"")
                {
                    overrideColor = Color.ForestGreen
                },
            };
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddIngredient(ItemID.ArcheryPotion);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class FrostAmuletRangedEffect : GlobalNPC
    {
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem.type == mod.ItemType<FrostAmulet>() && projectile.arrow && Main.rand.NextBool(25))
                npc.AddBuff(BuffID.Frostburn, 300);
        }
    }
}
