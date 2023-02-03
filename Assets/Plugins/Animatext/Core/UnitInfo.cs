// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0044 ,IDE0066, IDE1006

using UnityEngine;

namespace Animatext
{
    public struct UnitInfo
    {
        private Anchor _anchor;
        private Range _range;
        private string _text;
        private TextInfo _textInfo;

        public Anchor anchor
        {
            get { return _anchor; }
        }

        public Range range
        {
            get { return _range; }
        }

        public string text
        {
            get { return _text; }
        }

        public UnitInfo(TextInfo textInfo, Range range)
        {
            _anchor = new Anchor();
            _range = range;
            _text = range.count > 0 ? textInfo.effectText.Substring(range.startIndex, range.count) : string.Empty;
            _textInfo = textInfo;
        }

        public void Color(Color color)
        {
            _textInfo.ColorRange(_range, color);
        }

        public void Color(Color color, ColorMode colorMode)
        {
            _textInfo.ColorRange(_range, color, colorMode);
        }

        public void Move(Vector2 position)
        {
            _textInfo.MoveRange(_range, position);
        }

        public void Opacify(float opacity)
        {
            _textInfo.OpacifyRange(_range, opacity);
        }

        public void Opacify(float opacity, ColorMode colorMode)
        {
            _textInfo.OpacifyRange(_range, opacity, colorMode);
        }

        public void Rotate(float rotation)
        {
            _textInfo.RotateRange(_range, rotation, _anchor.center);
        }

        public void Rotate(float rotation, Vector2 anchorPoint)
        {
            _textInfo.RotateRange(_range, rotation, anchorPoint);
        }

        public void Scale(Vector2 scale)
        {
            _textInfo.ScaleRange(_range, scale, _anchor.center);
        }

        public void Scale(Vector2 scale, Vector2 anchorPoint)
        {
            _textInfo.ScaleRange(_range, scale, anchorPoint);
        }

        public void Skew(Vector2 skew)
        {
            _textInfo.SkewRange(_range, skew, _anchor.center);
        }

        public void Skew(Vector2 skew, Vector2 anchorPoint)
        {
            _textInfo.SkewRange(_range, skew, anchorPoint);
        }

        public void UpdateAnchor()
        {
            _textInfo.UpdateRangeAnchor(_range, _anchor);
        }

        public Vector2 GetAnchorPoint(AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.TopLeft:
                    return _anchor.topLeft;

                case AnchorType.Top:
                    return _anchor.top;

                case AnchorType.TopRight:
                    return _anchor.topRight;

                case AnchorType.Left:
                    return _anchor.left;

                case AnchorType.Center:
                    return _anchor.center;

                case AnchorType.Right:
                    return _anchor.right;

                case AnchorType.BottomLeft:
                    return _anchor.bottomLeft;

                case AnchorType.Bottom:
                    return _anchor.bottom;

                case AnchorType.BottomRight:
                    return _anchor.bottomRight;

                default:
                    return _anchor.center;
            }
        }
    }
}