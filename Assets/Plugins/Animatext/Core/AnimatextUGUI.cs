// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0066, IDE0090

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animatext
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Text))]
    [AddComponentMenu("Miscellaneous/Animatext/Animatext - UGUI")]
    public class AnimatextUGUI : BaseAnimatext, IMeshModifier
    {
        private enum UGUIGeneratorType
        {
            Null,
            Old,
            New,
        }

        private struct UGUIRangeInfo
        {
            public int id;
            public Range range;
        }

        private struct UGUITagInfo
        {
            public int type;
            public int id;
            public Range range;
        }

        private Text _component;
        private bool _dataDirty;
        private bool _updatingComponentData;
        private bool _willExecute;
        private int _vertexQuadCount;
        private int[] _outputToQuadMappings;
        private int[] _quadToVertexMappings;
        private UIVertex[][] _vertexQuads;

        private static UGUIGeneratorType _generatorType;

        /// <summary>
        /// The text of the text component.
        /// </summary>
        public override string text
        {
            get 
            { 
                if(_component == null)
                {
                    _component = GetComponent<Text>();

                    if (_component == null)
                    {
                        _component = gameObject.AddComponent<Text>();
                    }
                }

                return _component.text; 
            }

            set
            {
                if (_component == null)
                {
                    _component = GetComponent<Text>();

                    if (_component == null)
                    {
                        _component = gameObject.AddComponent<Text>();
                    }
                }

                _component.text = value;
            }
        }

        /// <summary>
        /// Method to determine whether the character can be displayed.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private bool IsDisplayableChar(char character)
        {
            switch (character)
            {
                case '\u0000':
                case '\u0009':
                case '\u000A':
                case '\u0020':
                    return false;

                default:
                    return true;
            }
        }

        /// <summary>
        /// Method to mark the texture of the text component as dirty.
        /// </summary>
        private void SetComponentTextureDirty(Font font)
        {
            if (CanvasUpdateRegistry.IsRebuildingGraphics())
            {
                SetComponentVerticesDirty();
            }
            else
            {
                SetComponentDirty();
            }

            _component.FontTextureChanged();
        }

        /// <summary>
        /// Method to mark the vertices of the text component as dirty.
        /// </summary>
        private void SetComponentVerticesDirty()
        {
            if (_updatingComponentData) return;

            _dataDirty = true;

            if (CheckTextDirty())
            {
                UpdateText();
            }
            else
            {
                UpdateInfo();
            }
        }
        
        #region <- MonoBehaviour Methods ->

        protected override void Awake()
        {
            base.Awake();

            if (_component == null)
            {
                _component = GetComponent<Text>();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _component.RegisterDirtyVerticesCallback(SetComponentVerticesDirty);

            Font.textureRebuilt += SetComponentTextureDirty;

            SetComponentDirty();
        }

        protected override void LateUpdate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                SetComponentDirty();

                return;
            }
#endif
            if (_dataDirty)
            {
                if (!_component.enabled)
                {
                    Execute();

                    _dataDirty = false;
                }
                else
                {
                    _willExecute = true;
                }
            }
            else
            {
                Execute();
            }

            base.LateUpdate();
        }

        protected override void OnDisable()
        {
            _component.UnregisterDirtyVerticesCallback(SetComponentVerticesDirty);

            Font.textureRebuilt -= SetComponentTextureDirty;

            _dataDirty = true;

            base.OnDisable();
        }

        #endregion
        
        /// <summary>
        /// Method to get the color array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <returns></returns>
        protected override Color[] GetCharColors(int outputIndex)
        {
            int quadIndex = _outputToQuadMappings[outputIndex];

            Color[] colors;

            if (quadIndex != -1)
            {
                colors = new Color[4];

                for (int i = 0; i < 4; i++)
                {
                    colors[i] = _vertexQuads[quadIndex][i].color;
                }
            }
            else
            {
                colors = new Color[0];
            }

            return colors;
        }

        /// <summary>
        /// Method to get the position array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <returns></returns>
        protected override Vector3[] GetCharPositions(int outputIndex)
        {
            int quadIndex = _outputToQuadMappings[outputIndex];

            Vector3[] positions;

            if (quadIndex != -1)
            {
                positions = new Vector3[4];

                for (int i = 0; i < 4; i++)
                {
                    positions[i] = _vertexQuads[quadIndex][i].position;
                }
            }
            else
            {
                positions = new Vector3[0];
            }

            return positions;
        }

        /// <summary>
        /// Method to get the parsed string information of the output text.
        /// </summary>
        /// <param name="parsedText">The parsed text of the Animatext component.</param>
        /// <returns></returns>
        protected override StringInfo GetOutputTextInfo(string parsedText)
        {
            #region <- Step A: Char Mapping ->

            bool isRichText = _component.supportRichText;
            List<int> ouputToParsedMappingList = new List<int>();

            if (isRichText)
            {
                int nextOpeningNameIndex = parsedText.IndexOf('<');
                List<UGUITagInfo> tagInfoList = new List<UGUITagInfo>();

                for (int i = 0; i < parsedText.Length; i++)
                {
                    int openingNameIndex = nextOpeningNameIndex;

                    if (openingNameIndex == -1 || openingNameIndex == parsedText.Length - 1)
                    {
                        break;
                    }
                    else
                    {
                        int closingNameIndex = int.MaxValue;
                        int equalSignIndex = parsedText.IndexOf('=', openingNameIndex);
                        int spaceSignIndex = parsedText.IndexOf(' ', openingNameIndex);
                        int closingSignIndex = parsedText.IndexOf('>', openingNameIndex);

                        if (equalSignIndex != -1)
                        {
                            closingNameIndex = Mathf.Min(equalSignIndex, closingNameIndex);
                        }

                        if (spaceSignIndex != -1)
                        {
                            closingNameIndex = Mathf.Min(spaceSignIndex, closingNameIndex);
                        }

                        if (closingSignIndex != -1)
                        {
                            closingNameIndex = Mathf.Min(closingSignIndex, closingNameIndex);
                        }

                        if (closingNameIndex == int.MaxValue)
                        {
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                nextOpeningNameIndex = parsedText.IndexOf('<', nextOpeningNameIndex + 1);

                                if (nextOpeningNameIndex == -1 || nextOpeningNameIndex >= closingNameIndex)
                                {
                                    break;
                                }

                                openingNameIndex = nextOpeningNameIndex;
                            }

                            if (parsedText[openingNameIndex + 1] != '/')
                            {
                                string tagName = parsedText.Substring(openingNameIndex + 1, closingNameIndex - openingNameIndex - 1).ToLower();

                                switch (tagName)
                                {
                                    case "b":
                                        {
                                            if (closingNameIndex != spaceSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 0;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }

                                            i = closingNameIndex;
                                        }
                                        break;

                                    case "i":
                                        {
                                            if (closingNameIndex != spaceSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 1;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }

                                            i = closingNameIndex;
                                        }
                                        break;

                                    case "size":
                                        {
                                            if (closingNameIndex == closingSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 2;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);

                                                i = closingNameIndex;
                                            }
                                            else if (closingNameIndex == equalSignIndex)
                                            {
                                                if (closingSignIndex != -1)
                                                {
                                                    UGUITagInfo tagInfo;

                                                    tagInfo.type = 1;
                                                    tagInfo.id = 2;
                                                    tagInfo.range = new Range(openingNameIndex, closingSignIndex);

                                                    tagInfoList.Add(tagInfo);

                                                    i = closingSignIndex;
                                                }
                                                else
                                                {
                                                    i = parsedText.Length - 1;
                                                }
                                            }
                                            else
                                            {
                                                i = closingNameIndex;
                                            }
                                        }
                                        break;

                                    case "a":
                                    case "color":
                                        {
                                            if (closingNameIndex == closingSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 3;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);

                                                i = closingNameIndex;
                                            }
                                            else if (closingNameIndex == equalSignIndex)
                                            {
                                                if (closingSignIndex != -1)
                                                {
                                                    UGUITagInfo tagInfo;

                                                    tagInfo.type = 1;
                                                    tagInfo.id = 3;
                                                    tagInfo.range = new Range(openingNameIndex, closingSignIndex);

                                                    tagInfoList.Add(tagInfo);

                                                    i = closingSignIndex;
                                                }
                                                else
                                                {
                                                    i = parsedText.Length - 1;
                                                }
                                            }
                                            else
                                            {
                                                i = closingNameIndex;
                                            }
                                        }
                                        break;

                                    case "material":
                                        {
                                            if (closingNameIndex == closingSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 4;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);

                                                i = closingNameIndex;
                                            }
                                            else if (closingNameIndex == equalSignIndex)
                                            {
                                                if (closingSignIndex != -1)
                                                {
                                                    UGUITagInfo tagInfo;

                                                    tagInfo.type = 1;
                                                    tagInfo.id = 4;
                                                    tagInfo.range = new Range(openingNameIndex, closingSignIndex);

                                                    tagInfoList.Add(tagInfo);

                                                    i = closingNameIndex;
                                                }
                                                else
                                                {
                                                    i = parsedText.Length - 1;
                                                }
                                            }
                                            else
                                            {
                                                i = closingNameIndex;
                                            }
                                        }
                                        break;

                                    case "quad":
                                        {
                                            if (closingSignIndex != -1)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 3;
                                                tagInfo.id = 5;
                                                tagInfo.range = new Range(openingNameIndex, closingSignIndex);

                                                tagInfoList.Add(tagInfo);

                                                i = closingSignIndex;
                                            }
                                            else
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 3;
                                                tagInfo.id = 5;
                                                tagInfo.range = new Range(openingNameIndex, parsedText.Length - 1);

                                                tagInfoList.Add(tagInfo);

                                                i = parsedText.Length - 1;
                                            }
                                        }
                                        break;

                                    case "x":
                                    case "y":
                                        {
                                            if (closingNameIndex == closingSignIndex)
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 1;
                                                tagInfo.id = 6;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);

                                                i = closingNameIndex;
                                            }
                                            else if (closingNameIndex == equalSignIndex)
                                            {
                                                if (closingSignIndex != -1)
                                                {
                                                    UGUITagInfo tagInfo;

                                                    tagInfo.type = 1;
                                                    tagInfo.id = 6;
                                                    tagInfo.range = new Range(openingNameIndex, closingSignIndex);

                                                    tagInfoList.Add(tagInfo);

                                                    i = closingSignIndex;
                                                }
                                                else
                                                {
                                                    i = parsedText.Length - 1;
                                                }
                                            }
                                            else
                                            {
                                                i = closingNameIndex;
                                            }
                                        }
                                        break;

                                    default:
                                        i = closingNameIndex;
                                        break;
                                }
                            }
                            else
                            {
                                if (closingNameIndex == closingSignIndex)
                                {
                                    string tagName = parsedText.Substring(openingNameIndex + 2, closingNameIndex - openingNameIndex - 2).ToLower();

                                    switch (tagName)
                                    {
                                        case "b":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 0;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        case "i":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 1;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        case "size":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 2;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        case "a":
                                        case "color":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 3;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        case "material":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 4;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        case "x":
                                        case "y":
                                            {
                                                UGUITagInfo tagInfo;

                                                tagInfo.type = 2;
                                                tagInfo.id = 6;
                                                tagInfo.range = new Range(openingNameIndex, closingNameIndex);

                                                tagInfoList.Add(tagInfo);
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                i = closingNameIndex;
                            }
                        }
                    }
                }

                int tagInfoCount = tagInfoList.Count;
                List<UGUIRangeInfo> rangeInfoList = new List<UGUIRangeInfo>();

                for (int i = 0; i < tagInfoCount; i++)
                {
                    if (tagInfoList[i].type == 1)
                    {
                        UGUIRangeInfo rangeInfo;

                        rangeInfo.id = tagInfoList[i].id;
                        rangeInfo.range = new Range() { startIndex = tagInfoList[i].range.startIndex, endIndex = -1, count = -1 };

                        rangeInfoList.Add(rangeInfo);
                    }

                    else if (tagInfoList[i].type == 2)
                    {
                        int id = tagInfoList[i].id;
                        int index = rangeInfoList.Count - 1;

                        while (index >= 0)
                        {
                            if (rangeInfoList[index].id == id && rangeInfoList[index].range.endIndex == -1)
                            {
                                break;
                            }

                            index--;
                        }

                        if (index == -1)
                        {
                            isRichText = false;
                            break;
                        }
                        else
                        {
                            UGUIRangeInfo rangeInfo = rangeInfoList[index];

                            rangeInfo.range.endIndex = tagInfoList[i].range.endIndex;
                            rangeInfo.range.count = rangeInfo.range.endIndex - rangeInfo.range.startIndex + 1;

                            rangeInfoList[index] = rangeInfo;
                        }
                    }
                    else if (tagInfoList[i].type == 3)
                    {
                        UGUIRangeInfo rangeInfo;

                        rangeInfo.id = tagInfoList[i].id;
                        rangeInfo.range = tagInfoList[i].range;

                        rangeInfoList.Add(rangeInfo);
                    }
                }

                int rangeInfoCount = rangeInfoList.Count;

                for (int i = 0; i < rangeInfoCount; i++)
                {
                    if (rangeInfoList[i].range.endIndex == -1 || rangeInfoList[i].id == 6)
                    {
                        isRichText = false;
                        break;
                    }
                }

                for (int i = 0; i < rangeInfoCount; i++)
                {
                    for (int j = i + 1; j < rangeInfoCount; j++)
                    {
                        if (rangeInfoList[i].range.endIndex > rangeInfoList[j].range.startIndex && rangeInfoList[i].range.endIndex < rangeInfoList[j].range.endIndex)
                        {
                            isRichText = false;
                            break;
                        }
                    }

                    if (!isRichText)
                    {
                        break;
                    }
                }

                if (isRichText)
                {
                    int startIndex = 0;

                    ouputToParsedMappingList = new List<int>();

                    for (int i = 0; i < tagInfoCount; i++)
                    {
                        int endIndex = tagInfoList[i].range.startIndex - 1;

                        for (int j = startIndex; j <= endIndex; j++)
                        {
                            ouputToParsedMappingList.Add(j);
                        }

                        if (tagInfoList[i].id == 5)
                        {
                            ouputToParsedMappingList.Add(tagInfoList[i].range.startIndex);
                        }

                        startIndex = tagInfoList[i].range.endIndex + 1;
                    }

                    if (startIndex < parsedText.Length)
                    {
                        int endIndex = parsedText.Length - 1;

                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            ouputToParsedMappingList.Add(i);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < parsedText.Length; i++)
                    {
                        ouputToParsedMappingList.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < parsedText.Length; i++)
                {
                    ouputToParsedMappingList.Add(i);
                }
            }

            #endregion

            #region <- Step B: Output Mapping ->

            _vertexQuadCount = 0;
            _vertexQuads = null;

            bool truncateText = false;
            int ouputToParsedMappingCount = ouputToParsedMappingList.Count;
            List<int> outputToQuadMappingList = new List<int>();
            List<int> quadToVertexMappingList = new List<int>();

            if (_generatorType == UGUIGeneratorType.Null)
            {
                _component.cachedTextGeneratorForLayout.Populate(string.Empty, new TextGenerationSettings() { font = new Font() });

                _generatorType = _component.cachedTextGeneratorForLayout.vertexCount > 0 ? UGUIGeneratorType.Old : UGUIGeneratorType.New;
            }

            if (_generatorType == UGUIGeneratorType.Old)
            {
                if (_component.verticalOverflow == VerticalWrapMode.Truncate)
                {
                    _component.cachedTextGeneratorForLayout.Populate(parsedText, _component.GetGenerationSettings(_component.rectTransform.rect.size));

                    int vertexCount = _component.cachedTextGeneratorForLayout.vertexCount - 4;

                    for (int i = 0; i < ouputToParsedMappingCount; i++)
                    {
                        int parsedIndex = ouputToParsedMappingList[i];

                        if (CharInfo.IsVisibleChar(parsedText[parsedIndex]))
                        {
                            int vertexIndex = parsedIndex * 4;

                            if (vertexIndex >= vertexCount)
                            {
                                truncateText = true;
                                ouputToParsedMappingList.RemoveRange(i, ouputToParsedMappingCount - i);
                                break;
                            }

                            outputToQuadMappingList.Add(_vertexQuadCount);
                            quadToVertexMappingList.Add(vertexIndex);

                            _vertexQuadCount++;
                        }
                        else
                        {
                            outputToQuadMappingList.Add(-1);
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < ouputToParsedMappingCount; i++)
                    {
                        int parsedIndex = ouputToParsedMappingList[i];

                        if (CharInfo.IsVisibleChar(parsedText[parsedIndex]))
                        {
                            outputToQuadMappingList.Add(_vertexQuadCount);
                            quadToVertexMappingList.Add(parsedIndex * 4);

                            _vertexQuadCount++;
                        }
                        else
                        {
                            outputToQuadMappingList.Add(-1);
                        }
                    }
                }
            }
            else if (_generatorType == UGUIGeneratorType.New)
            {
                if (_component.verticalOverflow == VerticalWrapMode.Truncate)
                {
                    _component.cachedTextGeneratorForLayout.Populate(parsedText, _component.GetGenerationSettings(_component.rectTransform.rect.size));

                    if (_component.cachedTextGeneratorForLayout.vertexCount == _component.cachedTextGeneratorForLayout.characterCount * 4)
                    {
                        int vertexCount = _component.cachedTextGeneratorForLayout.vertexCount;

                        for (int i = 0; i < ouputToParsedMappingCount; i++)
                        {
                            int parsedIndex = ouputToParsedMappingList[i];

                            if (CharInfo.IsVisibleChar(parsedText[parsedIndex]))
                            {
                                int vertexIndex = parsedIndex * 4;

                                if (vertexIndex >= vertexCount)
                                {
                                    truncateText = true;
                                    ouputToParsedMappingList.RemoveRange(i, ouputToParsedMappingCount - i);
                                    break;
                                }

                                outputToQuadMappingList.Add(_vertexQuadCount);
                                quadToVertexMappingList.Add(vertexIndex);

                                _vertexQuadCount++;
                            }
                            else
                            {
                                outputToQuadMappingList.Add(-1);
                            }
                        }
                    }
                    else
                    {
                        int vertexIndex = 0;

                        for (int i = 0; i < ouputToParsedMappingCount; i++)
                        {
                            int parsedIndex = ouputToParsedMappingList[i];

                            if (CharInfo.IsVisibleChar(parsedText[parsedIndex]))
                            {
                                outputToQuadMappingList.Add(_vertexQuadCount);
                                _vertexQuadCount++;

                                quadToVertexMappingList.Add(vertexIndex);
                                vertexIndex += 4;
                            }
                            else if (IsDisplayableChar(parsedText[parsedIndex]))
                            {
                                outputToQuadMappingList.Add(-1);
                                vertexIndex += 4;
                            }
                            else
                            {
                                outputToQuadMappingList.Add(-1);
                            }
                        }
                    }
                }
                else
                {
                    int vertexIndex = 0;

                    for (int i = 0; i < ouputToParsedMappingCount; i++)
                    {
                        int parsedIndex = ouputToParsedMappingList[i];

                        if (CharInfo.IsVisibleChar(parsedText[parsedIndex]))
                        {
                            outputToQuadMappingList.Add(_vertexQuadCount);
                            _vertexQuadCount++;

                            quadToVertexMappingList.Add(vertexIndex);
                            vertexIndex += 4;
                        }
                        else if (IsDisplayableChar(parsedText[parsedIndex]))
                        {
                            outputToQuadMappingList.Add(-1);
                            vertexIndex += 4;
                        }
                        else
                        {
                            outputToQuadMappingList.Add(-1);
                        }
                    }
                }
            }

            if (_vertexQuadCount > 0)
            {
                _vertexQuads = new UIVertex[_vertexQuadCount][];

                for (int i = 0; i < _vertexQuadCount; i++)
                {
                    _vertexQuads[i] = new UIVertex[4];
                }
            }

            _outputToQuadMappings = outputToQuadMappingList.ToArray();
            _quadToVertexMappings = quadToVertexMappingList.ToArray();

            #endregion

            #region <- Step C: Output Info ->

            StringInfo outputTextInfo;

            outputTextInfo.mappings = ouputToParsedMappingList.ToArray();

            outputTextInfo.ranges = new Range[1];

            if (outputTextInfo.mappings.Length <= 0)
            {
                outputTextInfo.ranges[0] = Range.empty;
            }
            else if (truncateText)
            {
                outputTextInfo.ranges[0] = new Range(-1, outputTextInfo.mappings[outputTextInfo.mappings.Length - 1]);
            }
            else
            {
                outputTextInfo.ranges[0] = new Range(-1, parsedText.Length - 1);
            }

            char[] outputTextChars = new char[outputTextInfo.mappings.Length];

            for (int i = 0; i < outputTextInfo.mappings.Length; i++)
            {
                outputTextChars[i] = parsedText[outputTextInfo.mappings[i]];
            }

            outputTextInfo.text = new string(outputTextChars);

            return outputTextInfo;

            #endregion
        }
        
        /// <summary>
        /// Method to set the color array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <param name="colors">The current color array of the character vertices.</param>
        /// <param name="originColors">The origin color array of the character vertices.</param>
        protected override void SetCharColors(int outputIndex, Color[] colors, Color[] originColors)
        {
            int quadIndex = _outputToQuadMappings[outputIndex];

            if (quadIndex != -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    _vertexQuads[quadIndex][i].color = colors[i];
                }
            }
        }

        /// <summary>
        /// Method to set the position array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <param name="positions">The current position array of the character vertices.</param>
        /// <param name="originPositions">The origin position array of the character vertices.</param>
        protected override void SetCharPositions(int outputIndex, Vector3[] positions, Vector3[] originPositions)
        {
            int quadIndex = _outputToQuadMappings[outputIndex];

            if (quadIndex != -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    _vertexQuads[quadIndex][i].position = positions[i];
                }
            }
        }

        /// <summary>
        /// Method to mark the text component as dirty.
        /// </summary>
        protected override void SetComponentDirty()
        {
            _component.SetVerticesDirty();
        }

        /// <summary>
        /// Method to update the text vertex data in the text component.
        /// </summary>
        protected override void UpdateComponentData()
        {
            if (_dataDirty) return;

            _updatingComponentData = true;

            SetComponentDirty();

            _updatingComponentData = false;
        }

        /// <summary>
        /// Method to update the text vertex data in the text information.
        /// </summary>
        protected override void UpdateData()
        {
            _component.cachedTextGeneratorForLayout.Invalidate();
            _component.cachedTextGeneratorForLayout.Populate(parsedText, _component.GetGenerationSettings(_component.rectTransform.rect.size));

            float unitsPerPixel = 1 / _component.pixelsPerUnit;
            IList<UIVertex> originVertices = _component.cachedTextGeneratorForLayout.verts;

            Vector2 roundingOffset;

            if (_component.cachedTextGeneratorForLayout.vertexCount == 0)
            {
                roundingOffset = Vector2.zero;
            }
            else
            {
                roundingOffset = new Vector2(originVertices[0].position.x, originVertices[0].position.y) * unitsPerPixel;
                roundingOffset = _component.PixelAdjustPoint(roundingOffset) - roundingOffset;
            }

#if UNITY_EDITOR
            if (!Application.isPlaying && _vertexQuads == null)
            {
                GetOutputTextInfo(parsedText);
            }
#endif
            for (int i = 0; i < outputText.Length; i++)
            {
                if (_outputToQuadMappings[i] > -1)
                {
                    int vertexIndex = _quadToVertexMappings[_outputToQuadMappings[i]];
                    UIVertex[] vertexQuad = _vertexQuads[_outputToQuadMappings[i]];

                    for (int j = 0; j < 4; j++)
                    {
                        vertexQuad[j] = originVertices[vertexIndex + j];
                        vertexQuad[j].position *= unitsPerPixel;
                        vertexQuad[j].position.x += roundingOffset.x;
                        vertexQuad[j].position.y += roundingOffset.y;
                    }
                }
            }

            base.UpdateData();
        }

        /// <summary>
        /// Method to update the text infomation in the Animatext component.
        /// </summary>
        protected override void UpdateInfo()
        {
            _updatingComponentData = true;

            base.UpdateInfo();

            _updatingComponentData = false;
        }

        /// <summary>
        /// Method to modify the mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public void ModifyMesh(Mesh mesh)
        {
            if (!isActiveAndEnabled) return;

            VertexHelper vertexHelper = new VertexHelper(mesh);

            ModifyMesh(vertexHelper);

            vertexHelper.FillMesh(mesh);
        }

        /// <summary>
        /// Method to modify the mesh.
        /// </summary>
        /// <param name="mesh"></param>
        public void ModifyMesh(VertexHelper vertexHelper)
        {
            if (!isActiveAndEnabled) return;

            vertexHelper.Clear();

            if (_dataDirty)
            {
                UpdateData();

                if (_willExecute)
                {
                    Execute();

                    _willExecute = false;
                }
                else
                {
                    ExtraExecute();
                }

                _dataDirty = false;
            }

            for (int i = 0; i < _vertexQuadCount; i++)
            {
                vertexHelper.AddUIVertexQuad(_vertexQuads[i]);
            }
        }

#if UNITY_EDITOR
        private static readonly string[] _componentRichTextTags = new string[]
        {
            "A",
            "B",
            "COLOR",
            "I",
            "MATERIAL",
            "QUAD",
            "SIZE",
        };

        /// <summary>
        /// Method to determine whether the tag is a specific tag in the text component.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        protected override bool IsComponentSpecificTag(string tag)
        {
            if (settings.tagSymbols != TagSymbols.AngleBrackets) return false;

            tag = tag.ToUpper();

            for (int i = 0; i < _componentRichTextTags.Length; i++)
            {
                if (tag == _componentRichTextTags[i])
                {
                    return true;
                }
            }

            return false;
        }
#endif
    }
}