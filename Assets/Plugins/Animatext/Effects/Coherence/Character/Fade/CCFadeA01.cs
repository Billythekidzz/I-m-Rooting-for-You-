// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Fade/Fade - A01", fileName = "New CCFadeA01 Preset", order = 369)]
    public sealed class CCFadeA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startOpacity = 1;
        public float opacity = 0;
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
                progress = EasingUtility.Basic(progress, true);

                characters[i].Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
            }
        }
    }
}