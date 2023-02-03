// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Group/Bounce/Bounce - B05", fileName = "New CGBounceB05 Preset", order = 369)]
    public sealed class CGBounceB05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 startSkew = Vector2.zero;
        public float rotation = 45;
        public Vector2 scale = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Group; }
        }

        protected override int unitCount
        {
            get { return groupCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < groupCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, groupCount, sortType));

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Bounce(progress, bounces, bounciness);

                Vector2 anchorPoint = groups[i].GetAnchorPoint(anchorType) + anchorOffset;

                groups[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
                groups[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
                groups[i].Skew(Vector2.LerpUnclamped(startSkew, skew, progress), anchorPoint);
            }
        }
    }
}