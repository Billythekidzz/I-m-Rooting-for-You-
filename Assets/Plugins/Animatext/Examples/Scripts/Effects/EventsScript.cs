// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using System.Collections;
using UnityEngine;

namespace Animatext.Examples
{
    public class EventsScript : BaseExampleScript
    {
        public GameObject titleA;
        public GameObject titleB;
        public GameObject titleC;
        public GameObject titleD;
        public GameObject titleE;

        private void Start()
        {
            SetExampleA(titleA);
            SetExampleB(titleB);
            SetExampleC(titleC);
            SetExampleD(titleD);
            SetExampleE(titleE);
        }

        private void SetExampleA(GameObject gameObject)
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
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onStart += LogOnStart;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(StartEffects(animatext));
        }

        private void SetExampleB(GameObject gameObject)
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
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onPlay += LogOnPlay;
            effect.onProceed += LogOnProceed;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(PlayEffects(animatext));
        }

        private void SetExampleC(GameObject gameObject)
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
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onPause += LogOnPause;
            effect.time = 2.5f;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(PauseEffects(animatext));
        }

        private void SetExampleD(GameObject gameObject)
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
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onEnd += LogOnEnd;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(EndEffects(animatext));
        }

        private void SetExampleE(GameObject gameObject)
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
            preset.interval = 0.5f;
            preset.singleTime = 1;
            preset.position = new Vector2(0, 45);
            preset.easingType = EasingType.Linear;
            preset.fadeMode = ColorMode.Multiply;
            preset.fadeRange = new FloatRange(0, 1);

            Effect effect = new Effect(preset);

            effect.autoStart = false;
            effect.autoPlay = false;
            effect.onStop += LogOnStop;

            BaseAnimatext animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
            
            StartCoroutine(StopEffects(animatext));
        }

        private IEnumerator StartEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.StartEffects();

                    break;
                }
            }
        }

        private IEnumerator PlayEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();

                    break;
                }
            }
        }

        private IEnumerator PauseEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();
                    animatext.PauseEffects();

                    break;
                }
            }
        }

        private IEnumerator EndEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.EndEffects();

                    break;
                }
            }
        }

        private IEnumerator StopEffects(BaseAnimatext animatext)
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    animatext.PlayEffects();
                    animatext.StopEffects();

                    break;
                }
            }
        }

        private void LogOnStart()
        {
            Debug.Log("OnStart - EffectState.Start");
        }

        private void LogOnPlay()
        {
            Debug.Log("OnPlay - EffectState.Play");
        }

        private void LogOnProceed()
        {
            Debug.Log("OnProceed - EffectState.Play");
        }

        private void LogOnPause()
        {
            Debug.Log("OnPause - EffectState.Pause");
        }

        private void LogOnEnd()
        {
            Debug.Log("OnEnd - EffectState.End");
        }

        private void LogOnStop()
        {
            Debug.Log("OnStop - EffectState.Stop");
        }
    }
}