// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using UnityEngine;

namespace Animatext.Effects
{
    public abstract class SimpleTemplateEffect : BaseEffect
    {
        [Space(5)]
        public bool reverse;

        [Space(5)]
        public float interval = 0.5f;

        protected abstract int unitCount { get; }
        protected abstract float unitTime { get; }

        protected override float GetRangeInterval()
        {
            return interval * unitCount;
        }

        protected override float GetExecutionInterval()
        {
            return unitCount > 0 ? (unitCount - 1) * interval + unitTime : 0;
        }

        protected float GetCurrentProgress(int index)
        {
            float value = Mathf.Clamp((time - interval * index) / Mathf.Max(unitTime, float.Epsilon), 0, 1);

            if (reverse)
            {
                value = 1 - value;
            }

            return value;
        }
    }
}