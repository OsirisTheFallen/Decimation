using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Items.Misc;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Decimation.Items.Amulets
{
    internal class HealtyAmulet : Amulet
    {
        protected override string ItemName => "Healty Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Healer;

        protected override void InitAmulet()
        {
        }

        protected override void UpdateAmulet(Player player)
        {
            player.statManaMax2 += 10;
            player.statLifeMax2 += (int)(player.statLifeMax * 0.05f);

            if (player.GetModPlayer<DecimationPlayer>().isInCombat && player.GetModPlayer<DecimationPlayer>().enchantedHeartDropTime % 300 == 0)
                Item.NewItem(new Vector2(player.position.X, player.position.Y), mod.ItemType<EnchantedHeart>());
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, false);

            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddIngredient(ItemID.Gel, 5);

            return new List<ModRecipe> { recipe };
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+10 to maximum mana")
                .AddEffect("+5% to maximum life")
                .AddEffect("Drop enchanted hearts each 5 seconds when you are in combat")
                .AddEffect("Your lifesteal will be given to your allies (WIP)");
        }
    }
}
