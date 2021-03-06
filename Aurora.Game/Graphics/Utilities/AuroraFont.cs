using System;
using System.Collections.Generic;
using osu.Framework.Graphics.Sprites;

namespace Aurora.Game.Graphics.Utilities
{
    public static class AuroraFont
    {
        public const float DEFAULT_FONT_SIZE = 26f;

        public static readonly Typeface TORUS_TYPEFACE = new("Torus", medium: 0, black: 0);

        public static readonly Typeface TORUS_NOTCHED_TYPEFACE = new("TorusNotched", 0, medium: 0, semiBold: 0, bold: 0, black: 0);

        public static FontUsage DefaultFont => GetFont();

        public static FontUsage TorusFont => GetFont(TORUS_TYPEFACE, weight: FontWeight.Regular);

        public static FontUsage TorusNotchedFont => GetFont(TORUS_NOTCHED_TYPEFACE, weight: FontWeight.Regular);

        public static FontUsage GetFont(
            Typeface? typeface = null,
            float size = DEFAULT_FONT_SIZE,
            FontWeight weight = FontWeight.Medium,
            bool italics = false,
            bool fixedWidth = false
        )
        {
            typeface ??= TORUS_TYPEFACE;

            return new FontUsage(
                typeface.Value.FontName,
                size,
                typeface.Value.GetWeightAsString(weight),
                italics,
                fixedWidth
            );
        }

        public enum FontWeight
        {
            Light,
            Regular,
            Medium,
            SemiBold,
            Bold,
            Black
        }

        /// <summary>
        ///     Simple information for fonts, should be used only in <see cref="AuroraFont"/>.
        /// </summary>
        public readonly struct Typeface
        {
            public readonly Dictionary<FontWeight, int> Weights;
            public readonly string FontName;

            /// <summary>
            ///     The only constructor that should be used to create a typeface.
            /// </summary>
            /// <param name="fontFontName">The name used for accessing a font.</param>
            /// <param name="light">The light weight.</param>
            /// <param name="regular">The regular weight.</param>
            /// <param name="medium">The medium weight.</param>
            /// <param name="semiBold">The semi-bold weight.</param>
            /// <param name="bold">The bold weight.</param>
            /// <param name="black">The black weight.</param>
            /// <remarks>
            ///     Set any weights to <c>0</c> to mark a weight as undefined, causing it to default to <paramref name="regular"/>.
            /// </remarks>
            public Typeface(
                string fontFontName,
                int light = 300,
                int regular = 400,
                int medium = 500,
                int semiBold = 600,
                int bold = 700,
                int black = 900
            )
            {
                FontName = fontFontName;

                Weights = new Dictionary<FontWeight, int>
                {
                    { FontWeight.Light, light },
                    { FontWeight.Regular, regular },
                    { FontWeight.Medium, medium },
                    { FontWeight.SemiBold, semiBold },
                    { FontWeight.Bold, bold },
                    { FontWeight.Black, black }
                };
            }

            public string GetWeightAsString(FontWeight weight)
            {
                if (Weights[weight] == 0)
                    weight = FontWeight.Regular;

                if (Weights[FontWeight.Regular] == 0)
                    throw new Exception($"Font \"{FontName}\" has an undefined regular weight.");

                return weight.ToString();
            }
        }
    }
}
