// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Effect/Speed", fileName = "New SetEffectSpeed Preset", order = 369)]
    public sealed class SetEffectSpeed : BaseEffect
    {
        public float executionTime = 5;
        public float effectSpeed = 1.5f;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                effect.speed = effectSpeed;
            }
        }
    }
}