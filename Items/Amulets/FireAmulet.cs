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
    internal class FireAmulet : Amulet
    {
        protected override string ItemName => "Fire Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Melee;

        protected override void InitAmulet()
        {
            height = 34;
        }

        protected override void UpdateAmulet(Player player)
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

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, false);

            recipe.AddIngredient(mod.ItemType<RedHotShackle>());
            recipe.AddIngredient(ItemID.Obsidian, 6);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Gel, 20);

            return new List<ModRecipe> { recipe };
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+3% melee speed")
                .AddEffect("+3% melee damages")
                .AddEffect("+3% melee critical strike chances")
                .AddEffect("+7 seconds of immunity to lava")
                .AddEffect("+4% chances to inflict \"Slimed!\" debuff to ennemies on strikes")
                .AddSynergy("The lava charm grant an additional 5 seconds of lava immunity");
        }
    }
}