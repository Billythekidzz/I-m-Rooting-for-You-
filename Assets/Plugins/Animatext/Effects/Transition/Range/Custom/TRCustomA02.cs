// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Custom/Custom - A02", fileName = "New TRCustomA02 Preset", order = 369)]
    public sealed class TRCustomA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public AnimationCurve rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public AnimationCurve fadeCurve = AnimationCurve.Linear(0, 0, 1, 1);

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
            float fadeValue = fadeCurve.Evaluate(progress);

            range.Opacify(fadeValue, fadeMode);

            if (fadeValue > 0)
            {
                progress = 1 - rotationCurve.Evaluate(progress);

                range.Rotate(rotation * progress, range.GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}