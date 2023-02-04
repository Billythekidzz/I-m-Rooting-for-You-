// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090, IDE1006

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Animatext
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("Miscellaneous/Animatext/Animatext - TMPro")]
    public class AnimatextTMPro : BaseAnimatext
    {
        private struct TMProRangeInfo
        {
            public string definition;
            public int index;
            public Range range;
        }

        private struct TMProTagInfo
        {
            public int type;
            public int index;
            public TMP_Style style;
            public Range range;
        }

        private TMP_Text _component;
        private int _linkedComponentCount;
        private TMP_Text[] _linkedComponents;
        private int[] _linkedComponentValues;
        private bool _modifyingComponentTextInfo;
        private Vector2Int[] _ouputToCharInfoMappings;
        private bool _willExecute;
        [SerializeField] private float _scaleSDF = 1f;

        /// <summary>
        /// The scale of SDF.
        /// </summary>
        public float scaleSDF
        {
            get { return _scaleSDF; }
            set { _scaleSDF = value; }
        }

        /// <summary>
        /// The text of the text component.
        /// </summary>
        public override string text
        {
            get
            {
                if (_component == null)
                {
                    _component = GetComponent<TMP_Text>();

                    if (_component == null)
                    {
                        if (GetComponent<MeshRenderer>() == null)
                        {
                            gameObject.AddComponent<TextMeshProUGUI>();
                        }
                        else
                        {
                            gameObject.AddComponent<TextMeshPro>();
                        }
                    }
                }

                return _component.text;
            }
            
            set
            {
                if (_component == null)
                {
                    _component = GetComponent<TMP_Text>();

                    if (_component == null)
                    {
                        if (GetComponent<MeshRenderer>() == null)
                        {
                            gameObject.AddComponent<TextMeshProUGUI>();
                        }
                        else
                        {
                            gameObject.AddComponent<TextMeshPro>();
                        }
                    }
                }

                _component.text = value;

                for (int i = 0; i < _linkedComponentCount; i++)
                {
                    _linkedComponents[i].text = value;
                }
            }
        }

        /// <summary>
        /// Method to modify the text info of the text component.
        /// </summary>
        /// <param name="obj"></param>
        private void ModifyComponentTextInfo(Object obj)
        {
            if (_modifyingComponentTextInfo) return;

            bool validComponent = false;
            TMP_Text textComponent = (TMP_Text)obj;

            if (textComponent == _component)
            {
                validComponent = true;
            }
            else
            {
                for (int i = 0; i < _linkedComponentCount; i++)
                {
                    if (textComponent == _linkedComponents[i])
                    {
                        validComponent = true;
                        break;
                    }
                }
            }

            if (validComponent)
            {
                if (CheckTextDirty())
                {
                    UpdateText();
                }
                else
                {
                    UpdateInfo();
                }

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

                UpdateComponentData();

                _component.havePropertiesChanged = false;

                for (int i = 0; i < _linkedComponentCount; i++)
                {
                    _linkedComponents[i].havePropertiesChanged = false;
                }
            }
        }

        /// <summary>
        /// Method to update the text info of the text component.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="text"></param>
        private void UpdateComponentTextInfo(TMP_Text component, string text)
        {
            TMP_Style textStyle = component.textStyle;

            component.textStyle = TMP_Style.NormalStyle;
            component.GetTextInfo(text);
            component.textStyle = textStyle;

            if (string.IsNullOrEmpty(component.GetParsedText()))
            {
                component.textInfo.ClearMeshInfo(false);
            }
        }

        #region <- MonoBehaviour Methods ->

        protected override void Awake()
        {
            _component = GetComponent<TMP_Text>();

            base.Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ModifyComponentTextInfo);

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
            if (_component.havePropertiesChanged)
            {
                if (!_component.enabled)
                {
                    Execute();

                    _component.havePropertiesChanged = false;                   
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
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ModifyComponentTextInfo);

            base.OnDisable();

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            if (settings.disabledText != DisabledText.InputText)
            {
                _component.textStyle = TMP_Style.NormalStyle;

                for (int i = 0; i < _linkedComponentCount; i++)
                {
                    _linkedComponents[i].textStyle = TMP_Style.NormalStyle;
                }
            }
        }

        #endregion

        /// <summary>
        /// Method to get the color array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <returns></returns>
        protected override Color[] GetCharColors(int outputIndex)
        {
            Color[] colors;

            if (_ouputToCharInfoMappings[outputIndex].x > -1)
            {
                int index = _ouputToCharInfoMappings[outputIndex].y;
                TMP_TextInfo textInfo = _ouputToCharInfoMappings[outputIndex].x == 0 ? _component.textInfo : _linkedComponents[_ouputToCharInfoMappings[outputIndex].x - 1].textInfo;

                colors = new Color[4];

                colors[0] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 1];
                colors[1] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 2];
                colors[2] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 3];
                colors[3] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex];
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
            Vector3[] positions;

            if (_ouputToCharInfoMappings[outputIndex].x > -1)
            {
                int index = _ouputToCharInfoMappings[outputIndex].y;
                TMP_TextInfo textInfo = _ouputToCharInfoMappings[outputIndex].x == 0 ? _component.textInfo : _linkedComponents[_ouputToCharInfoMappings[outputIndex].x - 1].textInfo;

                positions = new Vector3[4];

                positions[0] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 1];
                positions[1] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 2];
                positions[2] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 3];
                positions[3] = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex];
            }
            else
            {
                positions = new Vector3[0];
            }

            return positions;
        }

        /// <summary>
        /// Method to get the origin text of the text component.
        /// </summary>
        /// <param name="inputText">The text initially entered in the text component.</param>
        /// <returns></returns>
        protected override string GetOriginText(string inputText)
        {
            HashSet<string> effectTags = new HashSet<string>();

            for (int i = 0; i < effects.Count; i++)
            {
                effectTags.UnionWith(effects[i].GetTags());
            }

            if (effectTags.Contains(string.Empty))
            {
                effectTags.Remove(string.Empty);
            }

            int tagIndex = 0;
            Dictionary<string, int> tagDictionary = new Dictionary<string, int>();

            foreach (var tag in effectTags)
            {
                tagDictionary.Add(tag, tagIndex++);
            }

            TMP_Text textComponent = _component;
            int firstCharacter = _component.firstVisibleCharacter;
            bool lastValidComponent = false;
            int linkedComponentCount = 0;
            int parsedTextLength = 0;
            StringBuilder originText = new StringBuilder();
            List<TMP_Text> linkedComponentList = new List<TMP_Text>();
            List<int> linkedComponentValueList = new List<int>();

            while (textComponent != null)
            {
                if (textComponent != _component)
                {
                    linkedComponentList.Add(textComponent);
                    linkedComponentCount++;

                    BaseAnimatext animatext = textComponent.GetComponent<BaseAnimatext>();

                    if (animatext != null && animatext.enabled)
                    {
                        animatext.enabled = false;

                        Debug.LogError("<Animatext> Multiple Animatext components work on the same linked text component. Please add the Animatext component to the game object of the master text component and delete other Animatext components.");
                    }
                }

                if (lastValidComponent)
                {
                    textComponent.firstVisibleCharacter = firstCharacter;
                    UpdateComponentTextInfo(textComponent, string.Empty);

                    linkedComponentValueList.Add(parsedTextLength);
                }
                else
                {
                    TMP_StyleSheet componentStyleSheet = TMP_Settings.defaultStyleSheet;

                    if (textComponent.styleSheet != null)
                    {
                        componentStyleSheet = textComponent.styleSheet;
                    }

                    StringBuilder[] componentOriginTexts = new StringBuilder[3];

                    componentOriginTexts[0] = new StringBuilder(textComponent.textStyle.styleOpeningDefinition);
                    componentOriginTexts[1] = new StringBuilder(inputText);
                    componentOriginTexts[2] = new StringBuilder(textComponent.textStyle.styleClosingDefinition);

                    List<string> closingDefinitionList = new List<string>();
                    List<TMProTagInfo> componentTagInfoList = new List<TMProTagInfo>();
                    List<TMProRangeInfo> componentRangeInfoList = new List<TMProRangeInfo>();

                    int checkTimes = 0;

                    while (true)
                    {
                        string[] componentCheckedTexts = new string[3];

                        for (int j = 0; j < 3; j++)
                        {
                            componentCheckedTexts[j] = componentOriginTexts[j].ToString().ToUpper();
                        }

                        bool lastCheck = true;

                        for (int j = 0; j < 3; j++)
                        {
                            string tempText = componentCheckedTexts[j];

                            for (int k = 0; k < tempText.Length; k++)
                            {
                                k = tempText.IndexOf("STYLE", k);

                                if (k == -1 || k >= tempText.Length - 5)
                                {
                                    break;
                                }

                                if (k > 0)
                                {
                                    switch (tempText[k - 1])
                                    {
                                        case '<':

                                            if (tempText[k + 5] == '=')
                                            {
                                                int closingCharIndex = tempText.IndexOf('>', k + 5);

                                                if (closingCharIndex == -1)
                                                {
                                                    break;
                                                }

                                                int openingStyleIndex = k + 6;
                                                int closingStyleIndex = closingCharIndex - 1;

                                                while (openingStyleIndex < closingStyleIndex)
                                                {
                                                    if (tempText[closingStyleIndex] != '"')
                                                    {
                                                        break;
                                                    }

                                                    closingStyleIndex--;
                                                }

                                                while (openingStyleIndex < closingStyleIndex)
                                                {
                                                    if (tempText[openingStyleIndex] != '"')
                                                    {
                                                        break;
                                                    }

                                                    openingStyleIndex++;
                                                }

                                                if (openingStyleIndex < closingStyleIndex)
                                                {
                                                    TMProTagInfo tagInfo;

                                                    tagInfo.type = 0;
                                                    tagInfo.index = j;
                                                    tagInfo.style = componentStyleSheet.GetStyle(tempText.Substring(openingStyleIndex, closingStyleIndex - openingStyleIndex + 1));
                                                    tagInfo.range = new Range(k - 1, closingCharIndex);

                                                    if (tagInfo.style != null)
                                                    {
                                                        lastCheck = false;
                                                        componentTagInfoList.Add(tagInfo);
                                                    }

                                                    k = closingCharIndex;
                                                }
                                            }

                                            break;

                                        case '/':

                                            if (k != 1)
                                            {
                                                if (tempText[k - 2] == '<' && tempText[k + 5] == '>')
                                                {
                                                    TMProTagInfo tagInfo;

                                                    tagInfo.type = 1;
                                                    tagInfo.index = j;
                                                    tagInfo.style = null;
                                                    tagInfo.range = new Range(k - 2, k + 5);

                                                    componentTagInfoList.Add(tagInfo);

                                                    k += 5;
                                                }
                                            }

                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        if (checkTimes >= 99199)
                        {
                            lastCheck = true;
                        }

                        if (!lastCheck)
                        {
                            int componentTagInfoListCount = componentTagInfoList.Count;

                            for (int j = 0; j < componentTagInfoListCount; j++)
                            {
                                TMProTagInfo tagInfo = componentTagInfoList[j];

                                switch (tagInfo.type)
                                {
                                    case 0:
                                        {
                                            TMProRangeInfo rangeInfo;

                                            rangeInfo.definition = tagInfo.style.styleOpeningDefinition;
                                            rangeInfo.index = tagInfo.index;
                                            rangeInfo.range = tagInfo.range;

                                            componentRangeInfoList.Add(rangeInfo);
                                            closingDefinitionList.Add(tagInfo.style.styleClosingDefinition);
                                        }
                                        break;

                                    case 1:
                                        {
                                            if (closingDefinitionList.Count != 0)
                                            {
                                                TMProRangeInfo rangeInfo;

                                                rangeInfo.definition = closingDefinitionList[closingDefinitionList.Count - 1];
                                                rangeInfo.index = tagInfo.index;
                                                rangeInfo.range = tagInfo.range;

                                                componentRangeInfoList.Add(rangeInfo);
                                                closingDefinitionList.RemoveAt(closingDefinitionList.Count - 1);
                                            }
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            int componentTagInfoListCount = componentTagInfoList.Count;

                            for (int j = 0; j < componentTagInfoListCount; j++)
                            {
                                TMProRangeInfo rangeInfo;

                                rangeInfo.definition = string.Empty;
                                rangeInfo.index = componentTagInfoList[j].index;
                                rangeInfo.range = componentTagInfoList[j].range;

                                componentRangeInfoList.Add(rangeInfo);
                            }
                        }

                        for (int j = componentRangeInfoList.Count - 1; j >= 0; j--)
                        {
                            TMProRangeInfo rangeInfo = componentRangeInfoList[j];

                            componentOriginTexts[rangeInfo.index].Remove(rangeInfo.range.startIndex, rangeInfo.range.count);
                            componentOriginTexts[rangeInfo.index].Insert(rangeInfo.range.startIndex, rangeInfo.definition);
                        }

                        if (lastCheck)
                        {
                            break;
                        }

                        checkTimes++;

                        componentTagInfoList.Clear();
                        componentRangeInfoList.Clear();
                        closingDefinitionList.Clear();
                    }

                    StringInfo[] parsedTextInfo = new StringInfo[3];
                    List<TagInfo>[] parsedTextTagInfo = new List<TagInfo>[3];

                    for (int j = 0; j < 3; j++)
                    {
                        parsedTextInfo[j] = TextInfo.ParseText(componentOriginTexts[j].ToString(), tagDictionary, settings, out parsedTextTagInfo[j]);
                    }

                    string infoText = parsedTextInfo[0].text + parsedTextInfo[1].text + parsedTextInfo[2].text + (textComponent.overflowMode == TextOverflowModes.Linked ? "\u0003" : string.Empty);

                    textComponent.firstVisibleCharacter = firstCharacter;

                    UpdateComponentTextInfo(textComponent, infoText);

                    int parsedStartIndex = 0;
                    int parsedEndIndex = infoText.Length - 1;

                    if (textComponent.textInfo.characterCount > 0)
                    {
                        if (firstCharacter != 0)
                        {
                            parsedStartIndex = textComponent.textInfo.characterInfo[firstCharacter - 1].index + 1;
                        }

                        if (textComponent.overflowMode == TextOverflowModes.Linked)
                        {
                            parsedEndIndex = textComponent.textInfo.characterCount > 1 ? textComponent.textInfo.characterInfo[textComponent.textInfo.characterCount - 2].index : 0;
                            firstCharacter = textComponent.textInfo.characterCount - 1;

                            if (textComponent.textInfo.characterInfo[textComponent.textInfo.characterCount - 1].index == infoText.Length - 1)
                            {
                                UpdateComponentTextInfo(textComponent, infoText.Substring(0, infoText.Length - 1));

                                lastValidComponent = true;
                            }
                            else if (textComponent.linkedTextComponent == null)
                            {
                                lastValidComponent = true;
                            }
                        }
                        else
                        {
                            lastValidComponent = true;
                        }
                    }
                    else
                    {
                        if (textComponent.overflowMode == TextOverflowModes.Linked)
                        {
                            parsedEndIndex = -1;
                            firstCharacter = 0;
                        }
                        else
                        {
                            lastValidComponent = true;
                        }
                    }

                    if (textComponent != _component)
                    {
                        linkedComponentValueList.Add(parsedTextLength - parsedStartIndex);
                    }

                    if (parsedStartIndex <= parsedEndIndex)
                    {
                        parsedTextLength += parsedEndIndex - parsedStartIndex + 1;

                        int parsedStartPartIndex;
                        int parsedStartRelativeIndex;

                        if (parsedStartIndex < parsedTextInfo[0].text.Length)
                        {
                            parsedStartPartIndex = 0;
                            parsedStartRelativeIndex = parsedStartIndex;
                        }
                        else if (parsedStartIndex < parsedTextInfo[0].text.Length + parsedTextInfo[1].text.Length)
                        {
                            parsedStartPartIndex = 1;
                            parsedStartRelativeIndex = parsedStartIndex - parsedTextInfo[0].text.Length;
                        }
                        else
                        {
                            parsedStartPartIndex = 2;
                            parsedStartRelativeIndex = parsedStartIndex - parsedTextInfo[0].text.Length - parsedTextInfo[1].text.Length;
                        }

                        int parsedEndPartIndex;
                        int parsedEndRelativeIndex;

                        if (parsedEndIndex < parsedTextInfo[0].text.Length)
                        {
                            parsedEndPartIndex = 0;
                            parsedEndRelativeIndex = parsedEndIndex;
                        }
                        else if (parsedEndIndex < parsedTextInfo[0].text.Length + parsedTextInfo[1].text.Length)
                        {
                            parsedEndPartIndex = 1;
                            parsedEndRelativeIndex = parsedEndIndex - parsedTextInfo[0].text.Length;
                        }
                        else
                        {
                            parsedEndPartIndex = 2;
                            parsedEndRelativeIndex = parsedEndIndex - parsedTextInfo[0].text.Length - parsedTextInfo[1].text.Length;
                        }

                        if (parsedStartPartIndex != 0)
                        {
                            for (int j = 0; j < parsedTextTagInfo[0].Count; j++)
                            {
                                for (int k = parsedTextTagInfo[0][j].originRange.startIndex; k <= parsedTextTagInfo[0][j].originRange.endIndex; k++)
                                {
                                    originText.Append(componentOriginTexts[0][k]);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < parsedTextTagInfo[0].Count; j++)
                            {
                                if (parsedTextTagInfo[0][j].index >= parsedStartRelativeIndex)
                                {
                                    break;
                                }

                                for (int k = parsedTextTagInfo[0][j].originRange.startIndex; k <= parsedTextTagInfo[0][j].originRange.endIndex; k++)
                                {
                                    originText.Append(componentOriginTexts[0][k]);
                                }
                            }
                        }

                        while (parsedStartPartIndex != parsedEndPartIndex)
                        {
                            if (parsedTextInfo[parsedStartPartIndex].mappings.Length > 0)
                            {
                                if (parsedStartRelativeIndex > 0)
                                {
                                    for (int j = parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex - 1] + 1; j < parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j++)
                                    {
                                        originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                                    }
                                }
                                else
                                {
                                    for (int j = 0; j < parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j++)
                                    {
                                        originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                                    }
                                }

                                for (int j = parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j < componentOriginTexts[parsedStartPartIndex].Length; j++)
                                {
                                    originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                                }
                            }

                            parsedStartPartIndex++;
                            parsedStartRelativeIndex = 0;
                        }

                        if (parsedStartRelativeIndex > 0)
                        {
                            for (int j = parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex - 1] + 1; j < parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j++)
                            {
                                originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j++)
                            {
                                originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                            }
                        }

                        for (int j = parsedTextInfo[parsedStartPartIndex].mappings[parsedStartRelativeIndex]; j <= parsedTextInfo[parsedStartPartIndex].mappings[parsedEndRelativeIndex]; j++)
                        {
                            originText.Append(componentOriginTexts[parsedStartPartIndex][j]);
                        }

                        if (parsedEndPartIndex != 2)
                        {
                            if (lastValidComponent)
                            {
                                int tagInfoIndex = parsedTextTagInfo[1].Count - 1;

                                while (tagInfoIndex >= 0)
                                {
                                    if (parsedTextTagInfo[1][tagInfoIndex].index <= parsedEndRelativeIndex)
                                    {
                                        break;
                                    }

                                    tagInfoIndex--;
                                }

                                for (int j = tagInfoIndex + 1; j < parsedTextTagInfo[1].Count; j++)
                                {
                                    for (int k = parsedTextTagInfo[1][j].originRange.startIndex; k <= parsedTextTagInfo[1][j].originRange.endIndex; k++)
                                    {
                                        originText.Append(componentOriginTexts[1][k]);
                                    }
                                }
                            }

                            for (int j = 0; j < parsedTextTagInfo[2].Count; j++)
                            {
                                for (int k = parsedTextTagInfo[2][j].originRange.startIndex; k <= parsedTextTagInfo[2][j].originRange.endIndex; k++)
                                {
                                    originText.Append(componentOriginTexts[2][k]);
                                }
                            }
                        }
                        else
                        {
                            int tagInfoIndex = parsedTextTagInfo[2].Count - 1;

                            while (tagInfoIndex >= 0)
                            {
                                if (parsedTextTagInfo[2][tagInfoIndex].index <= parsedEndRelativeIndex)
                                {
                                    break;
                                }

                                tagInfoIndex--;
                            }

                            for (int j = tagInfoIndex + 1; j < parsedTextTagInfo[2].Count; j++)
                            {
                                for (int k = parsedTextTagInfo[2][j].originRange.startIndex; k <= parsedTextTagInfo[2][j].originRange.endIndex; k++)
                                {
                                    originText.Append(componentOriginTexts[2][k]);
                                }
                            }
                        }
                    }
                }

                textComponent = textComponent.overflowMode == TextOverflowModes.Linked ? textComponent.linkedTextComponent : null;
            }

            _linkedComponentCount = linkedComponentCount;
            _linkedComponents = linkedComponentCount > 0 ? linkedComponentList.ToArray() : null;
            _linkedComponentValues = linkedComponentCount > 0 ? linkedComponentValueList.ToArray() : null;

            return originText.ToString();
        }

        /// <summary>
        /// Method to get the parsed string information of the output text.
        /// </summary>
        /// <param name="parsedText">The parsed text of the Animatext component.</param>
        /// <returns></returns>
        protected override StringInfo GetOutputTextInfo(string parsedText)
        {
            StringBuilder outputText = new StringBuilder();
            List<int> ouputToParsedMappingList = new List<int>();
            List<Range> parsedRangeList = new List<Range>() { Range.empty };
            List<Vector2Int> ouputToCharInfoMappingList = new List<Vector2Int>();

            for (int i = 0; i <= _linkedComponentCount; i++)
            {
                TMP_Text textComponent = i == 0 ? _component : _linkedComponents[i - 1];
                int componentValue = i == 0 ? 0 : _linkedComponentValues[i - 1];

                if (textComponent.overflowMode == TextOverflowModes.Page)
                {
                    if (textComponent.textInfo.pageCount > 0)
                    {
                        int pageIndex = Mathf.Clamp(textComponent.pageToDisplay - 1, 0, textComponent.textInfo.pageCount - 1);
                        int startIndex = Mathf.Max(textComponent.firstVisibleCharacter, textComponent.textInfo.pageInfo[pageIndex].firstCharacterIndex);
                        int endIndex = textComponent.textInfo.pageInfo[pageIndex].lastCharacterIndex;

                        if (endIndex < startIndex)
                        {
                            endIndex = startIndex;
                        }

                        while (startIndex < endIndex)
                        {
                            if (textComponent.textInfo.characterInfo[endIndex].index + componentValue < parsedText.Length)
                            {
                                break;
                            }

                            endIndex--;
                        }

                        for (int j = startIndex; j <= endIndex; j++)
                        {
                            if (textComponent.textInfo.characterInfo[j].elementType == TMP_TextElementType.Character)
                            {
                                outputText.Append(textComponent.textInfo.characterInfo[j].character);
                            }
                            else
                            {
                                outputText.Append((char)textComponent.textInfo.characterInfo[j].spriteAsset.spriteCharacterTable[textComponent.textInfo.characterInfo[j].spriteIndex].unicode);
                            }

                            ouputToParsedMappingList.Add(textComponent.textInfo.characterInfo[j].index + componentValue);
                            ouputToCharInfoMappingList.Add(textComponent.textInfo.characterInfo[j].isVisible ? new Vector2Int(i, j) : new Vector2Int(-1, -1));
                        }

                        if (textComponent.pageToDisplay > textComponent.textInfo.pageCount)
                        {
                            parsedRangeList.Add(new Range(parsedText.Length, parsedText.Length));
                        }
                        else
                        {
                            int rangeStartIndex;

                            if (pageIndex != 0)
                            {
                                rangeStartIndex = textComponent.textInfo.characterInfo[textComponent.textInfo.pageInfo[pageIndex].firstCharacterIndex].index + componentValue;
                            }
                            else
                            {
                                if (textComponent.firstVisibleCharacter > 0)
                                {
                                    rangeStartIndex = textComponent.textInfo.characterInfo[textComponent.firstVisibleCharacter - 1].index + componentValue;
                                }
                                else
                                {
                                    rangeStartIndex = componentValue - 1;
                                }
                            }

                            int rangeEndIndex;

                            if (pageIndex != textComponent.textInfo.pageCount - 1)
                            {
                                rangeEndIndex = textComponent.textInfo.characterInfo[textComponent.textInfo.pageInfo[pageIndex].lastCharacterIndex].index + componentValue;
                            }
                            else
                            {
                                rangeEndIndex = parsedText.Length - 1;
                            }

                            parsedRangeList.Add(new Range(rangeStartIndex, rangeEndIndex));
                        }
                    }
                }
                else
                {
                    int characterCount = textComponent.textInfo.characterCount;

                    if (textComponent.textInfo.characterCount > 0)
                    {
                        Range range = parsedRangeList[0];

                        if (textComponent.textInfo.characterInfo[characterCount - 1].character == '\u0003')
                        {
                            range.endIndex = textComponent.textInfo.characterInfo[characterCount - 1].index + componentValue;
                            characterCount--;
                        }
                        else
                        {
                            range.endIndex = parsedText.Length - 1;
                        }

                        range.count = range.endIndex - range.startIndex + 1;

                        parsedRangeList[0] = range;
                    }

                    for (int j = textComponent.firstVisibleCharacter; j < characterCount; j++)
                    {
                        if (textComponent.textInfo.characterInfo[j].elementType == TMP_TextElementType.Character)
                        {
                            outputText.Append(textComponent.textInfo.characterInfo[j].character);
                        }
                        else
                        {
                            outputText.Append((char)textComponent.textInfo.characterInfo[j].spriteAsset.spriteCharacterTable[textComponent.textInfo.characterInfo[j].spriteIndex].unicode);
                        }

                        ouputToParsedMappingList.Add(textComponent.textInfo.characterInfo[j].index + componentValue);
                        ouputToCharInfoMappingList.Add(textComponent.textInfo.characterInfo[j].isVisible ? new Vector2Int(i, j) : new Vector2Int(-1, -1));
                    }
                }
            }

            if (parsedRangeList[0] == Range.empty)
            {
                parsedRangeList.RemoveAt(0);
            }

            _ouputToCharInfoMappings = ouputToCharInfoMappingList.ToArray();

            StringInfo outputTextInfo;

            outputTextInfo.text = outputText.ToString();
            outputTextInfo.mappings = ouputToParsedMappingList.ToArray();
            outputTextInfo.ranges = parsedRangeList.ToArray();

            return outputTextInfo;
        }

        /// <summary>
        /// Method to set the color array of the character vertices.
        /// </summary>
        /// <param name="outputIndex">The character index of the output text.</param>
        /// <param name="colors">The current color array of the character vertices.</param>
        /// <param name="originColors">The origin color array of the character vertices.</param>
        protected override void SetCharColors(int outputIndex, Color[] colors, Color[] originColors)
        {
            if (_ouputToCharInfoMappings[outputIndex].x > -1)
            {
                int index = _ouputToCharInfoMappings[outputIndex].y;
                TMP_TextInfo textInfo = _ouputToCharInfoMappings[outputIndex].x == 0 ? _component.textInfo : _linkedComponents[_ouputToCharInfoMappings[outputIndex].x - 1].textInfo;

                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex] = colors[3];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 1] = colors[0];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 2] = colors[1];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32[textInfo.characterInfo[index].vertexIndex + 3] = colors[2];
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
            if (_ouputToCharInfoMappings[outputIndex].x > -1)
            {
                int index = _ouputToCharInfoMappings[outputIndex].y;
                TMP_TextInfo textInfo = _ouputToCharInfoMappings[outputIndex].x == 0 ? _component.textInfo : _linkedComponents[_ouputToCharInfoMappings[outputIndex].x - 1].textInfo;

                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex] = positions[3];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 1] = positions[0];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 2] = positions[1];
                textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].vertices[textInfo.characterInfo[index].vertexIndex + 3] = positions[2];
            }
        }

        /// <summary>
        /// Method to mark the text component as dirty.
        /// </summary>
        protected override void SetComponentDirty()
        {
            _component.SetAllDirty();

            for (int i = 0; i < _linkedComponentCount; i++)
            {
                _linkedComponents[i].SetAllDirty();
            }
        }

        /// <summary>
        /// Method to update the text vertex data in the text component.
        /// </summary>
        protected override void UpdateComponentData()
        {
            _component.UpdateVertexData();

            for (int i = 0; i < _linkedComponentCount; i++)
            {
                _linkedComponents[i].UpdateVertexData();
            }
        }

        /// <summary>
        /// Method to update the text vertex data in the text information.
        /// </summary>
        protected override void UpdateData()
        {
            if (_scaleSDF != 1)
            {
                TMP_TextInfo textInfo = _component.textInfo;

                for (int i = 0; i < _component.textInfo.materialCount; i++)
                {
                    for (int j = 0; j < textInfo.meshInfo[i].uvs2.Length; j++)
                    {
                        textInfo.meshInfo[i].uvs2[j].y *= _scaleSDF;
                    }
                }

                for (int i = 0; i < _linkedComponentCount; i++)
                {
                    textInfo = _linkedComponents[i].textInfo;

                    for (int j = 0; j < textInfo.materialCount; j++)
                    {
                        for (int k = 0; k < textInfo.meshInfo[j].uvs2.Length; k++)
                        {
                            textInfo.meshInfo[j].uvs2[k].y *= _scaleSDF;
                        }
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
            _modifyingComponentTextInfo = true;

            base.UpdateInfo();

            _modifyingComponentTextInfo = false;

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                text = inputText;
            }
#endif 
        }

#if UNITY_EDITOR
        private static readonly string[] _componentRichTextTags = new string[]
        {
            "ALIGN",
            "ALPHA",
            "B",
            "COLOR",
            "CSPACE",
            "FONT",
            "I",
            "INDENT",
            "LINE-HEIGHT",
            "LINE-INDENT",
            "LINK",
            "LOWERCASE",
            "MARGIN",
            "MARK",
            "MSPACE",
            "NOPARSE",
            "NOBR",
            "PAGE",
            "POS",
            "S",
            "SIZE",
            "SMALLCAPS",
            "SPACE",
            "SPRITE",
            "STYLE",
            "SUB",
            "SUP",
            "U",
            "UPPERCASE",
            "VOFFSET",
            "WIDTH",
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