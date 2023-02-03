// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class MultipresetEffectScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;

        private void Start()
        {
            SetExampleA(titleA);
            SetExampleB(titleB);
            SetExampleC(titleC);
            SetExampleD(titleD);
        }

        private void SetExampleA(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 presetA = ScriptableObject.CreateInstance<TCBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = false;
            presetA.loopCount = 0;
            presetA.loopInterval = 1;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = false;
            presetA.interval = 0.25f;
            presetA.singleTime = 0.25f;
            presetA.sortType = SortType.FrontToBack;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetB = ScriptableObject.CreateInstance<TCBasicA03>();

            presetB.tag = "AT1";
            presetB.startInterval = 2;
            presetB.reverse = true;
            presetB.loopCount = 0;
            presetB.loopInterval = 1;
            presetB.loopBackInterval = 0;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = false;
            presetB.interval = 0.25f;
            presetB.singleTime = 0.25f;
            presetB.sortType = SortType.FrontToBack;
            presetB.scale = Vector2.zero;
            presetB.anchorType = AnchorType.Center;
            presetB.anchorOffset = Vector2.zero;
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Multiply;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetC = ScriptableObject.CreateInstance<TCBasicA03>();

            presetC.tag = "AT2";
            presetC.startInterval = 0;
            presetC.reverse = false;
            presetC.loopCount = 0;
            presetC.loopInterval = 1;
            presetC.loopBackInterval = 0;
            presetC.pingpongLoop = false;
            presetC.continuousLoop = false;
            presetC.interval = 0.25f;
            presetC.singleTime = 0.25f;
            presetC.sortType = SortType.FrontToBack;
            presetC.scale = Vector2.zero;
            presetC.anchorType = AnchorType.Center;
            presetC.anchorOffset = Vector2.zero;
            presetC.easingType = EasingType.Linear;
            presetC.fadeMode = ColorMode.Multiply;
            presetC.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA01 presetD = ScriptableObject.CreateInstance<TCBasicA01>();

            presetD.tag = "AT2";
            presetD.startInterval = 2;
            presetD.reverse = true;
            presetD.loopCount = 0;
            presetD.loopInterval = 1;
            presetD.loopBackInterval = 0;
            presetD.pingpongLoop = false;
            presetD.continuousLoop = false;
            presetD.interval = 0.25f;
            presetD.singleTime = 0.25f;
            presetD.sortType = SortType.FrontToBack;
            presetD.position = new Vector2(0, 30);
            presetD.easingType = EasingType.Linear;
            presetD.fadeMode = ColorMode.Multiply;
            presetD.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<AT1>Anima</AT1><AT2><color=\"#3087db\">text</color></AT2>", presetA, presetB, presetC, presetD);
        }

        private void SetExampleB(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 presetA = ScriptableObject.CreateInstance<TCBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = true;
            presetA.loopCount = 0;
            presetA.loopInterval = 1;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = false;
            presetA.interval = 0.25f;
            presetA.singleTime = 0.25f;
            presetA.sortType = SortType.FrontToBack;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetB = ScriptableObject.CreateInstance<TCBasicA03>();

            presetB.tag = "AT1";
            presetB.startInterval = 2;
            presetB.reverse = false;
            presetB.loopCount = 0;
            presetB.loopInterval = 1;
            presetB.loopBackInterval = 0;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = false;
            presetB.interval = 0.25f;
            presetB.singleTime = 0.25f;
            presetB.sortType = SortType.FrontToBack;
            presetB.scale = Vector2.zero;
            presetB.anchorType = AnchorType.Center;
            presetB.anchorOffset = Vector2.zero;
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Difference; // Note: fadeMode = ColorMode.Difference;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetC = ScriptableObject.CreateInstance<TCBasicA03>();

            presetC.tag = "AT2";
            presetC.startInterval = 0;
            presetC.reverse = true;
            presetC.loopCount = 0;
            presetC.loopInterval = 1;
            presetC.loopBackInterval = 0;
            presetC.pingpongLoop = false;
            presetC.continuousLoop = false;
            presetC.interval = 0.25f;
            presetC.singleTime = 0.25f;
            presetC.sortType = SortType.FrontToBack;
            presetC.scale = Vector2.zero;
            presetC.anchorType = AnchorType.Center;
            presetC.anchorOffset = Vector2.zero;
            presetC.easingType = EasingType.Linear;
            presetC.fadeMode = ColorMode.Multiply;
            presetC.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA01 presetD = ScriptableObject.CreateInstance<TCBasicA01>();

            presetD.tag = "AT2";
            presetD.startInterval = 2;
            presetD.reverse = false;
            presetD.loopCount = 0;
            presetD.loopInterval = 1;
            presetD.loopBackInterval = 0;
            presetD.pingpongLoop = false;
            presetD.continuousLoop = false;
            presetD.interval = 0.25f;
            presetD.singleTime = 0.25f;
            presetD.sortType = SortType.FrontToBack;
            presetD.position = new Vector2(0, 30);
            presetD.easingType = EasingType.Linear;
            presetD.fadeMode = ColorMode.Difference; // Note: fadeMode = ColorMode.Difference;
            presetD.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<AT1>Anima</AT1><AT2><color=\"#3087db\">text</color></AT2>", presetA, presetB, presetC, presetD);
        }

        private void SetExampleC(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 presetA = ScriptableObject.CreateInstance<TCBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = false;
            presetA.loopCount = 0;
            presetA.loopInterval = 2;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = true;
            presetA.interval = 0.25f;
            presetA.singleTime = 0.25f;
            presetA.sortType = SortType.FrontToBack;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetB = ScriptableObject.CreateInstance<TCBasicA03>();

            presetB.tag = "AT1";
            presetB.startInterval = 1;
            presetB.reverse = true;
            presetB.loopCount = 0;
            presetB.loopInterval = 2;
            presetB.loopBackInterval = 1; // Note: loopBackInterval = startInterval;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = true;
            presetB.interval = 0.25f;
            presetB.singleTime = 0.25f;
            presetB.sortType = SortType.FrontToBack;
            presetB.scale = Vector2.zero;
            presetB.anchorType = AnchorType.Center;
            presetB.anchorOffset = Vector2.zero;
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Multiply;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetC = ScriptableObject.CreateInstance<TCBasicA03>();

            presetC.tag = "AT2";
            presetC.startInterval = 0;
            presetC.reverse = false;
            presetC.loopCount = 0;
            presetC.loopInterval = 2;
            presetC.loopBackInterval = 0;
            presetC.pingpongLoop = false;
            presetC.continuousLoop = true;
            presetC.interval = 0.25f;
            presetC.singleTime = 0.25f;
            presetC.sortType = SortType.FrontToBack;
            presetC.scale = Vector2.zero;
            presetC.anchorType = AnchorType.Center;
            presetC.anchorOffset = Vector2.zero;
            presetC.easingType = EasingType.Linear;
            presetC.fadeMode = ColorMode.Multiply;
            presetC.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA01 presetD = ScriptableObject.CreateInstance<TCBasicA01>();

            presetD.tag = "AT2";
            presetD.startInterval = 1;
            presetD.reverse = true;
            presetD.loopCount = 0;
            presetD.loopInterval = 2;
            presetD.loopBackInterval = 1; // Note: loopBackInterval = startInterval;
            presetD.pingpongLoop = false;
            presetD.continuousLoop = true;
            presetD.interval = 0.25f;
            presetD.singleTime = 0.25f;
            presetD.sortType = SortType.FrontToBack;
            presetD.position = new Vector2(0, 30);
            presetD.easingType = EasingType.Linear;
            presetD.fadeMode = ColorMode.Multiply;
            presetD.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<AT1>Anima</AT1><AT2><color=\"#3087db\">text</color></AT2>", presetA, presetB, presetC, presetD);
        }

        private void SetExampleD(GameObject gameObject)
        {
            if (gameObject == null) return;

            TCBasicA01 presetA = ScriptableObject.CreateInstance<TCBasicA01>();

            presetA.tag = "AT1";
            presetA.startInterval = 0;
            presetA.reverse = true;
            presetA.loopCount = 0;
            presetA.loopInterval = 2;
            presetA.loopBackInterval = 0;
            presetA.pingpongLoop = false;
            presetA.continuousLoop = true;
            presetA.interval = 0.25f;
            presetA.singleTime = 0.25f;
            presetA.sortType = SortType.FrontToBack;
            presetA.position = new Vector2(0, 30);
            presetA.easingType = EasingType.Linear;
            presetA.fadeMode = ColorMode.Multiply;
            presetA.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetB = ScriptableObject.CreateInstance<TCBasicA03>();

            presetB.tag = "AT1";
            presetB.startInterval = 1;
            presetB.reverse = false;
            presetB.loopCount = 0;
            presetB.loopInterval = 2;
            presetB.loopBackInterval = 1; // Note: loopBackInterval = startInterval;
            presetB.pingpongLoop = false;
            presetB.continuousLoop = true;
            presetB.interval = 0.25f;
            presetB.singleTime = 0.25f;
            presetB.sortType = SortType.FrontToBack;
            presetB.scale = Vector2.zero;
            presetB.anchorType = AnchorType.Center;
            presetB.anchorOffset = Vector2.zero;
            presetB.easingType = EasingType.Linear;
            presetB.fadeMode = ColorMode.Difference; // Note: fadeMode = ColorMode.Difference;
            presetB.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA03 presetC = ScriptableObject.CreateInstance<TCBasicA03>();

            presetC.tag = "AT2";
            presetC.startInterval = 0;
            presetC.reverse = true;
            presetC.loopCount = 0;
            presetC.loopInterval = 2;
            presetC.loopBackInterval = 0;
            presetC.pingpongLoop = false;
            presetC.continuousLoop = true;
            presetC.interval = 0.25f;
            presetC.singleTime = 0.25f;
            presetC.sortType = SortType.FrontToBack;
            presetC.scale = Vector2.zero;
            presetC.anchorType = AnchorType.Center;
            presetC.anchorOffset = Vector2.zero;
            presetC.easingType = EasingType.Linear;
            presetC.fadeMode = ColorMode.Multiply;
            presetC.fadeRange = new FloatRange(0, 0.5f);

            TCBasicA01 presetD = ScriptableObject.CreateInstance<TCBasicA01>();

            presetD.tag = "AT2";
            presetD.startInterval = 1;
            presetD.reverse = false;
            presetD.loopCount = 0;
            presetD.loopInterval = 2;
            presetD.loopBackInterval = 1; // Note: loopBackInterval = startInterval;
            presetD.pingpongLoop = false;
            presetD.continuousLoop = true;
            presetD.interval = 0.25f;
            presetD.singleTime = 0.25f;
            presetD.sortType = SortType.FrontToBack;
            presetD.position = new Vector2(0, 30);
            presetD.easingType = EasingType.Linear;
            presetD.fadeMode = ColorMode.Difference; // Note: fadeMode = ColorMode.Difference;
            presetD.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, "<AT1>Anima</AT1><AT2><color=\"#3087db\">text</color></AT2>", presetA, presetB, presetC, presetD);
        }
    }
}