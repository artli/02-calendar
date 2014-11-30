using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalendarPageGenerator;
using System.Linq;

namespace CalendarPageGenerator.Tests {
    [TestClass]
    public class GridGeneratorTests {
        [TestMethod]
        public void DayOfWeekNumberMonday() {
            var mondayDate = DateTime.Parse("2015-02-02");
            var expected = 0;

            var actual = GridGenerator.DayOfWeekNumber(mondayDate);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DayOfWeekNumberSunday() {
            var sundayDate = DateTime.Parse("2012-06-03");
            var expected = 6;

            var actual = GridGenerator.DayOfWeekNumber(sundayDate);
            Assert.AreEqual(expected, actual);
        }


        public void TestMonthGrid(DateTime date, string[,] expectedGrid, Point expectedPosition) {
            var actual = GridGenerator.GenerateMonthGrid(date);
            CollectionAssert.AreEqual(expectedGrid, actual.Grid);
            Assert.AreEqual(expectedPosition, actual.CurrentDayPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid5Lines() {
            var date = DateTime.Parse("2014-11-29");
            var expectedGrid = 
                    Enumerable.Repeat("", 5)
                .Concat(
                    Enumerable.Range(1, 30)
                    .Select(num => num.ToString()))
                .ToTwoDimensionalArray(7);
            var expectedPosition = new Point(4, 5);

            TestMonthGrid(date, expectedGrid, expectedPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid6Lines() {
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

            TestMonthGrid(date, expectedGrid, expectedPosition);
        }

        [TestMethod]
        public void GenerateMonthGrid4Lines() {
            var date = DateTime.Parse("2010-02-02");
            var expectedGrid = Enumerable.Range(1, 28)
                .Select(num => num.ToString())
                .ToTwoDimensionalArray(7);
            var expectedPosition = new Point(0, 1);

            TestMonthGrid(date, expectedGrid, expectedPosition);
        }
    }
}
