using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace Decimation.UI
{
    public class AmuletSlot : UIElement
    {
        public Item item;
        private int context;
        private float scale;
        private bool newItem = false;
        internal Func<Item, bool> validItem;

        public AmuletSlot(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            this.context = context;
            this.scale = scale;
            item = new Item();
            item.SetDefaults(0);

            validItem = selectedItem => selectedItem.IsAir || (!selectedItem.IsAir && Decimation.amulets.Contains(selectedItem.type));

            Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
            Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
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
                    newItem = true;
                    ItemSlot.Handle(ref item, context);
                }
            }

            ItemSlot.Draw(spriteBatch, ref item, context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;

            if (IsMouseHovering && item.IsAir)
                Main.hoverItemName = "Amulets";
        }

        public void UpdateAmulet(DecimationPlayer player)
        {
            if (!item.IsAir)
                item.modItem.UpdateAccessory(Main.LocalPlayer, false);

            if (!newItem)
            {
                item = player.amuletSlotItem;
            }
            else
            {
                newItem = false;
                player.amuletSlotItem = item;
            }
        }
    }
}
