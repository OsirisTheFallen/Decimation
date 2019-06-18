using System;
using System.Collections.Generic;
using Decimation.Buffs.Debuffs;
using Decimation.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Decimation.Items.Amulets
{
    public class FireAmulet : Amulet
    {
        public override AmuletClasses AmuletClass
        {
            get { return AmuletClasses.MELEE; }
        }

        public override void SetAmuletDefaults()
        {
            item.width = 28;
            item.height = 34;
        }

        public override void UpdateAmulet(Player player)
        {
            player.meleeDamage *= 1.03f;
            player.meleeSpeed *= 1.03f;
            player.meleeCrit += 3;
            player.lavaMax += 420;

            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            modPlayer.amuletsBuff = mod.BuffType<Singed>();
            modPlayer.amuletsBuffChances = 4;
            modPlayer.amuletsBuffTime = 300;
        }

        public override AmuletTooltip GetAmuletTooltips()
        {
			return new AmuletTooltip(this)
				.addEffect("+3% melee speed")
				.addEffect("+3% melee damages")
				.addEffect("+3% melee critical strike chances")
				.addEffect("+7 seconds of immunity to lava")
				.addEffect("+4% chances to inflict \"Slimed!\" debuff to ennemies on strikes")
				.addSynergy("The lava charm grant an additional 5 seconds of lava immunity");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<RedHotShackle>());
            recipe.AddIngredient(ItemID.Obsidian, 6);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}