using System.Collections.Generic;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories
{
    internal class CrystalSkull : DecimationAccessory
    {
        protected override string ItemName => "Crystal Skull";
        protected override string ItemTooltip => "It seems that this skull has been enchanted.";

        protected override void InitAccessory()
        {
            width = 24;
            height = 24;
            rarity = Rarity.Green;
            this.item.value = Item.buyPrice(0, 0, 0, 10);
            this.item.defense = 2;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> {TileID.GlassKiln}, true);

            recipe.AddIngredient(ItemID.ObsidianSkull);
            recipe.AddIngredient(ItemID.CrystalShard, 5);
            recipe.AddRecipeGroup("AnyGem", 4);
            recipe.AddIngredient(ItemID.Glass, 6);

            return new List<ModRecipe> {recipe};
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Thorns, 1);
            Lighting.AddLight(player.Center, new Vector3(0.9f * 0.6f, 0.9f * 0.1f, 0.9f));
        }
    }
}