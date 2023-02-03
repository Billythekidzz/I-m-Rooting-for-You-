// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Character/Custom/Custom - A05", fileName = "New CCCustomA05 Preset", order = 369)]
    public sealed class CCCustomA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float startOpacity = 1;
        public float opacity = 0;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 0, 0, 2), new Keyframe(0.5f, 1, 2, -2), new Keyframe(1, 0, -2, 1));

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

                progress = fadeCurve.Evaluate(progress);

                characters[i].Opacify(Mathf.LerpUnclamped(startOpacity, opacity, progress), fadeMode);
            }
        }
    }
}