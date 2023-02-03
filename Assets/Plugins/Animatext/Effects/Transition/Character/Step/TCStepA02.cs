// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Character/Step/Step - A02", fileName = "New TCStepA02 Preset", order = 369)]
    public sealed class TCStepA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.25f);

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

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.EaseStep(progress, steps, stepType);

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

                    progress = 1 - progress;

                    characters[i].Rotate(rotation * progress, characters[i].GetAnchorPoint(anchorType) + anchorOffset);
                }
            }
        }
    }
}