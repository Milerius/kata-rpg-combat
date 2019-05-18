using System;
using System.Drawing;
using Kata_Rpg_Combat;
using NUnit.Framework;

namespace KataTests
{
    public class MathTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PointDistance()
        {
            var p = new Point(1, 1);
            var p2 = new Point(11, 14);

            Assert.GreaterOrEqual(16, MathUtilility.GetDistance(p, p2));
            Assert.AreEqual(16, MathUtilility.GetDistance(p, p2));
        }
    }
}