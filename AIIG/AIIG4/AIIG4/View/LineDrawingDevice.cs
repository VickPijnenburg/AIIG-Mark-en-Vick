using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG4.View
{
    public abstract class LineDrawingDevice
    {
        //Fields

        static Point currentPolyPoint = Point.Zero;



        //Single pixel method

        public static void SetPixel(Texture2D texture, int x, int y, Color color)
        {
            if ((x >= 0) && (x < texture.Width) && (y >= 0) && (y < texture.Height))
            {
                Rectangle r = new Rectangle(x, y, 1, 1);

                Color[] colorArray = new Color[1] { color };

                texture.SetData<Color>(0, r, colorArray, 0, 1);
            }
        }



        //Straight line methods

        public static void SetHorizontalLine(Texture2D texture, int x1, int x2, int y, Color color)
        {
            int leftX = Math.Min(x1, x2);
            int rightX = Math.Max(x1, x2);

            for (int pixelX = leftX; pixelX <= rightX; pixelX++)
            {
                SetPixel(texture, pixelX, y, color);
            }
        }

        public static void SetVerticalLine(Texture2D texture, int x, int y1, int y2, Color color)
        {
            int topY = Math.Min(y1, y2);
            int bottomY = Math.Max(y1, y2);

            for (int pixelY = topY; pixelY <= bottomY; pixelY++)
            {
                SetPixel(texture, x, pixelY, color);
            }
        }



        //DSC line methods

        public static void SetDirectScanConversionLine(Texture2D texture, Vector2 startPoint, Vector2 endPoint, Color color)
        {
            FloatDeltaValues deltaValues = new FloatDeltaValues(startPoint, endPoint);

            if (deltaValues.AbsDeltaX > deltaValues.AbsDeltaY)
            {
                SetDSCLineIncrementingX(texture, startPoint, endPoint, deltaValues, color);
            }
            else if (deltaValues.AbsDeltaY > 0)
            {
                SetDSCLineIncrementingY(texture, startPoint, endPoint, deltaValues, color);
            }
            else
            {
                SetPixel(texture, (int)startPoint.X, (int)startPoint.Y, color);
            }
        }

        private static void SetDSCLineIncrementingX(Texture2D texture, Vector2 startPoint, Vector2 endPoint, FloatDeltaValues deltaValues, Color color)
        {
            SwapForXDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            float yInterval = deltaValues.DeltaY / deltaValues.DeltaX;

            for (int xMovement = 0; xMovement <= deltaValues.AbsDeltaX; xMovement++)
            {
                int currentX = (int)Math.Round(startPoint.X + xMovement);
                int currentY = (int)Math.Round((startPoint.Y) + yInterval * xMovement);
                SetPixel(texture, currentX, currentY, color);
            }
        }

        private static void SetDSCLineIncrementingY(Texture2D texture, Vector2 startPoint, Vector2 endPoint, FloatDeltaValues deltaValues, Color color)
        {
            SwapForYDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            float xInterval = deltaValues.DeltaX / deltaValues.DeltaY;

            for (int yMovement = 0; yMovement <= deltaValues.AbsDeltaY; yMovement++)
            {
                int currentX = (int)Math.Round(startPoint.X + xInterval * yMovement);
                int currentY = (int)Math.Round((startPoint.Y) + yMovement);
                SetPixel(texture, currentX, currentY, color);
            }
        }



        //DDA line methods

        public static void SetDDALine(Texture2D texture, Vector2 startPoint, Vector2 endPoint, Color color)
        {
            FloatDeltaValues deltaValues = new FloatDeltaValues(startPoint, endPoint);

            if (deltaValues.AbsDeltaX > deltaValues.AbsDeltaY)
            {
                SetDDALineIncrementingX(texture, startPoint, endPoint, deltaValues, color);
            }
            else if (deltaValues.AbsDeltaY > 0)
            {
                SetDDALineIncrementingY(texture, startPoint, endPoint, deltaValues, color);
            }
            else
            {
                SetPixel(texture, (int)startPoint.X, (int)startPoint.Y, color);
            }
        }

        private static void SetDDALineIncrementingX(Texture2D texture, Vector2 startPoint, Vector2 endPoint, FloatDeltaValues deltaValues, Color color)
        {
            SwapForXDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            Vector2 incrementVector = new Vector2(1, deltaValues.DeltaY / deltaValues.DeltaX);

            for (Vector2 currentPos = startPoint; currentPos.X <= endPoint.X; currentPos = Vector2.Add(currentPos, incrementVector))
            {
                SetPixel(texture, (int)currentPos.X, (int)currentPos.Y, color);
            }
        }

        private static void SetDDALineIncrementingY(Texture2D texture, Vector2 startPoint, Vector2 endPoint, FloatDeltaValues deltaValues, Color color)
        {
            SwapForYDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            Vector2 incrementVector = new Vector2(deltaValues.DeltaX / deltaValues.DeltaY, 1);

            for (Vector2 currentPos = startPoint; currentPos.Y <= endPoint.Y; currentPos = Vector2.Add(currentPos, incrementVector))
            {
                SetPixel(texture, (int)currentPos.X, (int)currentPos.Y, color);
            }
        }



        //Bresenham lines

        public static void SetBHLine(Texture2D texture, Point startPoint, Point endPoint, Color color)
        {
            IntDeltaValues deltaValues = new IntDeltaValues(startPoint, endPoint);

            if (deltaValues.AbsDeltaX > deltaValues.AbsDeltaY)
            {
                SetBHLineIncrementingX(texture, startPoint, endPoint, deltaValues, color);
            }
            else if (deltaValues.AbsDeltaY > 0)
            {
                SetBHLineIncrementingY(texture, startPoint, endPoint, deltaValues, color);
            }
            else
            {
                SetPixel(texture, (int)startPoint.X, (int)startPoint.Y, color);
            }
        }

        private static void SetBHLineIncrementingX(Texture2D texture, Point startPoint, Point endPoint, IntDeltaValues deltaValues, Color color)
        {
            SwapForXDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            int slope = 0;
            int incE = 0;
            int incNE = 0;
            int d = 0;
            SetBHStartValues(deltaValues.AbsDeltaX, deltaValues.DeltaY, deltaValues.AbsDeltaY, ref slope, ref incE, ref incNE, ref d);

            for (Point currentPos = startPoint; currentPos.X <= endPoint.X; currentPos.X++)
            {
                SetPixel(texture, (int)currentPos.X, (int)currentPos.Y, color);

                if (d <= 0)
                {
                    d += incE;
                }
                else
                {
                    d += incNE;
                    currentPos.Y += slope;
                }
            }
        }

        private static void SetBHLineIncrementingY(Texture2D texture, Point startPoint, Point endPoint, IntDeltaValues deltaValues, Color color)
        {
            SwapForYDrawingAsNeeded(ref startPoint, ref endPoint, ref deltaValues);

            int slope = 0;
            int incE = 0;
            int incNE = 0;
            int d = 0;
            SetBHStartValues(deltaValues.AbsDeltaY, deltaValues.DeltaX, deltaValues.AbsDeltaX, ref slope, ref incE, ref incNE, ref d);

            for (Point currentPos = startPoint; currentPos.Y <= endPoint.Y; currentPos.Y++)
            {
                SetPixel(texture, (int)currentPos.X, (int)currentPos.Y, color);

                if (d <= 0)
                {
                    d += incE;
                }
                else
                {
                    d += incNE;
                    currentPos.X += slope;
                }
            }
        }

        private static void SetBHStartValues(int incAxisAbsD, int otherAxisD, int otherAxisAbsD, ref int slope, ref int incE, ref int incNE, ref int startD)
        {
            if (otherAxisD < 0)
            {
                slope = -1;
            }
            else
            {
                slope = 1;
            }

            incE = 2 * otherAxisAbsD;
            incNE = 2 * otherAxisAbsD - 2 * incAxisAbsD;
            startD = 2 * otherAxisAbsD - incAxisAbsD;
        }



        //Vector swap methods

        private static void SwapForXDrawingAsNeeded(ref Vector2 startPoint, ref Vector2 endPoint, ref FloatDeltaValues deltaValues)
        {
            if (startPoint.X > endPoint.X)
            {
                SwapStartAndEnd(ref startPoint, ref endPoint);
                deltaValues = new FloatDeltaValues(startPoint, endPoint);
            }
        }

        private static void SwapForYDrawingAsNeeded(ref Vector2 startPoint, ref Vector2 endPoint, ref FloatDeltaValues deltaValues)
        {
            if (startPoint.Y > endPoint.Y)
            {
                SwapStartAndEnd(ref startPoint, ref endPoint);
                deltaValues = new FloatDeltaValues(startPoint, endPoint);
            }
        }

        private static void SwapStartAndEnd(ref Vector2 startPoint, ref Vector2 endPoint)
        {
            Vector2 tempPoint = startPoint;
            startPoint = endPoint;
            endPoint = tempPoint;
        }



        //Poly line methods

        public static void MoveToPoint(Point point)
        {
            currentPolyPoint = point;
        }

        public static void DrawPolyLineToPoint(Texture2D texture, Point targetPoint, Color color)
        {
            SetBHLine(texture, currentPolyPoint, targetPoint, color);
            currentPolyPoint = targetPoint;
        }

        public static void DrawBezierCurveToPoint(Texture2D texture, Vector2 endPoint, Vector2 firstControlPoint, Vector2 secondControlPoint, int segmentCount, Color color)
        {
            SetBezierLines(texture, PointToVector(currentPolyPoint), endPoint, firstControlPoint, secondControlPoint, segmentCount, color);
        }



        //Bezier curve methods

        public static void SetBezierPoints(Texture2D texture, Vector2 startPoint, Vector2 endPoint, Vector2 firstControlPoint, Vector2 secondControlPoint, int segmentCount, Color color)
        {
            for (int i = 0; i <= segmentCount; i++)
            {
                float t = i / (float)segmentCount;
                Vector2 pixel = CalculateBezierPoint(t, startPoint, firstControlPoint, secondControlPoint, endPoint);
                SetPixel(texture, (int)pixel.X, (int)pixel.Y, color);
            }
        }

        public static void SetBezierLines(Texture2D texture, Vector2 startPoint, Vector2 endPoint, Vector2 firstControlPoint, Vector2 secondControlPoint, int segmentCount, Color color)
        {
            MoveToPoint(VectorToPoint(startPoint));
            for (int i = 0; i <= segmentCount; i++)
            {
                float t = i / (float)segmentCount;
                Vector2 pixel = CalculateBezierPoint(t, startPoint, firstControlPoint, secondControlPoint, endPoint);
                DrawPolyLineToPoint(texture, VectorToPoint(pixel), color);
            }
        }

        private static Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p = uuu * p0; //first term
            p += 3 * uu * t * p1; //second term
            p += 3 * u * tt * p2; //third term
            p += ttt * p3; //fourth term

            return p;
        }



        //Point swap methods

        private static void SwapForXDrawingAsNeeded(ref Point startPoint, ref Point endPoint, ref IntDeltaValues deltaValues)
        {
            if (startPoint.X > endPoint.X)
            {
                SwapStartAndEnd(ref startPoint, ref endPoint);
                deltaValues = new IntDeltaValues(startPoint, endPoint);
            }
        }

        private static void SwapForYDrawingAsNeeded(ref Point startPoint, ref Point endPoint, ref IntDeltaValues deltaValues)
        {
            if (startPoint.Y > endPoint.Y)
            {
                SwapStartAndEnd(ref startPoint, ref endPoint);
                deltaValues = new IntDeltaValues(startPoint, endPoint);
            }
        }

        private static void SwapStartAndEnd(ref Point startPoint, ref Point endPoint)
        {
            Point tempPoint = startPoint;
            startPoint = endPoint;
            endPoint = tempPoint;
        }



        //Conversion methods

        public static Point VectorToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static Vector2 PointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
