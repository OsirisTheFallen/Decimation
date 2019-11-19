using System.Collections.Generic;
using Decimation.Buffs.Debuffs;
using Decimation.Items.Accessories;
using Decimation.Core.Amulets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class SlimeAmulet : Amulet
    {
        protected override string ItemName => "Slime Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Summoner;

        protected override void InitAmulet()
        {
            width = 24;
            height = 24;
        }

        protected override void UpdateAmulet(Player player)
        {
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

            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            modPlayer.amuletsBuff = ModContent.BuffType<Singed>();
            modPlayer.amuletsBuffChances = 4;
            modPlayer.amuletsBuffTime = 300;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, true);

            recipe.AddIngredient(ItemID.RoyalGel);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ModContent.ItemType<SlimeBracelet>());

            return new List<ModRecipe> { recipe };
        }

        protected override void GetAmuletTooltip(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("Makes slimes friendly")
                .AddEffect("+3% minion damages")
                .AddEffect("+3% minion knockback")
                .AddEffect("+4% chances to inflict \"Slimed!\" debuff to enemies on strikes")
                .AddSynergy("Causes summoned Baby Slimes to shoot two slime spikes in a V formation, each 5 seconds.");
        }
    }

    public class SlimeAmuletSynergy : GlobalProjectile
    {
        private const int SpikeInterval = 300;

        private int _spikeIntervalCounter;
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.BabySlime)
            {
                if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>().AmuletSlotItem.type ==
                    ModContent.ItemType<SlimeAmulet>())
                {
                    if (_spikeIntervalCounter > SpikeInterval)
                    {
                        // Projectile
                        Projectile proj1 = Projectile.NewProjectileDirect(projectile.Center, new Vector2(-1, -2.5f), ProjectileID.JungleSpike, 10, 10);
                        proj1.friendly = true;
                        proj1.hostile = false;

                        Projectile proj2 = Projectile.NewProjectileDirect(projectile.Center, new Vector2(1, -2.5f), ProjectileID.JungleSpike, 10, 10);
                        proj2.friendly = true;
                        proj2.hostile = false;

                        _spikeIntervalCounter = 0;
                    }

                    _spikeIntervalCounter++;
                }
            }

            base.AI(projectile);
        }
    }
}