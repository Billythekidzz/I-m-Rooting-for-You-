// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using UnityEngine;

namespace Animatext.Effects
{
    public abstract class DefaultTemplateEffect : BaseEffect
    {
        [Space(5)]
        public bool reverse;

        [Space(5)] [Tooltip("How many times the preset loops itself. It means infinite loop when it's value is less than or equal 0.")]
        public int loopCount;

        [Tooltip("The interval between every two consecutive loops.")]
        public float loopInterval;

        [Tooltip("The loop interval to execute the next loop.")]
        public float loopBackInterval;

        [Tooltip("Whether to execute the loop in a back-and-forth manner.")]
        public bool pingpongLoop;

        [Tooltip("Whether the unit loops continuously. \"Yes\" means the unit will loop continuously. \"No\" means the unit will wait for all other units to finish before starting its next loop.")]
        public bool continuousLoop;

        [Space(5)]
        public float interval = 0.5f;

        protected abstract int unitCount { get; }
        protected abstract float unitTime { get; }

        protected override float GetRangeInterval()
        {
            return unitCount * interval;
        }

        protected override float GetExecutionInterval()
        {
            return unitCount > 0 ? (unitCount - 1) * interval + unitTime : 0;
        }

        protected float GetCurrentProgress(int index)
        {
            int loopIndex;
           
            float value = time;
            float unitTime = Mathf.Max(this.unitTime, float.Epsilon);
            float loopTime = loopInterval;

            if (continuousLoop)
            {
                value -= interval * index;
                loopTime += unitTime;
                loopIndex = Mathf.Max(Mathf.FloorToInt(value / loopTime), 0);
            }
            else
            {
                loopTime += totalInterval;
                loopIndex = Mathf.Max(Mathf.FloorToInt((value + frontInterval) / loopTime), 0);
                value -= interval * index;
            }

            if (loopCount <= loopIndex + 1 && loopCount >= 1)
            {
                loopIndex = loopCount - 1;
                value = Mathf.Clamp((value - loopIndex * loopTime) / unitTime, 0, 1);
            }
            else
            {
                value -= loopIndex * loopTime;

                if (value > unitTime)
                {
                    if (value == float.PositiveInfinity)
                    {
                        value = 1;
                    }
                    else if (continuousLoop)
                    {
                        value = (pingpongLoop ? value - unitTime : value + loopBackInterval - unitTime) < loopInterval ? 1 : 0;
                    }
                    else
                    {
                        value = (pingpongLoop ? value + frontInterval + interval * index : value + loopBackInterval + frontInterval + interval * index) < loopTime ? 1 : 0;
                    }
                }
                else
                {
                    value = Mathf.Max(value / unitTime, 0);
                }
            }

            if (pingpongLoop && (loopIndex & 1) != 0)
            {
                value = 1 - value;
            }

            if (reverse)
            {
                value = 1 - value;
            }

            return value;
        }
    }
}