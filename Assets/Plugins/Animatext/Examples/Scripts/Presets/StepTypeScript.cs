// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class StepTypeScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;

        private void Start()
        {
            SetExample(titleA, StepType.Round);
            SetExample(titleB, StepType.Ceil);
            SetExample(titleC, StepType.Floor);
        }

        private void SetExample(GameObject gameObject, StepType stepType)
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
            preset.steps = 5;
            preset.stepType = stepType;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(-1, -1);

            AddAnimatext(gameObject, preset);
        }
    }
}