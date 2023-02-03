// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0017, IDE0066, IDE0090, IDE1006

using Animatext.Effects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Animatext
{
    public class TextInfo
    {
        private struct TextUnitInfo
        {
            public Range originRange;
            public Range charRange;
            public Range characterRange;
            public Range wordRange;
            public Range lineRange;
            public Range groupRange;
        }

        private struct TextUnitRange
        {
            public Range range;
            public Range originRange;
        }

        private string _originText;
        private string _parsedText;
        private string _outputText;
        private string _effectText;
        private bool _dataDirty;
        private bool _positionsDirty;
        private bool _colorsDirty;
        private CharInfo[] _charInfo;
        private int _charCount;
        private TextUnitInfo[] _chars;
        private int _characterCount;
        private TextUnitInfo[] _characters;
        private int _wordCount;
        private TextUnitInfo[] _words;
        private int _lineCount;
        private TextUnitInfo[] _lines;
        private int _groupCount;
        private TextUnitInfo[] _groups;
        private readonly List<RangeInfo> _rangeInfoList = new List<RangeInfo>();
        private readonly Dictionary<string, int> _tagDictionary = new Dictionary<string, int>();

        private static readonly Dictionary<string, int> _systemTagDictionary = new Dictionary<string, int>()
        {
            { "c", -1 },
            { "w", -2 },
            { "l", -3 },
            { "g", -4 },
        };

        private static readonly HashSet<char> _wordBreaks = new HashSet<char>
        {
            '\u000A', //Line feeds.
            '\u0020', //Spaces.
            '\u00A0', //No-break spaces.
            '\u3000', //Ideographic spaces.
        };

        private static readonly HashSet<char> _lineBreaks = new HashSet<char>
        {
            '\u000A', //Line feeds.
        };

        public static HashSet<char> wordBreaks
        {
            get { return _wordBreaks; }
        }

        public static HashSet<char> lineBreaks
        {
            get { return _lineBreaks; }
        }

        public int charCount
        {
            get { return _charCount; }
        }

        public CharInfo[] charInfo
        {
            get { return _charInfo; }
        }

        public string originText
        {
            get { return _originText; }
        }

        public string parsedText
        {
            get { return _parsedText; }
        }

        public string outputText
        {
            get { return _outputText; }
        }

        public string effectText
        {
            get { return _effectText; }
        }

        public bool dataDirty
        {
            get { return _dataDirty; }
        }

        public bool positionsDirty
        {
            get { return _positionsDirty; }
        }

        public bool colorsDirty
        {
            get { return _colorsDirty; }
        }

        public void ColorRange(Range range, Color color)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Color(color);
            }
        }

        public void ColorRange(Range range, Color color, ColorMode colorMode)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Color(color, colorMode);
            }
        }

        public void MoveRange(Range range, Vector2 position)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Move(position);
            }
        }

        public void OpacifyRange(Range range, float opacity)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Opacify(opacity);
            }
        }

        public void OpacifyRange(Range range, float opacity, ColorMode colorMode)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Opacify(opacity, colorMode);
            }
        }

        public void RotateRange(Range range, float rotation)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Rotate(rotation);
            }
        }

        public void RotateRange(Range range, float rotation, Vector2 anchorPoint)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Rotate(rotation, anchorPoint);
            }
        }

        public void ScaleRange(Range range, Vector2 scale)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Scale(scale);
            }
        }

        public void ScaleRange(Range range, Vector2 scale, Vector2 anchorPoint)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Scale(scale, anchorPoint);
            }
        }

        public void SkewRange(Range range, Vector2 skew)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Skew(skew);
            }
        }

        public void SkewRange(Range range, Vector2 skew, Vector2 anchorPoint)
        {
            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                _charInfo[i].Skew(skew, anchorPoint);
            }
        }

        public void UpdateRangeAnchor(Range range, Anchor anchor)
        {
            Vector2 anchorMin = Vector2.positiveInfinity;
            Vector2 anchorMax = Vector2.negativeInfinity;

            for (int i = range.startIndex; i <= range.endIndex; i++)
            {
                anchorMin = Vector2.Min(anchorMin, _charInfo[i].positionGroup.GetMinPosition());
                anchorMax = Vector2.Max(anchorMax, _charInfo[i].positionGroup.GetMaxPosition());
            }

            anchor.Set(anchorMin, anchorMax);
        }

        public void Execute()
        {
            for (int i = 0; i < _charCount; i++)
            {
                _charInfo[i].Execute();
            }

            _positionsDirty = false;
            _colorsDirty = false;

            for (int i = 0; i < _charCount; i++)
            {
                if (_charInfo[i].positionGroup.isDirty)
                {
                    _positionsDirty = true;
                    break;
                }
            }

            for (int i = 0; i < _charCount; i++)
            {
                if (_charInfo[i].colorGroup.isDirty)
                {
                    _colorsDirty = true;
                    break;
                }
            }

            _dataDirty = _positionsDirty | _colorsDirty;
        }

        public ExecutionEffect[] GenerateExecutionEffects(BaseEffect[] presets, Effect effect, BaseAnimatext animatext)
        {
            if (presets == null)
            {
                return null;
            }

            Dictionary<string, List<BaseEffect>> presetDictionary = new Dictionary<string, List<BaseEffect>>();

            for (int i = 0; i < presets.Length; i++)
            {
                if (presets[i] != null)
                {
                    string presetTag = presets[i].tag ?? string.Empty;

                    if (presetDictionary.ContainsKey(presetTag))
                    {
                        presetDictionary[presetTag].Add(presets[i]);
                    }
                    else
                    {
                        presetDictionary.Add(presetTag, new List<BaseEffect>() { presets[i] });
                    }
                }
            }

            List<ExecutionEffect> executionEffectList = new List<ExecutionEffect>();

            int rangeInfoCount = _rangeInfoList.Count;

            for (int i = 0; i < rangeInfoCount; i++)
            {
                RangeInfo rangeInfo = _rangeInfoList[i];

                if (presetDictionary.ContainsKey(rangeInfo.tag))
                {
                    List<BaseEffect> presetList = presetDictionary[rangeInfo.tag];
                    int presetCount = presetList.Count;

                    ExecutionEffect executionEffect = new ExecutionEffect();

                    executionEffect.effect = effect;
                    executionEffect.order = new Vector3Int(rangeInfo.openingOrder, rangeInfo.closingOrder, rangeInfo.originRange.startIndex);
                    executionEffect.openingInterval = rangeInfo.openingInterval;
                    executionEffect.openingIntervalType = rangeInfo.openingIntervalType;
                    executionEffect.closingInterval = rangeInfo.closingInterval;
                    executionEffect.closingIntervalType = rangeInfo.closingIntervalType;
                    executionEffect.executionInfo = new ExecutionInfo[presetCount];
                    executionEffect.executionInfoCount = presetCount;
                    executionEffect.startInterval = 0;
                    executionEffect.rangeInterval = 0;
                    executionEffect.executionInterval = 0;

                    for (int j = 0; j < presetCount; j++)
                    {
                        BaseEffect preset = presetList[j];
                        ExecutionInfo executionInfo = new ExecutionInfo();

                        if ((preset.infoFlags & InfoFlags.Char) != 0)
                        {
                            UnitInfo[] chars = new UnitInfo[rangeInfo.range.count];

                            for (int k = rangeInfo.range.startIndex; k <= rangeInfo.range.endIndex; k++)
                            {
                                chars[k - rangeInfo.range.startIndex] = new UnitInfo(this, new Range(_chars[k].charRange.startIndex, _chars[k].charRange.endIndex));
                            }

                            executionInfo.chars = chars;
                            executionInfo.charCount = executionInfo.chars.Length;
                        }

                        if ((preset.infoFlags & InfoFlags.Character) != 0)
                        {
                            Range characterRange;

                            if (rangeInfo.range.startIndex < _charCount)
                            {
                                characterRange.startIndex = _chars[rangeInfo.range.startIndex].characterRange.startIndex;

                                while (characterRange.startIndex < _characterCount)
                                {
                                    if (_characters[characterRange.startIndex].originRange.endIndex > rangeInfo.originRange.startIndex)
                                    {
                                        break;
                                    }

                                    characterRange.startIndex++;
                                }
                            }
                            else
                            {
                                characterRange.startIndex = _characterCount;
                            }

                            if (rangeInfo.range.endIndex > -1)
                            {
                                characterRange.endIndex = _chars[rangeInfo.range.endIndex].characterRange.endIndex;

                                while (characterRange.endIndex + 1 < _characterCount)
                                {
                                    if (_characters[characterRange.endIndex + 1].originRange.startIndex > rangeInfo.originRange.endIndex)
                                    {
                                        break;
                                    }

                                    characterRange.endIndex++;
                                }
                            }
                            else
                            {
                                characterRange.endIndex = -1;
                            }

                            characterRange.count = characterRange.endIndex - characterRange.startIndex + 1;

                            UnitInfo[] characters = new UnitInfo[characterRange.count];

                            if (characterRange.count > 1)
                            {
                                characters[0] = new UnitInfo(this, new Range(Mathf.Max(_characters[characterRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), _characters[characterRange.startIndex].charRange.endIndex));

                                for (int k = characterRange.startIndex + 1; k < characterRange.endIndex; k++)
                                {
                                    characters[k - characterRange.startIndex] = new UnitInfo(this, new Range(_characters[k].charRange.startIndex, _characters[k].charRange.endIndex));
                                }

                                characters[characterRange.count - 1] = new UnitInfo(this, new Range(_characters[characterRange.endIndex].charRange.startIndex, Mathf.Min(_characters[characterRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }
                            else if (characterRange.count == 1)
                            {
                                characters[0] = new UnitInfo(this, new Range(Mathf.Max(_characters[characterRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), Mathf.Min(_characters[characterRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }

                            executionInfo.characters = characters;
                            executionInfo.characterCount = executionInfo.characters.Length;
                        }

                        if ((preset.infoFlags & InfoFlags.Word) != 0)
                        {
                            Range wordRange;

                            if (rangeInfo.range.startIndex < _charCount)
                            {
                                wordRange.startIndex = _chars[rangeInfo.range.startIndex].wordRange.startIndex;

                                while (wordRange.startIndex < _wordCount)
                                {
                                    if (_words[wordRange.startIndex].originRange.endIndex > rangeInfo.originRange.startIndex)
                                    {
                                        break;
                                    }

                                    wordRange.startIndex++;
                                }
                            }
                            else
                            {
                                wordRange.startIndex = _wordCount;
                            }

                            if (rangeInfo.range.endIndex > -1)
                            {
                                wordRange.endIndex = _chars[rangeInfo.range.endIndex].wordRange.endIndex;

                                while (wordRange.endIndex + 1 < _wordCount)
                                {
                                    if (_words[wordRange.endIndex + 1].originRange.startIndex > rangeInfo.originRange.endIndex)
                                    {
                                        break;
                                    }

                                    wordRange.endIndex++;
                                }
                            }
                            else
                            {
                                wordRange.endIndex = -1;
                            }

                            wordRange.count = wordRange.endIndex - wordRange.startIndex + 1;

                            UnitInfo[] words = new UnitInfo[wordRange.count];

                            if (wordRange.count > 1)
                            {
                                words[0] = new UnitInfo(this, new Range(Mathf.Max(_words[wordRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), _words[wordRange.startIndex].charRange.endIndex));

                                for (int k = wordRange.startIndex + 1; k < wordRange.endIndex; k++)
                                {
                                    words[k - wordRange.startIndex] = new UnitInfo(this, new Range(_words[k].charRange.startIndex, _words[k].charRange.endIndex));
                                }

                                words[wordRange.count - 1] = new UnitInfo(this, new Range(_words[wordRange.endIndex].charRange.startIndex, Mathf.Min(_words[wordRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }
                            else if (wordRange.count == 1)
                            {
                                words[0] = new UnitInfo(this, new Range(Mathf.Max(_words[wordRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), Mathf.Min(_words[wordRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }

                            executionInfo.words = words;
                            executionInfo.wordCount = executionInfo.words.Length;
                        }

                        if ((preset.infoFlags & InfoFlags.Line) != 0)
                        {
                            Range lineRange;

                            if (rangeInfo.range.startIndex < _charCount)
                            {
                                lineRange.startIndex = _chars[rangeInfo.range.startIndex].lineRange.startIndex;

                                while (lineRange.startIndex < _lineCount)
                                {
                                    if (_lines[lineRange.startIndex].originRange.endIndex > rangeInfo.originRange.startIndex)
                                    {
                                        break;
                                    }

                                    lineRange.startIndex++;
                                }
                            }
                            else
                            {
                                lineRange.startIndex = _lineCount;
                            }

                            if (rangeInfo.range.endIndex > -1)
                            {
                                lineRange.endIndex = _chars[rangeInfo.range.endIndex].lineRange.endIndex;

                                while (lineRange.endIndex + 1 < _lineCount)
                                {
                                    if (_lines[lineRange.endIndex + 1].originRange.startIndex > rangeInfo.originRange.endIndex)
                                    {
                                        break;
                                    }

                                    lineRange.endIndex++;
                                }
                            }
                            else
                            {
                                lineRange.endIndex = -1;
                            }

                            lineRange.count = lineRange.endIndex - lineRange.startIndex + 1;

                            UnitInfo[] lines = new UnitInfo[lineRange.count];

                            if (lineRange.count > 1)
                            {
                                lines[0] = new UnitInfo(this, new Range(Mathf.Max(_lines[lineRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), _lines[lineRange.startIndex].charRange.endIndex));

                                for (int k = lineRange.startIndex + 1; k < lineRange.endIndex; k++)
                                {
                                    lines[k - lineRange.startIndex] = new UnitInfo(this, new Range(_lines[k].charRange.startIndex, _lines[k].charRange.endIndex));
                                }

                                lines[lineRange.count - 1] = new UnitInfo(this, new Range(_lines[lineRange.endIndex].charRange.startIndex, Mathf.Min(_lines[lineRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }
                            else if (lineRange.count == 1)
                            {
                                lines[0] = new UnitInfo(this, new Range(Mathf.Max(_lines[lineRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), Mathf.Min(_lines[lineRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }

                            executionInfo.lines = lines;
                            executionInfo.lineCount = executionInfo.lines.Length;
                        }

                        if ((preset.infoFlags & InfoFlags.Group) != 0)
                        {
                            Range groupRange;

                            if (rangeInfo.range.startIndex < _charCount)
                            {
                                groupRange.startIndex = _chars[rangeInfo.range.startIndex].groupRange.startIndex;

                                while (groupRange.startIndex < _groupCount)
                                {
                                    if (_groups[groupRange.startIndex].originRange.endIndex > rangeInfo.originRange.startIndex)
                                    {
                                        break;
                                    }

                                    groupRange.startIndex++;
                                }
                            }
                            else
                            {
                                groupRange.startIndex = _groupCount;
                            }

                            if (rangeInfo.range.endIndex > -1)
                            {
                                groupRange.endIndex = _chars[rangeInfo.range.endIndex].groupRange.endIndex;

                                while (groupRange.endIndex + 1 < _groupCount)
                                {
                                    if (_groups[groupRange.endIndex + 1].originRange.startIndex > rangeInfo.originRange.endIndex)
                                    {
                                        break;
                                    }

                                    groupRange.endIndex++;
                                }
                            }
                            else
                            {
                                groupRange.endIndex = -1;
                            }

                            groupRange.count = groupRange.endIndex - groupRange.startIndex + 1;

                            UnitInfo[] groups = new UnitInfo[groupRange.count];

                            if (groupRange.count > 1)
                            {
                                groups[0] = new UnitInfo(this, new Range(Mathf.Max(_groups[groupRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), _groups[groupRange.startIndex].charRange.endIndex));

                                for (int k = groupRange.startIndex + 1; k < groupRange.endIndex; k++)
                                {
                                    groups[k - groupRange.startIndex] = new UnitInfo(this, new Range(_groups[k].charRange.startIndex, _groups[k].charRange.endIndex));
                                }

                                groups[groupRange.count - 1] = new UnitInfo(this, new Range(_groups[groupRange.endIndex].charRange.startIndex, Mathf.Min(_groups[groupRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }
                            else if (groupRange.count == 1)
                            {
                                groups[0] = new UnitInfo(this, new Range(Mathf.Max(_groups[groupRange.startIndex].charRange.startIndex, rangeInfo.range.startIndex), Mathf.Min(_groups[groupRange.endIndex].charRange.endIndex, rangeInfo.range.endIndex)));
                            }

                            executionInfo.groups = groups;
                            executionInfo.groupCount = executionInfo.groups.Length;
                        }

                        if ((preset.infoFlags & InfoFlags.Range) != 0)
                        {
                            UnitInfo unitInfo = new UnitInfo(this, rangeInfo.range);

                            executionInfo.range = unitInfo;
                            executionInfo.rangeAnchor = true;
                        }

                        executionInfo.preset = preset;
                        executionInfo.effect = effect;
                        executionInfo.animatext = animatext;
                        executionInfo.time = float.NaN;
                        executionInfo.lastTime = float.NaN;
                        executionInfo.frontInterval = 0;
                        executionInfo.startInterval = 0;
                        executionInfo.rangeInterval = 0;
                        executionInfo.executionInterval = 0;
                        executionInfo.totalInterval = 0;

                        executionEffect.executionInfo[j] = executionInfo;
                    }

                    executionEffectList.Add(executionEffect);
                }
            }

            executionEffectList.Sort();

            return executionEffectList.ToArray();
        }

        public void Update(string text, List<Effect> effects, Settings settings, Func<string, StringInfo> func)
        {
            #region <- Step A: Preparation ->

            if (string.IsNullOrEmpty(text))
            {
                _originText = string.Empty;
                _parsedText = string.Empty;
                _outputText = string.Empty;
                _effectText = string.Empty;

                _dataDirty = false;
                _positionsDirty = false;
                _colorsDirty = false;

                _charCount = 0;
                _characterCount = 0;
                _wordCount = 0;
                _lineCount = 0;
                _groupCount = 0;

                _charInfo = null;
                _chars = null;
                _characters = null;
                _words = null;
                _lines = null;
                _groups = null;

                _rangeInfoList.Clear();
                _tagDictionary.Clear();

                func(string.Empty);

                return;
            }

            InfoFlags a_infoFlags = InfoFlags.None;
            HashSet<string> a_effectTags = new HashSet<string>();

            for (int i = 0; i < effects.Count; i++)
            {
                a_infoFlags |= effects[i].GetInfoFlags();
                a_effectTags.UnionWith(effects[i].GetTags());
            }

            bool a_emptyTag = a_effectTags.Contains(string.Empty);

            if (a_emptyTag)
            {
                a_effectTags.Remove(string.Empty);
            }

            int a_tagIndex = 0;

             _tagDictionary.Clear();

            foreach (var tag in a_effectTags)
            {
                _tagDictionary.Add(tag, a_tagIndex++);
            }

            _dataDirty = false;
            _positionsDirty = false;
            _colorsDirty = false;
            _originText = text;

            #endregion

            #region <- Step B: Tag Info ->

            StringInfo b_parsedTextInfo = ParseText(text, _tagDictionary, settings, out List<TagInfo> b_tagInfoList);
            int b_tagInfoCount = b_tagInfoList.Count;
            List<RangeInfo> b_rangeInfoList = new List<RangeInfo>();

            for (int i = 0; i < b_tagInfoCount; i++)
            {
                TagInfo b_tagInfo = b_tagInfoList[i];

                switch (b_tagInfo.type)
                {
                    case 0:
                        {
                            RangeInfo b_rangeInfo;

                            b_rangeInfo.tag = b_tagInfo.tag;
                            b_rangeInfo.id = b_tagInfo.id;
                            b_rangeInfo.range = new Range() { startIndex = b_tagInfo.index, endIndex = -1, count = -1 };
                            b_rangeInfo.originRange = b_tagInfo.originRange;
                            b_rangeInfo.openingOrder = b_tagInfo.haveOrder ? b_tagInfo.order : 0;
                            b_rangeInfo.openingInterval = b_tagInfo.interval;
                            b_rangeInfo.openingIntervalType = b_tagInfo.intervalType;
                            b_rangeInfo.closingOrder = b_tagInfo.originRange.startIndex;
                            b_rangeInfo.closingInterval = 0;
                            b_rangeInfo.closingIntervalType = IntervalType.None;

                            b_rangeInfoList.Add(b_rangeInfo);

                            break;
                        }

                    case 1:
                        {
                            int b_openingTagIndex = b_rangeInfoList.Count - 1;

                            while (b_openingTagIndex >= 0)
                            {
                                if (b_rangeInfoList[b_openingTagIndex].range.count < 0 && b_rangeInfoList[b_openingTagIndex].id == b_tagInfo.id)
                                {
                                    RangeInfo b_rangeInfo = b_rangeInfoList[b_openingTagIndex];

                                    b_rangeInfo.range.endIndex = b_tagInfo.index - 1;
                                    b_rangeInfo.range.count = b_rangeInfo.range.endIndex - b_rangeInfo.range.startIndex + 1;
                                    b_rangeInfo.originRange.endIndex = b_tagInfo.originRange.endIndex;
                                    b_rangeInfo.originRange.count = b_rangeInfo.originRange.endIndex - b_rangeInfo.originRange.startIndex + 1;
                                    b_rangeInfo.closingInterval = b_tagInfo.interval;
                                    b_rangeInfo.closingIntervalType = b_tagInfo.intervalType;

                                    if (b_tagInfo.haveOrder)
                                    {
                                        b_rangeInfo.closingOrder = b_tagInfo.order;
                                    }

                                    b_rangeInfoList[b_openingTagIndex] = b_rangeInfo;

                                    break;
                                }

                                b_openingTagIndex--;
                            }

                            break;
                        }

                    case 2:
                        {
                            RangeInfo b_rangeInfo;

                            b_rangeInfo.tag = b_tagInfo.tag;
                            b_rangeInfo.id = b_tagInfo.id;
                            b_rangeInfo.range = new Range() { startIndex = b_tagInfo.index, endIndex = b_tagInfo.index - 1, count = 0 };
                            b_rangeInfo.originRange = b_tagInfo.originRange;
                            b_rangeInfo.openingOrder = b_tagInfo.haveOrder ? b_tagInfo.order : 0;
                            b_rangeInfo.openingInterval = b_tagInfo.interval;
                            b_rangeInfo.openingIntervalType = b_tagInfo.intervalType;
                            b_rangeInfo.closingOrder = b_tagInfo.originRange.startIndex;
                            b_rangeInfo.closingInterval = 0;
                            b_rangeInfo.closingIntervalType = IntervalType.None;

                            b_rangeInfoList.Add(b_rangeInfo);

                            break;
                        }

                    case 3:
                        {
                            int b_openingTagIndex = b_rangeInfoList.Count - 1;

                            if (b_tagInfo.id == 0)
                            {
                                while (b_openingTagIndex >= 0)
                                {
                                    if (b_rangeInfoList[b_openingTagIndex].range.count < 0)
                                    {
                                        RangeInfo b_rangeInfo = b_rangeInfoList[b_openingTagIndex];

                                        b_rangeInfo.range.endIndex = b_tagInfo.index - 1;
                                        b_rangeInfo.range.count = b_rangeInfo.range.endIndex - b_rangeInfo.range.startIndex + 1;
                                        b_rangeInfo.originRange.endIndex = b_tagInfo.originRange.endIndex;
                                        b_rangeInfo.originRange.count = b_rangeInfo.originRange.endIndex - b_rangeInfo.originRange.startIndex + 1;

                                        b_rangeInfoList[b_openingTagIndex] = b_rangeInfo;

                                        break;
                                    }

                                    b_openingTagIndex--;
                                }
                            }
                            else if (b_tagInfo.id == 1)
                            {
                                while (b_openingTagIndex >= 0)
                                {
                                    if (b_rangeInfoList[b_openingTagIndex].range.count < 0)
                                    {
                                        RangeInfo b_rangeInfo = b_rangeInfoList[b_openingTagIndex];

                                        b_rangeInfo.range.endIndex = b_tagInfo.index - 1;
                                        b_rangeInfo.range.count = b_rangeInfo.range.endIndex - b_rangeInfo.range.startIndex + 1;
                                        b_rangeInfo.originRange.endIndex = b_tagInfo.originRange.endIndex;
                                        b_rangeInfo.originRange.count = b_rangeInfo.originRange.endIndex - b_rangeInfo.originRange.startIndex + 1;

                                        b_rangeInfoList[b_openingTagIndex] = b_rangeInfo;
                                    }

                                    b_openingTagIndex--;
                                }
                            }

                            break;
                        }

                    default:
                        break;
                }
            }

            int b_rangeInfoCount = b_rangeInfoList.Count;

            for (int i = 0; i < b_rangeInfoCount; i++)
            {
                if (b_rangeInfoList[i].range.count == -1)
                {
                    RangeInfo b_rangeInfo = b_rangeInfoList[i];

                    b_rangeInfo.range.endIndex = b_parsedTextInfo.text.Length - 1;
                    b_rangeInfo.range.count = b_rangeInfo.range.endIndex - b_rangeInfo.range.startIndex + 1;

                    b_rangeInfo.originRange.endIndex = _originText.Length - 1;
                    b_rangeInfo.originRange.count = b_rangeInfo.originRange.endIndex - b_rangeInfo.originRange.startIndex + 1;

                    b_rangeInfoList[i] = b_rangeInfo;
                }
            }

            _parsedText = b_parsedTextInfo.text;

            #endregion

            #region <- Step C: Char Info ->

            bool[] c_boolArray = new bool[_parsedText.Length];
            StringInfo c_outputTextInfo = func(_parsedText);

            if (a_emptyTag)
            {
                for (int i = 0; i < c_boolArray.Length; i++)
                {
                    c_boolArray[i] = true;
                }
            }
            else
            {
                for (int i = 0; i < b_rangeInfoCount; i++)
                {
                    if (b_rangeInfoList[i].id >= 0)
                    {
                        int c_startIndex = b_rangeInfoList[i].range.startIndex;
                        int c_endIndex = b_rangeInfoList[i].range.endIndex;

                        for (int j = c_startIndex; j <= c_endIndex; j++)
                        {
                            c_boolArray[j] = true;
                        }
                    }
                }
            }

            StringBuilder c_effectText = new StringBuilder();
            List<int> c_effectToOutputMappingList = new List<int>();

            for (int i = 0; i < c_outputTextInfo.text.Length; i++)
            {
                if (c_boolArray[c_outputTextInfo.mappings[i]] && CharInfo.IsValidChar(c_outputTextInfo.text[i]))
                {
                    c_effectText.Append(c_outputTextInfo.text[i]);
                    c_effectToOutputMappingList.Add(i);
                }
            }

            int[] c_parsedToOriginMappings = b_parsedTextInfo.mappings;
            int[] c_effectToOutputMappings = c_effectToOutputMappingList.ToArray();
            int[] c_effectToParsedMappings = new int[c_effectText.Length];

            for (int i = 0; i < c_effectText.Length; i++)
            {
                c_effectToParsedMappings[i] = c_outputTextInfo.mappings[c_effectToOutputMappings[i]];
            }

            CharInfo[] c_charInfoArray = new CharInfo[c_effectText.Length];

            for (int i = 0; i < c_effectText.Length; i++)
            {
                c_charInfoArray[i] = new CharInfo(c_effectText[i], c_parsedToOriginMappings[c_effectToParsedMappings[i]], c_effectToParsedMappings[i], c_effectToOutputMappings[i]);
            }

            _charCount = c_effectText.Length;
            _charInfo = c_charInfoArray;
            _outputText = c_outputTextInfo.text;
            _effectText = c_effectText.ToString();

            #endregion

            #region <- Step D: Range Info ->

            int d_minParsedIndex;
            int d_maxParsedIndex;

            if (_charCount != 0)
            {
                d_minParsedIndex = c_effectToParsedMappings[0];
                d_maxParsedIndex = c_effectToParsedMappings[_charCount - 1];
            }
            else
            {
                d_minParsedIndex = _parsedText.Length;
                d_maxParsedIndex = -1;
            }

            _rangeInfoList.Clear();

            List<TextUnitRange> d_characterChangeRanges = new List<TextUnitRange>();
            List<TextUnitRange> d_wordChangeRanges = new List<TextUnitRange>();
            List<TextUnitRange> d_lineChangeRanges = new List<TextUnitRange>();
            List<TextUnitInfo> d_groupRanges = new List<TextUnitInfo>();

            for (int i = 0; i < b_rangeInfoCount; i++)
            {
                int d_startParsedIndex = b_rangeInfoList[i].range.startIndex;
                int d_endParsedIndex = b_rangeInfoList[i].range.endIndex;

                bool d_isValidRange = false;

                for (int j = 0; j < c_outputTextInfo.ranges.Length; j++)
                {
                    if (d_startParsedIndex > c_outputTextInfo.ranges[j].endIndex || d_endParsedIndex < c_outputTextInfo.ranges[j].startIndex)
                    {
                        if (d_endParsedIndex != c_outputTextInfo.ranges[j].endIndex)
                        {
                            continue;
                        }
                    }

                    d_isValidRange = true;
                    break;
                }

                if (d_isValidRange == false)
                {
                    continue;
                }

                int d_startIndex;

                if (d_startParsedIndex > d_maxParsedIndex)
                {
                    d_startIndex = _charCount;
                }
                else
                {
                    int d_minIndex = 0;
                    int d_maxIndex = _charCount - 1;

                    d_startIndex = (d_maxIndex + d_minIndex) / 2;

                    while (d_minIndex != d_maxIndex)
                    {
                        if (d_startParsedIndex < c_effectToParsedMappings[d_startIndex])
                        {
                            d_maxIndex = d_startIndex;
                        }
                        else if (d_startParsedIndex > c_effectToParsedMappings[d_startIndex])
                        {
                            d_minIndex = d_startIndex + 1;
                        }
                        else
                        {
                            break;
                        }

                        d_startIndex = (d_minIndex + d_maxIndex) / 2;
                    }
                }

                int d_endIndex;

                if (d_endParsedIndex < d_minParsedIndex)
                {
                    d_endIndex = -1;
                }
                else
                {
                    int d_minIndex = 0;
                    int d_maxIndex = _charCount - 1;

                    d_endIndex = (d_maxIndex + d_minIndex) / 2;

                    while (d_minIndex != d_maxIndex)
                    {
                        if (d_endParsedIndex < c_effectToParsedMappings[d_endIndex])
                        {
                            d_maxIndex = d_endIndex - 1;
                        }
                        else if (d_endParsedIndex > c_effectToParsedMappings[d_endIndex])
                        {
                            d_minIndex = d_endIndex;
                        }
                        else
                        {
                            break;
                        }

                        d_endIndex = (d_minIndex + d_maxIndex + 1) / 2;
                    }
                }

                if (b_rangeInfoList[i].id >= 0)
                {
                    RangeInfo d_rangeInfo = b_rangeInfoList[i];

                    d_rangeInfo.range.startIndex = d_startIndex;
                    d_rangeInfo.range.endIndex = d_endIndex;
                    d_rangeInfo.range.count = d_rangeInfo.range.endIndex - d_rangeInfo.range.startIndex + 1;

                    _rangeInfoList.Add(d_rangeInfo);
                }
                else
                {
                    switch (b_rangeInfoList[i].id)
                    {
                        case -1:
                            d_characterChangeRanges.Add(new TextUnitRange() { range = new Range(d_startIndex, d_endIndex), originRange = b_rangeInfoList[i].originRange });
                            break;

                        case -2:
                            d_wordChangeRanges.Add(new TextUnitRange() { range = new Range(d_startIndex, d_endIndex), originRange = b_rangeInfoList[i].originRange });
                            break;

                        case -3:
                            d_lineChangeRanges.Add(new TextUnitRange() { range = new Range(d_startIndex, d_endIndex), originRange = b_rangeInfoList[i].originRange });
                            break;

                        case -4:
                            d_groupRanges.Add(new TextUnitInfo() { charRange = new Range(d_startIndex, d_endIndex), originRange = b_rangeInfoList[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                            break;

                        default:
                            break;
                    }
                }
            }

            if ((a_infoFlags & InfoFlags.Group) != 0)
            {
                for (int i = d_groupRanges.Count - 1; i >= 1; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (d_groupRanges[i].charRange.startIndex <= d_groupRanges[j].charRange.endIndex && d_groupRanges[i].charRange.startIndex >= d_groupRanges[j].charRange.startIndex)
                        {
                            d_groupRanges.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else
            {
                d_groupRanges.Clear();
            }

            if (a_emptyTag)
            {
                RangeInfo d_emptyTagRangeInfo;

                d_emptyTagRangeInfo.tag = string.Empty;
                d_emptyTagRangeInfo.id = -1;
                d_emptyTagRangeInfo.range = new Range(0, _charCount - 1);
                d_emptyTagRangeInfo.originRange = new Range(-1, _originText.Length);
                d_emptyTagRangeInfo.openingOrder = 0;
                d_emptyTagRangeInfo.openingInterval = 0;
                d_emptyTagRangeInfo.openingIntervalType = IntervalType.None;
                d_emptyTagRangeInfo.closingOrder = -1;
                d_emptyTagRangeInfo.closingInterval = 0;
                d_emptyTagRangeInfo.closingIntervalType = IntervalType.None;

                _rangeInfoList.Add(d_emptyTagRangeInfo);
            }

            _groupCount = d_groupRanges.Count;

            #endregion

            #region <- Step E: Character Count ->

            List<TextUnitInfo> e_characterRanges = new List<TextUnitInfo>();

            if ((a_infoFlags & InfoFlags.Character) != 0)
            {
                for (int i = 0; i < _charCount; i++)
                {
                    if (_charInfo[i].isVisible)
                    {
                        e_characterRanges.Add(new TextUnitInfo() { charRange = new Range(i, i), originRange = new Range(_charInfo[i].originIndex, _charInfo[i].originIndex), characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }
                }

                for (int i = d_characterChangeRanges.Count - 1; i >= 1; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (d_characterChangeRanges[i].range.startIndex <= d_characterChangeRanges[j].range.endIndex && d_characterChangeRanges[i].range.startIndex >= d_characterChangeRanges[j].range.startIndex)
                        {
                            d_characterChangeRanges.RemoveAt(i);
                            break;
                        }
                    }
                }

                int e_changeRangeCount = d_characterChangeRanges.Count;
                int e_changeRangeStartIndex = 0;

                while (e_changeRangeStartIndex < e_changeRangeCount)
                {
                    if (d_characterChangeRanges[e_changeRangeStartIndex].range.endIndex != -1)
                    {
                        break;
                    }

                    e_changeRangeStartIndex++;
                }

                int e_rangesStartIndex = e_characterRanges.Count - 1;
                int e_rangesEndIndex;

                for (int i = e_changeRangeCount - 1; i >= e_changeRangeStartIndex; i--)
                {
                    int e_minIndex = 0;
                    int e_maxIndex = e_rangesStartIndex;

                    e_rangesEndIndex = (e_minIndex + e_maxIndex) / 2;

                    while (e_minIndex != e_maxIndex)
                    {
                        if (d_characterChangeRanges[i].range.endIndex < e_characterRanges[e_rangesEndIndex].charRange.startIndex)
                        {
                            e_maxIndex = e_rangesEndIndex - 1;
                        }
                        else if (d_characterChangeRanges[i].range.endIndex > e_characterRanges[e_rangesEndIndex].charRange.startIndex)
                        {
                            e_minIndex = e_rangesEndIndex;
                        }
                        else
                        {
                            break;
                        }

                        e_rangesEndIndex = (e_minIndex + e_maxIndex + 1) / 2;
                    }

                    e_minIndex = 0;
                    e_maxIndex = e_rangesEndIndex;
                    e_rangesStartIndex = (e_minIndex + e_maxIndex) / 2;

                    while (e_minIndex != e_maxIndex)
                    {
                        if (d_characterChangeRanges[i].range.startIndex < e_characterRanges[e_rangesStartIndex].charRange.endIndex)
                        {
                            e_maxIndex = e_rangesStartIndex;
                        }
                        else if (d_characterChangeRanges[i].range.startIndex > e_characterRanges[e_rangesStartIndex].charRange.endIndex)
                        {
                            e_minIndex = e_rangesStartIndex + 1;
                        }
                        else
                        {
                            break;
                        }

                        e_rangesStartIndex = (e_minIndex + e_maxIndex) / 2;
                    }

                    if (d_characterChangeRanges[i].range.endIndex < e_characterRanges[e_rangesEndIndex].charRange.endIndex)
                    {
                        Range e_charRange = new Range(d_characterChangeRanges[i].range.endIndex + 1, e_characterRanges[e_rangesEndIndex].charRange.endIndex);
                        Range e_originRange = new Range(d_characterChangeRanges[i].originRange.endIndex + 1, e_characterRanges[e_rangesEndIndex].originRange.endIndex);

                        e_characterRanges.Insert(e_rangesEndIndex + 1, new TextUnitInfo() { charRange = e_charRange, originRange = e_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    e_characterRanges.Insert(e_rangesEndIndex + 1, new TextUnitInfo() { charRange = d_characterChangeRanges[i].range, originRange = d_characterChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                    if (d_characterChangeRanges[i].range.startIndex > e_characterRanges[e_rangesStartIndex].charRange.startIndex)
                    {
                        Range e_charRange = new Range(e_characterRanges[e_rangesStartIndex].charRange.startIndex, d_characterChangeRanges[i].range.startIndex - 1);
                        Range e_originRange = new Range(e_characterRanges[e_rangesStartIndex].originRange.startIndex, d_characterChangeRanges[i].originRange.startIndex - 1);

                        e_characterRanges.Insert(e_rangesEndIndex + 1, new TextUnitInfo() { charRange = e_charRange, originRange = e_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    e_characterRanges.RemoveRange(e_rangesStartIndex, e_rangesEndIndex - e_rangesStartIndex + 1);
                }

                for (int i = e_changeRangeStartIndex - 1; i >= 0; i--)
                {
                    e_characterRanges.Insert(0, new TextUnitInfo() { charRange = d_characterChangeRanges[i].range, originRange = d_characterChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                }
            }

            _characterCount = e_characterRanges.Count;

            #endregion

            #region <- Step F: Word Count ->

            List<TextUnitInfo> f_wordRanges = new List<TextUnitInfo>();

            if ((a_infoFlags & InfoFlags.Word) != 0)
            {
                for (int i = 0; i < _charCount; i++)
                {
                    if (_charInfo[i].isVisible)
                    {
                        int f_nextWordBreakIndex = int.MaxValue;

                        foreach (char item in _wordBreaks)
                        {
                            int f_wordBreakIndex = _outputText.IndexOf(item, _charInfo[i].outputIndex);

                            if (f_wordBreakIndex != -1)
                            {
                                f_nextWordBreakIndex = Mathf.Min(f_wordBreakIndex, f_nextWordBreakIndex);
                            }
                        }

                        if (f_nextWordBreakIndex != int.MaxValue)
                        {
                            int f_endInfoIndex = i + 1;

                            while (f_endInfoIndex < _charCount)
                            {
                                if (_charInfo[f_endInfoIndex].outputIndex >= f_nextWordBreakIndex)
                                {
                                    f_endInfoIndex--;

                                    break;
                                }

                                f_endInfoIndex++;
                            }

                            if (f_endInfoIndex == _charCount)
                            {
                                f_endInfoIndex--;
                            }

                            while (f_endInfoIndex > i)
                            {
                                if (_charInfo[f_endInfoIndex].isVisible)
                                {
                                    break;
                                }

                                f_endInfoIndex--;
                            }

                            f_wordRanges.Add(new TextUnitInfo() { charRange = new Range(i, f_endInfoIndex), originRange = new Range(_charInfo[i].originIndex, _charInfo[f_endInfoIndex].originIndex), characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                            i = f_endInfoIndex;
                        }
                        else
                        {
                            int f_endIndex = _charCount - 1;

                            while (f_endIndex > i)
                            {
                                if (_charInfo[f_endIndex].isVisible)
                                {
                                    break;
                                }

                                f_endIndex--;
                            }

                            f_wordRanges.Add(new TextUnitInfo() { charRange = new Range(i, f_endIndex), originRange = new Range(_charInfo[i].originIndex, _charInfo[f_endIndex].originIndex), characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                            break;
                        }
                    }
                }

                for (int i = d_wordChangeRanges.Count - 1; i >= 1; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (d_wordChangeRanges[i].range.startIndex <= d_wordChangeRanges[j].range.endIndex && d_wordChangeRanges[i].range.startIndex >= d_wordChangeRanges[j].range.startIndex)
                        {
                            d_wordChangeRanges.RemoveAt(i);
                            break;
                        }
                    }
                }

                int f_changeRangeCount = d_wordChangeRanges.Count;
                int f_changeRangeStartIndex = 0;

                while (f_changeRangeStartIndex < f_changeRangeCount)
                {
                    if (d_wordChangeRanges[f_changeRangeStartIndex].range.endIndex != -1)
                    {
                        break;
                    }

                    f_changeRangeStartIndex++;
                }

                int f_rangesStartIndex = f_wordRanges.Count - 1;
                int f_rangesEndIndex;

                for (int i = f_changeRangeCount - 1; i >= f_changeRangeStartIndex; i--)
                {
                    int f_minIndex = 0;
                    int f_maxIndex = f_rangesStartIndex;

                    f_rangesEndIndex = (f_minIndex + f_maxIndex) / 2;

                    while (f_minIndex != f_maxIndex)
                    {
                        if (d_wordChangeRanges[i].range.endIndex < f_wordRanges[f_rangesEndIndex].charRange.startIndex)
                        {
                            f_maxIndex = f_rangesEndIndex - 1;
                        }
                        else if (d_wordChangeRanges[i].range.endIndex > f_wordRanges[f_rangesEndIndex].charRange.startIndex)
                        {
                            f_minIndex = f_rangesEndIndex;
                        }
                        else
                        {
                            break;
                        }

                        f_rangesEndIndex = (f_minIndex + f_maxIndex + 1) / 2;
                    }

                    f_minIndex = 0;
                    f_maxIndex = f_rangesEndIndex;
                    f_rangesStartIndex = (f_minIndex + f_maxIndex) / 2;

                    while (f_minIndex != f_maxIndex)
                    {
                        if (d_wordChangeRanges[i].range.startIndex < f_wordRanges[f_rangesStartIndex].charRange.endIndex)
                        {
                            f_maxIndex = f_rangesStartIndex;
                        }
                        else if (d_wordChangeRanges[i].range.startIndex > f_wordRanges[f_rangesStartIndex].charRange.endIndex)
                        {
                            f_minIndex = f_rangesStartIndex + 1;
                        }
                        else
                        {
                            break;
                        }

                        f_rangesStartIndex = (f_minIndex + f_maxIndex) / 2;
                    }

                    if (d_wordChangeRanges[i].range.endIndex < f_wordRanges[f_rangesEndIndex].charRange.endIndex)
                    {
                        Range f_charRange = new Range(d_wordChangeRanges[i].range.endIndex + 1, f_wordRanges[f_rangesEndIndex].charRange.endIndex);
                        Range f_originRange = new Range(d_wordChangeRanges[i].originRange.endIndex + 1, f_wordRanges[f_rangesEndIndex].originRange.endIndex);

                        f_wordRanges.Insert(f_rangesEndIndex + 1, new TextUnitInfo() { charRange = f_charRange, originRange = f_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    f_wordRanges.Insert(f_rangesEndIndex + 1, new TextUnitInfo() { charRange = d_wordChangeRanges[i].range, originRange = d_wordChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                    if (d_wordChangeRanges[i].range.startIndex > f_wordRanges[f_rangesStartIndex].charRange.startIndex)
                    {
                        Range f_charRange = new Range(f_wordRanges[f_rangesStartIndex].charRange.startIndex, d_wordChangeRanges[i].range.startIndex - 1);
                        Range f_originRange = new Range(f_wordRanges[f_rangesStartIndex].originRange.startIndex, d_wordChangeRanges[i].originRange.startIndex - 1);

                        f_wordRanges.Insert(f_rangesEndIndex + 1, new TextUnitInfo() { charRange = f_charRange, originRange = f_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    f_wordRanges.RemoveRange(f_rangesStartIndex, f_rangesEndIndex - f_rangesStartIndex + 1);
                }

                for (int i = f_changeRangeStartIndex - 1; i >= 0; i--)
                {
                    f_wordRanges.Insert(0, new TextUnitInfo() { charRange = d_wordChangeRanges[i].range, originRange = d_wordChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                }
            }

            _wordCount = f_wordRanges.Count;

            #endregion

            #region <- Step G: Line Count ->

            List<TextUnitInfo> g_lineRanges = new List<TextUnitInfo>();

            if ((a_infoFlags & InfoFlags.Line) != 0)
            {
                for (int i = 0; i < _charCount; i++)
                {
                    int g_nextLineBreakIndex = int.MaxValue;

                    foreach (char item in _lineBreaks)
                    {
                        int g_lineBreakIndex = _outputText.IndexOf(item, _charInfo[i].outputIndex);

                        if (g_lineBreakIndex != -1)
                        {
                            g_nextLineBreakIndex = Mathf.Min(g_lineBreakIndex, g_nextLineBreakIndex);
                        }
                    }

                    if (g_nextLineBreakIndex != int.MaxValue)
                    {
                        int g_endInfoIndex = i + 1;

                        while (g_endInfoIndex < _charCount)
                        {
                            if (_charInfo[g_endInfoIndex].outputIndex > g_nextLineBreakIndex)
                            {
                                g_endInfoIndex--;

                                break;
                            }

                            g_endInfoIndex++;
                        }

                        if (g_endInfoIndex == _charCount)
                        {
                            g_endInfoIndex--;
                        }

                        g_lineRanges.Add(new TextUnitInfo() { charRange = new Range(i, g_endInfoIndex), originRange = new Range(_charInfo[i].originIndex, _charInfo[g_endInfoIndex].originIndex), characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                        i = g_endInfoIndex;
                    }
                    else
                    {
                        g_lineRanges.Add(new TextUnitInfo() { charRange = new Range(i, _charCount - 1), originRange = new Range(_charInfo[i].originIndex, _charInfo[_charCount - 1].originIndex), characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                        break;
                    }
                }

                for (int i = d_lineChangeRanges.Count - 1; i >= 1; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (d_lineChangeRanges[i].range.startIndex <= d_lineChangeRanges[j].range.endIndex && d_lineChangeRanges[i].range.startIndex >= d_lineChangeRanges[j].range.startIndex)
                        {
                            d_lineChangeRanges.RemoveAt(i);
                            break;
                        }
                    }
                }

                int g_changeRangeCount = d_lineChangeRanges.Count;
                int g_changeRangeStartIndex = 0;

                while (g_changeRangeStartIndex < d_lineChangeRanges.Count)
                {
                    if (d_lineChangeRanges[g_changeRangeStartIndex].range.endIndex != -1)
                    {
                        break;
                    }

                    g_changeRangeStartIndex++;
                }

                int g_rangesStartIndex = g_lineRanges.Count - 1;
                int g_rangesEndIndex;

                for (int i = g_changeRangeCount - 1; i >= g_changeRangeStartIndex; i--)
                {
                    int g_minIndex = 0;
                    int g_maxIndex = g_rangesStartIndex;

                    g_rangesEndIndex = (g_minIndex + g_maxIndex) / 2;

                    while (g_minIndex != g_maxIndex)
                    {
                        if (d_lineChangeRanges[i].range.endIndex < g_lineRanges[g_rangesEndIndex].charRange.startIndex)
                        {
                            g_maxIndex = g_rangesEndIndex - 1;
                        }
                        else if (d_lineChangeRanges[i].range.endIndex > g_lineRanges[g_rangesEndIndex].charRange.startIndex)
                        {
                            g_minIndex = g_rangesEndIndex;
                        }
                        else
                        {
                            break;
                        }

                        g_rangesEndIndex = (g_minIndex + g_maxIndex + 1) / 2;
                    }

                    g_minIndex = 0;
                    g_maxIndex = g_rangesEndIndex;
                    g_rangesStartIndex = (g_minIndex + g_maxIndex) / 2;

                    while (g_minIndex != g_maxIndex)
                    {
                        if (d_lineChangeRanges[i].range.startIndex < g_lineRanges[g_rangesStartIndex].charRange.endIndex)
                        {
                            g_maxIndex = g_rangesStartIndex;
                        }
                        else if (d_lineChangeRanges[i].range.startIndex > g_lineRanges[g_rangesStartIndex].charRange.endIndex)
                        {
                            g_minIndex = g_rangesStartIndex + 1;
                        }
                        else
                        {
                            break;
                        }

                        g_rangesStartIndex = (g_minIndex + g_maxIndex) / 2;
                    }

                    if (d_lineChangeRanges[i].range.endIndex < g_lineRanges[g_rangesEndIndex].charRange.endIndex)
                    {
                        Range g_charRange = new Range(d_lineChangeRanges[i].range.endIndex + 1, g_lineRanges[g_rangesEndIndex].charRange.endIndex);
                        Range g_originRange = new Range(d_lineChangeRanges[i].originRange.endIndex + 1, g_lineRanges[g_rangesEndIndex].originRange.endIndex);

                        g_lineRanges.Insert(g_rangesEndIndex + 1, new TextUnitInfo() { charRange = g_charRange, originRange = g_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    g_lineRanges.Insert(g_rangesEndIndex + 1, new TextUnitInfo() { charRange = d_lineChangeRanges[i].range, originRange = d_lineChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });

                    if (d_lineChangeRanges[i].range.startIndex > g_lineRanges[g_rangesStartIndex].charRange.startIndex)
                    {
                        Range g_charRange = new Range(g_lineRanges[g_rangesStartIndex].charRange.startIndex, d_lineChangeRanges[i].range.startIndex - 1);
                        Range g_originRange = new Range(g_lineRanges[g_rangesStartIndex].originRange.startIndex, d_lineChangeRanges[i].originRange.startIndex - 1);

                        g_lineRanges.Insert(g_rangesEndIndex + 1, new TextUnitInfo() { charRange = g_charRange, originRange = g_originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                    }

                    g_lineRanges.RemoveRange(g_rangesStartIndex, g_rangesEndIndex - g_rangesStartIndex + 1);
                }

                for (int i = g_changeRangeStartIndex - 1; i >= 0; i--)
                {
                    g_lineRanges.Insert(0, new TextUnitInfo() { charRange = d_lineChangeRanges[i].range, originRange = d_lineChangeRanges[i].originRange, characterRange = Range.empty, wordRange = Range.empty, lineRange = Range.empty, groupRange = Range.empty });
                }
            }

            _lineCount = g_lineRanges.Count;

            #endregion

            #region <- Step H: Char Units ->

            TextUnitInfo[] h_chars = new TextUnitInfo[_charCount];

            int h_characterIndex = 0;
            int h_wordIndex = 0;
            int h_lineIndex = 0;
            int h_groupIndex = 0;

            for (int i = 0; i < _charCount; i++)
            {
                h_chars[i].charRange.startIndex = i;
                h_chars[i].charRange.endIndex = i;
                h_chars[i].charRange.count = 1;

                h_chars[i].characterRange.endIndex = -1;
                h_chars[i].wordRange.endIndex = -1;
                h_chars[i].lineRange.endIndex = -1;
                h_chars[i].groupRange.endIndex = -1;

                while (h_characterIndex < _characterCount)
                {
                    if (i < e_characterRanges[h_characterIndex].charRange.startIndex)
                    {
                        h_chars[i].characterRange.startIndex = h_characterIndex;
                        h_chars[i].characterRange.endIndex = h_characterIndex - 1;
                        h_chars[i].characterRange.count = 0;

                        break;
                    }
                    else if (i == e_characterRanges[h_characterIndex].charRange.startIndex)
                    {
                        h_chars[i].characterRange.startIndex = h_characterIndex;

                        while (h_characterIndex + 1 < _characterCount)
                        {
                            if (i != e_characterRanges[h_characterIndex + 1].charRange.startIndex)
                            {
                                break;
                            }

                            h_characterIndex++;
                        }

                        h_chars[i].characterRange.endIndex = h_characterIndex;
                        h_chars[i].characterRange.count = h_chars[i].characterRange.endIndex - h_chars[i].characterRange.startIndex + 1;

                        break;
                    }
                    else
                    {
                        if (i > e_characterRanges[h_characterIndex].charRange.endIndex)
                        {
                            h_characterIndex++;
                        }
                        else
                        {
                            h_chars[i].characterRange.startIndex = h_characterIndex;
                            h_chars[i].characterRange.endIndex = h_characterIndex;
                            h_chars[i].characterRange.count = 1;

                            break;
                        }
                    }
                }

                while (h_wordIndex < _wordCount)
                {
                    if (i < f_wordRanges[h_wordIndex].charRange.startIndex)
                    {
                        h_chars[i].wordRange.startIndex = h_wordIndex;
                        h_chars[i].wordRange.endIndex = h_wordIndex - 1;
                        h_chars[i].wordRange.count = 0;

                        break;
                    }
                    else if (i == f_wordRanges[h_wordIndex].charRange.startIndex)
                    {
                        h_chars[i].wordRange.startIndex = h_wordIndex;

                        while (h_wordIndex + 1 < _wordCount)
                        {
                            if (i != f_wordRanges[h_wordIndex + 1].charRange.startIndex)
                            {
                                break;
                            }

                            h_wordIndex++;
                        }

                        h_chars[i].wordRange.endIndex = h_wordIndex;
                        h_chars[i].wordRange.count = h_chars[i].wordRange.endIndex - h_chars[i].wordRange.startIndex + 1;

                        break;
                    }
                    else
                    {
                        if (i > f_wordRanges[h_wordIndex].charRange.endIndex)
                        {
                            h_wordIndex++;
                        }
                        else
                        {
                            h_chars[i].wordRange.startIndex = h_wordIndex;
                            h_chars[i].wordRange.endIndex = h_wordIndex;
                            h_chars[i].wordRange.count = 1;

                            break;
                        }
                    }
                }

                while (h_lineIndex < _lineCount)
                {
                    if (i < g_lineRanges[h_lineIndex].charRange.startIndex)
                    {
                        h_chars[i].lineRange.startIndex = h_lineIndex;
                        h_chars[i].lineRange.endIndex = h_lineIndex - 1;
                        h_chars[i].lineRange.count = 0;

                        break;
                    }
                    else if (i == g_lineRanges[h_lineIndex].charRange.startIndex)
                    {
                        h_chars[i].lineRange.startIndex = h_lineIndex;

                        while (h_lineIndex + 1 < _lineCount)
                        {
                            if (i != g_lineRanges[h_lineIndex + 1].charRange.startIndex)
                            {
                                break;
                            }

                            h_lineIndex++;
                        }

                        h_chars[i].lineRange.endIndex = h_lineIndex;
                        h_chars[i].lineRange.count = h_chars[i].lineRange.endIndex - h_chars[i].lineRange.startIndex + 1;

                        break;
                    }
                    else
                    {
                        if (i > g_lineRanges[h_lineIndex].charRange.endIndex)
                        {
                            h_lineIndex++;
                        }
                        else
                        {
                            h_chars[i].lineRange.startIndex = h_lineIndex;
                            h_chars[i].lineRange.endIndex = h_lineIndex;
                            h_chars[i].lineRange.count = 1;

                            break;
                        }
                    }
                }

                while (h_groupIndex < _groupCount)
                {
                    if (i < d_groupRanges[h_groupIndex].charRange.startIndex)
                    {
                        h_chars[i].groupRange.startIndex = h_groupIndex;
                        h_chars[i].groupRange.endIndex = h_groupIndex - 1;
                        h_chars[i].groupRange.count = 0;

                        break;
                    }
                    else if (i == d_groupRanges[h_groupIndex].charRange.startIndex)
                    {
                        h_chars[i].groupRange.startIndex = h_groupIndex;

                        while (h_groupIndex + 1 < _groupCount)
                        {
                            if (i != d_groupRanges[h_groupIndex + 1].charRange.startIndex)
                            {
                                break;
                            }

                            h_groupIndex++;
                        }

                        h_chars[i].groupRange.endIndex = h_groupIndex;
                        h_chars[i].groupRange.count = h_chars[i].groupRange.endIndex - h_chars[i].groupRange.startIndex + 1;

                        break;
                    }
                    else
                    {
                        if (i > d_groupRanges[h_groupIndex].charRange.endIndex)
                        {
                            h_groupIndex++;
                        }
                        else
                        {
                            h_chars[i].groupRange.startIndex = h_groupIndex;
                            h_chars[i].groupRange.endIndex = h_groupIndex;
                            h_chars[i].groupRange.count = 1;

                            break;
                        }
                    }
                }
            }

            _chars = h_chars;

            #endregion

            #region <- Step I: Other Units ->

            TextUnitInfo[] i_characters = e_characterRanges.ToArray();
            TextUnitInfo[] i_words = f_wordRanges.ToArray();
            TextUnitInfo[] i_lines = g_lineRanges.ToArray();
            TextUnitInfo[] i_groups = d_groupRanges.ToArray();

            if (_charCount > 0)
            {
                for (int i = 0; i < _characterCount; i++)
                {
                    if (i_characters[i].charRange.startIndex < _charCount)
                    {
                        i_characters[i].characterRange.startIndex = _chars[i_characters[i].charRange.startIndex].characterRange.startIndex;
                        i_characters[i].wordRange.startIndex = _chars[i_characters[i].charRange.startIndex].wordRange.startIndex;
                        i_characters[i].lineRange.startIndex = _chars[i_characters[i].charRange.startIndex].lineRange.startIndex;
                        i_characters[i].groupRange.startIndex = _chars[i_characters[i].charRange.startIndex].groupRange.startIndex;
                    }
                    else
                    {
                        i_characters[i].characterRange.startIndex = _chars[_charCount - 1].characterRange.endIndex + 1;
                        i_characters[i].wordRange.startIndex = _chars[_charCount - 1].wordRange.endIndex + 1;
                        i_characters[i].lineRange.startIndex = _chars[_charCount - 1].lineRange.endIndex + 1;
                        i_characters[i].groupRange.startIndex = _chars[_charCount - 1].groupRange.endIndex + 1;
                    }

                    if (i_characters[i].charRange.endIndex > -1)
                    {
                        i_characters[i].characterRange.endIndex = _chars[i_characters[i].charRange.endIndex].characterRange.endIndex;
                        i_characters[i].wordRange.endIndex = _chars[i_characters[i].charRange.endIndex].wordRange.endIndex;
                        i_characters[i].lineRange.endIndex = _chars[i_characters[i].charRange.endIndex].lineRange.endIndex;
                        i_characters[i].groupRange.endIndex = _chars[i_characters[i].charRange.endIndex].groupRange.endIndex;
                    }
                    else
                    {
                        i_characters[i].characterRange.endIndex = -1;
                        i_characters[i].wordRange.endIndex = -1;
                        i_characters[i].lineRange.endIndex = -1;
                        i_characters[i].groupRange.endIndex = -1;
                    }

                    i_characters[i].characterRange.count = i_characters[i].characterRange.endIndex - i_characters[i].characterRange.startIndex + 1;
                    i_characters[i].wordRange.count = i_characters[i].wordRange.endIndex - i_characters[i].wordRange.startIndex + 1;
                    i_characters[i].lineRange.count = i_characters[i].lineRange.endIndex - i_characters[i].lineRange.startIndex + 1;
                    i_characters[i].groupRange.count = i_characters[i].groupRange.endIndex - i_characters[i].groupRange.startIndex + 1;
                }

                for (int i = 0; i < _wordCount; i++)
                {
                    if (i_words[i].charRange.startIndex < _charCount)
                    {
                        i_words[i].characterRange.startIndex = _chars[i_words[i].charRange.startIndex].characterRange.startIndex;
                        i_words[i].wordRange.startIndex = _chars[i_words[i].charRange.startIndex].wordRange.startIndex;
                        i_words[i].lineRange.startIndex = _chars[i_words[i].charRange.startIndex].lineRange.startIndex;
                        i_words[i].groupRange.startIndex = _chars[i_words[i].charRange.startIndex].groupRange.startIndex;
                    }
                    else
                    {
                        i_words[i].characterRange.startIndex = _chars[_charCount - 1].characterRange.endIndex + 1;
                        i_words[i].wordRange.startIndex = _chars[_charCount - 1].wordRange.endIndex + 1;
                        i_words[i].lineRange.startIndex = _chars[_charCount - 1].lineRange.endIndex + 1;
                        i_words[i].groupRange.startIndex = _chars[_charCount - 1].groupRange.endIndex + 1;
                    }

                    if (i_words[i].charRange.endIndex > -1)
                    {
                        i_words[i].characterRange.endIndex = _chars[i_words[i].charRange.endIndex].characterRange.endIndex;
                        i_words[i].wordRange.endIndex = _chars[i_words[i].charRange.endIndex].wordRange.endIndex;
                        i_words[i].lineRange.endIndex = _chars[i_words[i].charRange.endIndex].lineRange.endIndex;
                        i_words[i].groupRange.endIndex = _chars[i_words[i].charRange.endIndex].groupRange.endIndex;
                    }
                    else
                    {
                        i_words[i].characterRange.endIndex = -1;
                        i_words[i].wordRange.endIndex = -1;
                        i_words[i].lineRange.endIndex = -1;
                        i_words[i].groupRange.endIndex = -1;
                    }

                    i_words[i].characterRange.count = i_words[i].characterRange.endIndex - i_words[i].characterRange.startIndex + 1;
                    i_words[i].wordRange.count = i_words[i].wordRange.endIndex - i_words[i].wordRange.startIndex + 1;
                    i_words[i].lineRange.count = i_words[i].lineRange.endIndex - i_words[i].lineRange.startIndex + 1;
                    i_words[i].groupRange.count = i_words[i].groupRange.endIndex - i_words[i].groupRange.startIndex + 1;
                }

                for (int i = 0; i < _lineCount; i++)
                {
                    if (i_lines[i].charRange.startIndex < _charCount)
                    {
                        i_lines[i].characterRange.startIndex = _chars[i_lines[i].charRange.startIndex].characterRange.startIndex;
                        i_lines[i].wordRange.startIndex = _chars[i_lines[i].charRange.startIndex].wordRange.startIndex;
                        i_lines[i].lineRange.startIndex = _chars[i_lines[i].charRange.startIndex].lineRange.startIndex;
                        i_lines[i].groupRange.startIndex = _chars[i_lines[i].charRange.startIndex].groupRange.startIndex;
                    }
                    else
                    {
                        i_lines[i].characterRange.startIndex = _chars[_charCount - 1].characterRange.endIndex + 1;
                        i_lines[i].wordRange.startIndex = _chars[_charCount - 1].wordRange.endIndex + 1;
                        i_lines[i].lineRange.startIndex = _chars[_charCount - 1].lineRange.endIndex + 1;
                        i_lines[i].groupRange.startIndex = _chars[_charCount - 1].groupRange.endIndex + 1;
                    }

                    if (i_lines[i].charRange.endIndex > -1)
                    {
                        i_lines[i].characterRange.endIndex = _chars[i_lines[i].charRange.endIndex].characterRange.endIndex;
                        i_lines[i].wordRange.endIndex = _chars[i_lines[i].charRange.endIndex].wordRange.endIndex;
                        i_lines[i].lineRange.endIndex = _chars[i_lines[i].charRange.endIndex].lineRange.endIndex;
                        i_lines[i].groupRange.endIndex = _chars[i_lines[i].charRange.endIndex].groupRange.endIndex;
                    }
                    else
                    {
                        i_lines[i].characterRange.endIndex = -1;
                        i_lines[i].wordRange.endIndex = -1;
                        i_lines[i].lineRange.endIndex = -1;
                        i_lines[i].groupRange.endIndex = -1;
                    }

                    i_lines[i].characterRange.count = i_lines[i].characterRange.endIndex - i_lines[i].characterRange.startIndex + 1;
                    i_lines[i].wordRange.count = i_lines[i].wordRange.endIndex - i_lines[i].wordRange.startIndex + 1;
                    i_lines[i].lineRange.count = i_lines[i].lineRange.endIndex - i_lines[i].lineRange.startIndex + 1;
                    i_lines[i].groupRange.count = i_lines[i].groupRange.endIndex - i_lines[i].groupRange.startIndex + 1;
                }

                for (int i = 0; i < _groupCount; i++)
                {
                    if (i_groups[i].charRange.startIndex < _charCount)
                    {
                        i_groups[i].characterRange.startIndex = _chars[i_groups[i].charRange.startIndex].characterRange.startIndex;
                        i_groups[i].wordRange.startIndex = _chars[i_groups[i].charRange.startIndex].wordRange.startIndex;
                        i_groups[i].lineRange.startIndex = _chars[i_groups[i].charRange.startIndex].lineRange.startIndex;
                        i_groups[i].groupRange.startIndex = _chars[i_groups[i].charRange.startIndex].groupRange.startIndex;
                    }
                    else
                    {
                        i_groups[i].characterRange.startIndex = _chars[_charCount - 1].characterRange.endIndex + 1;
                        i_groups[i].wordRange.startIndex = _chars[_charCount - 1].wordRange.endIndex + 1;
                        i_groups[i].lineRange.startIndex = _chars[_charCount - 1].lineRange.endIndex + 1;
                        i_groups[i].groupRange.startIndex = _chars[_charCount - 1].groupRange.endIndex + 1;
                    }

                    if (i_groups[i].charRange.endIndex > -1)
                    {
                        i_groups[i].characterRange.endIndex = _chars[i_groups[i].charRange.endIndex].characterRange.endIndex;
                        i_groups[i].wordRange.endIndex = _chars[i_groups[i].charRange.endIndex].wordRange.endIndex;
                        i_groups[i].lineRange.endIndex = _chars[i_groups[i].charRange.endIndex].lineRange.endIndex;
                        i_groups[i].groupRange.endIndex = _chars[i_groups[i].charRange.endIndex].groupRange.endIndex;
                    }
                    else
                    {
                        i_groups[i].characterRange.endIndex = -1;
                        i_groups[i].wordRange.endIndex = -1;
                        i_groups[i].lineRange.endIndex = -1;
                        i_groups[i].groupRange.endIndex = -1;
                    }

                    i_groups[i].characterRange.count = i_groups[i].characterRange.endIndex - i_groups[i].characterRange.startIndex + 1;
                    i_groups[i].wordRange.count = i_groups[i].wordRange.endIndex - i_groups[i].wordRange.startIndex + 1;
                    i_groups[i].lineRange.count = i_groups[i].lineRange.endIndex - i_groups[i].lineRange.startIndex + 1;
                    i_groups[i].groupRange.count = i_groups[i].groupRange.endIndex - i_groups[i].groupRange.startIndex + 1;
                }
            }

            _characters = i_characters;
            _words = i_words;
            _lines = i_lines;
            _groups = i_groups;

            #endregion
        }

        public static StringInfo ParseText(string text, Dictionary<string, int> tagDictionary, Settings settings, out List<TagInfo> tagInfoList)
        {
            tagInfoList = new List<TagInfo>();

            if (string.IsNullOrEmpty(text))
            {
                return StringInfo.GetStringInfo(string.Empty);
            }

            char openingChar;
            char closingChar;

            switch (settings.tagSymbols)
            {
                case TagSymbols.AngleBrackets:
                    openingChar = '<';
                    closingChar = '>';
                    break;

                case TagSymbols.RoundBrackets:
                    openingChar = '(';
                    closingChar = ')';
                    break;

                case TagSymbols.SquareBrackets:
                    openingChar = '[';
                    closingChar = ']';
                    break;

                case TagSymbols.CurlyBrackets:
                    openingChar = '{';
                    closingChar = '}';
                    break;

                default:
                    openingChar = '<';
                    closingChar = '>';
                    break;
            }

            char markerChar;

            switch (settings.markerSymbols)
            {
                case MarkerSymbols.Slashes:
                    markerChar = '/';
                    break;

                case MarkerSymbols.Backslashes:
                    markerChar = '\\';
                    break;

                case MarkerSymbols.VerticalBars:
                    markerChar = '|';
                    break;

                default:
                    markerChar = '/';
                    break;
            }

            StringBuilder parsedText = new StringBuilder();
            List<int> parsedToOriginMappingList = new List<int>();
            int nextOpeningCharIndex = text.IndexOf(openingChar);

            for (int i = 0; i < text.Length; i++)
            {
                int openingCharIndex = nextOpeningCharIndex;

                if (openingCharIndex == -1)
                {
                    for (int j = i; j < text.Length; j++)
                    {
                        parsedToOriginMappingList.Add(j);
                    }

                    parsedText.Append(text.Substring(i, text.Length - i));

                    break;
                }
                else
                {
                    int closingCharIndex = text.IndexOf(closingChar, openingCharIndex);

                    if (closingCharIndex == -1)
                    {
                        for (int j = i; j < text.Length; j++)
                        {
                            parsedToOriginMappingList.Add(j);
                        }

                        parsedText.Append(text.Substring(i, text.Length - i));

                        break;
                    }
                    else
                    {
                        while (true)
                        {
                            nextOpeningCharIndex = text.IndexOf(openingChar, nextOpeningCharIndex + 1);

                            if (nextOpeningCharIndex == -1 || nextOpeningCharIndex >= closingCharIndex)
                            {
                                break;
                            }

                            openingCharIndex = nextOpeningCharIndex;
                        }

                        if (i != openingCharIndex)
                        {
                            for (int j = i; j < openingCharIndex; j++)
                            {
                                parsedToOriginMappingList.Add(j);
                            }

                            parsedText.Append(text.Substring(i, openingCharIndex - i));
                        }

                        int typeID;
                        int openingTextIndex;
                        int closingTextIndex;

                        if (text[openingCharIndex + 1] == markerChar)
                        {
                            if (text[closingCharIndex - 1] == markerChar)
                            {
                                typeID = 3;
                                openingTextIndex = openingCharIndex + 2;
                                closingTextIndex = closingCharIndex - 2;
                            }

                            else
                            {
                                typeID = 1;
                                openingTextIndex = openingCharIndex + 2;
                                closingTextIndex = closingCharIndex - 1;
                            }
                        }
                        else
                        {
                            if (text[closingCharIndex - 1] == markerChar)
                            {
                                typeID = 2;
                                openingTextIndex = openingCharIndex + 1;
                                closingTextIndex = closingCharIndex - 2;
                            }

                            else
                            {
                                typeID = 0;
                                openingTextIndex = openingCharIndex + 1;
                                closingTextIndex = closingCharIndex - 1;
                            }
                        }

                        if (typeID != 3)
                        {
                            int markerCharIndex = text.IndexOf(markerChar, openingTextIndex, closingTextIndex - openingTextIndex + 1);
                            string tagName = text.Substring(openingTextIndex, markerCharIndex == -1 ? closingTextIndex - openingTextIndex + 1 : markerCharIndex - openingTextIndex);

                            if (_systemTagDictionary.ContainsKey(tagName))
                            {
                                if (markerCharIndex == -1)
                                {
                                    TagInfo tagInfo;

                                    tagInfo.tag = tagName;
                                    tagInfo.type = typeID;
                                    tagInfo.id = _systemTagDictionary[tagName];
                                    tagInfo.index = parsedText.Length;
                                    tagInfo.originRange = new Range(openingCharIndex, closingCharIndex);
                                    tagInfo.order = 0;
                                    tagInfo.haveOrder = false;
                                    tagInfo.interval = 0;
                                    tagInfo.intervalType = IntervalType.None;

                                    tagInfoList.Add(tagInfo);
                                }
                                else
                                {
                                    for (int j = openingCharIndex; j <= closingCharIndex; j++)
                                    {
                                        parsedToOriginMappingList.Add(j);
                                    }

                                    parsedText.Append(text.Substring(openingCharIndex, closingCharIndex - openingCharIndex + 1));
                                }
                            }
                            else if (tagDictionary.ContainsKey(tagName))
                            {
                                if (markerCharIndex == -1)
                                {
                                    TagInfo tagInfo;

                                    tagInfo.tag = tagName;
                                    tagInfo.type = typeID;
                                    tagInfo.id = tagDictionary[tagName];
                                    tagInfo.index = parsedText.Length;
                                    tagInfo.originRange = new Range(openingCharIndex, closingCharIndex);
                                    tagInfo.order = 0;
                                    tagInfo.haveOrder = false;
                                    tagInfo.interval = 0;
                                    tagInfo.intervalType = IntervalType.None;

                                    tagInfoList.Add(tagInfo);
                                }
                                else
                                {
                                    bool isValidTag = true;

                                    int nextMarkerCharIndex = text.IndexOf(markerChar, markerCharIndex + 1, closingTextIndex - markerCharIndex);

                                    TagInfo tagInfo;

                                    tagInfo.order = 0;
                                    tagInfo.haveOrder = false;
                                    tagInfo.interval = 0;
                                    tagInfo.intervalType = IntervalType.None;

                                    if (nextMarkerCharIndex == -1)
                                    {
                                        switch (text[markerCharIndex + 1])
                                        {
                                            case '#':
                                                if (int.TryParse(text.Substring(markerCharIndex + 2, closingTextIndex - markerCharIndex - 1), out tagInfo.order))
                                                {
                                                    tagInfo.haveOrder = true;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '+':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, closingTextIndex - markerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Add;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '=':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, closingTextIndex - markerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Equal;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '?':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, closingTextIndex - markerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Replace;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '!':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, closingTextIndex - markerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Cover;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            default:
                                                if (float.TryParse(text.Substring(markerCharIndex + 1, closingTextIndex - markerCharIndex), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Add;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (text[markerCharIndex + 1])
                                        {
                                            case '#':
                                                if (int.TryParse(text.Substring(markerCharIndex + 2, nextMarkerCharIndex - markerCharIndex - 2), out tagInfo.order))
                                                {
                                                    tagInfo.haveOrder = true;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '+':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, nextMarkerCharIndex - markerCharIndex - 2), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Add;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '=':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, nextMarkerCharIndex - markerCharIndex - 2), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Equal;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '?':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, nextMarkerCharIndex - markerCharIndex - 2), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Replace;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '!':
                                                if (float.TryParse(text.Substring(markerCharIndex + 2, nextMarkerCharIndex - markerCharIndex - 2), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Cover;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            default:
                                                if (float.TryParse(text.Substring(markerCharIndex + 1, nextMarkerCharIndex - markerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    tagInfo.intervalType = IntervalType.Add;
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;
                                        }

                                        switch (text[nextMarkerCharIndex + 1])
                                        {
                                            case '#':
                                                if (int.TryParse(text.Substring(nextMarkerCharIndex + 2, closingTextIndex - nextMarkerCharIndex - 1), out tagInfo.order))
                                                {
                                                    if (!tagInfo.haveOrder)
                                                    {
                                                        tagInfo.haveOrder = true;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '+':
                                                if (float.TryParse(text.Substring(nextMarkerCharIndex + 2, closingTextIndex - nextMarkerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    if (tagInfo.intervalType == IntervalType.None)
                                                    {
                                                        tagInfo.intervalType = IntervalType.Add;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '=':
                                                if (float.TryParse(text.Substring(nextMarkerCharIndex + 2, closingTextIndex - nextMarkerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    if (tagInfo.intervalType == IntervalType.None)
                                                    {
                                                        tagInfo.intervalType = IntervalType.Equal;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '?':
                                                if (float.TryParse(text.Substring(nextMarkerCharIndex + 2, closingTextIndex - nextMarkerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    if (tagInfo.intervalType == IntervalType.None)
                                                    {
                                                        tagInfo.intervalType = IntervalType.Replace;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            case '!':
                                                if (float.TryParse(text.Substring(nextMarkerCharIndex + 2, closingTextIndex - nextMarkerCharIndex - 1), out tagInfo.interval))
                                                {
                                                    if (tagInfo.intervalType == IntervalType.None)
                                                    {
                                                        tagInfo.intervalType = IntervalType.Cover;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;

                                            default:
                                                if (float.TryParse(text.Substring(nextMarkerCharIndex + 1, closingTextIndex - nextMarkerCharIndex), out tagInfo.interval))
                                                {
                                                    if (tagInfo.intervalType == IntervalType.None)
                                                    {
                                                        tagInfo.intervalType = IntervalType.Add;
                                                    }
                                                    else
                                                    {
                                                        isValidTag = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValidTag = false;
                                                }
                                                break;
                                        }
                                    }

                                    if (isValidTag)
                                    {
                                        tagInfo.tag = tagName;
                                        tagInfo.type = typeID;
                                        tagInfo.id = tagDictionary[tagName];
                                        tagInfo.index = parsedText.Length;
                                        tagInfo.originRange = new Range(openingCharIndex, closingCharIndex);

                                        tagInfoList.Add(tagInfo);
                                    }
                                    else
                                    {
                                        for (int j = openingCharIndex; j <= closingCharIndex; j++)
                                        {
                                            parsedToOriginMappingList.Add(j);
                                        }

                                        parsedText.Append(text.Substring(openingCharIndex, closingCharIndex - openingCharIndex + 1));
                                    }
                                }
                            }
                            else
                            {
                                for (int j = openingCharIndex; j <= closingCharIndex; j++)
                                {
                                    parsedToOriginMappingList.Add(j);
                                }

                                parsedText.Append(text.Substring(openingCharIndex, closingCharIndex - openingCharIndex + 1));
                            }
                        }
                        else
                        {
                            if (openingCharIndex == closingCharIndex - 2)
                            {
                                TagInfo tagInfo;

                                tagInfo.tag = string.Empty;
                                tagInfo.type = typeID;
                                tagInfo.id = 0;
                                tagInfo.index = parsedText.Length;
                                tagInfo.originRange = new Range(openingCharIndex, closingCharIndex);
                                tagInfo.order = 0;
                                tagInfo.haveOrder = false;
                                tagInfo.interval = 0;
                                tagInfo.intervalType = IntervalType.None;

                                tagInfoList.Add(tagInfo);
                            }
                            else if (openingCharIndex == closingCharIndex - 3)
                            {
                                TagInfo tagInfo;

                                tagInfo.tag = string.Empty;
                                tagInfo.type = typeID;
                                tagInfo.id = 1;
                                tagInfo.index = parsedText.Length;
                                tagInfo.originRange = new Range(openingCharIndex, closingCharIndex);
                                tagInfo.order = 0;
                                tagInfo.haveOrder = false;
                                tagInfo.interval = 0;
                                tagInfo.intervalType = IntervalType.None;

                                tagInfoList.Add(tagInfo);
                            }
                            else
                            {
                                for (int j = openingCharIndex; j <= closingCharIndex; j++)
                                {
                                    parsedToOriginMappingList.Add(j);
                                }

                                parsedText.Append(text.Substring(openingCharIndex, closingCharIndex - openingCharIndex + 1));
                            }
                        }

                        i = closingCharIndex;
                    }
                }
            }

            StringInfo parsedTextInfo;

            parsedTextInfo.text = parsedText.ToString();
            parsedTextInfo.mappings = parsedToOriginMappingList.ToArray();
            parsedTextInfo.ranges = new Range[] { new Range(-1, text.Length) };

            return parsedTextInfo;
        }
    }
}