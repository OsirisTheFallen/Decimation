using Decimation.Buffs.Debuffs;
using Decimation.Core.Items;
using Decimation.Core.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Bloodshot
{
    internal class BloodStream : DecimationWeapon
    {
        protected override string ItemName => "Blood Stream";
        protected override string ItemTooltip => "Bathe your enemies in boiling blood.";
        protected override DamageType DamagesType => DamageType.MAGIC;
        protected override int Damages => 14;
        protected override string Projectile => "BloodBeamFriendly";


        protected override void InitWeapon()
        {
            width = 20;
            height = 20;
            value = Item.buyPrice(0, 2);
            rarity = Rarity.Green;
            useStyle = 5;
            shootSpeed = 5f;
            this.item.mana = 1;
            useTime = 5;
            useAnimation = 5;
            autoReuse = true;
            useSound = SoundID.Item34;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Slimed>(), 300);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Slimed>(), 300);
        }
    }
}