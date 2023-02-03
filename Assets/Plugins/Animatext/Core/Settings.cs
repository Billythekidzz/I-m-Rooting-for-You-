// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System;

namespace Animatext
{
    [Serializable]
    public class Settings
    {
        public RetainedText retainedText;
        public DisabledText disabledText;
        public DisabledEffects disabledEffects;
        public TagSymbols tagSymbols;
        public MarkerSymbols markerSymbols;
    }
}