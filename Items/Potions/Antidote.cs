using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Potions
{
    // Not an actual potion, since it doesn't give the player any buff.
    internal class Antidote : DecimationItem
    {
        protected override string ItemName => "Antidote";
        protected override string ItemTooltip => "Cure poison and venom.";

        protected override void Init()
        {
            width = 20;
            height = 20;
            this.item.maxStack = 30;
            consumable = true;
            useAnimation = 17;
            useTime = 17;
            this.item.useTurn = true;
            useStyle = 2;
        }

        public override bool UseItem(Player player)
        {
            player.ClearBuff(BuffID.Poisoned);
            player.ClearBuff(BuffID.Venom);
            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AlchemyTable);

            recipe.AddIngredient(ItemID.Waterleaf);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(ItemID.BottledHoney);

            return recipe;
        }
    }
}