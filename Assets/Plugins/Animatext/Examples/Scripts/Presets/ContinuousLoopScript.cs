// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class ContinuousLoopScript : BaseExampleScript
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

        private void SetExampleA(GameObject gameObject, bool continuousLoop)
        {
            if (gameObject == null) return;

            TCBasicA01 preset = ScriptableObject.CreateInstance<TCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = continuousLoop;
            preset.interval = 0.25f;
            preset.singleTime = 0.25f;
            preset.sortType = SortType.FrontToBack;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB(GameObject gameObject, bool continuousLoop)
        {
            if (gameObject == null) return;

            TCBasicA03 preset = ScriptableObject.CreateInstance<TCBasicA03>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = continuousLoop;
            preset.interval = 0.25f;
            preset.singleTime = 0.25f;
            preset.sortType = SortType.FrontToBack;
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