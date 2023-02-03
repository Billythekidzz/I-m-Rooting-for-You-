// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Group/Fade/Fade - A02", fileName = "New TGFadeA02 Preset", order = 369)]
    public sealed class TGFadeA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Group; }
        }

        protected override int unitCount
        {
            get { return groupCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < groupCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, groupCount, sortType));

                progress = 1 - EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.EaseBounce(progress, bounces, bounciness);

                groups[i].Opacify(1 - progress, fadeMode);
            }
        }
    }
}