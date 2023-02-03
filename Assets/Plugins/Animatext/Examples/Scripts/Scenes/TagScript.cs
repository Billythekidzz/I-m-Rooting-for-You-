// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TagScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExample(titleA, "<AT>Anima<color=\"#3087db\">text</color>");
            SetExample(titleB, "<AT>Anima</AT><color=\"#3087db\">text</color>");
            SetExample(titleC, "Anima<AT/><color=\"#3087db\">text</color>");
            SetExample(titleD, "<AT><AT>Anima</><color=\"#3087db\">text</color>");
            SetExample(titleE, "<AT><AT>Anima<//><color=\"#3087db\">text</color>");
        }

        private void SetExample(GameObject gameObject, string text)
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
            preset.interval = 0;
            preset.singleTime = 2;
            preset.position = new Vector2(0, 24);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, text, preset);
        }
    }
}