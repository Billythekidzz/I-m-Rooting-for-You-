// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class CFadeA01Script : BaseEffectScript
    {
        private void Start()
        {
            CCFadeA01 a1 = ScriptableObject.CreateInstance<CCFadeA01>();

            a1.tag = tagName;
            a1.startInterval = startInverval;
            a1.reverse = true;
            a1.loopCount = 0;
            a1.loopInterval = 0.4f;
            a1.loopBackInterval = 0;
            a1.pingpongLoop = false;
            a1.continuousLoop = false;
            a1.interval = 0.32f;
            a1.singleTime = 0.08f;
            a1.sortType = SortType.MiddleToSidesFront;
            a1.startOpacity = 1;
            a1.opacity = 0;
            a1.easingType = EasingType.Linear;
			a1.fadeMode = ColorMode.Multiply;

            presetsA1 = new BaseEffect[] { a1 };

            CCFadeA01 a2 = ScriptableObject.CreateInstance<CCFadeA01>();

            a2.tag = tagName;
            a2.startInterval = startInverval;
            a2.reverse = false;
            a2.loopCount = 0;
            a2.loopInterval = 0.4f;
            a2.loopBackInterval = 0;
            a2.pingpongLoop = false;
            a2.continuousLoop = false;
            a2.interval = 0.32f;
            a2.singleTime = 0.08f;
            a2.sortType = SortType.MiddleToSidesBack;
            a2.startOpacity = 1;
            a2.opacity = 0;
            a2.easingType = EasingType.QuadInOut;
			a2.fadeMode = ColorMode.Multiply;

            presetsA2 = new BaseEffect[] { a2 };

            CCFadeA01 a3 = ScriptableObject.CreateInstance<CCFadeA01>();

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
            a3.startOpacity = 1;
            a3.opacity = 0;
            a3.easingType = EasingType.QuadOut;
			a3.fadeMode = ColorMode.Multiply;

            presetsA3 = new BaseEffect[] { a3 };

            CCFadeA01 a4 = ScriptableObject.CreateInstance<CCFadeA01>();

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
            a4.startOpacity = 1;
            a4.opacity = 0;
            a4.easingType = EasingType.QuadIn;
			a4.fadeMode = ColorMode.Multiply;

            presetsA4 = new BaseEffect[] { a4 };

            CWFadeA01 b1 = ScriptableObject.CreateInstance<CWFadeA01>();

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
            b1.startOpacity = 1;
            b1.opacity = 0;
            b1.easingType = EasingType.Linear;
			b1.fadeMode = ColorMode.Multiply;

            presetsB1 = new BaseEffect[] { b1 };

            CWFadeA01 b2 = ScriptableObject.CreateInstance<CWFadeA01>();

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
            b2.startOpacity = 1;
            b2.opacity = 0.5f;
            b2.easingType = EasingType.QuadInOut;
			b2.fadeMode = ColorMode.Multiply;

            presetsB2 = new BaseEffect[] { b2 };

            CWFadeA01 b3 = ScriptableObject.CreateInstance<CWFadeA01>();

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
            b3.startOpacity = 1;
            b3.opacity = 0.5f;
            b3.easingType = EasingType.QuadOut;
			b3.fadeMode = ColorMode.Multiply;

            presetsB3 = new BaseEffect[] { b3 };

            CWFadeA01 b4 = ScriptableObject.CreateInstance<CWFadeA01>();

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
            b4.startOpacity = 1;
            b4.opacity = 0;
            b4.easingType = EasingType.QuadIn;
			b4.fadeMode = ColorMode.Multiply;

            presetsB4 = new BaseEffect[] { b4 };

            CLFadeA01 c1 = ScriptableObject.CreateInstance<CLFadeA01>();

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
            c1.startOpacity = 1;
            c1.opacity = 0;
            c1.easingType = EasingType.QuadIn;
			c1.fadeMode = ColorMode.Multiply;

            presetsC1 = new BaseEffect[] { c1 };

            CLFadeA01 c2 = ScriptableObject.CreateInstance<CLFadeA01>();

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
            c2.startOpacity = 1;
            c2.opacity = 0.5f;
            c2.easingType = EasingType.QuadOut;
			c2.fadeMode = ColorMode.Multiply;

            presetsC2 = new BaseEffect[] { c2 };

            CLFadeA01 c3 = ScriptableObject.CreateInstance<CLFadeA01>();

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
            c3.startOpacity = 1;
            c3.opacity = 0.5f;
            c3.easingType = EasingType.QuadInOut;
			c3.fadeMode = ColorMode.Multiply;

            presetsC3 = new BaseEffect[] { c3 };

            CLFadeA01 c4 = ScriptableObject.CreateInstance<CLFadeA01>();

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
            c4.startOpacity = 1;
            c4.opacity = 0;
            c4.easingType = EasingType.Linear;
			c4.fadeMode = ColorMode.Multiply;

            presetsC4 = new BaseEffect[] { c4 };

            CGFadeA01 d1 = ScriptableObject.CreateInstance<CGFadeA01>();

            d1.tag = tagName;
            d1.startInterval = startInverval;
            d1.reverse = true;
            d1.loopCount = 0;
            d1.loopInterval = 0.4f;
            d1.loopBackInterval = 0;
            d1.pingpongLoop = false;
            d1.continuousLoop = false;
            d1.interval = 0.7f;
            d1.singleTime = 0.1f;
            d1.sortType = SortType.BackToFront;
            d1.startOpacity = 1;
            d1.opacity = 0;
            d1.easingType = EasingType.Linear;
			d1.fadeMode = ColorMode.Multiply;

            presetsD1 = new BaseEffect[] { d1 };

            CGFadeA01 d2 = ScriptableObject.CreateInstance<CGFadeA01>();

            d2.tag = tagName;
            d2.startInterval = startInverval;
            d2.reverse = false;
            d2.loopCount = 0;
            d2.loopInterval = 0.4f;
            d2.loopBackInterval = 0;
            d2.pingpongLoop = false;
            d2.continuousLoop = false;
            d2.interval = 0.7f;
            d2.singleTime = 0.1f;
            d2.sortType = SortType.FrontToBack;
            d2.startOpacity = 1;
            d2.opacity = 0;
            d2.easingType = EasingType.QuadInOut;
			d2.fadeMode = ColorMode.Multiply;

            presetsD2 = new BaseEffect[] { d2 };

            CGFadeA01 d3 = ScriptableObject.CreateInstance<CGFadeA01>();

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
            d3.startOpacity = 1;
            d3.opacity = 0;
            d3.easingType = EasingType.QuadOut;
			d3.fadeMode = ColorMode.Multiply;

            presetsD3 = new BaseEffect[] { d3 };

            CGFadeA01 d4 = ScriptableObject.CreateInstance<CGFadeA01>();

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
            d4.startOpacity = 1;
            d4.opacity = 0;
            d4.easingType = EasingType.QuadIn;
			d4.fadeMode = ColorMode.Multiply;

            presetsD4 = new BaseEffect[] { d4 };

            AddAnimatexts();
        }
    }
}