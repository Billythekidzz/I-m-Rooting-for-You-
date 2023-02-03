// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Word/Wave/Wave - A03", fileName = "New CWWaveA03 Preset", order = 369)]
    public sealed class CWWaveA03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 startScale = Vector2.one;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int waves = 1;
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
                progress = EasingUtility.Wave(progress, waves);

                words[i].Scale(Vector2.LerpUnclamped(startScale, scale, progress), words[i].GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}