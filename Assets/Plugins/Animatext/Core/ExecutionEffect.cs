// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System;
using UnityEngine;

namespace Animatext
{
    public class ExecutionEffect : IComparable<ExecutionEffect>
    {
        public Effect effect;
        public Vector3Int order;
        public float openingInterval;
        public IntervalType openingIntervalType;
        public float closingInterval;
        public IntervalType closingIntervalType;
        public int executionInfoCount;
        public ExecutionInfo[] executionInfo;
        public float startInterval;
        public float rangeInterval;
        public float executionInterval;

        public int CompareTo(ExecutionEffect other)
        {
            return order.x == other.order.x ? (order.y == other.order.y ? order.z - other.order.z : order.y - other.order.y) : order.x - other.order.x;
        }

        public void EarlyExecute(ref float time, ref float frontInterval)
        {
            if (executionInfoCount <= 0) return ;

            switch (openingIntervalType)
            {
                case IntervalType.None:
                    startInterval = executionInfo[0].preset.startInterval;
                    break;

                case IntervalType.Add:
                    startInterval = openingInterval + executionInfo[0].preset.startInterval;
                    break;

                case IntervalType.Equal:
                    startInterval = openingInterval;
                    break;

                case IntervalType.Replace:
                    if (effect.state == EffectState.Start)
                    {                  
                        time = 0;
                    }
                    else if (effect.state == EffectState.End)
                    {
                        time = float.PositiveInfinity;
                    }
                    else
                    {
                        time = effect.time;
                    }

                    frontInterval = 0;
                    startInterval = openingInterval + executionInfo[0].preset.startInterval;
                    break;

                case IntervalType.Cover:
                    if (effect.state == EffectState.Start)
                    {

                        time = 0;
                    }
                    else if (effect.state == EffectState.End)
                    {
                        time = float.PositiveInfinity;
                    }
                    else
                    {
                        time = effect.time;
                    }

                    frontInterval = 0;
                    startInterval = openingInterval;
                    break;

                default:
                    startInterval = executionInfo[0].preset.startInterval;
                    break;
            }

            time -= startInterval;
            frontInterval += startInterval;

            if (frontInterval < 0)
            {
                time += frontInterval;
                startInterval -= frontInterval;
                frontInterval = 0;
            }

            float executionInfoTime = time;       
            float executionInfoInterval = frontInterval;

            executionInfo[0].EarlyExecute(executionInfoTime, executionInfoInterval);

            rangeInterval = executionInfo[0].rangeInterval;
            executionInterval = executionInfo[0].executionInterval;

            for (int i = 1; i < executionInfoCount; i++)
            {
                executionInfoTime -= executionInfo[i].preset.startInterval;
                executionInfoInterval += executionInfo[i].preset.startInterval;

                if (executionInfoInterval < 0)
                {
                    executionInfoTime += executionInfoInterval;
                    executionInfoInterval = 0;
                }

                executionInfo[i].EarlyExecute(executionInfoTime, executionInfoInterval);

                executionInterval = Mathf.Max(executionInterval, executionInfo[i].executionInterval + executionInfoInterval - frontInterval);
            }

            switch (closingIntervalType)
            {
                case IntervalType.None:
                    break;

                case IntervalType.Add:
                    rangeInterval += closingInterval;
                    break;

                case IntervalType.Equal:
                    rangeInterval = closingInterval;
                    break;

                case IntervalType.Replace:
                    if (effect.state == EffectState.Start)
                    {
                        time = 0;
                    }
                    else if (effect.state == EffectState.End)
                    {
                        time = float.PositiveInfinity;
                    }
                    else
                    {
                        time = effect.time;
                    }

                    frontInterval = 0;
                    rangeInterval += closingInterval;
                    break;

                case IntervalType.Cover:
                    if (effect.state == EffectState.Start)
                    {
                        time = 0;
                    }
                    else if (effect.state == EffectState.End)
                    {
                        time = float.PositiveInfinity;
                    }
                    else
                    {
                        time = effect.time;
                    }

                    frontInterval = 0;
                    rangeInterval = closingInterval;
                    break;

                default:
                    break;
            }

            time -= rangeInterval;
            frontInterval += rangeInterval;

            if (frontInterval < 0)
            {
                time += frontInterval;
                rangeInterval -= frontInterval;
                frontInterval = 0;
            }
        }

        public void Execute()
        {
            for (int i = 0; i < executionInfoCount; i++)
            {
                executionInfo[i].Execute();
            }
        }

        public void SetTotalInterval(float totalInterval)
        {
            for (int i = 0; i < executionInfoCount; i++)
            {
                executionInfo[i].totalInterval = totalInterval;
            }
        }
    }
}