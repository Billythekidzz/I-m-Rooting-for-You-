// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class LoopBackIntervalScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExampleA(titleA, 0);
            SetExampleA(titleB, 2);
            SetExampleB(titleC, 0);
            SetExampleB(titleD, 2);
        }

        private void SetExampleA(GameObject gameObject, float loopBackInterval)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 4;
            preset.loopBackInterval = loopBackInterval;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB(GameObject gameObject, float loopBackInterval)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = true;
            preset.loopCount = 0;
            preset.loopInterval = 4;
            preset.loopBackInterval = loopBackInterval;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}