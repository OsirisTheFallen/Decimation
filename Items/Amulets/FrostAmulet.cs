using System.Collections.Generic;
using Decimation.Core.Amulets;
using Terraria;
using Terraria.ID;
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

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.TinkerersWorkbench});

            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddIngredient(ItemID.ArcheryPotion);

            return new List<ModRecipe> {recipe};
        }

        protected override void GetAmuletTooltip(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+3% ranged damages")
                .AddEffect("+3% ranged velocity")
                .AddEffect("+3% ranged critical strike chances")
                .AddEffect("+2% chance to not consume ammo")
                .AddEffect("+4% chance that arrows will inflict \"Forstburn\"");
        }
    }

    public class FrostAmuletRangedEffect : GlobalNPC
    {
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>().AmuletSlotItem.type ==
                this.mod.ItemType<FrostAmulet>() && projectile.arrow && Main.rand.NextBool(25))
                npc.AddBuff(BuffID.Frostburn, 300);
        }
    }
}