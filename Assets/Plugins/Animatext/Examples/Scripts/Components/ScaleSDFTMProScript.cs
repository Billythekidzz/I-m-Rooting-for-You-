// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class ScaleSDFTMProScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;

        private void Start()
        {
            SetExample(titleA, 0.5f);
            SetExample(titleB, 2);
        }

        private void SetExample(GameObject gameObject, float scaleSDF)
        {
            if (gameObject == null) return;

            CRBasicA03 preset = ScriptableObject.CreateInstance<CRBasicA03>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 2;
            preset.startScale = new Vector2(2, 2);
            preset.scale = new Vector2(2, 2);
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = true;

            AnimatextTMPro animatext = gameObject.AddComponent<AnimatextTMPro>();

            animatext.scaleSDF = scaleSDF;
            animatext.effects.Add(new Effect(preset));
            animatext.Refresh(true);
        }
    }
}