// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

namespace Animatext
{
    public struct RangeInfo
    {
        public string tag;
        public int id;
        public Range range;
        public Range originRange;
        public int openingOrder;
        public float openingInterval;
        public IntervalType openingIntervalType;
        public int closingOrder;
        public float closingInterval;
        public IntervalType closingIntervalType;
    }
}