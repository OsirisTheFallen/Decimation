using Decimation.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    public class SlimeAmulet : Amulet
    {
        public override AmuletClasses AmuletClass => AmuletClasses.SUMMONER;

        public override void SetAmuletDefaults()
        {
            item.width = 24;
            item.height = 24;
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

        public override AmuletTooltip GetAmuletTooltips()
        {
            return new AmuletTooltip(this)
                .addEffect("Makes slimes friendly")
                .addEffect("+3% minion damages")
                .addEffect("+3% minion knockback")
                .addEffect("+4% chances to inflict \"Slimed!\" debuff to enemies on strikes")
                .addSynergy("Causes summoned Baby Slimes to shoot two slime spikes in a V formation, each 5 seconds.");
        }

        public override void UpdateAmulet(Player player)
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
            modPlayer.amuletsBuff = mod.BuffType<Singed>();
            modPlayer.amuletsBuffChances = 4;
            modPlayer.amuletsBuffTime = 300;
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
                if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem.type ==
                    Decimation.Instance.ItemType<SlimeAmulet>())
                {
                    if (_spikeIntervalCounter > SpikeInterval)
                    {
                        // Shoot
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