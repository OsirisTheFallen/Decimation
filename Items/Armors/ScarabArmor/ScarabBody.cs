using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Tiles;
using Decimation.Items.Misc.Souls;
using Decimation.Core.Items;
using Decimation.Core.Util;

namespace Decimation.Items.Armors.ScarabArmor
{
    [AutoloadEquip(EquipType.Body)]
    class ScarabBody : DecimationItem
    {
        protected override string ItemName => "Solar Scarab Shell";
        protected override string ItemTooltip => "Immunity to knockback" +
        "\n25% increased melee damages" +
        "\nEnemies are more likely to target you";

        protected override void Init()
        {
            width = 38;
            height = 22;
            rarity = Rarity.Red;

            item.maxStack = 1;
            item.defense = 45;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.25f;
            player.aggro += 250;
            player.noKnockback = true;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { ModContent.TileType<TitanForge>() }, false);

            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.BeetleShell);
            recipe.AddIngredient(ItemID.LunarOre, 20);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ModContent.ItemType<SoulofSpite>(), 5);
            recipe.AddIngredient(ItemID.LavaBucket);

            return new List<ModRecipe>() { recipe };
        }
    }
}
