// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    [CustomEditor(typeof(AnimatextTMPro), true)]
    public class AnimatextTMProEditor : BaseAnimatextEditor
    {
        private static readonly GUIContent scaleSDFContent = new GUIContent("Scale SDF", "The scale of SDF.");

        private SerializedProperty scaleSDFProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            scaleSDFProperty = serializedObject.FindProperty("_scaleSDF");
        }

        protected override void OnChildSettingsGUI()
        {
            EditorGUILayout.PropertyField(scaleSDFProperty, scaleSDFContent);
        }
    }
}