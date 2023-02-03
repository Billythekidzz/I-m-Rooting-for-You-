// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class AmplitudeScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, 0);
            SetExample(titleB, 1.5f);
            SetExample(titleC, 3);
            SetExample(titleD, 4.5f);
        }

        private void SetExample(GameObject gameObject, float amplitude)
        {
            if (gameObject == null) return;

            TRBackA03 preset = ScriptableObject.CreateInstance<TRBackA03>();

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
            preset.scale = Vector2.zero;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.amplitude = amplitude;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }
    }
}