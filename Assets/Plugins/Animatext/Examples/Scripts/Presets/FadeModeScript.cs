// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class FadeModeScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;

        private void Start()
        {
            SetExample(titleA, ColorMode.Difference);
            SetExample(titleB, ColorMode.Multiply);
            SetExample(titleC, ColorMode.Replace);
        }

        private void SetExample(GameObject gameObject, ColorMode colorMode)
        {
            if (gameObject == null) return;

            TRFadeA01 preset = ScriptableObject.CreateInstance<TRFadeA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 2;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = colorMode;

            AddAnimatext(gameObject, preset);
        }
    }
}