// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Word/Custom/Custom - A05", fileName = "New CWCustomA05 Preset", order = 369)]
    public sealed class CWCustomA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startOpacity = 1;
        public float opacity = 0;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 0, 0, 2), new Keyframe(0.5f, 1, 2, -2), new Keyframe(1, 0, -2, 1));

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

                progress = fadeCurve.Evaluate(progress);

                words[i].Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
            }
        }
    }
}