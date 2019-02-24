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

        public virtual List<TooltipLine> GetTooltipLines() { return new List<TooltipLine>(); }

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
            tooltips.Add(new TooltipLine(mod, "Class", AmuletClass.ToString("F"))
            {
                overrideColor = ChatManager.WaveColor(Color.Fuchsia)
            });
            tooltips.AddRange(GetTooltipLines());
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
}
