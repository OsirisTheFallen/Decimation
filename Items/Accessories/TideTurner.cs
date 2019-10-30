using System.Collections.Generic;
using Decimation.Items.Ores;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class TideTurner : DecimationAccessory
    {
        protected override string ItemName => "Tide Turner";

        protected override string ItemTooltip => "Not one of ya’s going to survive this!\n" +
                                                 "Deals the same amount of damage as held item\n" +
                                                 "Increase underwater mobility\n" +
                                                 "+10% chance to dodge attacks when charging";

        protected override void InitAccessory()
        {
            width = 46;
            height = 36;
            rarity = Rarity.Rainbow;
            item.value = Item.buyPrice(0, 3);
            item.defense = 3;
            item.shieldSlot = 5;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();

            item.damage = player.HeldItem.damage;
            modPlayer.dashDamages = player.HeldItem.damage;
            modPlayer.dash = 2;

            player.accFlipper = true;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {mod.TileType<ChlorophyteAnvil>()}, false);

            recipe.AddIngredient(ItemID.EoCShield);
            recipe.AddIngredient(ItemID.Coral, 10);
            recipe.AddIngredient(mod.ItemType<DenziumBar>(), 5);

            return new List<ModRecipe> {recipe};
        }
    }
}