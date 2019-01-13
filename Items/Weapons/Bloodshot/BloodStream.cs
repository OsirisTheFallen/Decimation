using Decimation.Buffs.Debuffs;
using Decimation.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items.Weapons.Bloodshot
{
    public class BloodStream : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Stream");
            Tooltip.SetDefault("Bathe your enemies in boiling blood.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.value = 20000;
            item.rare = 2;
            item.maxStack = 1;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType<BloodBeamFriendly>();
            item.shootSpeed = 5f;
            item.magic = true;
            item.damage = 14;
            item.mana = 1;
            item.useTime = 5;
            item.useAnimation = 5;
            item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType<Slimed>(), 300);
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Slimed>(), 300);
        }
    }
}