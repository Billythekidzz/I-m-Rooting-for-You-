// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System;
using UnityEngine;

namespace Animatext
{
    [Serializable]
    public struct FloatRange
    {
        public float start;
        public float end;

        public FloatRange(float start, float end)
        {
            this.start = start;
            this.end = end;
        }

        public FloatRange(Vector2 range)
        {
            start = range.x;
            end = range.y;
        }
    }
}