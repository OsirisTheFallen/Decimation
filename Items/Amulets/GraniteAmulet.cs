using System.Collections.Generic;
using Decimation.Core.Amulets;
using Decimation.Core.Amulets.Synergy;
using Decimation.Items.Accessories;
using Decimation.Synergies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class GraniteAmulet : Amulet
    {
        protected override string ItemName => "Granite Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Tank;
        public override IAmuletsSynergy Synergy => new GraniteAmuletSynergy();

        protected override void UpdateAmulet(Player player)
        {
            player.statDefense += 1;

            if (player.statLife >= player.statLifeMax * 0.75f)
            {
                player.moveSpeed *= 0.95f;
                player.statDefense = (int) (player.statDefense * 1.05f);
                player.noKnockback = true;
            }
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.TinkerersWorkbench});

            recipe.AddIngredient(ModContent.ItemType<GraniteLinedTunic>());
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Granite, 6);
            recipe.AddIngredient(ItemID.Gel, 20);

            return new List<ModRecipe> {recipe};
        }

        protected override void GetAmuletTooltip(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+1 to defense")
                .AddEffect("-3% of incoming damage to team members (WIP)")
                .AddEffect(
                    $"When above {Main.LocalPlayer.statLifeMax * 0.75f} hp, your are slowed by 5%, but your defense is increased by 5% and the knockback is cancelled")
                .AddSynergy(
                    "Cobalt Shield, Ankh Shield, Paladins Shield and Obsidian Shield gives 10% chance to block attacks");
        }
    }
}