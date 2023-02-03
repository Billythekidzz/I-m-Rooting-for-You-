// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Line/Step/Step - A04", fileName = "New TLStepA04 Preset", order = 369)]
    public sealed class TLStepA04 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int steps = 5;
        public StepType stepType = StepType.Round;
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

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.EaseStep(progress, steps, stepType);

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

                    progress = 1 - progress;

                    lines[i].Skew(skew * progress, lines[i].GetAnchorPoint(anchorType) + anchorOffset);
                }
            }
        }
    }
}