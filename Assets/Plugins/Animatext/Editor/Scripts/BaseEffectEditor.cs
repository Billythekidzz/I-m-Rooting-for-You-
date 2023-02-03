// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using Animatext.Effects;
using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomEditor(typeof(BaseEffect), true)]
    public class BaseEffectDrawer : UnityEditor.Editor
    {
        private SerializedProperty _tagProperty;
        private SerializedProperty _startIntervalProperty;

        protected virtual void OnEnable()
        {
            _tagProperty = serializedObject.FindProperty("_tag");
            _startIntervalProperty = serializedObject.FindProperty("_startInterval");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (EditorApplication.isPlaying)
            {
                GUI.enabled = false;
            }

            EditorGUILayout.PropertyField(_tagProperty);

            if (EditorApplication.isPlaying)
            {
                GUI.enabled = true;
            }

            EditorGUILayout.PropertyField(_startIntervalProperty);
            GUILayout.Space(5);

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}