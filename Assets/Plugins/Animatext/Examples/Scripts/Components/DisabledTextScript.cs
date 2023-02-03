// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using System.Collections;
using UnityEngine;

namespace Animatext.Examples
{
    public class DisabledTextScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExample(titleA, DisabledText.InputText);
            SetExample(titleB, DisabledText.ParsedText);
            SetExample(titleC, DisabledText.OutputText);
            SetExample(titleD, DisabledText.EffectText);
            SetExample(titleE, DisabledText.BlankText);
        }

        private void SetExample(GameObject gameObject, DisabledText disabledText)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = "AT";
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

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.settings.disabledText = disabledText;
            animatext.effects.Add(new Effect(preset));
            animatext.SetText("<AT>Anima</AT><color=\"#3087db\">text</color>");

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

                    Debug.Log(animatext.settings.disabledText + ": " + animatext.text.Replace(">", "\u001E>"));

                    break;
                }
            }
        }
    }
}