using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Decimation.Tiles
{
    public class EnchantedAnvil : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Enchanted Anvil");
            AddMapEntry(new Color(108, 239, 64), name);
            dustType = DustID.Iron;
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Anvils, TileID.MythrilAnvil };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType("EnchantedAnvil"));
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.LocalPlayer.AddBuff(mod.BuffType("FatesSmile"), 60);
                Main.LocalPlayer.GetModPlayer<DecimationPlayer>(mod).closeToEnchantedAnvil = true;
            } else
            {
                Main.LocalPlayer.GetModPlayer<DecimationPlayer>(mod).closeToEnchantedAnvil = false;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public class EnchantedAnvilGlobalItem : GlobalItem
        {
            public override void OnCraft(Item item, Recipe recipe)
            {
                //Work only on prefix, not on effects
                if (Main.LocalPlayer.GetModPlayer<DecimationPlayer>(mod).closeToEnchantedAnvil)
                {
                    // Damages
                    if (item.melee && item.useStyle != 1 && item.pick == 0 && item.axe == 0 && item.hammer == 0)
                    {
                        if (Main.rand.Next(0, 21) == 5)
                        {
                            item.Prefix(PrefixID.Godly);
                            return;
                        }
                    }
                    else if (item.melee && (item.pick != 0 || item.axe != 0 || item.hammer != 0))
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Light);
                            return;
                        }
                    }
                    else if (item.melee)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Legendary);
                            return;
                        }
                    }
                    else if (item.summon)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Ruthless);
                            return;
                        }
                    }
                    else if ((item.magic || item.ranged) && item.knockBack == 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Demonic);
                            return;
                        }
                    }
                    else if (item.ranged)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Unreal);
                            return;
                        }
                    }
                    else if (item.summon)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Mythical);
                            return;
                        }
                        // Accessories
                    }
                    else if (item.accessory && item.defense != 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Warding);
                            return;
                        }
                    }
                    else if (item.accessory && item.mana != 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Arcane);
                            return;
                        }
                    }
                    else if (item.accessory && item.crit != 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Lucky);
                            return;
                        }
                    }
                    else if (item.accessory && item.damage != 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Menacing);
                            return;
                        }
                    }
                    else if (item.accessory && (item.velocity.X != 0 || item.velocity.Y != 0))
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Quick2);
                            return;
                        }
                    }
                    else if (item.accessory && item.shootSpeed != 0)
                    {
                        if (Main.rand.Next(0, 11) == 5)
                        {
                            item.Prefix(PrefixID.Violent);
                            return;
                        }
                    }
                }
            }
        }
    }
}
