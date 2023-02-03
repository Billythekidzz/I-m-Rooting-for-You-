// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomPropertyDrawer(typeof(FadeModeAttribute))]
    public class FadeModeDrawer : PropertyDrawer
    {
        private static readonly GUIContent _content = new GUIContent("Fade Mode");
        private static readonly int[] _fadeModeValues = new int[] { 0, 1, 3 };
        private static readonly GUIContent[] _fadeModeContents = new GUIContent[]
        {
            new GUIContent("Replace"),
            new GUIContent("Multiply"),
            new GUIContent("Difference"),
        };
        private static readonly int[] _colorModeValues = new int[] { 0, 4, 5, 1, 6, 2, 7, 8, 9, 10, 11, 12, 13, 3, 14, 15, 16 };
        private static readonly GUIContent[] _colorModeContents = new GUIContent[]
        {
            new GUIContent("Replace"),
            new GUIContent("Normal"),
            new GUIContent("Darken"),
            new GUIContent("Multiply"),
            new GUIContent("ColorBurn"),
            new GUIContent("Add"),
            new GUIContent("Lighten"),
            new GUIContent("Screen"),
            new GUIContent("ColorDodge"),
            new GUIContent("Overlay"),
            new GUIContent("SoftLight"),
            new GUIContent("HardLight"),
            new GUIContent("LinearLight"),
            new GUIContent("Difference"),
            new GUIContent("Exclusion"),
            new GUIContent("Subtract"),
            new GUIContent("Divide"),
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.enumValueIndex < 4 && property.enumValueIndex != 2)
            {
                property.enumValueIndex = EditorGUI.IntPopup(position, _content, property.enumValueIndex, _fadeModeContents, _fadeModeValues);
            }
            else
            {
                property.enumValueIndex = EditorGUI.IntPopup(position, _content, property.enumValueIndex, _colorModeContents, _colorModeValues);
            }
        }
    }
}