// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class AnchorTypeScript : BaseExampleScript
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

        private void Start()
        {
            SetExample(titleA, AnchorType.TopLeft);
            SetExample(titleB, AnchorType.Top);
            SetExample(titleC, AnchorType.TopRight);
            SetExample(titleD, AnchorType.Left);
            SetExample(titleE, AnchorType.Center);
            SetExample(titleF, AnchorType.Right);
            SetExample(titleG, AnchorType.BottomLeft);
            SetExample(titleH, AnchorType.Bottom);
            SetExample(titleI, AnchorType.BottomRight);
        }

        private void SetExample(GameObject gameObject, AnchorType anchorType)
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
            preset.anchorType = anchorType;
            preset.anchorOffset = Vector2.zero;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.5f);

            AddAnimatext(gameObject, preset);
        }
    }
}