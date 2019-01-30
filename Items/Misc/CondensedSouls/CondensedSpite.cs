using Decimation.Items.Misc.Souls;
using Decimation.Tiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Misc.CondensedSouls
{
    class CondensedSpite : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("his soul emanates a primal sense of hatred");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.SoulofSight);
            item.width = 44;
            item.height = 44;
            item.value = 500000;
            item.rare = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 50);
            recipe.AddTile(mod.TileType<ChlorophyteAnvil>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
