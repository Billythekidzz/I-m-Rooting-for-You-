// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Group/Basic/Basic - B05", fileName = "New TGBasicB05 Preset", order = 369)]
    public sealed class TGBasicB05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float rotation = 45;
        public Vector2 scale = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

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

                if (progress <= fadeRange.start)
                {
                    groups[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        groups[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        groups[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    Vector2 anchorPoint = groups[i].GetAnchorPoint(anchorType) + anchorOffset;

                    groups[i].Rotate(rotation * progress, anchorPoint);
                    groups[i].Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), anchorPoint);
                    groups[i].Skew(skew * progress, anchorPoint);
                }
            }
        }
    }
}