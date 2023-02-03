// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Word/Bounce/Bounce - B03", fileName = "New TWBounceB03 Preset", order = 369)]
    public sealed class TWBounceB03 : DefaultTemplateEffect
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
            get { return InfoFlags.Word; }
        }

        protected override int unitCount
        {
            get { return wordCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < wordCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, wordCount, sortType));

                if (progress <= fadeRange.start)
                {
                    words[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        words[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        words[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    float progressA = EasingUtility.EaseBounce(progress, bounces, bounciness, out progress);
                    Vector2 anchorPoint = words[i].GetAnchorPoint(anchorType) + anchorOffset;

                    words[i].Scale(Vector2.LerpUnclamped(Vector2.one, scale, progressA), anchorPoint);
                    words[i].Rotate(rotation * progress, anchorPoint);
                    words[i].Move(positionA * progressA + positionB * progress);
                }
            }
        }
    }
}
