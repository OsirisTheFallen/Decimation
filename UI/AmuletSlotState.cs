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
        public AmuletSlot slot;

        public AmuletSlotState()
        {
            slot = new AmuletSlot(scale: Main.inventoryScale);

            slot.SetPadding(0);

            Append(slot);
        }

        public void UpdateAmulet(DecimationPlayer player)
        {
            slot.UpdateAmulet(player);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // TODO to improve
            if (Main.screenHeight > 660 && (Main.mapStyle != 1 || Main.screenHeight > 900))
            {
                slot.Left.Set(Main.screenWidth - 91f, 0f);
                slot.Top.Set(Main.screenHeight - 100f, 0f);
            }
            else
            {
                slot.Left.Set(Main.screenWidth - 234f, 0f);
                slot.Top.Set(Main.screenHeight - 152f, 0f);
            }
        }
    }
}
