using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    internal class EnchantedMushroom : DecimationPotion
    {
        protected override string ItemName => "Enchanted Mushroom";
        protected override string ItemTooltip => "Cures poison \nGives Happy! buff";
        protected override int BuffType => BuffID.Sunflower;
        protected override int BuffTime => 7200;
        protected override int HealLife => 90;

        protected override void InitPotion()
        {
            value = Item.buyPrice(0, 0, 15);
        }

        public override bool UseItem(Player player)
        {
            player.ClearBuff(BuffID.Poisoned);
            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AlchemyTable);

            recipe.AddIngredient(ItemID.Mushroom);
            recipe.AddIngredient(ItemID.GlowingMushroom);
            recipe.AddIngredient(ItemID.BottledHoney);
            recipe.AddIngredient(ItemID.FallenStar);

            return recipe;
        }
    }
}