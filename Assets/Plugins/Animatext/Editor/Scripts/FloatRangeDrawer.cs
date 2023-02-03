// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        private readonly static GUIContent _contentStart = new GUIContent("Start");
        private readonly static GUIContent _contentEnd = new GUIContent("End");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 2 * EditorGUIUtility.singleLineHeight + 1 * EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty startProperty = property.FindPropertyRelative("start");
            SerializedProperty endProperty = property.FindPropertyRelative("end");

            float startValue = startProperty.floatValue;
            float endValue = endProperty.floatValue;

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PrefixLabel(position, new GUIContent(property.displayName));

            position.x += EditorGUIUtility.labelWidth + 2;
            position.width -= EditorGUIUtility.labelWidth + 2;

#if !UNITY_2019_3_OR_NEWER
            position.x -= 2;
            position.width += 2;
#endif
            EditorGUI.BeginChangeCheck();
            {
                EditorGUI.MinMaxSlider(position, ref startValue, ref endValue, 0, 1);
                startValue = Mathf.Round(startValue * 10000) / 10000;
                endValue = Mathf.Round(endValue * 10000) / 10000;
            }
            if (EditorGUI.EndChangeCheck())
            {
                GUI.FocusControl(null);
            }

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            position.width /= 2;
            EditorGUIUtility.labelWidth = 36;
            startValue = EditorGUI.FloatField(position, _contentStart, startValue);
            startValue = Mathf.Round(startValue * 10000) / 10000;

            position.x += position.width + 6;
            position.width -= 6;
            EditorGUIUtility.labelWidth -= 6;
            endValue = EditorGUI.FloatField(position, _contentEnd, endValue);
            endValue = Mathf.Round(endValue * 10000) / 10000;

            startProperty.floatValue = startValue;
            endProperty.floatValue = endValue;
        }
    }
}