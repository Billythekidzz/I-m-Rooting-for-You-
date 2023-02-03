// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class ContinuousEasingScript : BaseExampleScript
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

        private void SetExampleA(GameObject gameObject, bool continuousEasing)
        {
            if (gameObject == null) return;

            CCBasicA01 preset = ScriptableObject.CreateInstance<CCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 0.5f;
            preset.sortType = SortType.FrontToBack;
            preset.startPosition = Vector2.zero;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = continuousEasing;

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB(GameObject gameObject, bool continuousEasing)
        {
            if (gameObject == null) return;

            CCBasicA02 preset = ScriptableObject.CreateInstance<CCBasicA02>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 1;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = false;
            preset.interval = 0.5f;
            preset.singleTime = 0.5f;
            preset.sortType = SortType.FrontToBack;
            preset.startRotation = 0;
            preset.rotation = -180;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = continuousEasing;

            AddAnimatext(gameObject, preset);
        }
    }
}