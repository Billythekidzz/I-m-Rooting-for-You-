// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class StepsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, 2);
            SetExample(titleB, 4);
            SetExample(titleC, 6);
            SetExample(titleD, 8);
        }

        private void SetExample(GameObject gameObject, int steps)
        {
            if (gameObject == null) return;

            TRStepA01 preset = ScriptableObject.CreateInstance<TRStepA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 4;
            preset.position = new Vector2(0, 100);
            preset.steps = steps;
            preset.stepType = StepType.Round;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(-1, -1);

            AddAnimatext(gameObject, preset);
        }
    }
}