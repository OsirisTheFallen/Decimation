using System;
using Decimation.Core.Collections;
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
        private readonly int _context;
        private readonly float _scale;
        private bool _newItem;
        internal Func<Item, bool> validItem;

        public AmuletSlot(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            this._context = context;
            this._scale = scale;
            this.item = new Item();
            this.item.SetDefaults(0);

            this.validItem = selectedItem => selectedItem.IsAir || (!selectedItem.IsAir && AmuletList.Instance.Contains(selectedItem));

            Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
            Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (validItem == null || validItem(Main.mouseItem))
                {
                    _newItem = true;
                    ItemSlot.Handle(ref item, _context);
                }
            }

            ItemSlot.Draw(spriteBatch, ref item, _context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;

            if (IsMouseHovering && item.IsAir)
                Main.hoverItemName = "Amulets";
        }

        public void UpdateAmulet(DecimationPlayer player)
        {
            if (!item.IsAir)
                item.modItem.UpdateAccessory(Main.LocalPlayer, false);

            if (!_newItem)
            {
                item = player.AmuletSlotItem;
            }
            else
            {
                _newItem = false;
                player.AmuletSlotItem = item;
            }
        }
    }
}
