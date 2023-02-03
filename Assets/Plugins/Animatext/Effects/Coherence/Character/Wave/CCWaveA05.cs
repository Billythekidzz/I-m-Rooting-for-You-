// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    // [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Wave/Wave - A05", fileName = "New CCWaveA05 Preset", order = 369)]
    public sealed class CCWaveA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public float rotation = 180;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = new Vector2(0, 50);
        public int waves = 1;
        public EasingType easingType;

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
                progress = EasingUtility.Wave(progress, waves);

                characters[i].Move(PositionUtility.Rotate(characters[i].anchor.center, Mathf.LerpUnclamped(startRotation, rotation, progress), characters[i].GetAnchorPoint(anchorType) + anchorOffset) - characters[i].anchor.center);
            }
        }
    }
}