// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext.Examples
{
    public class CircleScript : MonoBehaviour
    {
        private RectTransform rectTransform;

        public float amplitude;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            rectTransform.localScale = Vector3.one * (1 + amplitude * Mathf.Sin(Time.time * Mathf.PI * 0.25f - Mathf.PI * 0.5f));
        }
    }
}