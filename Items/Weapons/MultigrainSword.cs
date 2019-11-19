using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class MultigrainSword : DecimationWeapon
    {
        protected override string ItemName => "Multigrain Sword";
        protected override string ItemTooltip => "Smells like honeysuckle";
        protected override int Damages => 30;
        protected override string Projectile => "Stinger";

        protected override void InitWeapon()
        {
            width = 36;
            height = 37;
            useTime = 26;
            useAnimation = 26;
            knockBack = 5;
            value = Item.buyPrice(0, 0, 40);
            rarity = Rarity.Green;
            criticalStrikeChance = 4;
            autoReuse = true;
            this.item.expert = false;
            shootSpeed = 10f;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, TileID.Anvils, true);

            recipe.AddIngredient(ItemID.CactusSword);
            recipe.AddIngredient(ItemID.Pumpkin, 15);
            recipe.AddIngredient(ItemID.Acorn, 5);
            recipe.AddIngredient(ItemID.Hay, 15);
            recipe.AddIngredient(ModContent.ItemType<TheGreatwoodSword>());

            return recipe;
        }
    }
}