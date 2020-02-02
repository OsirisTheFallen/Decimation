using Decimation.Core.Items;
using Decimation.Core.Util.Builder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    internal class SoulFruit : DecimationItem
    {
        public static readonly int LifeHealAmount = 5;

        protected override string ItemName => "Soul Fruit";
        protected override bool IsClone => true;
        protected override string ItemTooltip => "Adds 5 to maximum life";

        protected override void Init()
        {
            this.item.CloneDefaults(ItemID.LifeFruit);
            this.item.width = 34;
            this.item.height = 46;
        }

        public override bool CanUseItem(Player player)
        {
            return player.statLifeMax >= 500 && player.GetModPlayer<DecimationPlayer>().soulFruits < 10;
        }

        public override bool UseItem(Player player)
        {
            player.statLifeMax2 += LifeHealAmount;
            player.GetModPlayer<DecimationPlayer>().soulFruits++;

            if (Main.myPlayer == player.whoAmI) player.HealEffect(LifeHealAmount);

            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            return new RecipeBuilder(this.mod, this)
                .WithIngredient(ItemID.LifeFruit)
                .WithIngredient(ItemID.Ectoplasm, 5)
                .WithStation(TileID.MythrilAnvil)
                .Build();
        }
    }
}