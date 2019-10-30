using System.Collections.Generic;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class EndlessPouchofLife : DecimationAccessory
    {
        protected override string ItemName => "Endless Pouch of Life";

        protected override string ItemTooltip => "Cancels ammunition consumption." +
                                                 "\nIncrease maximum life by 15" +
                                                 "\nChange ammunitions in Chlorophyte Bullets \nProvide a life regeneration boost \n+8% ranged damage \n+15% critical strike chance";

        protected override void InitAccessory()
        {
            width = 24;
            height = 32;
            rarity = Rarity.Lime;
            this.item.value = Item.buyPrice(0, 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 5;
            player.rangedDamage += 0.08f;
            player.rangedCrit += 5;
            player.statLifeMax2 += 15;
            Main.LocalPlayer.GetModPlayer<DecimationPlayer>().endlessPouchofLifeEquipped = true;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {this.mod.TileType<ChlorophyteAnvil>()});

            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddIngredient(this.mod.ItemType<EnergyFocuser>());
            recipe.AddIngredient(ItemID.EndlessMusketPouch);
            recipe.AddIngredient(ItemID.SoulofSight, 50);

            return new List<ModRecipe> {recipe};
        }
    }
}