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
    [AutoloadEquip(EquipType.Legs)]
    class ScarabLeggings : DecimationItem
    {
        protected override string ItemName => "Solar Scarab Greaves";

        protected override string ItemTooltip => "Immunity to fire blocks" +
                                                 "\n20% increased movement and melee speed" +
                                                 "\nEnemies are more likely to target you";

        protected override void Init()
        {
            width = 26;
            height = 18;
            rarity = Rarity.Red;

            item.maxStack = 1;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed *= 1.20f;
            player.moveSpeed *= 1.20f;
            player.aggro += 250;
            player.fireWalk = true;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { mod.TileType<TitanForge>() });

            recipe.AddIngredient(ItemID.SolarFlareLeggings);
            recipe.AddIngredient(ItemID.BeetleLeggings);
            recipe.AddIngredient(ItemID.LunarOre, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 5);
            recipe.AddIngredient(ItemID.LavaBucket);

            return new List<ModRecipe>() { recipe };
        }
    }
}
