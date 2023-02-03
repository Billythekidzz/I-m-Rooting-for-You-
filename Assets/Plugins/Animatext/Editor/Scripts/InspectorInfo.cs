// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using System.Collections.Generic;
using UnityEngine;

namespace Animatext.Editor
{
    public class InspectorInfo : ScriptableObject
    {
        public string text;
        public readonly List<EffectInspectorInfo> list = new List<EffectInspectorInfo>();
    }
}