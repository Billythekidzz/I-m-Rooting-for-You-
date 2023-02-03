// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Fade/Fade - A02", fileName = "New CCFadeA02 Preset", order = 369)]
    public sealed class CCFadeA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startOpacity = 1;
        public float opacity = 0;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;

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
                progress = EasingUtility.Bounce(progress, bounces, bounciness);

                characters[i].Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
            }
        }
    }
}