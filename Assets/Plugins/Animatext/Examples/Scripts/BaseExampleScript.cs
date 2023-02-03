// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEngine;

namespace Animatext.Examples
{
    public class BaseExampleScript : MonoBehaviour
    {
        protected void AddAnimatext(GameObject gameObject, params BaseEffect[] presets)
        {
            if (gameObject == null) return;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(new Effect(presets));
            animatext.Refresh(true);
        }

        protected void AddAnimatext(GameObject gameObject, string text, params BaseEffect[] presets)
        {
            if (gameObject == null) return;

            AnimatextUGUI animatext = gameObject.AddComponent<AnimatextUGUI>();

            animatext.effects.Add(new Effect(presets));
            animatext.SetText(text);
        }
    }
}