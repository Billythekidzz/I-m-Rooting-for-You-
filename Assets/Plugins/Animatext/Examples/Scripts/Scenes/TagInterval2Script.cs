// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TagInterval2Script : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, "<AT>Ani</AT><AT/+1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 1);
            SetExample(titleB, "<AT>Ani</AT><AT/=1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 1.5f);
            SetExample(titleC, "<AT>Ani</AT><AT/?1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 3);
            SetExample(titleD, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 3.5f);
        }

        private void SetExample(GameObject gameObject, string text, float loopInterval)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = "AT";
            preset.startInterval = 0.5f;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = loopInterval;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 1.5f;
            preset.singleTime = 1.5f;
            preset.position = new Vector2(0, 15);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, text, preset);
        }
    }
}