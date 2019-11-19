using Decimation.Items;
using Decimation.NPCs.TownNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Decimation.UI
{
    class SkeletonUI : UIState
    {
        private VanillaItemSlotWrapper vanillaItemSlot;

        public override void OnInitialize()
        {
            vanillaItemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
            {
                Left = { Pixels = 50 },
                Top = { Pixels = 270 },
                ValidItemFunc = item => item.IsAir || (!item.IsAir && item.accessory)
            };

            Append(vanillaItemSlot);
        }

        public override void OnDeactivate()
        {
            if (!vanillaItemSlot.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(vanillaItemSlot.Item, vanillaItemSlot.Item.stack);

                vanillaItemSlot.Item.TurnToAir();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Main.LocalPlayer.talkNPC == -1 ||
                Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<Skeleton>())
            {
                Decimation.Instance.skeletonUserInterface.SetState(null);
            }
        }

        private bool tickPlayed;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            const int slotX = 50;
            const int slotY = 270;
            if (!vanillaItemSlot.Item.IsAir)
            {
                int itemValue = vanillaItemSlot.Item.value;
                int cursePrice = (int)(itemValue * (1 / 3f));

                string costText = Language.GetTextValue("LegacyInterface.46") + ": ";
                string coinsText = "";
                int[] coins = Terraria.Utils.CoinsSplit(cursePrice);
                if (coins[3] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
                }
                if (coins[2] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
                }
                if (coins[1] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
                }
                if (coins[0] > 0)
                {
                    coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
                }
                ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costText, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, coinsText, new Vector2(slotX + 50 + Main.fontMouseText.MeasureString(costText).X, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                int reforgeX = slotX + 70;
                int reforgeY = slotY + 40;
                bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;
                Texture2D reforgeTexture = Main.reforgeTexture[hoveringOverReforgeButton ? 1 : 0];
                Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
                if (hoveringOverReforgeButton)
                {
                    Main.hoverItemName = Language.GetTextValue("LegacyInterface.19");
                    if (!tickPlayed)
                    {
                        Main.PlaySound(12, -1, -1, 1, 1f, 0f);
                    }
                    tickPlayed = true;
                    Main.LocalPlayer.mouseInterface = true;
                    if (Main.mouseLeftRelease && Main.mouseLeft && Main.LocalPlayer.CanBuyItem(cursePrice, -1) && ItemLoader.PreReforge(vanillaItemSlot.Item))
                    {
                        Main.LocalPlayer.BuyItem(cursePrice, -1);
                        bool favorited = vanillaItemSlot.Item.favorited;
                        int stack = vanillaItemSlot.Item.stack;
                        Item reforgeItem = new Item();
                        reforgeItem.netDefaults(vanillaItemSlot.Item.netID);
                        reforgeItem = reforgeItem.CloneWithModdedDataFrom(vanillaItemSlot.Item);

                        // This is the main effect of this slot.
                        reforgeItem.GetGlobalItem<CursedItem>().Curse(reforgeItem);

                        vanillaItemSlot.Item = reforgeItem.Clone();
                        vanillaItemSlot.Item.position.X = Main.LocalPlayer.position.X + (Main.LocalPlayer.width / 2f) - (vanillaItemSlot.Item.width / 2f);
                        vanillaItemSlot.Item.position.Y = Main.LocalPlayer.position.Y + (Main.LocalPlayer.height / 2f) - (vanillaItemSlot.Item.height / 2f);
                        vanillaItemSlot.Item.favorited = favorited;
                        vanillaItemSlot.Item.stack = stack;
                        ItemLoader.PostReforge(vanillaItemSlot.Item);
                        ItemText.NewText(vanillaItemSlot.Item, vanillaItemSlot.Item.stack, true);
                        Main.PlaySound(SoundID.Item37, -1, -1);
                    }
                }
                else
                {
                    tickPlayed = false;
                }
            }
            else
            {
                // TODO change message after implementing the item
                string message = "Place an item here to curse\nWarning: This action can't be undone.";
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            }
        }
    }
}
