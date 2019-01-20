using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decimation.Items.Amulets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Decimation.UI
{
    class AmuletSlot : UIElement
    {
        internal Item item;
        private int context;
        private float scale;
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
                    // Handle handles all the click and hover actions based on the context.
                    ItemSlot.Handle(ref item, context);
                }
            }
            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref item, context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;

            if (IsMouseHovering && item.IsAir)
                Main.hoverItemName = "Amulets";
        }

        public void UpdateAmulet()
        {
            if (!item.IsAir)
                item.modItem.UpdateAccessory(Main.LocalPlayer, false);
            Main.LocalPlayer.GetModPlayer<DecimationPlayer>().amuletSlotItem = item;
            Main.LocalPlayer.GetModPlayer<DecimationPlayer>().amuletOwner = Main.LocalPlayer.name;
        }

        public void LoadItem(Item item)
        {
            this.item = item;
        }

        public void UnLoad()
        {
            item = new Item();
            item.SetDefaults(0);
        }
    }
}
