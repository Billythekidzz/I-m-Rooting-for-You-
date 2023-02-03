// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Step/Step - A01", fileName = "New CRStepA01 Preset", order = 369)]
    public sealed class CRStepA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 startPosition = Vector2.zero;
        public Vector2 position = new Vector2(0, 100);
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        public bool continuousEasing = true;

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
            progress = EasingUtility.EaseStep(progress, steps, stepType);
            progress = EasingUtility.Basic(progress, continuousEasing);

            range.Move(Vector2.LerpUnclamped(startPosition, position, progress));
        }
    }
}