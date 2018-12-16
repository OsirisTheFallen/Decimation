using Decimation.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Decimation
{
    public class DecimationPlayer : ModPlayer
    {
        public bool closeToEnchantedAnvil = false;
        public bool jestersQueverEquiped = false;
        public bool deadeyesQuiverEquipped = false;
        public bool endlessPouchofLifeEquipped = false;

        public Item amuletSlotItem;

        private bool wasJumping = false;

        public override void Initialize()
        {
            amuletSlotItem = new Item();
            amuletSlotItem.SetDefaults(0, true);
        }

        public override void ResetEffects()
        {
            closeToEnchantedAnvil = false;
            jestersQueverEquiped = false;
            deadeyesQuiverEquipped = false;
            endlessPouchofLifeEquipped = false;
        }

        public override TagCompound Save()
        {
            return new TagCompound() {
                {"amuletSlotItem", ItemIO.Save(amuletSlotItem) }
            };
        }

        public override void Load(TagCompound tag)
        {
            amuletSlotItem = ItemIO.Load(tag.GetCompound("amuletSlotItem"));

            // Load to slot
            Decimation.amuletSlotState.LoadItem(amuletSlotItem);
        }

        public override void PostUpdate()
        {
            // Not working
            /*Keys[] pressedKeys = Main.keyState.GetPressedKeys();

            for (int j = 0; j < pressedKeys.Length; j++)
            {
                string a = string.Concat(pressedKeys[j]);
                
                if (a == Main.cJump)
                {
                    if (!wasJumping && Main.LocalPlayer.wingTime == Main.LocalPlayer.wingTimeMax)
                    {
                        //Main.NewText(Player.jumpHeight);
                    }
                    wasJumping = true;
                    break;
                }
                wasJumping = false;
            }

            Player.jumpHeight = 0;
            Player.jumpSpeed = 0f;*/
        }

        // FIND AN ALTERNATIVE! THIS METHOD DOESN'T GET CALLED WITH ALL THE WEAPONS
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile toCheck = Main.projectile[type];

            if (jestersQueverEquiped && toCheck.arrow)
                type = ProjectileID.JestersArrow;

            if (endlessPouchofLifeEquipped && References.bullets.Contains(type))
                type = ProjectileID.ChlorophyteBullet;

            if (deadeyesQuiverEquipped && (toCheck.arrow || References.bullets.Contains(type)))
            {
                if (toCheck.arrow)
                    type = ProjectileID.IchorArrow;
                else
                    type = ProjectileID.ChlorophyteBullet;

                speedX *= 1.15f;
                speedY *= 1.15f;
            }

            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (deadeyesQuiverEquipped && (ammo.ammo == AmmoID.Arrow || ammo.ammo == AmmoID.Bullet) && Main.rand.Next(20) > 3)
                return false;
            if (endlessPouchofLifeEquipped && ammo.ammo == AmmoID.Bullet)
                return false;

            return base.ConsumeAmmo(weapon, ammo);
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit)
        {
            if (target.HasBuff(mod.BuffType<ScarabEndurance>())) player.AddBuff(BuffID.OnFire, 300);
        }
    }
}
