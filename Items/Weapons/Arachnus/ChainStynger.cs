using Decimation.Buffs.Debuffs;
using Decimation.Core.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Arachnus
{
    internal class ChainStynger : DecimationWeapon
    {
        protected override string ItemName => "Chain Stynger";
        protected override string ItemTooltip => "Unleashe molten ashes upon your foes.";
        protected override bool IsClone => true;
        protected override DamageType DamagesType => DamageType.RANGED;
        protected override int Damages => 800;
        protected override string Projectile => "MoltenStyngerBolt";

        protected override void InitWeapon()
        {
            this.item.CloneDefaults(ItemID.Stynger);

            shootSpeed = 9f;
            this.item.crit *= 2;
            this.item.knockBack *= 2;
            this.item.rare = 10;
            this.item.value = Item.buyPrice(0, 45);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Singed>(), 480);
        }
    }
}