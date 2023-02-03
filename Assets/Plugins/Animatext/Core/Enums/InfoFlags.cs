// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System;

namespace Animatext
{
    [Flags]
    public enum InfoFlags
    {
        None = 0,
        Char = 1,
        Character = 2,
        Word = 4,
        Line = 8,
        Group = 16,
        Range = 32,
    }
}