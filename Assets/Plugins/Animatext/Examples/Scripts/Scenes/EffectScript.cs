// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class EffectScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, "<AT1>Ani</AT1><AT2>ma<color=\"#3087db\">t</AT2><AT2>ext</color></AT2>");
            SetExample(titleB, "<AT2>Ani</AT2><AT1>ma<color=\"#3087db\">t</AT1><AT2>ext</color></AT2>");
            SetExample(titleC, "<AT2>Ani</AT2><AT2>ma<color=\"#3087db\">t</AT2><AT1>ext</color></AT1>");
            SetExampleD(titleD, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>");
        }

        private void SetExample(GameObject gameObject, string text)
        {
            if (gameObject == null) return;

            TRBasicA01 presetA = ScriptableObject.CreateInstance<TRBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = false;
            presetA.loopCount = 0;
            presetA.loopInterval = 3;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = false;
            presetA.interval = 1;
            presetA.singleTime = 1;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            Effect effectA = new Effect(presetA);

            TRBasicA01 presetB = ScriptableObject.CreateInstance<TRBasicA01>();

            presetB.tag = "AT2";
            presetB.startInterval = 0;
            presetB.reverse = false;
            presetB.loopCount = 0;
            presetB.loopInterval = 2;
            presetB.loopBackInterval = 0;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = false;
            presetB.interval = 1;
            presetB.singleTime = 1;
            presetB.position = new Vector2(0, 30);
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Multiply;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            Effect effectB = new Effect(presetB);

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effectA);
            animatext.effects.Add(effectB);
            animatext.SetText(text);
        }

        private void SetExampleD(GameObject gameObject, string text)
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
            preset.interval = 1;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, text, preset);
        }
    }
}