// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomPropertyDrawer(typeof(Settings))]
    public class SettingsDrawer : PropertyDrawer
    {
        private static readonly GUIContent retainedTextContent = new GUIContent("Retained Text", "The retained text content in the text component after the Animatext component parsing.");
        private static readonly GUIContent disabledTextContent = new GUIContent("Disabled Text", "The retained text content in the text component when the Animatext component is disabled.");
        private static readonly GUIContent disabledEffectsContent = new GUIContent("Disabled Effects", "The way effects are processed when the Animatext component is disabled.");
        private static readonly GUIContent tagSymbolsContent = new GUIContent("Tag Symbols", "The symbols of the tag.");
        private static readonly GUIContent markerSymbolsContent = new GUIContent("Marker Symbols", "The symbols of the tag marker.");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 5 * EditorGUIUtility.singleLineHeight + 4 * EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty retainedTextProperty = property.FindPropertyRelative("retainedText");
            SerializedProperty disabledTextProperty = property.FindPropertyRelative("disabledText");
			SerializedProperty disabledEffectsProperty = property.FindPropertyRelative("disabledEffects");
            SerializedProperty tagSymbolsProperty = property.FindPropertyRelative("tagSymbols");
            SerializedProperty markerSymbolsProperty = property.FindPropertyRelative("markerSymbols");

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, retainedTextProperty, retainedTextContent);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, disabledTextProperty, disabledTextContent);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, disabledEffectsProperty, disabledEffectsContent);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, tagSymbolsProperty, tagSymbolsContent);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(position, markerSymbolsProperty, markerSymbolsContent);
        }
    }
}