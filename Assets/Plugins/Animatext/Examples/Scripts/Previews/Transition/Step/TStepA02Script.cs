// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TStepA02Script : BaseEffectScript
    {
        private void Start()
        {
            TCStepA02 a1 = ScriptableObject.CreateInstance<TCStepA02>();

            a1.tag = tagName;
            a1.startInterval = startInverval;
            a1.reverse = true;
            a1.loopCount = 0;
            a1.loopInterval = 0.4f;
            a1.loopBackInterval = 0;
            a1.pingpongLoop = false;
            a1.continuousLoop = false;
            a1.interval = 0.275f;
            a1.singleTime = 0.575f;
            a1.sortType = SortType.MiddleToSidesFront;
            a1.rotation = 45;
            a1.anchorType = AnchorType.Center;
            a1.anchorOffset = Vector2.zero;
            a1.steps = 4;
            a1.stepType = StepType.Round;
            a1.easingType = EasingType.Linear;
            a1.fadeMode = ColorMode.Multiply;
            a1.fadeRange = new FloatRange(0, 0.5f);

            presetsA1 = new BaseEffect[] { a1 };

            TCStepA02 a2 = ScriptableObject.CreateInstance<TCStepA02>();

            a2.tag = tagName;
            a2.startInterval = startInverval;
            a2.reverse = false;
            a2.loopCount = 0;
            a2.loopInterval = 0.4f;
            a2.loopBackInterval = 0;
            a2.pingpongLoop = false;
            a2.continuousLoop = false;
            a2.interval = 0.275f;
            a2.singleTime = 0.575f;
            a2.sortType = SortType.MiddleToSidesBack;
            a2.rotation = -45;
            a2.anchorType = AnchorType.Center;
            a2.anchorOffset = Vector2.zero;
            a2.steps = 4;
            a2.stepType = StepType.Round;
            a2.easingType = EasingType.Linear;
            a2.fadeMode = ColorMode.Multiply;
            a2.fadeRange = new FloatRange(0, 0.5f);

            presetsA2 = new BaseEffect[] { a2 };

            TCStepA02 a3 = ScriptableObject.CreateInstance<TCStepA02>();

            a3.tag = tagName;
            a3.startInterval = startInverval;
            a3.reverse = false;
            a3.loopCount = 0;
            a3.loopInterval = 0.5f;
            a3.loopBackInterval = 0;
            a3.pingpongLoop = false;
            a3.continuousLoop = false;
            a3.interval = 0.25f;
            a3.singleTime = 0.75f;
            a3.sortType = SortType.BackToFront;
            a3.rotation = 45;
            a3.anchorType = AnchorType.Bottom;
            a3.anchorOffset = Vector2.zero;
            a3.steps = 6;
            a3.stepType = StepType.Round;
            a3.easingType = EasingType.Linear;
            a3.fadeMode = ColorMode.Multiply;
            a3.fadeRange = new FloatRange(0, 0.5f);

            presetsA3 = new BaseEffect[] { a3 };

            TCStepA02 a4 = ScriptableObject.CreateInstance<TCStepA02>();

            a4.tag = tagName;
            a4.startInterval = startInverval;
            a4.reverse = true;
            a4.loopCount = 0;
            a4.loopInterval = 0.5f;
            a4.loopBackInterval = 0;
            a4.pingpongLoop = false;
            a4.continuousLoop = false;
            a4.interval = 0.25f;
            a4.singleTime = 0.75f;
            a4.sortType = SortType.FrontToBack;
            a4.rotation = -45;
            a4.anchorType = AnchorType.Top;
            a4.anchorOffset = Vector2.zero;
            a4.steps = 6;
            a4.stepType = StepType.Round;
            a4.easingType = EasingType.Linear;
            a4.fadeMode = ColorMode.Multiply;
            a4.fadeRange = new FloatRange(0, 0.5f);

            presetsA4 = new BaseEffect[] { a4 };

            TWStepA02 b1 = ScriptableObject.CreateInstance<TWStepA02>();

            b1.tag = tagName;
            b1.startInterval = startInverval;
            b1.reverse = true;
            b1.loopCount = 0;
            b1.loopInterval = 0.5f;
            b1.loopBackInterval = 0;
            b1.pingpongLoop = false;
            b1.continuousLoop = false;
            b1.interval = 0.5f;
            b1.singleTime = 1;
            b1.sortType = SortType.BackToFront;
            b1.rotation = 6;
            b1.anchorType = AnchorType.TopLeft;
            b1.anchorOffset = Vector2.zero;
            b1.steps = 4;
            b1.stepType = StepType.Floor;
            b1.easingType = EasingType.Linear;
            b1.fadeMode = ColorMode.Multiply;
            b1.fadeRange = new FloatRange(0, 0.25f);

            presetsB1 = new BaseEffect[] { b1 };

            TWStepA02 b2 = ScriptableObject.CreateInstance<TWStepA02>();

            b2.tag = tagName;
            b2.startInterval = startInverval;
            b2.reverse = false;
            b2.loopCount = 0;
            b2.loopInterval = 0.5f;
            b2.loopBackInterval = 0;
            b2.pingpongLoop = false;
            b2.continuousLoop = false;
            b2.interval = 0.5f;
            b2.singleTime = 1;
            b2.sortType = SortType.FrontToBack;
            b2.rotation = 6;
            b2.anchorType = AnchorType.TopRight;
            b2.anchorOffset = Vector2.zero;
            b2.easingType = EasingType.Linear;
            b2.steps = 4;
            b2.stepType = StepType.Floor;
            b2.fadeMode = ColorMode.Multiply;
            b2.fadeRange = new FloatRange(0, 0.25f);

            presetsB2 = new BaseEffect[] { b2 };

            TWStepA02 b3 = ScriptableObject.CreateInstance<TWStepA02>();

            b3.tag = tagName;
            b3.startInterval = startInverval;
            b3.reverse = true;
            b3.loopCount = 0;
            b3.loopInterval = 0.5f;
            b3.loopBackInterval = 0;
            b3.pingpongLoop = true;
            b3.continuousLoop = false;
            b3.interval = 0.75f;
            b3.singleTime = 0.75f;
            b3.sortType = SortType.BackToFront;
            b3.rotation = -6;
            b3.anchorType = AnchorType.BottomLeft;
            b3.anchorOffset = Vector2.zero;
            b3.steps = 6;
            b3.stepType = StepType.Ceil;
            b3.easingType = EasingType.Linear;
            b3.fadeMode = ColorMode.Multiply;
            b3.fadeRange = new FloatRange(0, 0.75f);

            presetsB3 = new BaseEffect[] { b3 };

            TWStepA02 b4 = ScriptableObject.CreateInstance<TWStepA02>();

            b4.tag = tagName;
            b4.startInterval = startInverval;
            b4.reverse = false;
            b4.loopCount = 0;
            b4.loopInterval = 0.5f;
            b4.loopBackInterval = 0;
            b4.pingpongLoop = true;
            b4.continuousLoop = false;
            b4.interval = 0.75f;
            b4.singleTime = 0.75f;
            b4.sortType = SortType.FrontToBack;
            b4.rotation = -6;
            b4.anchorType = AnchorType.BottomRight;
            b4.anchorOffset = Vector2.zero;
            b4.steps = 6;
            b4.stepType = StepType.Ceil;
            b4.easingType = EasingType.Linear;
            b4.fadeMode = ColorMode.Multiply;
            b4.fadeRange = new FloatRange(0, 0.75f);

            presetsB4 = new BaseEffect[] { b4 };

            TLStepA02 c1 = ScriptableObject.CreateInstance<TLStepA02>();

            c1.tag = tagName;
            c1.startInterval = startInverval;
            c1.reverse = false;
            c1.loopCount = 0;
            c1.loopInterval = 0.5f;
            c1.loopBackInterval = 0;
            c1.pingpongLoop = true;
            c1.continuousLoop = false;
            c1.interval = 1.5f;
            c1.singleTime = 1.5f;
            c1.sortType = SortType.FrontToBack;
            c1.rotation = -6;
            c1.anchorType = AnchorType.BottomLeft;
            c1.anchorOffset = Vector2.zero;
            c1.steps = 4;
            c1.stepType = StepType.Floor;
            c1.easingType = EasingType.Linear;
            c1.fadeMode = ColorMode.Multiply;
            c1.fadeRange = new FloatRange(0, 0.75f);

            presetsC1 = new BaseEffect[] { c1 };

            TLStepA02 c2 = ScriptableObject.CreateInstance<TLStepA02>();

            c2.tag = tagName;
            c2.startInterval = startInverval;
            c2.reverse = true;
            c2.loopCount = 0;
            c2.loopInterval = 0.5f;
            c2.loopBackInterval = 0;
            c2.pingpongLoop = true;
            c2.continuousLoop = false;
            c2.interval = 1.5f;
            c2.singleTime = 1.5f;
            c2.sortType = SortType.FrontToBack;
            c2.rotation = -6;
            c2.anchorType = AnchorType.TopLeft;
            c2.anchorOffset = Vector2.zero;
            c2.steps = 4;
            c2.stepType = StepType.Floor;
            c2.easingType = EasingType.Linear;
            c2.fadeMode = ColorMode.Multiply;
            c2.fadeRange = new FloatRange(0, 0.75f);

            presetsC2 = new BaseEffect[] { c2 };

            TLStepA02 c3 = ScriptableObject.CreateInstance<TLStepA02>();

            c3.tag = tagName;
            c3.startInterval = startInverval;
            c3.reverse = false;
            c3.loopCount = 0;
            c3.loopInterval = 0.5f;
            c3.loopBackInterval = 0;
            c3.pingpongLoop = false;
            c3.continuousLoop = false;
            c3.interval = 1.5f;
            c3.singleTime = 1.5f;
            c3.sortType = SortType.FrontToBack;
            c3.rotation = 6;
            c3.anchorType = AnchorType.BottomRight;
            c3.anchorOffset = Vector2.zero;
            c3.steps = 6;
            c3.stepType = StepType.Ceil;
            c3.easingType = EasingType.Linear;
            c3.fadeMode = ColorMode.Multiply;
            c3.fadeRange = new FloatRange(0, 0.25f);

            presetsC3 = new BaseEffect[] { c3 };

            TLStepA02 c4 = ScriptableObject.CreateInstance<TLStepA02>();

            c4.tag = tagName;
            c4.startInterval = startInverval;
            c4.reverse = true;
            c4.loopCount = 0;
            c4.loopInterval = 0.5f;
            c4.loopBackInterval = 0;
            c4.pingpongLoop = false;
            c4.continuousLoop = false;
            c4.interval = 1.5f;
            c4.singleTime = 1.5f;
            c4.sortType = SortType.FrontToBack;
            c4.rotation = 6;
            c4.anchorType = AnchorType.TopRight;
            c4.anchorOffset = Vector2.zero;
            c4.steps = 6;
            c4.stepType = StepType.Ceil;
            c4.easingType = EasingType.Linear;
            c4.fadeMode = ColorMode.Multiply;
            c4.fadeRange = new FloatRange(0, 0.25f);

            presetsC4 = new BaseEffect[] { c4 };

            TGStepA02 d1 = ScriptableObject.CreateInstance<TGStepA02>();

            d1.tag = tagName;
            d1.startInterval = startInverval;
            d1.reverse = true;
            d1.loopCount = 0;
            d1.loopInterval = 0.4f;
            d1.loopBackInterval = 0;
            d1.pingpongLoop = false;
            d1.continuousLoop = false;
            d1.interval = 0.6f;
            d1.singleTime = 0.6f;
            d1.sortType = SortType.BackToFront;
            d1.rotation = -30;
            d1.anchorType = AnchorType.Bottom;
            d1.anchorOffset = Vector2.zero;
            d1.steps = 4;
            d1.stepType = StepType.Round;
            d1.easingType = EasingType.Linear;
            d1.fadeMode = ColorMode.Multiply;
            d1.fadeRange = new FloatRange(0, 0.5f);

            presetsD1 = new BaseEffect[] { d1 };

            TGStepA02 d2 = ScriptableObject.CreateInstance<TGStepA02>();

            d2.tag = tagName;
            d2.startInterval = startInverval;
            d2.reverse = false;
            d2.loopCount = 0;
            d2.loopInterval = 0.4f;
            d2.loopBackInterval = 0;
            d2.pingpongLoop = false;
            d2.continuousLoop = false;
            d2.interval = 0.6f;
            d2.singleTime = 0.6f;
            d2.sortType = SortType.FrontToBack;
            d2.rotation = 30;
            d2.anchorType = AnchorType.Top;
            d2.anchorOffset = Vector2.zero;
            d2.steps = 4;
            d2.stepType = StepType.Round;
            d2.easingType = EasingType.Linear;
            d2.fadeMode = ColorMode.Multiply;
            d2.fadeRange = new FloatRange(0, 0.5f);

            presetsD2 = new BaseEffect[] { d2 };

            TGStepA02 d3 = ScriptableObject.CreateInstance<TGStepA02>();

            d3.tag = tagName;
            d3.startInterval = startInverval;
            d3.reverse = false;
            d3.loopCount = 0;
            d3.loopInterval = 0.4f;
            d3.loopBackInterval = 0;
            d3.pingpongLoop = false;
            d3.continuousLoop = false;
            d3.interval = 0.45f;
            d3.singleTime = 1.35f;
            d3.sortType = SortType.SidesToMiddleFront;
            d3.rotation = -30;
            d3.anchorType = AnchorType.Center;
            d3.anchorOffset = Vector2.zero;
            d3.steps = 6;
            d3.stepType = StepType.Round;
            d3.easingType = EasingType.Linear;
            d3.fadeMode = ColorMode.Multiply;
            d3.fadeRange = new FloatRange(0, 0.5f);

            presetsD3 = new BaseEffect[] { d3 };

            TGStepA02 d4 = ScriptableObject.CreateInstance<TGStepA02>();

            d4.tag = tagName;
            d4.startInterval = startInverval;
            d4.reverse = true;
            d4.loopCount = 0;
            d4.loopInterval = 0.4f;
            d4.loopBackInterval = 0;
            d4.pingpongLoop = false;
            d4.continuousLoop = false;
            d4.interval = 0.45f;
            d4.singleTime = 1.35f;
            d4.sortType = SortType.SidesToMiddleBack;
            d4.rotation = 30;
            d4.anchorType = AnchorType.Center;
            d4.anchorOffset = Vector2.zero;
            d4.steps = 6;
            d4.stepType = StepType.Round;
            d4.easingType = EasingType.Linear;
            d4.fadeMode = ColorMode.Multiply;
            d4.fadeRange = new FloatRange(0, 0.5f);

            presetsD4 = new BaseEffect[] { d4 };

            AddAnimatexts();
        }
    }
}