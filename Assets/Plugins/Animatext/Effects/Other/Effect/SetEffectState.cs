// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Effect/State", fileName = "New SetEffectState Preset", order = 369)]
    public sealed class SetEffectState : BaseEffect
    {
        public float executionTime = 5;
        public EffectState effectState = EffectState.Stop;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                switch (effectState)
                {
                    case EffectState.Stop:
                        effect.Stop();
                        break;

                    case EffectState.Start:
                        effect.Start();
                        break;

                    case EffectState.Play:
                        effect.Play();
                        break;

                    case EffectState.Pause:
                        effect.Pause();
                        break;

                    case EffectState.End:
                        effect.End();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}