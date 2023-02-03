// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Custom/Custom - A02", fileName = "New CCCustomA02 Preset", order = 369)]
    public sealed class CCCustomA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startRotation = 0;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public AnimationCurve rotationCurve = new AnimationCurve(new Keyframe(0, 0, 0, 2), new Keyframe(0.5f, 1, 2, -2), new Keyframe(1, 0, -2, 1));

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

                progress = rotationCurve.Evaluate(progress);

                characters[i].Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), characters[i].GetAnchorPoint(anchorType) + anchorOffset);
            }
        }
    }
}