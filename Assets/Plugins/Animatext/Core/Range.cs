// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

namespace Animatext
{
    public struct Range
    {
        public int startIndex;
        public int endIndex;
        public int count;

        public Range(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            count = endIndex - startIndex + 1;
        }

        public static Range empty
        {
            get
            {
                return new Range { startIndex = -1, endIndex = -1, count = -1 };
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Range a, Range b)
        {
            return a.startIndex == b.startIndex && a.endIndex == b.endIndex && a.count == b.count;
        }

        public static bool operator !=(Range a, Range b)
        {
            return a.startIndex != b.startIndex || a.endIndex != b.endIndex || a.count != b.count;
        }
    }
}