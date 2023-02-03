// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Elastic/Elastic - A02", fileName = "New CRElasticA02 Preset", order = 369)]
    public sealed class CRElasticA02 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float startRotation = 0;
        public float rotation = 45;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int oscillations = 2;
        public float stiffness = 5;
        public EasingType easingType;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Range; }
        }

        protected override int unitCount
        {
            get { return 1; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            float progress = GetCurrentProgress(0);

            progress = EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.Elastic(progress, oscillations, stiffness);

            range.Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), range.GetAnchorPoint(anchorType) + anchorOffset);
        }
    }
}