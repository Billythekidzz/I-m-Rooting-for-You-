// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Effect/Time", fileName = "New SetEffectTime Preset", order = 369)]
    public sealed class SetEffectTime : BaseEffect
    {
        public float executionTime = 5;
        public float effectTime = 0;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                effect.time = effectTime;
            }
        }
    }
}