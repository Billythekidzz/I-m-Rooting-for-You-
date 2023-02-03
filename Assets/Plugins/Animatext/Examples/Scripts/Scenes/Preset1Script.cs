// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class Preset1Script : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExampleA(titleA);
            SetExampleB(titleB);
            SetExampleC(titleC);
            SetExampleD(titleD);
            SetExampleE(titleE);
        }

        private void SetExampleA(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0.5f;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.0625f;
            preset.singleTime = 0.0625f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(9, 0);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB(GameObject gameObject)
        {
            if (gameObject == null) return;

            TWBasicA01 preset = ScriptableObject.CreateInstance<TWBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0.5f;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.375f;
            preset.singleTime = 0.375f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(15, 0);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleC(GameObject gameObject)
        {
            if (gameObject == null) return;

            TLBasicA01 preset = ScriptableObject.CreateInstance<TLBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0.5f;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.75f;
            preset.singleTime = 0.75f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(15, 0);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleD(GameObject gameObject)
        {
            if (gameObject == null) return;

            TGBasicA01 preset = ScriptableObject.CreateInstance<TGBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0.5f;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.125f;
            preset.singleTime = 0.125f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(9, 0);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<g>A</g><g>nim</g><g>a</g><g>ted</g> <color=\"#3087db\"><g>Te</g><g>xt</g></color>\n<g>An</g><g>i</g><g>mat</g><g>ed</g> <color=\"#3087db\"><g>Tex</g><g>t</g></color>", preset);
        }

        private void SetExampleE(GameObject gameObject)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0.5f;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1.5f;
            preset.singleTime = 1.5f;
            preset.position = new Vector2(15, 0);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}