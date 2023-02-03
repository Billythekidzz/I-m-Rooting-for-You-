// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

using UnityEngine;

namespace Animatext
{
    public class PositionGroup
    {
        public int count;
        public bool isDirty;
        public Vector3[] cachePositions;
        public Vector3[] currentPositions;
        public Vector3[] originPositions;

        public PositionGroup()
        {
            count = 0;
            isDirty = false;
            cachePositions = null;
            currentPositions = null;
            originPositions = null;
        }

        public PositionGroup(Vector3[] positions)
        {
            count = positions.Length;
            isDirty = false;

            cachePositions = new Vector3[count];
            currentPositions = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = positions[i];
                currentPositions[i] = positions[i];
            }

            originPositions = positions;
        }

        public void Move(Vector2 position)
        {
            for (int i = 0; i < count; i++)
            {
                cachePositions[i].x += position.x;
                cachePositions[i].y += position.y;
            }
        }

        public void Rotate(float rotation)
        {
            Vector2 anchorPoint = (GetMinPosition() + GetMaxPosition()) / 2;

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Rotate(cachePositions[i], rotation, anchorPoint);
            }
        }

        public void Rotate(float rotation, Vector2 anchorPoint)
        {
            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Rotate(cachePositions[i], rotation, anchorPoint);
            }
        }

        public void Scale(Vector2 scale)
        {
            Vector2 anchorPoint = (GetMinPosition() + GetMaxPosition()) / 2;

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Scale(cachePositions[i], scale, anchorPoint);
            }
        }

        public void Scale(Vector2 scale, Vector2 anchorPoint)
        {
            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Scale(cachePositions[i], scale, anchorPoint);
            }
        }

        public void Skew(Vector2 skew)
        {
            Vector2 anchorPoint = (GetMinPosition() + GetMaxPosition()) / 2;

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Skew(cachePositions[i], skew, anchorPoint);
            }
        }

        public void Skew(Vector2 skew, Vector2 anchorPoint)
        {
            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = PositionUtility.Skew(cachePositions[i], skew, anchorPoint);
            }
        }

        public void Execute()
        {
            isDirty = false;

            Vector3[] tempPositions = cachePositions;

            cachePositions = currentPositions;
            currentPositions = tempPositions;

            for (int i = 0; i < count; i++)
            {
                if (currentPositions[i] != cachePositions[i])
                {
                    isDirty = true;
                    break;
                }
            }

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = originPositions[i];
            }
        }

        public Vector2 GetMaxPosition()
        {
            float x = float.NegativeInfinity;
            float y = float.NegativeInfinity;

            for (int i = 0; i < count; i++)
            {
                x = Mathf.Max(x, cachePositions[i].x);
                y = Mathf.Max(y, cachePositions[i].y);
            }

            return new Vector2(x, y);
        }

        public Vector2 GetMinPosition()
        {
            float x = float.PositiveInfinity;
            float y = float.PositiveInfinity;

            for (int i = 0; i < count; i++)
            {
                x = Mathf.Min(x, cachePositions[i].x);
                y = Mathf.Min(y, cachePositions[i].y);
            }

            return new Vector2(x, y);
        }

        public void SetPositions(Vector3[] positions)
        {
            count = positions.Length;
            originPositions = positions;

            cachePositions = new Vector3[count];
            currentPositions = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                cachePositions[i] = positions[i];
                currentPositions[i] = positions[i];
            }

            isDirty = false;
        }
    }
}