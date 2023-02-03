// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Fade/Fade - A02", fileName = "New TRFadeA02 Preset", order = 369)]
    public sealed class TRFadeA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public int bounces = 2;
        public float bounciness = 0.5f;
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

            progress = 1 - EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.EaseBounce(progress, bounces, bounciness);

            range.Opacify(1 - progress, fadeMode);
        }
    }
}