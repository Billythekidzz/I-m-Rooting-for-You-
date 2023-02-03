// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class OscillationsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, 0);
            SetExample(titleB, 1);
            SetExample(titleC, 2);
            SetExample(titleD, 3);
        }

        private void SetExample(GameObject gameObject, int oscillations)
        {
            if (gameObject == null) return;

            TRElasticA02 preset = ScriptableObject.CreateInstance<TRElasticA02>();

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
            preset.rotation = -24;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.oscillations = oscillations;
            preset.stiffness = 2;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }
    }
}