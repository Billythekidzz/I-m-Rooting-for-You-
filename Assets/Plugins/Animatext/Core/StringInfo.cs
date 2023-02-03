// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

namespace Animatext
{
    public struct StringInfo
    {
        public string text;
        public int[] mappings;
        public Range[] ranges;

        public static StringInfo GetStringInfo(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            int length = text.Length;

            StringInfo stringInfo;

            stringInfo.text = text;
            stringInfo.mappings = new int[length];
            stringInfo.ranges = new Range[1] { new Range(-1, length - 1) };

            for (int i = 0; i < length; i++)
            {
                stringInfo.mappings[i] = i;
            }

            return stringInfo;
        }
    }
}