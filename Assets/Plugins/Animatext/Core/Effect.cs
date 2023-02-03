// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090, IDE1006

using Animatext.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animatext
{
    [Serializable]
    public class Effect
    {
        private int _executionEffectCount;
        private ExecutionEffect[] _executionEffects;
        private int _executionRangeCount;
        private Range[] _executionRanges;
        private bool _intervalDirty;
        private bool _proceed;
        [SerializeField] private BaseEffect[] _presets;
        [SerializeField] private bool _autoPlay;
        [SerializeField] private bool _autoStart;
        [SerializeField] private RefreshMode _refreshMode;
        [SerializeField] private EffectState _state;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _time;

        public Effect(params BaseEffect[] presets)
        {
            _presets = presets;
            _autoStart = true;
            _autoPlay = true;
            _refreshMode = RefreshMode.Start;
        }

        public bool autoPlay
        {
            get { return _autoPlay; }
            set { _autoPlay = value; }
        }

        public bool autoStart
        {
            get { return _autoStart; }
            set { _autoStart = value; }
        }

        public EffectState state
        {
            get { return _state; }
        }

        public RefreshMode refreshMode
        {
            get { return _refreshMode; }
            set { _refreshMode = value; }
        }

        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float time
        {
            get { return _time; }
            set { _time = value; }
        }

        public event Action onEnd;
        public event Action onPause;
        public event Action onPlay;
        public event Action onProceed;
        public event Action onStart;
        public event Action onStop;

        private void BaseExecute(float time)
        {
            float frontInterval = 0;

            for (int i = 0; i < _executionEffectCount; i++)
            {
                _executionEffects[i].EarlyExecute(ref time, ref frontInterval);
            }

            if (_intervalDirty)
            {
                float totalInterval = 0;

                for (int i = 0; i < _executionRangeCount; i++)
                {
                    int startIndex = _executionRanges[i].startIndex;
                    int endIndex = _executionRanges[i].endIndex;

                    float executionTotalInterval;

                    if (startIndex > endIndex)
                    {
                        executionTotalInterval = 0;
                    }
                    else
                    {
                        executionTotalInterval = _executionEffects[endIndex].startInterval + _executionEffects[endIndex].executionInterval;

                        for (int j = endIndex - 1; j >= startIndex; j--)
                        {
                            executionTotalInterval += _executionEffects[j].rangeInterval;

                            if (executionTotalInterval < _executionEffects[j].executionInterval)
                            {
                                executionTotalInterval = _executionEffects[j].executionInterval;
                            }

                            executionTotalInterval += _executionEffects[j].startInterval;
                        }

                        if (_executionEffects[startIndex].openingIntervalType != IntervalType.Replace && _executionEffects[startIndex].openingIntervalType != IntervalType.Cover && startIndex > 0)
                        {
                            executionTotalInterval += _executionEffects[startIndex - 1].rangeInterval;
                        }
                    }

                    totalInterval = Mathf.Max(totalInterval, executionTotalInterval);
                }

                for (int i = 0; i < _executionEffectCount; i++)
                {
                    _executionEffects[i].SetTotalInterval(totalInterval);
                }

                _intervalDirty = false;
            }

            for (int i = 0; i < _executionEffectCount; i++)
            {
                _executionEffects[i].Execute();
            }

            if (time >= 0 && !_proceed)
            {
                _proceed = true;

                onProceed?.Invoke();
            }
        }

        private void Reset()
        {
            _time = 0;
            _proceed = false;
        }

        public void End()
        {
            if (_state != EffectState.End)
            {
                _state = EffectState.End;

                onEnd?.Invoke();
            }
        }

        public void Execute()
        {
            switch (_state)
            {
                case EffectState.Start:
                    if (_autoPlay)
                    {
                        _state = EffectState.Play;
                        BaseExecute(_time);
                    }
                    else
                    {
                        BaseExecute(0);
                    }
                    break;

                case EffectState.Play:
                    _time += Time.deltaTime * _speed;
                    BaseExecute(_time);
                    break;

                case EffectState.Pause:
                    BaseExecute(_time);
                    break;

                case EffectState.End:
                    BaseExecute(float.PositiveInfinity);
                    break;

                default:
                    return;
            }
        }

        public void ExtraExecute()
        {
            switch (_state)
            {
                case EffectState.Start:
                    BaseExecute(0);
                    break;

                case EffectState.Play:
                case EffectState.Pause:
                    BaseExecute(_time);
                    break;

                case EffectState.End:
                    BaseExecute(float.PositiveInfinity);
                    break;

                default:
                    break;
            }
        }

        public InfoFlags GetInfoFlags()
        {
            InfoFlags infoFlags = InfoFlags.None;

            if (_presets != null)
            {
                for (int i = 0; i < _presets.Length; i++)
                {
                    if (_presets[i] != null)
                    {
                        infoFlags |= _presets[i].infoFlags;
                    }
                }
            }

            return infoFlags;
        }

        public HashSet<string> GetTags()
        {
            HashSet<string> tags = new HashSet<string>();

            if (_presets != null)
            {
                for (int i = 0; i < _presets.Length; i++)
                {
                    if (_presets[i] != null)
                    {
                        tags.Add(_presets[i].tag ?? string.Empty);
                    }
                }
            }

            return tags;
        }

        public void Pause()
        {
            if (_state == EffectState.Start || _state == EffectState.Play)
            {
                _state = EffectState.Pause;

                onPause?.Invoke();
            }
        }

        public void Play()
        {
            if (_state != EffectState.Play)
            {
                _state = EffectState.Play;

                onPlay?.Invoke();
            }
            else
            {
                Reset();
            }
        }

        public void Refresh()
        {
            if (_state == EffectState.Stop)
            {
                if (_autoStart)
                {
                    Start();
                }
            }
            else
            {
                switch (_refreshMode)
                {
                    case RefreshMode.Start:
                        Start();
                        break;

                    case RefreshMode.Replay:
                        Play();
                        break;

                    case RefreshMode.Pause:
                        Pause();
                        break;

                    case RefreshMode.End:
                        End();
                        break;

                    default:
                        break;
                }
            }
        }

        public void SetIntervalDirty()
        {
            if (_intervalDirty) return;

            _intervalDirty = true;
        }

        public void Start()
        {
            if (_state != EffectState.Start)
            {
                if (_state != EffectState.Stop)
                {
                    Reset();
                }

                _state = EffectState.Start;

                onStart?.Invoke();
            }
        }

        public void Stop()
        {
            if (_state != EffectState.Stop)
            {
                Reset();

                _state = EffectState.Stop;
                
                onStop?.Invoke();
            }
        }

        public void Update(TextInfo textInfo, BaseAnimatext animatext)
        {
            _executionEffects = textInfo.GenerateExecutionEffects(_presets, this, animatext);

            if (_executionEffects == null)
            {
                _executionEffectCount = 0;
                _executionRangeCount = 0;
            }
            else
            {
                _executionEffectCount = _executionEffects.Length;

                int startIndex = 0;
                List<Range> executionRangeList = new List<Range>();

                for (int i = 0; i < _executionEffectCount; i++)
                {
                    switch (_executionEffects[i].openingIntervalType)
                    {
                        case IntervalType.Replace:
                        case IntervalType.Cover:
                            executionRangeList.Add(new Range(startIndex, i - 1));
                            startIndex = i;
                            break;

                        default:
                            break;
                    }

                    switch (_executionEffects[i].closingIntervalType)
                    {
                        case IntervalType.Replace:
                        case IntervalType.Cover:
                            executionRangeList.Add(new Range(startIndex, i));
                            startIndex = i + 1;
                            break;

                        default:
                            break;
                    }
                }

                if (startIndex != _executionEffectCount)
                {
                    executionRangeList.Add(new Range(startIndex, _executionEffectCount - 1));
                }

                _executionRanges = executionRangeList.ToArray();
                _executionRangeCount = _executionRanges.Length;
            }

            if (_state == EffectState.Stop)
            {
                if (_autoStart)
                {
                    Start();
                }
            }

            SetIntervalDirty();
        }
    }
}