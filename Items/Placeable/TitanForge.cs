﻿using Decimation.Items.Misc.Souls;
using Decimation.Core.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Placeable
{
    internal class TitanForge : DecimationPlaceableItem
    {
        protected override string ItemName => "Titan Forge";
        protected override string ItemTooltip => "Used to craft powerful weapons and armor.";
        protected override int Tile => this.mod.TileType<Tiles.TitanForge>();

        protected override void InitPlaceable()
        {
            width = 20;
            height = 20;
            this.item.maxStack = 1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, this.mod.TileType<Tiles.ChlorophyteAnvil>(), true);

            recipe.AddIngredient(ItemID.AdamantiteForge);
            recipe.AddIngredient(ItemID.Autohammer);
            recipe.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.AddIngredient(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(this.mod.ItemType<SoulofSpite>(), 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);

            return recipe;
        }
    }
}