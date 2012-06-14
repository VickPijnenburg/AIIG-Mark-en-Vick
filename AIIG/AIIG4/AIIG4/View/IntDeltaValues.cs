using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG4.View
{
    class IntDeltaValues
    {
        private int deltaX;
        private int deltaY;
        private int absDeltaX;
        private int absDeltaY;

        public IntDeltaValues(Point startPoint, Point endPoint)
        {
            deltaX = endPoint.X - startPoint.X;
            deltaY = endPoint.Y - startPoint.Y;
            absDeltaX = Math.Abs(deltaX);
            absDeltaY = Math.Abs(deltaY);
        }

        public int DeltaX
        {
            get { return deltaX; }
        }

        public int DeltaY
        {
            get { return deltaY; }
        }

        public int AbsDeltaX
        {
            get { return absDeltaX; }
        }

        public int AbsDeltaY
        {
            get { return absDeltaY; }
        }
    }
}
