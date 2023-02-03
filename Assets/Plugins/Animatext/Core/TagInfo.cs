// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

namespace Animatext
{
    public struct TagInfo
    {
        public string tag;
        public int type;
        public int id;
        public int index;
        public Range originRange;
        public int order;
        public bool haveOrder;
        public float interval;
        public IntervalType intervalType;
    }
}