// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TagSymbolsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, TagSymbols.AngleBrackets);
            SetExample(titleB, TagSymbols.RoundBrackets);
            SetExample(titleC, TagSymbols.SquareBrackets);
            SetExample(titleD, TagSymbols.CurlyBrackets);
        }

        private void SetExample(GameObject gameObject, TagSymbols tagSymbols)
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

            char openingChar;
            char closingChar;

            switch (tagSymbols)
            {
                case TagSymbols.AngleBrackets:
                    openingChar = '<';
                    closingChar = '>';
                    break;

                case TagSymbols.RoundBrackets:
                    openingChar = '(';
                    closingChar = ')';
                    break;

                case TagSymbols.SquareBrackets:
                    openingChar = '[';
                    closingChar = ']';
                    break;

                case TagSymbols.CurlyBrackets:
                    openingChar = '{';
                    closingChar = '}';
                    break;

                default:
                    openingChar = '<';
                    closingChar = '>';
                    break;
            }

            string text = openingChar + preset.tag + closingChar + "Anima<color=\"#3087db\">text</color>" + openingChar + '/' + preset.tag + closingChar;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.settings.tagSymbols = tagSymbols;
            animatext.effects.Add(new Effect(preset));
            animatext.SetText(text);
        }
    }
}