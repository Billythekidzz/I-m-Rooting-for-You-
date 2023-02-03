// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Wave/Wave - A01", fileName = "New CRWaveA01 Preset", order = 369)]
    public sealed class CRWaveA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startPosition = Vector2.zero;
        public Vector2 position = new Vector2(0, 100);
        public int waves = 1;
        public EasingType easingType;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Range; }
        }

        protected override int unitCount
        {
            get { return 1; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            float progress = GetCurrentProgress(0);

            progress = EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.Wave(progress, waves);

            range.Move(Vector2.LerpUnclamped(startPosition, position, progress));
        }
    }
}