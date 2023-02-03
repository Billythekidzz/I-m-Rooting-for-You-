// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Bounce/Bounce - B01", fileName = "New CCBounceB01 Preset", order = 369)]
    public sealed class CCBounceB01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startPosition = Vector2.zero;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 positionA = new Vector2(0, 100);
        public Vector2 positionB = new Vector2(100, 0);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
        public EasingType easingType;
        public bool continuousEasing = true;

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

                float progressA = EasingUtility.Bounce(progress, bounces, bounciness, out progress);
                Vector2 anchorPoint = characters[i].GetAnchorPoint(anchorType) + anchorOffset;

                progress = EasingUtility.Basic(progress, continuousEasing);

                characters[i].Scale(Vector2.LerpUnclamped(startScale, scale, progressA), anchorPoint);
                characters[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progressA), anchorPoint);
                characters[i].Move(Vector2.LerpUnclamped(startPosition, positionA, progressA) + Vector2.LerpUnclamped(startPosition, positionB, progress));
            }
        }
    }
}