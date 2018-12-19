using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Tiles;

namespace Decimation.Items.Armors.ScarabArmor
{
    [AutoloadEquip(EquipType.Body)]
    class ScarabBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Solar Scarab Shell");
            Tooltip.SetDefault("Immunity to knockback" +
                "\n25% increased melee damages" +
                "\nEnnemis are more likely to target you");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 22;
            item.rare = 10;
            item.defense = 45;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.25f;
            player.aggro += 250;
            player.noKnockback = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.BeetleShell);
            recipe.AddIngredient(ItemID.LunarOre, 20);
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
