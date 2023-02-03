// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class EffectStateScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExample(titleA, EffectState.Stop);
            SetExample(titleB, EffectState.Start);
            SetExample(titleC, EffectState.Play);
            SetExample(titleD, EffectState.Pause);
            SetExample(titleE, EffectState.End);
        }

        private void SetExample(GameObject gameObject, EffectState effectState)
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
            preset.singleTime = 1;
            preset.position = new Vector2(0, 24);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.refreshMode = RefreshMode.Start;
            effect.time = 0.25f;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);

            switch (effectState)
            {
                case EffectState.Stop:
                    break;

                case EffectState.Start:
                    effect.Start();
                    break;

                case EffectState.Play:
                    effect.Play();
                    break;

                case EffectState.Pause:
                    effect.Play();
                    effect.Pause();
                    break;

                case EffectState.End:
                    effect.End();
                    break;

                default:
                    break;
            }
        }
    }
}