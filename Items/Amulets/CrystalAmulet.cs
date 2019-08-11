using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Amulets
{
    internal class CrystalAmulet : Amulet
    {
        protected override string ItemName => "Crystal Amulet";
        public override AmuletClasses AmuletClass => AmuletClasses.Mage;

        protected override void InitAmulet()
        {
            height = 32;
        }

        protected override void UpdateAmulet(Player player)
        {
            player.statManaMax2 += 5;
            player.magicDamage *= 1.03f;
            player.magicCrit += 3;
        }

        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int> { TileID.TinkerersWorkbench }, false);

            recipe.AddRecipeGroup("AnyGem", 2);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddIngredient(ItemID.ManaRegenerationBand);

            return new List<ModRecipe> { recipe };
        }

        protected override void SetAmuletTooltips(ref AmuletTooltip tooltip)
        {
            tooltip
                .AddEffect("+5 maximum mana")
                .AddEffect("+3% magic damages")
                .AddEffect("+3% magic critical strike chances")
                .AddEffect("+4% chances to shoot out a burst of crystal shards when taking damages")
                .AddSynergy("Causes all basics staffs to burst into a quad spread of crystals on hit, dealing a small amount of extra damages");
        }
    }

    public class CrystalAmuletSynergy : GlobalProjectile
    {
        private static readonly List<int> Bolts = new List<int>()
        {
            ProjectileID.AmethystBolt,
            ProjectileID.DiamondBolt,
            ProjectileID.EmeraldBolt,
            ProjectileID.RubyBolt,
            ProjectileID.SapphireBolt,
            ProjectileID.TopazBolt
        };

        private const double BaseAngle = Math.PI / 2;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem.type == Decimation.Instance.ItemType<CrystalAmulet>() && Bolts.Contains(projectile.type))
            {
                // Create 4 crystal sparks in 4 different direction
                for (int i = 1; i <= 4; i++)
                {
                    double angle = BaseAngle * i;
                    float velocityX = (float)(Math.Cos(angle) - Math.Sin(angle)) * 2f;
                    float velocityY = (float)(Math.Sin(angle) + Math.Cos(angle)) * 2f;
                    Projectile spark = Projectile.NewProjectileDirect(projectile.Center,
                        new Vector2(velocityX, velocityY), ProjectileID.CrystalShard, 20, 5, Main.LocalPlayer.whoAmI);
                    spark.hostile = false;
                    spark.friendly = true;
                }
            }
        }
    }
}
