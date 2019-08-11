using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Decimation;
using Decimation.Items.Misc.Souls;
using Decimation.Items.Misc;
using Decimation.Tiles;

namespace Decimation.Items.Accessories
{
    internal class DeadeyesQuiver : DecimationAccessory
    {
        protected override string ItemName => "Deadeye's Quiver";

        protected override string ItemTooltip =>
            "Turns wooden arrows into ichor arrows and turns musket balls into chlorophyte bullets\n+16% ranged damage\n+15% arrow and bullet velocity\n15% chance not to consume ammo\n+2% increased ranged crit chance";

        protected override void InitAccessory()
        {
            width = 30;
            height = 30;
            rarity = Rarity.Red;
            item.value = Item.buyPrice(0, 15);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { mod.TileType<ChlorophyteAnvil>() }, false);
            recipe.AddIngredient(mod.ItemType<JestersQuiver>());
            //r.AddIngredient(mod.ItemType<SoulofKight>());
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(mod.ItemType<EndlessPouchofLife>());
            recipe.AddIngredient(mod.ItemType<RedThread>(), 5);
            recipe.AddIngredient(ItemID.FlaskofIchor, 5);
            recipe.AddIngredient(ItemID.BlackDye, 3);
            recipe.AddIngredient(ItemID.RedDye, 3);

            return new List<ModRecipe>() { recipe };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.16f;
            player.rangedCrit += 2;
            player.GetModPlayer<DecimationPlayer>().deadeyesQuiverEquipped = true;
        }
    }
}
