// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Custom/Custom - A01", fileName = "New CRCustomA01 Preset", order = 369)]
    public sealed class CRCustomA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startPosition = Vector2.zero;
        public Vector2 position = new Vector2(0, 100);
        public AnimationCurve positionCurve = new AnimationCurve(new Keyframe(0, 0, 0, 2), new Keyframe(0.5f, 1, 2, -2), new Keyframe(1, 0, -2, 1));

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

            progress = positionCurve.Evaluate(progress);

            range.Move(Vector2.LerpUnclamped(startPosition, position, progress));
        }
    }
}