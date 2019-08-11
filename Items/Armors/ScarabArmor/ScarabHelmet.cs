using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Decimation.Tiles;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using Decimation.Buffs.Buffs;
using Decimation.Items.Misc.Souls;

namespace Decimation.Items.Armors.ScarabArmor
{
    [AutoloadEquip(EquipType.Head)]
    class ScarabHelmet : DecimationItem
    {
        protected override string ItemName => "Solar Scarab Helmet";

        protected override string ItemTooltip => "25 % increased melee critical hit chances" +
                                                 "\nEnemis are more likely to target you";

        protected override void Init()
        {
            width = 26;
            height = 26;
            rarity = Rarity.Red;

            item.maxStack = 1;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 25;
            player.aggro += 250;
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 1.05f, 0.95f, 0.55f);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == item.type && body.type == mod.ItemType<ScarabBody>() && legs.type == mod.ItemType<ScarabLeggings>())
                return true;
            return false;
        }

        public override void UpdateArmorSet(Player player)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();

            player.AddBuff(mod.BuffType<ScarabEndurance>(), 1);
            player.arrowDamage *= 1.05f;
            player.bulletDamage *= 1.05f;
            player.magicDamage *= 1.05f;
            player.meleeDamage *= 1.05f;
            player.minionDamage *= 1.05f;
            player.rangedDamage *= 1.05f;
            player.rocketDamage *= 1.05f;
            player.thrownDamage *= 1.05f;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.lavaImmune = true;

            player.setSolar = true;
            player.setBonus = "Summons scarabs to protect you, add 5% damages to each attacks, gives immunity to On Fire!, Cursed Inferno and Lava.";
            modPlayer.solarCounter++;
            int num11 = 240;
            if (modPlayer.solarCounter >= num11)
            {
                if (player.solarShields > 0 && player.solarShields < 3)
                {
                    for (int num12 = 0; num12 < 22; num12++)
                    {
                        if (player.buffType[num12] >= 170 && player.buffType[num12] <= 171)
                        {
                            player.DelBuff(num12);
                        }
                    }
                }
                if (player.solarShields < 3)
                {
                    player.AddBuff(170 + player.solarShields, 5, false);
                    for (int num13 = 0; num13 < 16; num13++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0f, 0f, 100, default(Color), 1f)];
                        dust.noGravity = true;
                        dust.scale = 1.7f;
                        dust.fadeIn = 0.5f;
                        Dust dust2 = dust;
                        dust2.velocity *= 5f;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                    }
                    modPlayer.solarCounter = 0;
                }
                else
                {
                    modPlayer.solarCounter = num11;
                }
            }
            for (int num14 = player.solarShields; num14 < 3; num14++)
            {
                player.solarShieldPos[num14] = Vector2.Zero;
            }
            for (int num15 = 0; num15 < player.solarShields; num15++)
            {
                player.solarShieldPos[num15] += player.solarShieldVel[num15];
                Vector2 value = ((float)player.miscCounter / 100f * 6.28318548f + (float)num15 * (6.28318548f / (float)player.solarShields)).ToRotationVector2() * 6f;
                value.X = (float)(player.direction * 20);
                player.solarShieldVel[num15] = (value - player.solarShieldPos[num15]) * 0.2f;
            }
            if (player.dashDelay >= 0)
            {
                player.solarDashing = false;
                player.solarDashConsumedFlare = false;
            }
            bool flag = player.solarDashing && player.dashDelay < 0;
            if (player.solarShields > 0 | flag)
            {
                player.dash = 3;
            }
        }


        protected override List<ModRecipe> GetAdditionalRecipes()
        {
            ModRecipe recipe = GetNewModRecipe(this, 1, new List<int>() { mod.TileType<TitanForge>() });

            recipe.AddIngredient(ItemID.SolarFlareHelmet);
            recipe.AddIngredient(ItemID.BeetleHelmet);
            recipe.AddIngredient(ItemID.LunarOre, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(mod.ItemType<SoulofSpite>(), 5);
            recipe.AddIngredient(ItemID.LavaBucket);

            return new List<ModRecipe>() { recipe };
        }
    }
}
