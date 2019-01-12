using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Tiles;
using Decimation.Items.Misc.Souls;

namespace Decimation.Items.Armors.ScarabArmor
{
    [AutoloadEquip(EquipType.Legs)]
    class ScarabLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Solar Scarab Greaves");
            Tooltip.SetDefault("Immunity to fire blocks" +
                "\n20% increased movement and melee speed" + 
                "\nEnemies are more likely to target you");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.rare = 10;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed *= 1.20f;
            player.moveSpeed *= 1.20f;
            player.aggro += 250;
            player.fireWalk = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarFlareLeggings);
            recipe.AddIngredient(ItemID.BeetleLeggings);
            recipe.AddIngredient(ItemID.LunarOre, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 5);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddTile(mod.TileType<TitanForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
