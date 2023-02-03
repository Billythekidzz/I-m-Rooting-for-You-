// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Preset/Empty", fileName = "New Empty Preset", order = 369)]
    public sealed class Empty : BaseEffect
    {
        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate() { }
    }
}