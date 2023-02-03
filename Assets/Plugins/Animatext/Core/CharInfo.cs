// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext
{
    public struct CharInfo
    {
        public char character;
        public bool isVisible;
        public int originIndex;
        public int parsedIndex;
        public int outputIndex;
        public ColorGroup colorGroup;
        public PositionGroup positionGroup;

        public CharInfo(char character, int originIndex, int parsedIndex, int outputIndex)
        {
            this.character = character;
            this.originIndex = originIndex;
            this.parsedIndex = parsedIndex;
            this.outputIndex = outputIndex;

            isVisible = IsVisibleChar(character);

            colorGroup = new ColorGroup();
            positionGroup = new PositionGroup();
        }

        public static bool IsValidChar(char character)
        {
            return character < '\u007F' ? character >= '\u0020' : character >= '\u00A0';
        }

        public static bool IsVisibleChar(char character)
        {
            return character < '\u007F' ? character > '\u0020' : (character > '\u00A0' && character != '\u3000');
        }

        public void Color(Color color)
        {
            colorGroup.Color(color);
        }

        public void Color(Color color, ColorMode colorMode)
        {
            colorGroup.Color(color, colorMode);
        }

        public void Move(Vector2 position)
        {
            positionGroup.Move(position);
        }

        public void Opacify(float opacity)
        {
            colorGroup.Opacify(opacity);
        }

        public void Opacify(float opacity, ColorMode colorMode)
        {
            colorGroup.Opacify(opacity, colorMode);
        }

        public void Rotate(float rotation)
        {
            positionGroup.Rotate(rotation);
        }

        public void Rotate(float rotation, Vector2 anchorPoint)
        {
            positionGroup.Rotate(rotation, anchorPoint);
        }

        public void Scale(Vector2 scale)
        {
            positionGroup.Scale(scale);
        }

        public void Scale(Vector2 scale, Vector2 anchorPoint)
        {
            positionGroup.Scale(scale, anchorPoint);
        }

        public void Skew(Vector2 skew)
        {
            positionGroup.Skew(skew);
        }

        public void Skew(Vector2 skew, Vector2 anchorPoint)
        {
            positionGroup.Skew(skew, anchorPoint);
        }

        public void Execute()
        {
            positionGroup.Execute();
            colorGroup.Execute();
        }

        public void SetColors(Color[] colors)
        {
            colorGroup.SetColors(colors);
        }

        public void SetPositions(Vector3[] positions)
        {
            positionGroup.SetPositions(positions);
        }
    }
}