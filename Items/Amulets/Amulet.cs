using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Decimation.Items.Amulets
{
    public abstract class Amulet : ModItem
    {
        public abstract AmuletClasses AmuletClass { get; }

        public virtual void SetAmuletDefaults() { }

        public virtual void UpdateAmulet(Player player) { }

        public virtual AmuletTooltip GetAmuletTooltips() { return new AmuletTooltip(this); }

        public sealed override void SetDefaults()
        {
            SetAmuletDefaults();

            item.noMelee = true;
            item.rare = 2;

            Decimation.amulets.Add(item.type);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateAmulet(Main.LocalPlayer);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.AddRange(GetAmuletTooltips().Lines);
        }
    }

    public enum AmuletClasses
    {
        MELEE,
        SUMMONER,
        MAGE,
        RANGER,
        THROWING,
        TANK,
        HEALER,
        BUILDER,
        MINER,
        CREATOR
    }

    public class AmuletTooltip
    {
        private readonly Color classColor = ChatManager.WaveColor(Color.Fuchsia);
        private readonly Color effectColor = Color.ForestGreen;
        private readonly Color synergyColor = Color.CadetBlue;
        private readonly Mod mod = Decimation.Instance;
        private readonly Amulet amulet;

        private int effectCount = 0;
        private bool hasSynergy = false;

        public List<TooltipLine> Lines { get; set; }

        public AmuletTooltip(Amulet amulet)
        {
            this.amulet = amulet;

            Lines = new List<TooltipLine>();

            SetClassTooltip();
        }

        private void SetClassTooltip()
        {
            Lines.Add(new TooltipLine(mod, "DecimationAmuletClass", amulet.AmuletClass.ToString("F"))
            {
                overrideColor = classColor
            });
        }

        public AmuletTooltip addEffect(String tooltip)
        {
            Lines.Add(new TooltipLine(mod, $"Effect{effectCount}", tooltip)
            {
                overrideColor = effectColor
            });

            effectCount++;
            return this;
        }

        public AmuletTooltip addSynergy(String tooltip)
        {
            if(!hasSynergy)
            {
                Lines.Add(new TooltipLine(mod, "Synergy", tooltip)
                {
                    overrideColor = synergyColor
                });

                hasSynergy = true;
            } else
            {
                throw new InvalidOperationException($"Can't add more than one synergy tooltip to {amulet.Name}");
            }

            return this;
        }
    }
}
