using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Decimation.Items
{
    internal abstract class DecimationItem : ModItem
    {
        protected abstract string ItemName { get; }
        protected virtual string ItemTooltip { get; } = "";
        protected virtual DrawAnimation Animation { get; } = null;
        protected virtual bool IsClone { get; } = false;

        protected int width = 16;
        protected int height = 16;
        protected Rarity rarity = Rarity.White;
        protected bool consumable = false;
        protected bool autoReuse = false;
        protected int useStyle = 1;
        protected int useTime = 20;
        protected int useAnimation = 20;
        protected LegacySoundStyle useSound = SoundID.Item1;
        protected int value = 1;

        public sealed override void SetStaticDefaults()
        {
            DisplayName.SetDefault(ItemName);
            Tooltip.SetDefault(ItemTooltip);

            if (Animation != null)
            {
                Main.RegisterItemAnimation(item.type, Animation);
            }
        }

        public sealed override void SetDefaults()
        {
            item.maxStack = 999;
            item.noMelee = true;

            Init();

            if (!IsClone)
            {
                item.width = width;
                item.height = height;
                item.rare = (int)rarity;
                item.consumable = consumable;
                item.autoReuse = autoReuse;
                item.useStyle = useStyle;
                item.useTime = useTime;
                item.useAnimation = useAnimation;
                item.UseSound = useSound;
                item.value = value;
            }
        }

        public sealed override void AddRecipes()
        {
            List<ModRecipe> recipes = GetAdditionalRecipes();
            recipes.Add(GetRecipe());

            foreach (ModRecipe recipe in recipes)
            {
                recipe?.AddRecipe();
            }
        }

        protected abstract void Init();

        protected virtual ModRecipe GetRecipe()
        {
            return null;
        }

        protected virtual List<ModRecipe> GetAdditionalRecipes() { return new List<ModRecipe>(); }

        protected static ModRecipe GetNewModRecipe(DecimationItem result, int quantity, int tile, bool anyIronBar = false)
        {
            return GetNewModRecipe(result, quantity, new List<int>() { tile }, anyIronBar);
        }

        protected static ModRecipe GetNewModRecipe(DecimationItem result, int quantity, List<int> tiles, bool anyIronBar = false)
        {
            ModRecipe recipe = new ModRecipe(Decimation.Instance);
            recipe.anyIronBar = anyIronBar;

            recipe.SetResult(result, quantity);

            foreach (int tile in tiles)
            {
                recipe.AddTile(tile);
            }

            return recipe;
        }
    }
}
