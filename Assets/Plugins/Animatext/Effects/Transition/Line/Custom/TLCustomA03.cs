// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Line/Custom/Custom - A03", fileName = "New TLCustomA03 Preset", order = 369)]
    public sealed class TLCustomA03 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 scale = Vector2.zero;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public AnimationCurve fadeCurve = AnimationCurve.Linear(0, 0, 1, 1);

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
                float fadeValue = fadeCurve.Evaluate(progress);

                lines[i].Opacify(fadeValue, fadeMode);
                    
                if (fadeValue > 0)
                {
                    progress = 1 - scaleCurve.Evaluate(progress);

                    lines[i].Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), lines[i].GetAnchorPoint(anchorType) + anchorOffset);
                }
            }
        }
    }
}