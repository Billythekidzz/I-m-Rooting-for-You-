// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE1006

using UnityEngine;

namespace Animatext
{
    public class Anchor
    {
        private Vector2 _center;
        private Vector2 _vectorX;
        private Vector2 _vectorY;

        public Anchor()
        {
            Vector2 min = Vector2.positiveInfinity;
            Vector2 max = Vector2.negativeInfinity;

            _center = (min + max) / 2;
            _vectorX = new Vector2((max.x - min.x) / 2, 0);
            _vectorY = new Vector2(0, (max.y - min.y) / 2);
        }

        public Anchor(Vector2 min, Vector2 max)
        {
            _center = (min + max) / 2;
            _vectorX = new Vector2((max.x - min.x) / 2, 0);
            _vectorY = new Vector2(0, (max.y - min.y) / 2);
        }

        public Vector2 bottomLeft
        {
            get { return _center - _vectorX - _vectorY; }
        }

        public Vector2 bottom
        {
            get { return _center - _vectorY; }
        }

        public Vector2 bottomRight
        {
            get { return _center + _vectorX - _vectorY; }
        }

        public Vector2 left
        {
            get { return _center - _vectorX; }
        }

        public Vector2 center
        {
            get { return _center; }
        }

        public Vector2 right
        {
            get { return _center + _vectorX; }
        }

        public Vector2 topLeft
        {
            get { return _center - _vectorX + _vectorY; }
        }

        public Vector2 top
        {
            get { return _center + _vectorY; }
        }

        public Vector2 topRight
        {
            get { return _center + _vectorX + _vectorY; }
        }

        public void Set(Vector2 min, Vector2 max)
        {
            _center = (min + max) / 2;
            _vectorX = new Vector3((max.x - min.x) / 2, 0);
            _vectorY = new Vector3(0, (max.y - min.y) / 2);
        }

        public void Move(Vector2 position)
        {
            _center += position;
        }

        public void Rotate(float rotation)
        {
            _vectorX = PositionUtility.Rotate(_vectorX, rotation);
            _vectorY = PositionUtility.Rotate(_vectorY, rotation);
        }

        public void Rotate(float rotation, Vector2 anchorPoint)
        {
            _center = PositionUtility.Rotate(_center, rotation, anchorPoint);
            _vectorX = PositionUtility.Rotate(_vectorX, rotation);
            _vectorY = PositionUtility.Rotate(_vectorY, rotation);
        }

        public void Scale(Vector2 scale)
        {
            _vectorX = PositionUtility.Scale(_vectorX, scale);
            _vectorY = PositionUtility.Scale(_vectorY, scale);
        }

        public void Scale(Vector2 scale, Vector2 anchorPoint)
        {
            _center = PositionUtility.Scale(_center, scale, anchorPoint);
            _vectorX = PositionUtility.Scale(_vectorX, scale);
            _vectorY = PositionUtility.Scale(_vectorY, scale);
        }

        public void Skew(Vector2 skew)
        {
            _vectorX = PositionUtility.Skew(_vectorX, skew);
            _vectorY = PositionUtility.Skew(_vectorY, skew);
        }

        public void Skew(Vector2 skew, Vector2 anchorPoint)
        {
            _center = PositionUtility.Skew(_center, skew, anchorPoint);
            _vectorX = PositionUtility.Skew(_vectorX, skew);
            _vectorY = PositionUtility.Skew(_vectorY, skew);
        }
    }
}