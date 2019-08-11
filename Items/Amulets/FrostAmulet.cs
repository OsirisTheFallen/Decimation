using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class FrostAmulet : Amulet
    {
        protected override string ItemName => "Frost Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Ranger;

        protected override void InitAmulet()
        {
            height = 32;
        }

        protected override void UpdateAmulet(Player player)
        {
            player.rangedDamage *= 1.03f;
            player.rangedCrit += 3;
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+3% ranged damages")
                .AddEffect("+3% ranged velocity")
                .AddEffect("+3% ranged critical strike chances")
                .AddEffect("+2% chance to not consume ammo")
                .AddEffect("+4% chance that arrows will inflict \"Forstburn\"");
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, false);

            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddIngredient(ItemID.ArcheryPotion);

            return new List<ModRecipe> { recipe };
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
