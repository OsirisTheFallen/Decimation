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
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.RANGER; }
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override void UpdateAmulet(Player player)
        {
            player.rangedDamage *= 1.03f;
            player.rangedCrit += 3;
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("+3% ranged damages")
                .addEffect("+3% ranged velocity")
                .addEffect("+3% ranged critical strike chances")
                .addEffect("+2% chance to not consume ammo")
                .addEffect("+4% chance that arrows will inflict \"Forstburn\"");
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
