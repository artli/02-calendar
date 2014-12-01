using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalendarPageGenerator;

namespace CalendarPageGenerator.Tests {
    [TestClass]
    public class GridGeneratorTests {
        [TestMethod]
        public void WeekdayNumber_Monday_Return0() {
            var mondayDate = DateTime.Parse("2015-02-02");
            var expected = 0;

            var actual = GridGenerator.WeekdayNumber(mondayDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeekdayNumber_Sunday_Return6() {
            var sundayDate = DateTime.Parse("2012-06-03");
            var expected = 6;

            var actual = GridGenerator.WeekdayNumber(sundayDate);
            Assert.AreEqual(expected, actual);
        }


        private void TestGenerateMonthGrid(DateTime date, string[,] expectedGrid, Point expectedPosition) {
            var actual = GridGenerator.GenerateMonthGrid(date);
            CollectionAssert.AreEqual(expectedGrid, actual.Grid);
            Assert.AreEqual(expectedPosition, actual.CurrentDayPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid_5Lines() {
            var date = DateTime.Parse("2014-11-29");
            var expectedGrid = 
                    Enumerable.Repeat("", 5)
                .Concat(
                    Enumerable.Range(1, 30)
                    .Select(num => num.ToString()))
                .ToTwoDimensionalArray(7);
            var expectedPosition = new Point(4, 5);

            TestGenerateMonthGrid(date, expectedGrid, expectedPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid_6Lines() {
            var date = DateTime.Parse("2014-06-30");
            var expectedGrid = 
                    Enumerable.Repeat("", 6)
                .Concat(
                    Enumerable.Range(1, 30)
                    .Select(num => num.ToString()))
                .Concat(
                    Enumerable.Repeat("", 6))
                .ToTwoDimensionalArray(7);
            var expectedPosition = new Point(5, 0);

            TestGenerateMonthGrid(date, expectedGrid, expectedPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid_4Lines() {
            var date = DateTime.Parse("2010-02-02");
            var expectedGrid = Enumerable.Range(1, 28)
                .Select(num => num.ToString())
                .ToTwoDimensionalArray(7);
            var expectedPosition = new Point(0, 1);

            TestGenerateMonthGrid(date, expectedGrid, expectedPosition);
        }
    }
}
