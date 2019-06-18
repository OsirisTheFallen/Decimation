using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    class CreatorAmulet : Amulet
    {
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.CREATOR; }
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 32;
        }

        public override void UpdateAmulet(Player player)
        {
            player.blockRange += 2;
            player.tileSpeed *= 1.04f;
            player.meleeSpeed *= 1.04f;
            player.pickSpeed *= 1.03f;

            player.AddBuff(BuffID.NightOwl, 1);
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1, 1, 1);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<MinersAmulet>());
            recipe.AddIngredient(mod.ItemType<BuildersAmulet>());
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("+4% tile placement speed")
                .addEffect("+4% melee speed")
                .addEffect("+4% mining speed")
                .addEffect("+2 block interaction range")
                .addEffect("Provides light")
                .addEffect("Provides Night Owl buff");
        }
    }
}
