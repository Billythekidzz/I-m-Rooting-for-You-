// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0090

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class BackgroundTextScript : BaseExampleScript
    {
        public Vector2 position;
        public SortType sortType;

        private void Start()
        {
            CCBasicA01 preset = ScriptableObject.CreateInstance<CCBasicA01>();

            preset.tag = string.Empty;
            preset.startInterval = 0;
            preset.reverse = false;
            preset.loopCount = 0;
            preset.loopInterval = 0;
            preset.loopBackInterval = 0;
            preset.pingpongLoop = false;
            preset.continuousLoop = true;
            preset.interval = 2;
            preset.singleTime = 8;
            preset.sortType = sortType;
            preset.startPosition = Vector2.zero;
            preset.position = position;
            preset.easingType = EasingType.Linear;
            preset.continuousEasing = true;

            Effect effect = new Effect(preset);

            effect.time = 8;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(effect);
            animatext.Refresh(true);
        }
    }
}