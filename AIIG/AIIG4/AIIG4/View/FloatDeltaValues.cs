using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG4.View
{
    class FloatDeltaValues
    {
        private float deltaX;
        private float deltaY;
        private float absDeltaX;
        private float absDeltaY;

        public FloatDeltaValues(Vector2 startPoint, Vector2 endPoint)
        {
            deltaX = endPoint.X - startPoint.X;
            deltaY = endPoint.Y - startPoint.Y;
            absDeltaX = Math.Abs(deltaX);
            absDeltaY = Math.Abs(deltaY);
        }

        public float DeltaX
        {
            get { return deltaX; }
        }

        public float DeltaY
        {
            get { return deltaY; }
        }

        public float AbsDeltaX
        {
            get { return absDeltaX; }
        }

        public float AbsDeltaY
        {
            get { return absDeltaY; }
        }
    }
}
