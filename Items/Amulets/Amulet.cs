using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Decimation.Items.Amulets
{
    internal abstract class Amulet : DecimationItem
    {
        public abstract AmuletClasses AmuletClass { get; }

        protected abstract void InitAmulet();

        protected virtual void UpdateAmulet(Player player) { }

        protected virtual void SetAmuletTooltips(ref AmuletTooltip tooltip) { }

        protected sealed override void Init()
        {
            width = 28;
            height = 30;
            rarity = Rarity.Green;

            InitAmulet();

            Decimation.amulets.Add(item.type);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateAmulet(Main.LocalPlayer);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            AmuletTooltip tooltip = new AmuletTooltip(this);
            SetAmuletTooltips(ref tooltip);
            tooltips.AddRange(tooltip.Lines);
        }
    }

    public enum AmuletClasses
    {
        Melee,
        Summoner,
        Mage,
        Ranger,
        Throwing,
        Tank,
        Healer,
        Builder,
        Miner,
        Creator
    }

    public class AmuletTooltip
    {
        private readonly Color _classColor = ChatManager.WaveColor(Color.Fuchsia);
        private readonly Color _effectColor = Color.ForestGreen;
        private readonly Color _synergyColor = Color.CadetBlue;
        private readonly Mod _mod = Decimation.Instance;
        private readonly Amulet _amulet;

        private int _effectCount;
        private bool _hasSynergy;

        public List<TooltipLine> Lines { get; set; }

        internal AmuletTooltip(Amulet amulet)
        {
            _amulet = amulet;

            Lines = new List<TooltipLine>();

            SetClassTooltip();
        }

        private void SetClassTooltip()
        {
            Lines.Add(new TooltipLine(_mod, "DecimationAmuletClass", _amulet.AmuletClass.ToString("F"))
            {
                overrideColor = _classColor
            });
        }

        public AmuletTooltip AddEffect(string tooltip)
        {
            Lines.Add(new TooltipLine(_mod, $"Effect{_effectCount}", tooltip)
            {
                overrideColor = _effectColor
            });

            _effectCount++;
            return this;
        }

        public AmuletTooltip AddSynergy(string tooltip)
        {
            if (!_hasSynergy)
            {
                Lines.Add(new TooltipLine(_mod, "Synergy", tooltip)
                {
                    overrideColor = _synergyColor
                });

                _hasSynergy = true;
            }
            else
            {
                throw new InvalidOperationException($"Can't add more than one synergy tooltip to {_amulet.Name}");
            }

            return this;
        }
    }
}
