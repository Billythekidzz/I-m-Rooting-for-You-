// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Custom/Custom - A01", fileName = "New TRCustomA01 Preset", order = 369)]
    public sealed class TRCustomA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public Vector2 position = new Vector2(0, 100);
        public AnimationCurve positionCurve = AnimationCurve.Linear(0, 0, 1, 1);
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
                progress = 1 - positionCurve.Evaluate(progress);

                range.Move(position * progress);
            }
        }
    }
}