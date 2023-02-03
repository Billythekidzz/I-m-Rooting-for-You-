// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext
{
    public static class PositionUtility
    {
        public static Vector2 Rotate(Vector2 point, float rotation)
        {
            rotation *= Mathf.Deg2Rad;

            Vector2 v;

            v.x = point.x * Mathf.Cos(rotation) - point.y * Mathf.Sin(rotation);
            v.y = point.x * Mathf.Sin(rotation) + point.y * Mathf.Cos(rotation);

            return v;
        }

        public static Vector2 Rotate(Vector2 point, float rotation, Vector2 anchorPoint)
        {
            return Rotate(point - anchorPoint, rotation) + anchorPoint;
        }

        public static Vector3 Rotate(Vector3 point, float rotation)
        {
            rotation *= Mathf.Deg2Rad;

            Vector3 v;

            v.x = point.x * Mathf.Cos(rotation) - point.y * Mathf.Sin(rotation);
            v.y = point.x * Mathf.Sin(rotation) + point.y * Mathf.Cos(rotation);
            v.z = point.z;

            return v;
        }

        public static Vector3 Rotate(Vector3 point, float rotation, Vector2 anchorPoint)
        {
            Vector2 v = new Vector2(point.x - anchorPoint.x, point.y - anchorPoint.y);

            v = Rotate(v, rotation);

            return new Vector3(v.x + anchorPoint.x, v.y + anchorPoint.y, point.z);
        }

        public static Vector2 Scale(Vector2 point, Vector2 scale)
        {
            Vector2 v;

            v.x = point.x * scale.x;
            v.y = point.y * scale.y;

            return v;
        }

        public static Vector2 Scale(Vector2 point, Vector2 scale, Vector2 anchorPoint)
        {
            return Scale(point - anchorPoint, scale) + anchorPoint;
        }

        public static Vector3 Scale(Vector3 point, Vector2 scale)
        {
            Vector3 v;

            v.x = point.x * scale.x;
            v.y = point.y * scale.y;
            v.z = point.z;

            return v;
        }

        public static Vector3 Scale(Vector3 point, Vector2 scale, Vector2 anchorPoint)
        {
            Vector2 v = new Vector2(point.x - anchorPoint.x, point.y - anchorPoint.y);

            v = Scale(v, scale);

            return new Vector3(v.x + anchorPoint.x, v.y + anchorPoint.y, point.z);
        }

        public static Vector2 Skew(Vector2 point, Vector2 skew)
        {
            skew *= Mathf.Deg2Rad;

            Vector2 v;

            v.x = point.x + point.y * Mathf.Tan(skew.y);
            v.y = point.y + point.x * Mathf.Tan(skew.x);

            return v;
        }

        public static Vector2 Skew(Vector2 point, Vector2 skew, Vector2 anchorPoint)
        {
            return Skew(point - anchorPoint, skew) + anchorPoint;
        }

        public static Vector3 Skew(Vector3 point, Vector2 skew)
        {
            skew *= Mathf.Deg2Rad;

            Vector3 v;

            v.x = point.x + point.y * Mathf.Tan(skew.y);
            v.y = point.y + point.x * Mathf.Tan(skew.x);
            v.z = point.z;

            return v;
        }

        public static Vector3 Skew(Vector3 point, Vector2 skew, Vector2 anchorPoint)
        {
            Vector2 v = new Vector2(point.x - anchorPoint.x, point.y - anchorPoint.y);

            v = Skew(v, skew);

            return new Vector3(v.x + anchorPoint.x, v.y + anchorPoint.y, point.z);
        }
    }
}