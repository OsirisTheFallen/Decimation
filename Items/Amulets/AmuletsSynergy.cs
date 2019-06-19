using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Items.Amulets
{
    class AmuletsSynergy
    {
        private Decimation decimation;

        public AmuletsSynergy(Decimation decimation)
        {
            this.decimation = decimation;
        }

        public void OnHitPlayer(Item amulet, DecimationPlayer modPlayer, ref int damages)
        {
            if (amulet.type == decimation.ItemType<GraniteAmulet>())
            {
                if (modPlayer.hasShield && Main.rand.NextBool(10))
                {
                    damages = 0;
                    CombatText.NewText(modPlayer.player.getRect(), Color.MediumPurple, "Blocked");
                }
            }
        }

        public void OnShoot(Item amulet, DecimationPlayer modPlayer, Item item, ref Vector2 position, ref float speedX,
            ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int itemType = item.type;

            if (amulet.type == decimation.ItemType<MarbleAmulet>() && Main.rand.NextBool(4))
            {
                if (itemType == ItemID.Javelin || itemType == ItemID.Shuriken || itemType == ItemID.ThrowingKnife || itemType == ItemID.StarAnise || itemType == ItemID.BoneJavelin || itemType == ItemID.PoisonedKnife || itemType == ItemID.FrostDaggerfish)
                {
                    // Creation of the second projectile, with 10 degrees (0.174533 rad) rotation
                    const double angle = 0.174533d;
                    float x2 = (float)(Math.Cos(angle) * speedX - Math.Sin(angle) * speedY);
                    float y2 = (float)(Math.Sin(angle) * speedX + Math.Cos(angle) * speedY);

                    Projectile.NewProjectile(position, new Vector2(x2, y2), type, damage, knockBack);
                }
            }
        }

        public void Update(Item amulet, DecimationPlayer modPlayer)
        {
            if (amulet.type == decimation.ItemType<FireAmulet>() && modPlayer.hasLavaCharm)
            {
                modPlayer.player.lavaMax += 500;
            }
        }
    }

    public class AmuletDetection : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            if (item.type == ItemID.CobaltShield || item.type == ItemID.AnkhShield || item.type == ItemID.PaladinsShield || item.type == ItemID.ObsidianShield) modPlayer.hasShield = true;
            if (item.type == ItemID.LavaCharm) modPlayer.hasLavaCharm = true;
        }
    }
}
