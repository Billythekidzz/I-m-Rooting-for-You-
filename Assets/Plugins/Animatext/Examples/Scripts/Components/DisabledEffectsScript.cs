// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using System.Collections;
using UnityEngine;

namespace Animatext.Examples
{
    public class DisabledEffectsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, DisabledEffects.Stop);
            SetExample(titleB, DisabledEffects.Refresh);
            SetExample(titleC, DisabledEffects.Clear);
            SetExample(titleD, DisabledEffects.Retain);
        }

        private void SetExample(GameObject gameObject, DisabledEffects disabledEffects)
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
            preset.fadeRange = new FloatRange(0, 0.5f);

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.settings.disabledEffects = disabledEffects;
            animatext.effects.Add(new Effect(preset));
            animatext.SetText("Anima<color=\"#3087db\">text</color>");

            StartCoroutine(Disable(animatext));
        }

        private IEnumerator Disable(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.enabled = false;

                    if (animatext.effects.Count > 0)
                    {
                        Debug.Log(animatext.settings.disabledEffects + ": EffectState." + animatext.effects[0].state);
                    }
                    else
                    {
                        Debug.Log(animatext.settings.disabledEffects + ": No Effect");
                    }

                    break;
                }
            }
        }
    }
}