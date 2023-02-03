// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class BouncinessScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, 0);
            SetExample(titleB, 0.5f);
            SetExample(titleC, 1);
            SetExample(titleD, 1.2f);
        }

        private void SetExample(GameObject gameObject, float bounciness)
        {
            if (gameObject == null) return;

            TRBounceA03 preset = ScriptableObject.CreateInstance<TRBounceA03>();

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
            preset.scale = new Vector2(0, 0);
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.bounces = 2;
            preset.bounciness = bounciness;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }
    }
}