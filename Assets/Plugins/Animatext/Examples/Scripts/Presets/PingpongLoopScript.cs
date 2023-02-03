// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class PingpongLoopScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExampleA(titleA, false);
            SetExampleA(titleB, true);
            SetExampleB(titleC, false);
            SetExampleB(titleD, true);
        }

        private void SetExampleA(GameObject gameObject, bool pingpongLoop)
        {
            if (gameObject == null) return;

            TRBasicA01 preset = ScriptableObject.CreateInstance<TRBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = pingpongLoop;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB(GameObject gameObject, bool pingpongLoop)
        {
            if (gameObject == null) return;

            TRBasicA03 preset = ScriptableObject.CreateInstance<TRBasicA03>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = pingpongLoop;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.scale = Vector2.zero;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}