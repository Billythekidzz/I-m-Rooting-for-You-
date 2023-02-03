// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0066

using UnityEngine;

namespace Animatext
{
    public static class ColorUtility
    {
        public static float Blend(float baseValue, float blendValue, ColorMode colorMode)
        {
            switch (colorMode)
            {
                case ColorMode.Replace:
                case ColorMode.Normal:
                    return blendValue;

                case ColorMode.Darken:
                    return BlendDarken(baseValue, blendValue);

                case ColorMode.Multiply:
                    return BlendMultiply(baseValue, blendValue);

                case ColorMode.ColorBurn:
                    return BlendColorBurn(baseValue, blendValue);

                case ColorMode.Add:
                    return BlendAdd(baseValue, blendValue);

                case ColorMode.Lighten:
                    return BlendLighten(baseValue, blendValue);

                case ColorMode.Screen:
                    return BlendScreen(baseValue, blendValue);

                case ColorMode.ColorDodge:
                    return BlendColorDodge(baseValue, blendValue);

                case ColorMode.Overlay:
                    return BlendOverlay(baseValue, blendValue);

                case ColorMode.SoftLight:
                    return BlendSoftLight(baseValue, blendValue);

                case ColorMode.HardLight:
                    return BlendHardLight(baseValue, blendValue);

                case ColorMode.LinearLight:
                    return BlendLinearLight(baseValue, blendValue);

                case ColorMode.Difference:
                    return BlendDifference(baseValue, blendValue);

                case ColorMode.Exclusion:
                    return BlendExclusion(baseValue, blendValue);

                case ColorMode.Subtract:
                    return BlendSubtract(baseValue, blendValue);

                case ColorMode.Divide:
                    return BlendDivide(baseValue, blendValue);

                default:
                    return baseValue;
            }
        }

        public static Color Blend(Color baseColor, Color blendColor, ColorMode colorMode)
        {
            switch (colorMode)
            {
                case ColorMode.Replace:
                    return blendColor;

                case ColorMode.Normal:
                    return BlendNormal(baseColor, blendColor);

                case ColorMode.Darken:
                    return BlendDarken(baseColor, blendColor);

                case ColorMode.Multiply:
                    return BlendMultiply(baseColor, blendColor);

                case ColorMode.ColorBurn:
                    return BlendColorBurn(baseColor, blendColor);

                case ColorMode.Add:
                    return BlendAdd(baseColor, blendColor);

                case ColorMode.Lighten:
                    return BlendLighten(baseColor, blendColor);

                case ColorMode.Screen:
                    return BlendScreen(baseColor, blendColor);

                case ColorMode.ColorDodge:
                    return BlendColorDodge(baseColor, blendColor);

                case ColorMode.Overlay:
                    return BlendOverlay(baseColor, blendColor);

                case ColorMode.SoftLight:
                    return BlendSoftLight(baseColor, blendColor);

                case ColorMode.HardLight:
                    return BlendHardLight(baseColor, blendColor);

                case ColorMode.LinearLight:
                    return BlendLinearLight(baseColor, blendColor);

                case ColorMode.Difference:
                    return BlendDifference(baseColor, blendColor);

                case ColorMode.Exclusion:
                    return BlendExclusion(baseColor, blendColor);

                case ColorMode.Subtract:
                    return BlendSubtract(baseColor, blendColor);

                case ColorMode.Divide:
                    return BlendDivide(baseColor, blendColor);

                default:
                    return baseColor;
            }
        }

        public static Color BlendNormal(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = (blendColor.r * blendColor.a + (1 - blendColor.a) * baseColor.r * baseColor.a) / color.a;
            color.g = (blendColor.g * blendColor.a + (1 - blendColor.a) * baseColor.g * baseColor.a) / color.a;
            color.b = (blendColor.b * blendColor.a + (1 - blendColor.a) * baseColor.b * baseColor.a) / color.a;

            return color;
        }

        public static float BlendDarken(float baseValue, float blendValue)
        {
            return Mathf.Min(baseValue, blendValue);
        }

        public static Color BlendDarken(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendDarken(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendDarken(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendDarken(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendMultiply(float baseValue, float blendValue)
        {
            return baseValue * blendValue;
        }

        public static Color BlendMultiply(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendMultiply(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendMultiply(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendMultiply(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendColorBurn(float baseValue, float blendValue)
        {
            return Mathf.Max(0, (baseValue + blendValue - 1) / blendValue);
        }

        public static Color BlendColorBurn(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendColorBurn(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendColorBurn(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendColorBurn(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendAdd(float baseValue, float blendValue)
        {
            return Mathf.Min(1, baseValue + blendValue);
        }

        public static Color BlendAdd(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendAdd(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendAdd(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendAdd(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendLighten(float baseValue, float blendValue)
        {
            return Mathf.Max(baseValue, blendValue);
        }

        public static Color BlendLighten(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendLighten(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendLighten(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendLighten(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendScreen(float baseValue, float blendValue)
        {
            return baseValue + blendValue - baseValue * blendValue;
        }

        public static Color BlendScreen(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendScreen(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendScreen(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendScreen(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendColorDodge(float baseValue, float blendValue)
        {
            return Mathf.Min(1, baseValue / (1 - blendValue));
        }

        public static Color BlendColorDodge(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendColorDodge(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendColorDodge(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendColorDodge(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendOverlay(float baseValue, float blendValue)
        {
            return baseValue > 0.5f ? 1 - 2 * (1 - baseValue) * (1 - blendValue) : 2 * baseValue * blendValue;
        }

        public static Color BlendOverlay(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendOverlay(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendOverlay(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendOverlay(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendSoftLight(float baseValue, float blendValue)
        {
            return baseValue + (2 * blendValue - 1) * (blendValue > 0.5f ? Mathf.Sqrt(baseValue) - baseValue : baseValue - baseValue * baseValue);
        }

        public static Color BlendSoftLight(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendSoftLight(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendSoftLight(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendSoftLight(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendHardLight(float baseValue, float blendValue)
        {
            return blendValue > 0.5f ? 1 - 2 * (1 - baseValue) * (1 - blendValue) : 2 * baseValue * blendValue;
        }

        public static Color BlendHardLight(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendHardLight(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendHardLight(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendHardLight(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendLinearLight(float baseValue, float blendValue)
        {
            return Mathf.Clamp(baseValue + 2 * blendValue - 1, 0, 1);
        }

        public static Color BlendLinearLight(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendLinearLight(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendLinearLight(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendLinearLight(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendDifference(float baseValue, float blendValue)
        {
            return Mathf.Abs(baseValue - blendValue);
        }

        public static Color BlendDifference(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendDifference(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendDifference(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendDifference(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendExclusion(float baseValue, float blendValue)
        {
            return baseValue + blendValue - 2 * baseValue * blendValue;
        }

        public static Color BlendExclusion(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendExclusion(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendExclusion(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendExclusion(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendSubtract(float baseValue, float blendValue)
        {
            return Mathf.Max(0, baseValue - blendValue);
        }

        public static Color BlendSubtract(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendSubtract(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendSubtract(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendSubtract(baseColor.b, blendColor.b)) / color.a;

            return color;
        }

        public static float BlendDivide(float baseValue, float blendValue)
        {
            return Mathf.Clamp(baseValue / blendValue, 0, 1);
        }

        public static Color BlendDivide(Color baseColor, Color blendColor)
        {
            Color color;

            color.a = baseColor.a + blendColor.a - baseColor.a * blendColor.a;
            color.r = ((color.a - blendColor.a) * baseColor.r + (color.a - baseColor.a) * blendColor.r + baseColor.a * blendColor.a * BlendDivide(baseColor.r, blendColor.r)) / color.a;
            color.g = ((color.a - blendColor.a) * baseColor.g + (color.a - baseColor.a) * blendColor.g + baseColor.a * blendColor.a * BlendDivide(baseColor.g, blendColor.g)) / color.a;
            color.b = ((color.a - blendColor.a) * baseColor.b + (color.a - baseColor.a) * blendColor.b + baseColor.a * blendColor.a * BlendDivide(baseColor.b, blendColor.b)) / color.a;

            return color;
        }
    }
}