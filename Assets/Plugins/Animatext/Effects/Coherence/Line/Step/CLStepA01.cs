// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Line/Step/Step - A01", fileName = "New CLStepA01 Preset", order = 369)]
    public sealed class CLStepA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startPosition = Vector2.zero;
        public Vector2 position = new Vector2(0, 100);
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        public bool continuousEasing = true;

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
                progress = EasingUtility.Basic(progress, continuousEasing);

                lines[i].Move(Vector2.LerpUnclamped(startPosition, position, progress));
            }
        }
    }
}