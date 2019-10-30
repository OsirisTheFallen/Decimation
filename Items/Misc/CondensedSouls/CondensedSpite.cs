using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.CondensedSouls
{
    internal class CondensedSpite : DecimationItem
    {
        protected override string ItemName => "Condensed Spite";
        protected override string ItemTooltip => "his soul emanates a primal sense of hatred";
        protected override bool IsClone => true;
        protected override DrawAnimation Animation => new DrawAnimationVertical(5, 4);

        protected override void Init()
        {
            this.item.CloneDefaults(ItemID.SoulofSight);

            width = 44;
            height = 44;
            value = Item.buyPrice(0, 50);
            rarity = Rarity.Red;

            ItemID.Sets.AnimatesAsSoul[this.item.type] = true;
            ItemID.Sets.ItemIconPulse[this.item.type] = true;
            ItemID.Sets.ItemNoGravity[this.item.type] = true;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, this.mod.TileType<ChlorophyteAnvil>());

            recipe.AddIngredient(this.mod.ItemType<SoulofSpite>(), 50);

            return recipe;
        }
    }
}