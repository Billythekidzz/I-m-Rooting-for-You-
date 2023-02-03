// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Other/Message/Broadcast", fileName = "New BroadcastMessage Preset", order = 369)]
    public sealed class BroadcastMessage : BaseEffect
    {
        public float executionTime = 5;
        public string methodName = string.Empty;

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.None; }
        }

        protected override void Animate()
        {
            if (lastTime < executionTime && time >= executionTime)
            {
                animatext.BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}