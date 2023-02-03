// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Fade/Fade - A01", fileName = "New CRFadeA01 Preset", order = 369)]
    public sealed class CRFadeA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float startOpacity = 1;
        public float opacity = 0;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;

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
            progress = EasingUtility.Basic(progress, true);

            range.Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
        }
    }
}