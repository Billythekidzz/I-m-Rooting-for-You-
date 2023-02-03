// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Character/Basic/Basic - B03", fileName = "New TCBasicB03 Preset", order = 369)]
    public sealed class TCBasicB03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 position = new Vector2(0, 100);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Character; }
        }

        protected override int unitCount
        {
            get { return characterCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < characterCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, characterCount, sortType));

                if (progress <= fadeRange.start)
                {
                    characters[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        characters[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        characters[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    Vector2 anchorPoint = characters[i].GetAnchorPoint(anchorType) + anchorOffset;

                    characters[i].Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), anchorPoint);
                    characters[i].Move(position * progress);
                    characters[i].Rotate(rotation * progress, anchorPoint);
                }
            }
        }
    }
}