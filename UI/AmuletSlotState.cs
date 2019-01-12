using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;

namespace Decimation.UI
{
    public class AmuletSlotState : UIState
    {
        AmuletSlot amuletSlot;

        public AmuletSlotState()
        {
            amuletSlot = new AmuletSlot(scale: Main.inventoryScale);

            amuletSlot.SetPadding(0);

            Append(amuletSlot);
        }

        public void UpdateAmulet()
        {
            amuletSlot.UpdateAmulet();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Main.screenHeight > 660 && (Main.mapStyle != 1 || Main.screenHeight > 900))
            {
                amuletSlot.Left.Set(Main.screenWidth - 91f, 0f);
                amuletSlot.Top.Set(Main.screenHeight - 100f, 0f);
            }
            else
            {
                amuletSlot.Left.Set(Main.screenWidth - 234f, 0f);
                amuletSlot.Top.Set(Main.screenHeight - 152f, 0f);
            }
        }

        public void LoadItem(Item item)
        {
            amuletSlot.LoadItem(item);
        }
    }
}
