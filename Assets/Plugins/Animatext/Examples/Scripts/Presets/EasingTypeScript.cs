// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class EasingTypeScript : BaseExampleScript
    {
        public GameObject titleA1;
        public GameObject titleB1;
        public GameObject titleB2;
        public GameObject titleB3;
        public GameObject titleC1;
        public GameObject titleC2;
        public GameObject titleC3;
        public GameObject titleD1;
        public GameObject titleD2;
        public GameObject titleD3;
        public GameObject titleE1;
        public GameObject titleE2;
        public GameObject titleE3;
        public GameObject titleF1;
        public GameObject titleF2;
        public GameObject titleF3;
        public GameObject titleG1;
        public GameObject titleG2;
        public GameObject titleG3;

        private void Start()
        {
            SetExample(titleA1, EasingType.Linear);
            SetExample(titleB1, EasingType.QuadIn);
            SetExample(titleB2, EasingType.QuadOut);
            SetExample(titleB3, EasingType.QuadInOut);
            SetExample(titleC1, EasingType.CubicIn);
            SetExample(titleC2, EasingType.CubicOut);
            SetExample(titleC3, EasingType.CubicInOut);
            SetExample(titleD1, EasingType.QuartIn);
            SetExample(titleD2, EasingType.QuartOut);
            SetExample(titleD3, EasingType.QuartInOut);
            SetExample(titleE1, EasingType.QuintIn);
            SetExample(titleE2, EasingType.QuintOut);
            SetExample(titleE3, EasingType.QuintInOut);
            SetExample(titleF1, EasingType.CircIn);
            SetExample(titleF2, EasingType.CircOut);
            SetExample(titleF3, EasingType.CircInOut);
            SetExample(titleG1, EasingType.SineIn);
            SetExample(titleG2, EasingType.SineOut);
            SetExample(titleG3, EasingType.SineInOut);
        }

        private void SetExample(GameObject gameObject, EasingType esingType)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

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
            preset.position = new Vector2(0, 30);
            preset.easingType = esingType;
            preset.fadeMode = ColorMode.Multiply;

            AddAnimatext(gameObject, preset);
        }
    }
}