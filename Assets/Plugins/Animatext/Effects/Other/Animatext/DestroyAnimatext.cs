// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Animatext/Destroy", fileName = "New DestroyAnimatext Preset", order = 369)]
    public sealed class DestroyAnimatext : BaseEffect
    {
        public float executionTime = 5;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                Destroy(animatext);
            }
        }
    }
}