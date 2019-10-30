using System;
using Decimation.Core;
using Decimation.Core.Amulets.Synergy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Decimation.Synergies
{
    internal class MarbleAmuletSynergy : AmuletSynergyAdapter
    {

        public override void OnShoot(DecimationModPlayer modPlayer, Item item, ref Vector2 position, ref float speedX, ref float speedY,
            ref int projectileType, ref int damages, ref float knockBack)
        {
            int itemType = modPlayer.player.HeldItem.type;

            if (Main.rand.NextBool(4))
            {
                if (itemType == ItemID.Javelin || itemType == ItemID.Shuriken || itemType == ItemID.ThrowingKnife || itemType == ItemID.StarAnise || itemType == ItemID.BoneJavelin || itemType == ItemID.PoisonedKnife || itemType == ItemID.FrostDaggerfish)
                {
                    // Creation of the second projectile, with 10 degrees (0.174533 rad) rotation
                    const double angle = 0.174533d;
                    float x2 = (float)(Math.Cos(angle) * speedX - Math.Sin(angle) * speedY);
                    float y2 = (float)(Math.Sin(angle) * speedX + Math.Cos(angle) * speedY);

                    Projectile.NewProjectile(position, new Vector2(x2, y2), projectileType, damages, knockBack);
                }
            }
        }

    }
}
