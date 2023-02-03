// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class TagOrderScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, "<AT/#0>Ani</AT><AT/#-1>ma<color=\"#3087db\">t</AT><AT/#1>ext</color></AT>");
            SetExample(titleB, "<AT>Ani</AT><AT/#-1>ma<color=\"#3087db\">t</AT><AT/#1>ext</color></AT>");
            SetExample(titleC, "<AT>Ani</AT/#0><AT>ma<color=\"#3087db\">t</AT/#-1><AT>ext</color></AT/#1>");
            SetExample(titleD, "<AT>Ani</AT><AT>ma<color=\"#3087db\">t</AT/#-1><AT>ext</color></AT/#1>");
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