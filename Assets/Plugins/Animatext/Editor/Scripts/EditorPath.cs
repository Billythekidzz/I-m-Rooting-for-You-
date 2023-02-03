// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using UnityEditor;
using UnityEngine;

namespace Animatext.Editor
{
    public class EditorPath : ScriptableObject
    {
        private static string _basePath;

        public static string basePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_basePath)) return _basePath;

                var obj = CreateInstance<EditorPath>();
                MonoScript monoScript = MonoScript.FromScriptableObject(obj);
                string assetPath = AssetDatabase.GetAssetPath(monoScript);
                DestroyImmediate(obj);

                _basePath = assetPath.Substring(0, assetPath.IndexOf("Editor"));

                return _basePath;
            }
        }

        public static string skinPath
        {
            get
            {
                if (EditorGUIUtility.isProSkin)
                {
                    return basePath + "Editor/Themes/SkinDark.guiskin";
                }
                else
                {
                    return basePath + "Editor/Themes/SkinLight.guiskin";
                }
            }
        }

        public static string documentationURL
        {
            get
            {
                return Application.dataPath + basePath.Substring(6) + "Editor/Documentation/Pages/Manual/Index.html";
            }
        }
    }
}