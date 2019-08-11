using Decimation.Items.Misc.CondensedSouls;
using Decimation.Items.Ores;
using Decimation.Projectiles;
using Decimation.Tiles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    internal class ScarabWings : DecimationAccessory
    {
        protected override string ItemName => "Scarab Wings";
        protected override string ItemTooltip => "Blessed by the sun";

        protected override void InitAccessory()
        {
            width = 26;
            height = 26;
            rarity = Rarity.Red;

            item.value = Item.buyPrice(0, 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 240;
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);

            if (player.wingTime % 2 == 1)
                Projectile.NewProjectile(player.Center, new Microsoft.Xna.Framework.Vector2(0, 0), mod.ProjectileType<Ember>(), 25, 5, player.whoAmI);
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 9f;
            acceleration *= 2.5f;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { mod.TileType<TitanForge>() }, false);

            recipe.AddIngredient(ItemID.BeetleWings);
            recipe.AddIngredient(ItemID.WingsSolar);
            recipe.AddIngredient(mod.ItemType<CondensedSpite>(), 2);
            recipe.AddIngredient(mod.ItemType<DenziumBar>(), 5);

            return new List<ModRecipe>() { recipe };
        }
    }
}
