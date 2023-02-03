// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Word/Bounce/Bounce - A02", fileName = "New CWBounceA02 Preset", order = 369)]
    public sealed class CWBounceA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;

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

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Bounce(progress, bounces, bounciness);

                words[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), words[i].GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}