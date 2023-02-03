// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class RefreshModeScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExample(titleA, RefreshMode.Start);
            SetExample(titleB, RefreshMode.Replay);
            SetExample(titleC, RefreshMode.Pause);
            SetExample(titleD, RefreshMode.Continue);
            SetExample(titleE, RefreshMode.End);
        }

        private void SetExample(GameObject gameObject, RefreshMode refreshMode)
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
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.refreshMode = refreshMode;
            effect.time = 1;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true); // EffectState = Stop
            animatext.PlayEffects(); // EffectState = Play
            animatext.RefreshEffects();
        }
    }
}