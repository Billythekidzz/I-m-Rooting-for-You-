// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class UnitTagScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExampleA(titleA);
            SetExampleB(titleB);
            SetExampleC(titleC);
            SetExampleD(titleD);
        }

        private void SetExampleA(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 0.5f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "Anima<color=\"#3087db\"><c>text</color>", preset);
        }

        private void SetExampleB(GameObject gameObject)
        {
            if (gameObject == null) return;

            TWBasicA01 preset = ScriptableObject.CreateInstance<TWBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1;
            preset.singleTime = 1;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "Ani<w>ma</w><color=\"#3087db\">text</color>", preset);
        }

        private void SetExampleC(GameObject gameObject)
        {
            if (gameObject == null) return;

            TLBasicA01 preset = ScriptableObject.CreateInstance<TLBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1.5f;
            preset.singleTime = 1.5f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "Ani<l>ma<color=\"#3087db\"><l>te</l>xt</l></color>", preset);
        }

        private void SetExampleD(GameObject gameObject)
        {
            if (gameObject == null) return;

            TGBasicA01 preset = ScriptableObject.CreateInstance<TGBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1;
            preset.singleTime = 1;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<g>Anima</g><g/><color=\"#3087db\"><g>text</g></color>", preset);
        }
    }
}