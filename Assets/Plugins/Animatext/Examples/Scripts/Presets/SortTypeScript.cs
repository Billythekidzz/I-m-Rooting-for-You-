// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class SortTypeScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;
        public GameObject titleF;
        public GameObject titleG;
        public GameObject titleH;
        public GameObject titleI;
        public GameObject titleJ;

        private void Start()
        {
            SetExample(titleA, SortType.FrontToBack);
            SetExample(titleB, SortType.BackToFront);
            SetExample(titleC, SortType.FrontToMiddle);
            SetExample(titleD, SortType.MiddleToFront);
            SetExample(titleE, SortType.BackToMiddle);
            SetExample(titleF, SortType.MiddleToBack);
            SetExample(titleG, SortType.MiddleToSidesFront);
            SetExample(titleH, SortType.MiddleToSidesBack);
            SetExample(titleI, SortType.SidesToMiddleFront);
            SetExample(titleJ, SortType.SidesToMiddleBack);
        }

        private void SetExample(GameObject gameObject, SortType sortType)
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
            preset.continuousLoop = false;
            preset.interval = 0.25f;
            preset.singleTime = 0.25f;
            preset.sortType = sortType;
            preset.position = new Vector2(0, 30);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}