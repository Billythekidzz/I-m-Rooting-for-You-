// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Line/Custom/Custom - A01", fileName = "New TLCustomA01 Preset", order = 369)]
    public sealed class TLCustomA01 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public Vector2 position = new Vector2(0, 100);
        public AnimationCurve positionCurve = AnimationCurve.Linear(0, 0, 1, 1);
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
                    progress = 1 - positionCurve.Evaluate(progress);

                    lines[i].Move(position * progress);
                }
            }
        }
    }
}