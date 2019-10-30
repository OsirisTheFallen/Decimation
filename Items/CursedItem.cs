using System;
using System.Collections.Generic;
using System.IO;
using Decimation.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;

namespace Decimation.Items
{
    internal class CursedItem : GlobalItem
    {
        private string _originalName;
        public bool Cursed { get; set; }
        public CurseType Type { get; set; }

        public override bool InstancePerEntity => true;

        public void Curse(Item item)
        {
            this.Cursed = true;
            this.Type = CurseType.GetRandomType();
            _originalName = item.Name;

            item.SetNameOverride($"{item.Name} {this.Type.Name}");
        }

        public void RemoveCurse(Item item)
        {
            this.Cursed = false;
            this.Type = null;

            item.SetNameOverride(_originalName);
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            CursedItem clone = (CursedItem) base.Clone(item, itemClone);
            clone.Cursed = this.Cursed;
            clone.Type = this.Type;
            clone._originalName = _originalName;

            return clone;
        }

        public override bool OnPickup(Item item, Player player)
        {
            if (item.accessory && Main.rand.NextBool(200)) Curse(item);

            return base.OnPickup(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (this.Cursed)
            {
                tooltips.Add(new TooltipLine(Decimation.Instance, "DecimationCursed", "Cursed")
                {
                    overrideColor = ChatManager.WaveColor(Color.DarkViolet)
                });

                tooltips.Add(new TooltipLine(Decimation.Instance, "DecimationCursedAdvantageTooltip",
                    this.Type.Advantage)
                {
                    overrideColor = ChatManager.WaveColor(Color.Violet),
                    isModifier = true
                });

                tooltips.Add(new TooltipLine(Decimation.Instance, "DecimationCursedDisadvantageTooltip",
                    this.Type.Disadvantage)
                {
                    overrideColor = ChatManager.WaveColor(Color.DarkViolet),
                    isModifierBad = true
                });
            }
        }

        public override void Load(Item item, TagCompound tag)
        {
            this.Cursed = tag.GetBool("Cursed");
            this.Type = CurseType.GetTypeFromName(tag.GetString("CurseType"));
            _originalName = tag.GetString("originalName");

            item.SetNameOverride($"{item.Name} {this.Type.Name}");
        }

        public override bool NeedsSaving(Item item)
        {
            return this.Cursed;
        }

        public override TagCompound Save(Item item)
        {
            return new TagCompound
            {
                ["Cursed"] = this.Cursed,
                ["CurseType"] = this.Type,
                ["originalName"] = _originalName
            };
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();

            if (this.Cursed)
            {
                modPlayer.hasCursedAccessory = true;

                // Effects on player for each type of curse
                if (this.Type == CurseType.Cursed)
                {
                    player.magicDamage *= 1.08f;
                    player.meleeDamage *= 1.08f;
                    player.rangedDamage *= 1.08f;
                    player.thrownDamage *= 1.08f;

                    CursedNPC.LifeBonus = 10;
                }
                else if (this.Type == CurseType.Sacrilegious)
                {
                    player.manaCost *= 0.95f;

                    CursedNPC.ManaLeech = 5;
                    CursedNPC.DamageBonus = 5;
                }
                else if (this.Type == CurseType.Blasphemous)
                {
                    player.maxMinions += 1;
                    player.minionDamage *= 0.95f;

                    CursedNPC.ManaLeech = 5;
                    CursedNPC.DamageBonus = 7;
                }
                else if (this.Type == CurseType.Heretic)
                {
                    player.statLifeMax2 += 10;

                    CursedNPC.DamageBonus = 7;
                }
            }
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            // Maximize to one cursed item at once
            DecimationPlayer modPlayer = player.GetModPlayer<DecimationPlayer>();
            return !modPlayer.hasCursedAccessory ||
                   modPlayer.hasCursedAccessory && !item.GetGlobalItem<CursedItem>().Cursed;
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(this.Cursed);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            this.Cursed = reader.ReadBoolean();
        }

        public class CurseType
        {
            public static readonly CurseType Cursed =
                new CurseType("The Cursed", "+8% damage", "Enemies have 10% more life.");

            public static readonly CurseType Sacrilegious = new CurseType("The Sacrilegious", "-5% mana usage",
                "Enemies leech mana and deal 5% more damages.");

            public static readonly CurseType Blasphemous = new CurseType("The Blasphemous", "+1 extra minion",
                "-5% minion damages \nEnemies leech mana and deal 7% more damage.");

            public static readonly CurseType Heretic =
                new CurseType("Of the Heretic", "+10 maximum life", "Enemies deal 7% more damage.");

            public CurseType(string name, string advantage, string disadvantage)
            {
                this.Name = name;
                this.Advantage = advantage;
                this.Disadvantage = disadvantage;
            }

            public static IEnumerable<CurseType> Values
            {
                get
                {
                    yield return Cursed;
                    yield return Sacrilegious;
                    yield return Blasphemous;
                    yield return Heretic;
                }
            }

            public string Name { get; }
            public string Advantage { get; }
            public string Disadvantage { get; }

            public static CurseType GetTypeFromName(string name)
            {
                foreach (CurseType type in Values)
                    if (name == type.Name)
                        return type;

                return null;
            }

            public static CurseType GetRandomType()
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        return Cursed;
                    case 1:
                        return Sacrilegious;
                    case 2:
                        return Blasphemous;
                    case 3:
                        return Heretic;
                    default:
                        return Cursed;
                }
            }
        }
    }

    public class PixieDustCurseRemover : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.PixieDust)
            {
                item.useStyle = 1;
                item.useTime = 20;
                item.useAnimation = 20;
                item.consumable = true;
            }

            base.SetDefaults(item);
        }

        public override bool UseItem(Item item, Player player)
        {
            if (item.type == ItemID.PixieDust)
            {
                int index = Array.IndexOf(player.inventory, item);

                Item itemUp = player.inventory[index + 1];
                Item itemDown = player.inventory[index - 1];

                if (!itemUp.IsAir)
                {
                    CursedItem cursedItemUp = itemUp.GetGlobalItem<CursedItem>();
                    if (cursedItemUp.Cursed)
                    {
                        Main.PlaySound(SoundID.Item29, new Vector2(player.Center.X, player.Center.Y));
                        cursedItemUp.RemoveCurse(itemUp);
                        return true;
                    }
                }
                else if (!itemDown.IsAir)
                {
                    CursedItem cursedItemDown = itemDown.GetGlobalItem<CursedItem>();
                    if (cursedItemDown.Cursed)
                    {
                        Main.PlaySound(SoundID.Item29, new Vector2(player.Center.X, player.Center.Y));
                        cursedItemDown.RemoveCurse(itemDown);
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            return true;
        }
    }
}