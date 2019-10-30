using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Decimation.Core.Amulets
{
    public class AmuletTooltip
    {
        private readonly Amulet _amulet;
        private readonly Color _classColor = ChatManager.WaveColor(Color.Fuchsia);
        private readonly Color _effectColor = Color.ForestGreen;
        private readonly Mod _mod;
        private readonly Color _synergyColor = Color.CadetBlue;

        private int _effectCount;
        private bool _hasSynergy;

        public AmuletTooltip(Mod mod, Amulet amulet)
        {
            _mod = mod;
            _amulet = amulet;
            this.Lines = new List<TooltipLine>();

            SetClassTooltip();
        }

        public List<TooltipLine> Lines { get; set; }

        private void SetClassTooltip()
        {
            this.Lines.Add(new TooltipLine(_mod, "DecimationAmuletClass", _amulet.AmuletClass.ToString("F"))
            {
                overrideColor = _classColor
            });
        }

        public AmuletTooltip AddEffect(string tooltip)
        {
            this.Lines.Add(new TooltipLine(_mod, $"Effect{_effectCount}", tooltip)
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
                this.Lines.Add(new TooltipLine(_mod, "Synergy", tooltip)
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