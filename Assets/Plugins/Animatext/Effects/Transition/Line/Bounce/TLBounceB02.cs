// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Line/Bounce/Bounce - B02", fileName = "New TLBounceB02 Preset", order = 369)]
    public sealed class TLBounceB02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 positionA = new Vector2(0, 100);
        public Vector2 positionB = new Vector2(100, 0);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.25f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Line; }
        }

        protected override int unitCount
        {
            get { return lineCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < lineCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, lineCount, sortType));

                if (progress <= fadeRange.start)
                {
                    lines[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        lines[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        lines[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    float progressA = EasingUtility.EaseBounce(progress, bounces, bounciness, out progress);
                    Vector2 anchorPoint = lines[i].GetAnchorPoint(anchorType) + anchorOffset;

                    lines[i].Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), anchorPoint);
                    lines[i].Rotate(rotation * progressA, anchorPoint);
                    lines[i].Move(positionA * progressA + positionB * progress);
                }
            }
        }
    }
}