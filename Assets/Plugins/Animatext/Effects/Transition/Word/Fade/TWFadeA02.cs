// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Word/Fade/Fade - A02", fileName = "New TWFadeA02 Preset", order = 369)]
    public sealed class TWFadeA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Word; }
        }

        protected override int unitCount
        {
            get { return wordCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < wordCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, wordCount, sortType));

                progress = 1 - EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.EaseBounce(progress, bounces, bounciness);

                words[i].Opacify(1 - progress, fadeMode);
            }
        }
    }
}