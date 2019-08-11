using System;
using Decimation.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    internal class TitanicPike : DecimationWeapon
    {
        protected override string ItemName => "Titanic Pike";
        protected override int Damages => 120;
        protected override string Projectile => "TitanicPikeProjectile";

        protected override void InitWeapon()
        {
            criticalStrikeChance = 14;
            knockBack = 12;
            useStyle = 5;
            item.value = Item.buyPrice(gold: 45);
            rarity = Rarity.LightPurple;
            item.noUseGraphic = true;
            item.useTurn = true;
            autoReuse = true;
            width = 94;
            height = 94;
            useAnimation = 18;
            useTime = 24;
            shootSpeed = 3.7f;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        protected override ModRecipe GetRecipe()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, mod.TileType<TitanForge>());

            recipe.AddIngredient(mod.ItemType("TitaniteBar"), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);

            return recipe;
        }
    }
}
