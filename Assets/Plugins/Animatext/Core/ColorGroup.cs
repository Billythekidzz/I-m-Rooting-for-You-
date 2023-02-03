// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext
{
    public class ColorGroup
    {
        public int count;
        public bool isDirty;
        public Color[] cacheColors;
        public Color[] currentColors;
        public Color[] originColors;

        public ColorGroup()
        {
            count = 0;
            isDirty = false;
            cacheColors = null;
            currentColors = null;
            originColors = null;
        }

        public ColorGroup(Color[] colors)
        {
            count = colors.Length;
            isDirty = false;

            cacheColors = new Color[count];
            currentColors = new Color[count];

            for (int i = 0; i < count; i++)
            {
                cacheColors[i] = colors[i];
                currentColors[i] = colors[i];
            }

            originColors = colors;
        }

        public void Color(Color color)
        {
            for (int i = 0; i < count; i++)
            {
                cacheColors[i] = color;
            }
        }

        public void Color(Color color, ColorMode colorMode)
        {
            for (int i = 0; i < count; i++)
            {
                cacheColors[i] = ColorUtility.Blend(cacheColors[i], color, colorMode);
            }
        }

        public void Opacify(float opacity)
        {
            for (int i = 0; i < count; i++)
            {
                cacheColors[i].a = ColorUtility.BlendMultiply(cacheColors[i].a, opacity);
            }
        }

        public void Opacify(float opacity, ColorMode colorMode)
        {
            for (int i = 0; i < count; i++)
            {
                cacheColors[i].a = ColorUtility.Blend(cacheColors[i].a, opacity, colorMode);
            }
        }

        public void Execute()
        {
            isDirty = false;

            Color[] tempColors = cacheColors;

            cacheColors = currentColors;
            currentColors = tempColors;

            for (int i = 0; i < count; i++)
            {
                if (currentColors[i] != cacheColors[i])
                {
                    isDirty = true;
                    break;
                }
            }

            for (int i = 0; i < count; i++)
            {
                cacheColors[i] = originColors[i];
            }
        }

        public void SetColors(Color[] colors)
        {
            count = colors.Length;
            isDirty = false;

            cacheColors = new Color[count];
            currentColors = new Color[count];

            for (int i = 0; i < count; i++)
            {
                cacheColors[i] = colors[i];
                currentColors[i] = colors[i];
            }

            originColors = colors;
        }
    }
}