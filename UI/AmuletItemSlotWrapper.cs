using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Decimation.UI
{
    class AmuletItemSlotWrapper : UIElement
    {
        internal Item item;
        int context;
        float scale;
        internal Func<Item, bool> validItem;

        public AmuletItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            this.context = context;
            this.scale = scale;
            this.item = new Item();
            item.SetDefaults(0);

            this.Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
            this.Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (validItem == null || validItem(Main.mouseItem))
                {
                    // Handle handles all the click and hover actions based on the context.
                    ItemSlot.Handle(ref item, context);
                }
            }
            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref item, context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }
    }
}
