using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons
{
    class TitanicLongsword : ModItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 145;
            item.useTime = 21;
            item.useAnimation = 21;
            item.crit = 14;
            item.knockBack = 7;
            item.useStyle = 1;
            item.value = Item.buyPrice(gold: 45);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.width = 84;
            item.height = 84;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Amnesia"), 480);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TitaniteBar"), 12);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddTile(mod.TileType("TitanForge"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
