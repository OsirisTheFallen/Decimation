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
    internal class MarbleAmulet : Amulet
    {
        protected override string ItemName => "Marble Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Throwing;
        public override IAmuletsSynergy Synergy => new MarbleAmuletSynergy();

        protected override void UpdateAmulet(Player player)
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

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.TinkerersWorkbench});

            recipe.AddIngredient(this.mod.ItemType<LightweightGlove>());
            recipe.AddIngredient(ItemID.Marble, 6);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Shuriken, 2);

            return new List<ModRecipe> {recipe};
        }

        protected override void GetAmuletTooltip(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+3% throwing damages")
                .AddEffect("+3% throwing velocity")
                .AddEffect("+3% throwing critical strikes chances")
                .AddEffect("+2% chances to not consume ammo on throwing attacks")
                .AddEffect("+4% chances to inflict confusion to enemies on throwing attacks")
                .AddSynergy(
                    "Javelins, Shurikens, Throwing Knives, Bone Throwing Knives, Star Anises, Bone Javelins,\nPoisoned Throwing Knives and Frost Daggerfish to have 25% chance to throw a second projectile.");
        }
    }
}