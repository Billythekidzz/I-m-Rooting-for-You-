// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Bounce/Bounce - A03", fileName = "New CRBounceA03 Preset", order = 369)]
    public sealed class CRBounceA03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startScale = Vector2.one;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
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
            progress = EasingUtility.Bounce(progress, bounces, bounciness);

            range.Scale(Vector2.LerpUnclamped(startScale, scale, progress), range.GetAnchorPoint(anchorType) + anchorOffset);
        }
    }
}