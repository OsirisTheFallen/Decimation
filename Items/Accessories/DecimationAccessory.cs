using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Decimation.Items.Accessories
{
    internal abstract class DecimationAccessory : DecimationItem
    {
        protected abstract void InitAccessory();

        protected sealed override void Init()
        {
            item.accessory = true;
            item.maxStack = 1;
        }
    }
}
