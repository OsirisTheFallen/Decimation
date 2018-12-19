using System;
using System.Collections.Generic;
using Decimation.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Decimation.Items.Accessories
{
    public class SlimeAmulet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Amulet");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 10;
            item.rare = 2;

            Decimation.amulets.Add(item.type);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.anyIronBar = true;
            recipe.AddIngredient(ItemID.RoyalGel);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(null, "SlimeBracelet");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine amuletClass = new TooltipLine(mod, "Class", "SUMMONER")
            {
                overrideColor = ChatManager.WaveColor(Color.Fuchsia)
            };
            TooltipLine effect1 = new TooltipLine(mod, "Effect", "Makes slimes friendly")
            {
                overrideColor = Color.ForestGreen
            };
            TooltipLine effect2 = new TooltipLine(mod, "Effect", "+3% minion damages")
            {
                overrideColor = Color.ForestGreen
            };
            TooltipLine effect3 = new TooltipLine(mod, "Effect", "+3% minion knockback")
            {
                overrideColor = Color.ForestGreen
            };
            TooltipLine effect4 = new TooltipLine(mod, "Effect", "+4% chances to inflict \"Slimed!\" debuff to attackers for 5 seconds")
            {
                overrideColor = Color.ForestGreen
            };

            tooltips.Add(amuletClass);
            tooltips.Add(effect1);
            tooltips.Add(effect2);
            tooltips.Add(effect3);
            tooltips.Add(effect4);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Player player = Main.LocalPlayer;

            player.minionDamage *= 0.03f;
            player.minionKB *= 0.03f;
            player.npcTypeNoAggro[1] = true;
            player.npcTypeNoAggro[16] = true;
            player.npcTypeNoAggro[59] = true;
            player.npcTypeNoAggro[71] = true;
            player.npcTypeNoAggro[81] = true;
            player.npcTypeNoAggro[121] = true;
            player.npcTypeNoAggro[122] = true;
            player.npcTypeNoAggro[138] = true;
            player.npcTypeNoAggro[147] = true;
            player.npcTypeNoAggro[183] = true;
            player.npcTypeNoAggro[184] = true;
            player.npcTypeNoAggro[187] = true;
            player.npcTypeNoAggro[204] = true;
            player.npcTypeNoAggro[225] = true;
            player.npcTypeNoAggro[244] = true;
            player.npcTypeNoAggro[302] = true;
            player.npcTypeNoAggro[304] = true;
            player.npcTypeNoAggro[333] = true;
            player.npcTypeNoAggro[334] = true;
            player.npcTypeNoAggro[335] = true;
            player.npcTypeNoAggro[336] = true;
            player.npcTypeNoAggro[535] = true;
            player.npcTypeNoAggro[537] = true;

            player.GetModPlayer<DecimationPlayer>().slimeAmulet = true;
        }
    }
}