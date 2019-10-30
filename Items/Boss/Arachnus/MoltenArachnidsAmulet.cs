using Decimation.Items.Misc;
using Decimation.Tiles;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Boss.Arachnus
{
    internal class MoltenArachnidsAmulet : DecimationItem
    {
        protected override string ItemName => "Molten Arachnid's Amulet";
        protected override string ItemTooltip => "Infuriates the guardian of the planet's core.";

        protected override void Init()
        {
            width = 32;
            height = 32;
            value = Item.buyPrice(0, 45);
            rarity = Rarity.Red;
            consumable = true;
            useStyle = 4;
            useAnimation = 30;
            useTime = 30;

            this.item.maxStack = 1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, this.mod.TileType<TitanForge>());

            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(this.mod.ItemType<Thermoplasm>());

            return recipe;
        }
    }
}