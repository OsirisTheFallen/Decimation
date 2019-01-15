using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Dusts
{
    class Blood : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = 0.02f;
            dust.scale = 1f;
        }
    }
}
