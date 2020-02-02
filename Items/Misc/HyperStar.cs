using Decimation.Core.Items;
using Decimation.Core.Util.Builder;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc
{
    internal class HyperStar : DecimationItem
    {
        public static readonly int ManaHealAmount = 10;

        protected override string ItemName => "Hyper Star";
        protected override bool IsClone => true;
        protected override string ItemTooltip => "Adds 10 to maximum mana";

        protected override void Init()
        {
            this.item.CloneDefaults(ItemID.ManaCrystal);
        }

        public override bool CanUseItem(Player player)
        {
            return player.statManaMax >= 200 && player.GetModPlayer<DecimationPlayer>().hyperStars < 10;
        }

        public override bool UseItem(Player player)
        {
            player.statManaMax2 += ManaHealAmount;
            player.GetModPlayer<DecimationPlayer>().hyperStars++;

            if (Main.myPlayer == player.whoAmI) player.ManaEffect(ManaHealAmount);

            return true;
        }

        protected override ModRecipe GetRecipe()
        {
            return new RecipeBuilder(this.mod, this)
                .WithIngredient(ItemID.ManaCrystal)
                .WithIngredient(ItemID.SoulofMight)
                .WithIngredient(ItemID.SoulofLight)
                .WithStation(TileID.MythrilAnvil)
                .Build();
        }
    }
}