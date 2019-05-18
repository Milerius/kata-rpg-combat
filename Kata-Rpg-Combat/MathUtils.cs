using System;
using System.Drawing;

namespace Kata_Rpg_Combat
{
    public class MathUtilility
    {
        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <returns>The distance between two points.</returns>
        public static int GetDistance(Point p1, Point p2)
        {
            double xDelta = p1.X - p2.X;
            double yDelta = p1.Y - p2.Y;

            return (int)(Math.Sqrt(Math.Pow(xDelta, 2) + Math.Pow(yDelta, 2)));
        }
    }
}