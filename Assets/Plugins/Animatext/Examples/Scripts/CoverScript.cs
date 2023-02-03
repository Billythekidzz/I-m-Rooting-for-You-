// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using System.Collections;
using UnityEngine;

namespace Animatext.Examples
{
    public class CoverScript : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SetActive());
        }

        private IEnumerator SetActive()
        {
            while (true)
            {
                yield return null;

                if (Time.frameCount >= 3)
                {
                    gameObject.SetActive(false);

                    break;
                }
            }
        }
    }
}