using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalendarPageGenerator.Tests {
    [TestClass]
    public class IEnumerableExtensionsTests {
        [TestMethod]
        public void ToTwoDimensionalArraySimple() {
            var ienumerable = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var secondDimension = 2;
            var expected = new[,] {
                {1, 2},
                {3, 4},
                {5, 6},
                {7, 8}
            };

            var actual = ienumerable.ToTwoDimensionalArray(secondDimension);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToTwoDimensionalArrayWithRemainder() {
            var ienumerable = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var secondDimension = 3;
            var expected = new[,] {
                {1, 2, 3},
                {4, 5, 6}
            };

            var actual = ienumerable.ToTwoDimensionalArray(secondDimension);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
