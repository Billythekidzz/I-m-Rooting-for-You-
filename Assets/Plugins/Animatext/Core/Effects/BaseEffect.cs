// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using System;
using UnityEngine;

namespace Animatext.Effects
{
    [Serializable]
    public abstract class BaseEffect : ScriptableObject
    {
        private ExecutionInfo _executionInfo;

        [HideInInspector] [SerializeField] [Tooltip("The waiting time before executing the preset.")]
        private float _startInterval;

        [HideInInspector] [SerializeField] [Tooltip("The tag name of the preset. The preset is automatically added to all text without using the preset tag when the tag name is null or empty.")]
        private string _tag = string.Empty;

        protected BaseAnimatext animatext 
        {
            get { return _executionInfo.animatext; }
        }

        protected Effect effect
        {
            get { return _executionInfo.effect; }
        }
        
        protected float time
        {
            get { return _executionInfo.time; }
        }

        protected float lastTime
        {
            get { return _executionInfo.lastTime; }
        }

        protected int charCount
        {
            get { return _executionInfo.charCount; }
        }

        protected UnitInfo[] chars
        {
            get { return _executionInfo.chars; }
        }

        protected int characterCount
        {
            get { return _executionInfo.characterCount; }
        }

        protected UnitInfo[] characters
        {
            get { return _executionInfo.characters; }
        }

        protected int wordCount
        {
            get { return _executionInfo.wordCount; }
        }

        protected UnitInfo[] words
        {
            get { return _executionInfo.words; }
        }

        protected int lineCount
        {
            get { return _executionInfo.lineCount; }
        }

        protected UnitInfo[] lines
        {
            get { return _executionInfo.lines; }
        }

        protected int groupCount
        {
            get { return _executionInfo.groupCount; }
        }

        protected UnitInfo[] groups
        {
            get { return _executionInfo.groups; }
        }

        protected UnitInfo range
        {
            get { return _executionInfo.range; }
        }

        protected float frontInterval
        {
            get { return _executionInfo.frontInterval; }
        }

        protected float rangeInterval
        {
            get { return _executionInfo.rangeInterval; }
        }

        protected float executionInterval
        {
            get { return _executionInfo.executionInterval; }
        }

        protected float totalInterval
        {
            get { return _executionInfo.totalInterval; }
        }

        public abstract InfoFlags infoFlags { get; }

        public float startInterval
        {
            get { return _startInterval; }
            set { _startInterval = value; }
        }

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        protected abstract void Animate();

        protected virtual float GetRangeInterval()
        {
            return 0;
        }

        protected virtual float GetExecutionInterval()
        {
            return 0;
        }

        public void EarlyExecute(ExecutionInfo executionInfo)
        {
            _executionInfo = executionInfo;

            _executionInfo.UpdateAnchor();

            float interval = _startInterval;

            if (_executionInfo.startInterval != interval)
            {
                _executionInfo.startInterval = interval;
                _executionInfo.effect.SetIntervalDirty();
            }

            interval = GetRangeInterval();

            if (_executionInfo.rangeInterval != interval)
            {
                _executionInfo.rangeInterval = interval;
                _executionInfo.effect.SetIntervalDirty();
            }

            interval = GetExecutionInterval();

            if (_executionInfo.executionInterval != interval)
            {
                _executionInfo.executionInterval = interval;
                _executionInfo.effect.SetIntervalDirty();
            }

            _executionInfo = null;
        }

        public void Execute(ExecutionInfo executionInfo)
        {
            _executionInfo = executionInfo;

            Animate();

            _executionInfo = null;
        }
    }
}