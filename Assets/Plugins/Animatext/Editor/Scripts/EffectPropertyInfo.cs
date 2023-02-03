// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System;
using UnityEditor;

namespace Animatext.Editor
{
    [Serializable]
    public class EffectPropertyInfo
    {
        public bool currentDirty;
        public SerializedProperty currentPreset;
        public SerializedProperty effect;
        public SerializedProperty presets;
        public SerializedProperty autoStart;
        public SerializedProperty autoPlay;
        public SerializedProperty refreshMode;
    }
}