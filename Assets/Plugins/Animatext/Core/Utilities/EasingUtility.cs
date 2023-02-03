// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0066

using UnityEngine;

namespace Animatext
{
    public static class EasingUtility
    {
        public static float Ease(float value, EasingType type)
        {
            switch (type)
            {
                case EasingType.Linear:
                    return EaseLinear(value);

                case EasingType.QuadIn:
                    return EaseQuadIn(value);

                case EasingType.QuadOut:
                    return EaseQuadOut(value);

                case EasingType.QuadInOut:
                    return EaseQuadInOut(value);

                case EasingType.CubicIn:
                    return EaseCubicIn(value);

                case EasingType.CubicOut:
                    return EaseCubicOut(value);

                case EasingType.CubicInOut:
                    return EaseCubicInOut(value);

                case EasingType.QuartIn:
                    return EaseQuartIn(value);

                case EasingType.QuartOut:
                    return EaseQuartOut(value);

                case EasingType.QuartInOut:
                    return EaseQuartInOut(value);

                case EasingType.QuintIn:
                    return EaseQuintIn(value);

                case EasingType.QuintOut:
                    return EaseQuintOut(value);

                case EasingType.QuintInOut:
                    return EaseQuintInOut(value);

                case EasingType.CircIn:
                    return EaseCircIn(value);

                case EasingType.CircOut:
                    return EaseCircOut(value);

                case EasingType.CircInOut:
                    return EaseCircInOut(value);

                case EasingType.SineIn:
                    return EaseSineIn(value);

                case EasingType.SineOut:
                    return EaseSineOut(value);

                case EasingType.SineInOut:
                    return EaseSineInOut(value);

                default:
                    return value;
            }
        }

        public static float EaseLinear(float value)
        {
            return value;
        }

        public static float EaseQuadIn(float value)
        {
            return value * value;
        }

        public static float EaseQuadOut(float value)
        {
            return 1 - EaseQuadIn(1 - value);
        }

        public static float EaseQuadInOut(float value)
        {
            return value < 0.5f ? EaseQuadIn(value * 2) * 0.5f : 1 - EaseQuadIn((1 - value) * 2) * 0.5f;
        }

        public static float EaseCubicIn(float value)
        {
            return value * value * value;
        }

        public static float EaseCubicOut(float value)
        {
            return 1 - EaseCubicIn(1 - value);
        }

        public static float EaseCubicInOut(float value)
        {
            return value < 0.5f ? EaseCubicIn(value * 2) * 0.5f : 1 - EaseCubicIn((1 - value) * 2) * 0.5f;
        }

        public static float EaseQuartIn(float value)
        {
            return value * value * value * value;
        }

        public static float EaseQuartOut(float value)
        {
            return 1 - EaseQuartIn(1 - value);
        }

        public static float EaseQuartInOut(float value)
        {
            return value < 0.5f ? EaseQuartIn(value * 2) * 0.5f : 1 - EaseQuartIn((1 - value) * 2) * 0.5f;
        }

        public static float EaseQuintIn(float value)
        {
            return value * value * value * value * value;
        }

        public static float EaseQuintOut(float value)
        {
            return 1 - EaseQuintIn(1 - value);
        }

        public static float EaseQuintInOut(float value)
        {
            return value < 0.5f ? EaseQuintIn(value * 2) * 0.5f : 1 - EaseQuintIn((1 - value) * 2) * 0.5f;
        }

        public static float EaseCircIn(float value)
        {
            return 1 - Mathf.Sqrt(1 - value * value);
        }

        public static float EaseCircOut(float value)
        {
            return 1 - EaseCircIn(1 - value);
        }

        public static float EaseCircInOut(float value)
        {
            return value < 0.5f ? EaseCircIn(value * 2) * 0.5f : 1 - EaseCircIn((1 - value) * 2) * 0.5f;
        }

        public static float EaseSineIn(float value)
        {
            return 1 - Mathf.Cos(value * Mathf.PI * 0.5f);
        }

        public static float EaseSineOut(float value)
        {
            return 1 - EaseSineIn(1 - value);
        }

        public static float EaseSineInOut(float value)
        {
            return value < 0.5f ? EaseSineIn(value * 2) * 0.5f : 1 - EaseSineIn((1 - value) * 2) * 0.5f;
        }

        public static float Basic(float value, bool continuousEasing)
        {
            return continuousEasing ? 1 - Mathf.Abs(value * 2 - 1) : (value - Mathf.RoundToInt(value)) * 2;
        }

        public static float Elastic(float value, int oscillations, float stiffness)
        {
            return oscillations > 0 ? Mathf.Exp(-stiffness * value) * Mathf.Sin(Mathf.PI * 2 * oscillations * value) : 0;
        }

        public static float Elastic(float value, int oscillations, float stiffness, out float extraValue)
        {
            extraValue = stiffness == 0 ? value : 1 - (Mathf.Exp(stiffness * (1 - value)) - 1) / (Mathf.Exp(stiffness) - 1);

            return oscillations > 0 ? Mathf.Exp(-stiffness * value) * Mathf.Sin(Mathf.PI * 2 * oscillations * value) : 0;
        }

        public static float EaseElastic(float value, int oscillations, float stiffness)
        {
            return Mathf.Exp(stiffness * value - stiffness) * Mathf.Sin(value * Mathf.PI * (0.5f + (oscillations > 0 ? oscillations * 2 : 0)));
        }

        public static float EaseElastic(float value, int oscillations, float stiffness, out float extraValue)
        {
            extraValue = stiffness == 0 ? value : (Mathf.Exp(stiffness * value) - 1) / (Mathf.Exp(stiffness) - 1);

            return Mathf.Exp(stiffness * value - stiffness) * Mathf.Sin(value * Mathf.PI * (0.5f + (oscillations > 0 ? oscillations * 2 : 0)));
        }

        public static float Bounce(float value, int bounces, float bounciness)
        {
            if (bounces <= 0 || bounciness <= 0)
            {
                return 0;
            }
            else if (bounciness == 1)
            {
                float v1 = value * bounces;
                float v2 = v1 - Mathf.FloorToInt(v1);

                return 4 * v2 * (1 - v2);
            }
            else
            {
                float v1 = Mathf.Max(float.Epsilon, Mathf.Pow(bounciness, bounces) * value - value + 1);
                float v2 = Mathf.Pow(bounciness, Mathf.FloorToInt(Mathf.Log(v1, bounciness)));

                return 4 * (v1 - v2) * (v2 * bounciness - v1) / ((1 - bounciness) * (1 - bounciness));
            }
        }

        public static float Bounce(float value, int bounces, float bounciness, out float extraValue)
        {
            if (bounces <= 0 || bounciness <= 0)
            {
                extraValue = value;

                return 0;
            }
            else if (bounciness == 1)
            {
                float v1 = value * bounces;
                float v2 = v1 - Mathf.FloorToInt(v1);

                extraValue = value;

                return 4 * v2 * (1 - v2);
            }
            else
            {
                extraValue = Mathf.Pow(bounciness, bounces);

                float v1 = Mathf.Max(float.Epsilon, extraValue * value - value + 1);
                float v2 = Mathf.Pow(bounciness, Mathf.FloorToInt(Mathf.Log(v1, bounciness)));

                extraValue = (v2 * (v1 - v2) * bounciness + v1 * v2 - 1) / (extraValue * extraValue - 1);

                return 4 * (v1 - v2) * (v2 * bounciness - v1) / ((1 - bounciness) * (1 - bounciness));
            }
        }

        public static float EaseBounce(float value, int bounces, float bounciness)
        {
            if (bounces <= 0 || bounciness <= 0)
            {
                return value * (2 - value);
            }
            else if (bounciness == 1)
            {
                float v1 = (bounces * 2 + 1) * (1 - value);
                float v2 = v1 - Mathf.FloorToInt((v1 + 1) / 2) * 2;

                return 1 - v2 * v2;
            }
            else
            {
                float v1 = Mathf.Max(float.Epsilon, Mathf.Pow(bounciness, bounces + 1) * (1 - value) + (bounciness + 1) * value * 0.5f);
                float v2 = Mathf.Pow(bounciness, Mathf.FloorToInt(Mathf.Log(v1, bounciness)));

                return 4 * (v1 - v2) * (v2 * bounciness - v1) / ((1 - bounciness) * (1 - bounciness));
            }
        }

        public static float EaseBounce(float value, int bounces, float bounciness, out float extraValue)
        {
            if (bounces <= 0 || bounciness <= 0)
            {
                extraValue = value;

                return value * (2 - value);
            }
            else if (bounciness == 1)
            {
                float v1 = (bounces * 2 + 1) * (1 - value);
                float v2 = v1 - Mathf.FloorToInt((v1 + 1) / 2) * 2;

                extraValue = value;

                return 1 - v2 * v2;
            }
            else
            {
                extraValue = Mathf.Pow(bounciness, bounces + 1);

                float v1 = Mathf.Max(float.Epsilon, extraValue * (1 - value) + (bounciness + 1) * value * 0.5f);
                float v2 = Mathf.Pow(bounciness, Mathf.FloorToInt(Mathf.Log(v1, bounciness)));

                extraValue = 1 - (v2 * (v1 - v2) * bounciness + v1 * v2 - (1 + bounciness * bounciness) * 0.5f) / (extraValue * extraValue - (1 + bounciness * bounciness) * 0.5f);

                return 4 * (v1 - v2) * (v2 * bounciness - v1) / ((1 - bounciness) * (1 - bounciness));
            }
        }

        public static float EaseBack(float value, float amplitude)
        {
            return (amplitude * value - amplitude + value) * value * value;
        }

        public static float Wave(float value, int waves)
        {
            return waves > 0 ? Mathf.Sin(Mathf.PI * 2 * waves * value) : 0;
        }

        public static float EaseStep(float value, int steps, StepType stepType)
        {
            steps = Mathf.Max(1, steps);

            switch (stepType)
            {
                case StepType.Round:
                    return (float)Mathf.RoundToInt(value * steps) / steps;

                case StepType.Ceil:
                    return (float)Mathf.CeilToInt(value * steps) / steps;

                case StepType.Floor:
                    return (float)Mathf.FloorToInt(value * steps) / steps;

                default:
                    return value;
            }
        }
    }
}