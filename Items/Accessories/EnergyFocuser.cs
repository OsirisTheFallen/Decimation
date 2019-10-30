using System.Collections.Generic;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class EnergyFocuser : DecimationAccessory
    {
        protected override string ItemName => "Energy Focuser";
        protected override string ItemTooltip => "Opens one's chakra points.";

        protected override void InitAccessory()
        {
            width = 62;
            height = 46;
            rarity = Rarity.Green;
            this.item.value = Item.buyPrice(0, 0, 0, 10);
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.MythrilAnvil}, true);

            recipe.AddIngredient(this.mod.ItemType<EnchantedFocuser>());
            recipe.AddIngredient(ItemID.PixieDust, 40);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(this.mod.ItemType<Focuser>());

            return new List<ModRecipe> {recipe};
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.10f;
            player.magicDamage += 0.10f;
            player.rangedCrit += 05;
            player.meleeCrit += 05;
            player.magicCrit += 05;
            player.thrownCrit += 05;
            player.manaRegen += 2;
            player.statManaMax2 += 20;
            player.statLifeMax2 += 20;
            player.lifeRegen += 2;
            player.meleeDamage += 0.04f;
        }
    }
}