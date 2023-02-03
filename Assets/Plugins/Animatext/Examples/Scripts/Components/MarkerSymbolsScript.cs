// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0066

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class MarkerSymbolsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;

        private void Start()
        {
            SetExample(titleA, MarkerSymbols.Slashes);
            SetExample(titleB, MarkerSymbols.Backslashes);
            SetExample(titleC, MarkerSymbols.VerticalBars);
        }

        private void SetExample(GameObject gameObject, MarkerSymbols markerSymbols)
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
            preset.fadeRange = new FloatRange(0, 0.5f);

            char markerChar;

            switch (markerSymbols)
            {
                case MarkerSymbols.Slashes:
                    markerChar = '/';
                    break;

                case MarkerSymbols.Backslashes:
                    markerChar = '\\';
                    break;

                case MarkerSymbols.VerticalBars:
                    markerChar = '|';
                    break;

                default:
                    markerChar = '/';
                    break;
            }

            string text = '<' + preset.tag + '>' + "Anima<color=\"#3087db\">text</color>" + '<' + markerChar + preset.tag + '>';

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.settings.markerSymbols = markerSymbols;
            animatext.effects.Add(new Effect(preset));
            animatext.SetText(text);
        }
    }
}