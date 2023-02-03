// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class PositionABScript : BaseExampleScript
    {
        public GameObject titleA1;
        public GameObject titleA2;
        public GameObject titleA3;
        public GameObject titleA4;
        public GameObject titleB1;
        public GameObject titleB2;
        public GameObject titleB3;
        public GameObject titleB4;

        private void Start()
        {
            SetExampleA1(titleA1);
            SetExampleA2(titleA2);
            SetExampleA3(titleA3);
            SetExampleA4(titleA4);
            SetExampleB1(titleB1);
            SetExampleB2(titleB2);
            SetExampleB3(titleB3);
            SetExampleB4(titleB4);
        }

        private void SetExampleA1(GameObject gameObject)
        {
            if (gameObject == null) return;

            TRElasticB04 preset = ScriptableObject.CreateInstance<TRElasticB04>();

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
            preset.positionA = new Vector2(0, 30);
            preset.positionB = Vector2.zero;
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.oscillations = 2;
            preset.stiffness = 3;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleA2(GameObject gameObject)
        {
            if (gameObject == null) return;

            CRBounceB04 preset = ScriptableObject.CreateInstance<CRBounceB04>();

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
            preset.startPosition = Vector2.zero;
            preset.startRotation = 0;
            preset.startScale = Vector2.one;
            preset.positionA = new Vector2(0, 30);
            preset.positionB = Vector2.zero;
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.bounces = 2;
            preset.bounciness = 0.75f;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = false;

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleA3(GameObject gameObject)
        {
            if (gameObject == null) return;

            TRBackB04 preset = ScriptableObject.CreateInstance<TRBackB04>();

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
            preset.positionA = new Vector2(0, 30);
            preset.positionB = Vector2.zero;
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.amplitude = 4;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleA4(GameObject gameObject)
        {
            if (gameObject == null) return;

            CRWaveB04 preset = ScriptableObject.CreateInstance<CRWaveB04>();

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
            preset.startPosition = Vector2.zero;
            preset.startRotation = 0;
            preset.startScale = Vector2.one;
            preset.positionA = new Vector2(0, 30);
            preset.positionB = Vector2.zero;
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.waves = 1;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = false;

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB1(GameObject gameObject)
        {
            if (gameObject == null) return;

            TRElasticB04 preset = ScriptableObject.CreateInstance<TRElasticB04>();

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
            preset.positionA = Vector2.zero;
            preset.positionB = new Vector2(0, 30);
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.oscillations = 2;
            preset.stiffness = 3;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB2(GameObject gameObject)
        {
            if (gameObject == null) return;

            CRBounceB04 preset = ScriptableObject.CreateInstance<CRBounceB04>();

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
            preset.startPosition = Vector2.zero;
            preset.startRotation = 0;
            preset.startScale = Vector2.one;
            preset.positionA = Vector2.zero;
            preset.positionB = new Vector2(0, 30);
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.bounces = 2;
            preset.bounciness = 0.75f;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = false;

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB3(GameObject gameObject)
        {
            if (gameObject == null) return;

            TRBackB04 preset = ScriptableObject.CreateInstance<TRBackB04>();

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
            preset.positionA = Vector2.zero;
            preset.positionB = new Vector2(0, 30);
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.amplitude = 4;
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 0.25f);

            AddAnimatext(gameObject, preset);
        }

        private void SetExampleB4(GameObject gameObject)
        {
            if (gameObject == null) return;

            CRWaveB04 preset = ScriptableObject.CreateInstance<CRWaveB04>();

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
            preset.startPosition = Vector2.zero;
            preset.startRotation = 0;
            preset.startScale = Vector2.one;
            preset.positionA = Vector2.zero;
            preset.positionB = new Vector2(0, 30);
            preset.rotation = 0;
            preset.scale = Vector2.one;
            preset.anchorType = AnchorType.Center;
            preset.anchorOffset = Vector2.zero;
            preset.waves = 1;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = false;

            AddAnimatext(gameObject, preset);
        }
    }
}