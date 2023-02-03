// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class AnchorOffsetScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExample(titleA, new Vector2(0, 0));
            SetExample(titleB, new Vector2(0, 100));
            SetExample(titleC, new Vector2(100, 0));
            SetExample(titleD, new Vector2(100, 100));
        }

        private void SetExample(GameObject gameObject, Vector2 offset)
        {
            if (gameObject == null) return;

            TRBasicA03 preset = ScriptableObject.CreateInstance<TRBasicA03>();

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
            preset.scale = Vector2.zero;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = offset;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}