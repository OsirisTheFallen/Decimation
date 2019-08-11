﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class EnchantedAnvil : DecimationPlaceableItem
    {
        protected override string ItemName => "Enchanted Anvil";
        protected override int Tile => mod.TileType<Tiles.EnchantedAnvil>();

        protected override void InitPlaceable()
        {
            width = 20;
            height = 20;
            item.maxStack = 1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.AdamantiteForge, true);

            recipe.AddIngredient(ItemID.MythrilAnvil, 1);
            recipe.AddIngredient(ItemID.IronAnvil, 1);
            recipe.AddIngredient(mod.ItemType("SoulofLife"), 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);

            return recipe;
        }
    }
}
