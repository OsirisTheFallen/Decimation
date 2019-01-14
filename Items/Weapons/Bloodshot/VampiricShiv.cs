using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Bloodshot
{
    public class VampiricShiv : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampiric Shiv");
            Tooltip.SetDefault("Heal 10% of damages inflicted");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 100;
            item.rare = 1;
            item.crit = 4;
            item.maxStack = 999;
            item.melee = true;
            item.damage = 12;
            item.useStyle = 3;
            item.useTime = 12;
            item.useAnimation = 12;
            item.rare = 2;
            item.maxStack = 1;
            item.knockBack = 5;
            item.value = 20000;
            item.UseSound = SoundID.Item1;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int lifeSteal = (int)(damage * 0.1f);

            player.lifeSteal += lifeSteal;
            player.HealEffect(lifeSteal);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            int lifeSteal = (int)(damage * 0.1f);

            player.lifeSteal += lifeSteal;
            player.HealEffect(lifeSteal);
        }
    }
}