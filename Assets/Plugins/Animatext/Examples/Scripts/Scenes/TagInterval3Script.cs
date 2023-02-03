// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TagInterval3Script : BaseExampleScript
    {
        public GameObject titleA1;
        public GameObject titleA2;
        public GameObject titleA3;
        public GameObject titleA4;
        public GameObject titleB1;
        public GameObject titleB2;
        public GameObject titleB3;
        public GameObject titleB4;
        public GameObject titleC1;
        public GameObject titleC2;
        public GameObject titleC3;
        public GameObject titleC4;

        private void Start()
        {
            SetExample(titleA1, "<AT>Ani</AT><AT/+1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 1);
            SetExample(titleA2, "<AT>Ani</AT><AT/=1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 1.5f);
            SetExample(titleA3, "<AT>Ani</AT><AT/?1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 3);
            SetExample(titleA4, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT><AT>ext</color></AT>", 3.5f);
            SetExample(titleB1, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT/+1><AT>ext</color></AT>", 1);
            SetExample(titleB2, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT/=1><AT>ext</color></AT>", 2.5f);
            SetExample(titleB3, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT/?1><AT>ext</color></AT>", 3.5f);
            SetExample(titleB4, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT/!1><AT>ext</color></AT>", 4);
            SetExample(titleC1, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT/+1><AT>ext</color></AT>", 2.5f);
            SetExample(titleC2, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT/=1><AT>ext</color></AT>", 4);
            SetExample(titleC3, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT/?1><AT>ext</color></AT>", 3.5f);
            SetExample(titleC4, "<AT>Ani</AT><AT/!1>ma<color=\"#3087db\">t</AT/!1><AT>ext</color></AT>", 5);
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