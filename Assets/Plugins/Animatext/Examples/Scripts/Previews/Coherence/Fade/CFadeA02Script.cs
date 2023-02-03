// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class CFadeA02Script : BaseEffectScript
    {
        private void Start()
        {
            CCFadeA02 a1 = ScriptableObject.CreateInstance<CCFadeA02>();

            a1.tag = tagName;
            a1.startInterval = startInverval;
            a1.reverse = true;
            a1.loopCount = 0;
            a1.loopInterval = 0.4f;
            a1.loopBackInterval = 0;
            a1.pingpongLoop = false;
            a1.continuousLoop = false;
            a1.interval = 0.3f;
            a1.singleTime = 0.3f;
            a1.sortType = SortType.MiddleToSidesFront;
            a1.startOpacity = 1;
            a1.opacity = 0;
            a1.bounces = 1;
            a1.bounciness = 0.9f;
            a1.easingType = EasingType.Linear;
			a1.fadeMode = ColorMode.Multiply;

            presetsA1 = new BaseEffect[] { a1 };

            CCFadeA02 a2 = ScriptableObject.CreateInstance<CCFadeA02>();

            a2.tag = tagName;
            a2.startInterval = startInverval;
            a2.reverse = false;
            a2.loopCount = 0;
            a2.loopInterval = 0.4f;
            a2.loopBackInterval = 0;
            a2.pingpongLoop = false;
            a2.continuousLoop = false;
            a2.interval = 0.3f;
            a2.singleTime = 0.3f;
            a2.sortType = SortType.MiddleToSidesBack;
            a2.startOpacity = 1;
            a2.opacity = 0;
            a2.bounces = 1;
            a2.bounciness = 0.9f;
            a2.easingType = EasingType.Linear;
			a2.fadeMode = ColorMode.Multiply;

            presetsA2 = new BaseEffect[] { a2 };

            CCFadeA02 a3 = ScriptableObject.CreateInstance<CCFadeA02>();

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
            a3.bounces = 2;
            a3.bounciness = 0.95f;
            a3.easingType = EasingType.Linear;
			a3.fadeMode = ColorMode.Multiply;

            presetsA3 = new BaseEffect[] { a3 };

            CCFadeA02 a4 = ScriptableObject.CreateInstance<CCFadeA02>();

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
            a4.bounces = 2;
            a4.bounciness = 0.95f;
            a4.easingType = EasingType.Linear;
			a4.fadeMode = ColorMode.Multiply;

            presetsA4 = new BaseEffect[] { a4 };

            CWFadeA02 b1 = ScriptableObject.CreateInstance<CWFadeA02>();

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
            b1.bounces = 2;
            b1.bounciness = 0.95f;
            b1.easingType = EasingType.Linear;
			b1.fadeMode = ColorMode.Multiply;

            presetsB1 = new BaseEffect[] { b1 };

            CWFadeA02 b2 = ScriptableObject.CreateInstance<CWFadeA02>();

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
            b2.bounces = 2;
            b2.bounciness = 0.95f;
            b2.easingType = EasingType.Linear;
			b2.fadeMode = ColorMode.Multiply;

            presetsB2 = new BaseEffect[] { b2 };

            CWFadeA02 b3 = ScriptableObject.CreateInstance<CWFadeA02>();

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
            b3.bounces = 1;
            b3.bounciness = 0.9f;
            b3.easingType = EasingType.Linear;
			b3.fadeMode = ColorMode.Multiply;

            presetsB3 = new BaseEffect[] { b3 };

            CWFadeA02 b4 = ScriptableObject.CreateInstance<CWFadeA02>();

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
            b4.bounces = 1;
            b4.bounciness = 0.9f;
            b4.easingType = EasingType.Linear;
			b4.fadeMode = ColorMode.Multiply;

            presetsB4 = new BaseEffect[] { b4 };

            CLFadeA02 c1 = ScriptableObject.CreateInstance<CLFadeA02>();

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
            c1.bounces = 4;
            c1.bounciness = 0.95f;
            c1.easingType = EasingType.Linear;
			c1.fadeMode = ColorMode.Multiply;

            presetsC1 = new BaseEffect[] { c1 };

            CLFadeA02 c2 = ScriptableObject.CreateInstance<CLFadeA02>();

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
            c2.bounces = 4;
            c2.bounciness = 0.95f;
            c2.easingType = EasingType.Linear;
			c2.fadeMode = ColorMode.Multiply;

            presetsC2 = new BaseEffect[] { c2 };

            CLFadeA02 c3 = ScriptableObject.CreateInstance<CLFadeA02>();

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
            c3.bounces = 2;
            c3.bounciness = 0.9f;
            c3.easingType = EasingType.Linear;
			c3.fadeMode = ColorMode.Multiply;

            presetsC3 = new BaseEffect[] { c3 };

            CLFadeA02 c4 = ScriptableObject.CreateInstance<CLFadeA02>();

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
            c4.bounces = 2;
            c4.bounciness = 0.9f;
            c4.easingType = EasingType.Linear;
			c4.fadeMode = ColorMode.Multiply;

            presetsC4 = new BaseEffect[] { c4 };

            CGFadeA02 d1 = ScriptableObject.CreateInstance<CGFadeA02>();

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
            d1.startOpacity = 1;
            d1.opacity = 0;
            d1.bounces = 1;
            d1.bounciness = 0.9f;
            d1.easingType = EasingType.Linear;
			d1.fadeMode = ColorMode.Multiply;

            presetsD1 = new BaseEffect[] { d1 };

            CGFadeA02 d2 = ScriptableObject.CreateInstance<CGFadeA02>();

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
            d2.startOpacity = 1;
            d2.opacity = 0;
            d2.bounces = 1;
            d2.bounciness = 0.9f;
            d2.easingType = EasingType.Linear;
			d2.fadeMode = ColorMode.Multiply;

            presetsD2 = new BaseEffect[] { d2 };

            CGFadeA02 d3 = ScriptableObject.CreateInstance<CGFadeA02>();

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
            d3.bounces = 2;
            d3.bounciness = 0.95f;
            d3.easingType = EasingType.Linear;
			d3.fadeMode = ColorMode.Multiply;

            presetsD3 = new BaseEffect[] { d3 };

            CGFadeA02 d4 = ScriptableObject.CreateInstance<CGFadeA02>();

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
            d4.bounces = 2;
            d4.bounciness = 0.95f;
            d4.easingType = EasingType.Linear;
			d4.fadeMode = ColorMode.Multiply;

            presetsD4 = new BaseEffect[] { d4 };

            AddAnimatexts();
        }
    }
}