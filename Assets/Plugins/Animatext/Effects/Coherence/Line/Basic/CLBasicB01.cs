// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Line/Basic/Basic - B01", fileName = "New CLBasicB01 Preset", order = 369)]
    public sealed class CLBasicB01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startPosition = Vector2.zero;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 position = new Vector2(0, 100);
        public float rotation = 180;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public EasingType easingType;
        public bool continuousEasing = true;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Line; }
        }

        protected override int unitCount
        {
            get { return lineCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < lineCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, lineCount, sortType));

                progress = EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.Basic(progress, continuousEasing);

                Vector2 anchorPoint = lines[i].GetAnchorPoint(anchorType) + anchorOffset;

                lines[i].Move(Vector2.LerpUnclamped(startPosition, position, progress));
                lines[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
                lines[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
            }
        }
    }
}