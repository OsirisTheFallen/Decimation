using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Bloodshot
{
    internal class VampiricShiv : DecimationWeapon
    {
        protected override string ItemName => "Vampiric Shiv";
        protected override string ItemTooltip => "Heal 10% of damages inflicted";
        protected override int Damages => 12;

        protected override void InitWeapon()
        {
            width = 20;
            height = 20;
            criticalStrikeChance = 4;
            useStyle = 3;
            useTime = 12;
            useAnimation = 12;
            rarity = Rarity.Green;
            knockBack = 5;
            value = Item.buyPrice(0, 2);
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
