// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TBackA04Script : BaseEffectScript
    {
        private void Start()
        {
            TCBackA04 a1 = ScriptableObject.CreateInstance<TCBackA04>();

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
            a1.skew = new Vector2(-45, 0);
            a1.anchorType = AnchorType.Center;
            a1.anchorOffset = Vector2.zero;
            a1.amplitude = 3;
            a1.easingType = EasingType.Linear;
            a1.fadeMode = ColorMode.Multiply;
            a1.fadeRange = new FloatRange(0, 0.25f);

            presetsA1 = new BaseEffect[] { a1 };

            TCBackA04 a2 = ScriptableObject.CreateInstance<TCBackA04>();

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
            a2.skew = new Vector2(45, 0);
            a2.anchorType = AnchorType.Center;
            a2.anchorOffset = Vector2.zero;
            a2.amplitude = 3;
            a2.easingType = EasingType.Linear;
            a2.fadeMode = ColorMode.Multiply;
            a2.fadeRange = new FloatRange(0, 0.25f);

            presetsA2 = new BaseEffect[] { a2 };

            TCBackA04 a3 = ScriptableObject.CreateInstance<TCBackA04>();

            a3.tag = tagName;
            a3.startInterval = startInverval;
            a3.reverse = false;
            a3.loopCount = 0;
            a3.loopInterval = 0.5f;
            a3.loopBackInterval = 0;
            a3.pingpongLoop = false;
            a3.continuousLoop = false;
            a3.interval = 0.2f;
            a3.singleTime = 1.3f;
            a3.sortType = SortType.BackToFront;
            a3.skew = new Vector2(0, -45);
            a3.anchorType = AnchorType.Bottom;
            a3.anchorOffset = Vector2.zero;
            a3.amplitude = 4.5f;
            a3.easingType = EasingType.Linear;
            a3.fadeMode = ColorMode.Multiply;
            a3.fadeRange = new FloatRange(0, 0.25f);

            presetsA3 = new BaseEffect[] { a3 };

            TCBackA04 a4 = ScriptableObject.CreateInstance<TCBackA04>();

            a4.tag = tagName;
            a4.startInterval = startInverval;
            a4.reverse = true;
            a4.loopCount = 0;
            a4.loopInterval = 0.5f;
            a4.loopBackInterval = 0;
            a4.pingpongLoop = false;
            a4.continuousLoop = false;
            a4.interval = 0.2f;
            a4.singleTime = 1.3f;
            a4.sortType = SortType.FrontToBack;
            a4.skew = new Vector2(0, 45);
            a4.anchorType = AnchorType.Top;
            a4.anchorOffset = Vector2.zero;
            a4.amplitude = 4.5f;
            a4.easingType = EasingType.Linear;
            a4.fadeMode = ColorMode.Multiply;
            a4.fadeRange = new FloatRange(0, 0.25f);

            presetsA4 = new BaseEffect[] { a4 };

            TWBackA04 b1 = ScriptableObject.CreateInstance<TWBackA04>();

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
            b1.skew = new Vector2(-15, 0);
            b1.anchorType = AnchorType.TopLeft;
            b1.anchorOffset = Vector2.zero;
            b1.amplitude = 3;
            b1.easingType = EasingType.Linear;
            b1.fadeMode = ColorMode.Multiply;
            b1.fadeRange = new FloatRange(0, 0.25f);

            presetsB1 = new BaseEffect[] { b1 };

            TWBackA04 b2 = ScriptableObject.CreateInstance<TWBackA04>();

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
            b2.skew = new Vector2(15, 0);
            b2.anchorType = AnchorType.TopRight;
            b2.anchorOffset = Vector2.zero;
            b2.amplitude = 3;
            b2.easingType = EasingType.Linear;
            b2.fadeMode = ColorMode.Multiply;
            b2.fadeRange = new FloatRange(0, 0.25f);

            presetsB2 = new BaseEffect[] { b2 };

            TWBackA04 b3 = ScriptableObject.CreateInstance<TWBackA04>();

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
            b3.skew = new Vector2(0, 30);
            b3.anchorType = AnchorType.BottomLeft;
            b3.anchorOffset = Vector2.zero;
            b3.amplitude = 4.5f;
            b3.easingType = EasingType.Linear;
            b3.fadeMode = ColorMode.Multiply;
            b3.fadeRange = new FloatRange(0, 0.25f);

            presetsB3 = new BaseEffect[] { b3 };

            TWBackA04 b4 = ScriptableObject.CreateInstance<TWBackA04>();

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
            b4.skew = new Vector2(0, 30);
            b4.anchorType = AnchorType.BottomRight;
            b4.anchorOffset = Vector2.zero;
            b4.amplitude = 4.5f;
            b4.easingType = EasingType.Linear;
            b4.fadeMode = ColorMode.Multiply;
            b4.fadeRange = new FloatRange(0, 0.25f);

            presetsB4 = new BaseEffect[] { b4 };

            TLBackA04 c1 = ScriptableObject.CreateInstance<TLBackA04>();

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
            c1.skew = new Vector2(-9, 0);
            c1.anchorType = AnchorType.BottomLeft;
            c1.anchorOffset = Vector2.zero;
            c1.amplitude = 3;
            c1.easingType = EasingType.Linear;
            c1.fadeMode = ColorMode.Multiply;
            c1.fadeRange = new FloatRange(0, 0.25f);

            presetsC1 = new BaseEffect[] { c1 };

            TLBackA04 c2 = ScriptableObject.CreateInstance<TLBackA04>();

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
            c2.skew = new Vector2(9, 0);
            c2.anchorType = AnchorType.TopLeft;
            c2.anchorOffset = Vector2.zero;
            c2.amplitude = 3;
            c2.easingType = EasingType.Linear;
            c2.fadeMode = ColorMode.Multiply;
            c2.fadeRange = new FloatRange(0, 0.25f);

            presetsC2 = new BaseEffect[] { c2 };

            TLBackA04 c3 = ScriptableObject.CreateInstance<TLBackA04>();

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
            c3.skew = new Vector2(0, -30);
            c3.anchorType = AnchorType.BottomRight;
            c3.anchorOffset = Vector2.zero;
            c3.amplitude = 4.5f;
            c3.easingType = EasingType.Linear;
            c3.fadeMode = ColorMode.Multiply;
            c3.fadeRange = new FloatRange(0, 0.25f);

            presetsC3 = new BaseEffect[] { c3 };

            TLBackA04 c4 = ScriptableObject.CreateInstance<TLBackA04>();

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
            c4.skew = new Vector2(0, 30);
            c4.anchorType = AnchorType.TopRight;
            c4.anchorOffset = Vector2.zero;
            c4.amplitude = 4.5f;
            c4.easingType = EasingType.Linear;
            c4.fadeMode = ColorMode.Multiply;
            c4.fadeRange = new FloatRange(0, 0.25f);

            presetsC4 = new BaseEffect[] { c4 };

            TGBackA04 d1 = ScriptableObject.CreateInstance<TGBackA04>();

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
            d1.skew = new Vector2(-45, 0);
            d1.anchorType = AnchorType.Bottom;
            d1.anchorOffset = Vector2.zero;
            d1.amplitude = 3;
            d1.easingType = EasingType.Linear;
            d1.fadeMode = ColorMode.Multiply;
            d1.fadeRange = new FloatRange(0, 0.25f);

            presetsD1 = new BaseEffect[] { d1 };

            TGBackA04 d2 = ScriptableObject.CreateInstance<TGBackA04>();

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
            d2.skew = new Vector2(45, 0);
            d2.anchorType = AnchorType.Top;
            d2.anchorOffset = Vector2.zero;
            d2.amplitude = 3;
            d2.easingType = EasingType.Linear;
            d2.fadeMode = ColorMode.Multiply;
            d2.fadeRange = new FloatRange(0, 0.25f);

            presetsD2 = new BaseEffect[] { d2 };

            TGBackA04 d3 = ScriptableObject.CreateInstance<TGBackA04>();

            d3.tag = tagName;
            d3.startInterval = startInverval;
            d3.reverse = false;
            d3.loopCount = 0;
            d3.loopInterval = 0.4f;
            d3.loopBackInterval = 0;
            d3.pingpongLoop = false;
            d3.continuousLoop = false;
            d3.interval = 0.4f;
            d3.singleTime = 1.6f;
            d3.sortType = SortType.SidesToMiddleFront;
            d3.skew = new Vector2(0, -45);
            d3.anchorType = AnchorType.Center;
            d3.anchorOffset = Vector2.zero;
            d3.amplitude = 4.5f;
            d3.easingType = EasingType.Linear;
            d3.fadeMode = ColorMode.Multiply;
            d3.fadeRange = new FloatRange(0, 0.25f);

            presetsD3 = new BaseEffect[] { d3 };

            TGBackA04 d4 = ScriptableObject.CreateInstance<TGBackA04>();

            d4.tag = tagName;
            d4.startInterval = startInverval;
            d4.reverse = true;
            d4.loopCount = 0;
            d4.loopInterval = 0.4f;
            d4.loopBackInterval = 0;
            d4.pingpongLoop = false;
            d4.continuousLoop = false;
            d4.interval = 0.4f;
            d4.singleTime = 1.6f;
            d4.sortType = SortType.SidesToMiddleBack;
            d4.skew = new Vector2(0, 45);
            d4.anchorType = AnchorType.Center;
            d4.anchorOffset = Vector2.zero;
            d4.amplitude = 4.5f;
            d4.easingType = EasingType.Linear;
            d4.fadeMode = ColorMode.Multiply;
            d4.fadeRange = new FloatRange(0, 0.25f);

            presetsD4 = new BaseEffect[] { d4 };

            AddAnimatexts();
        }
    }
}