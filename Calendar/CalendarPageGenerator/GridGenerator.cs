using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalendarPageGenerator {
    public class MonthGrid {
        public readonly string[,] Grid;
        public readonly Point CurrentDayPosition;

        public MonthGrid(string[,] grid, Point currentDayPosition) {
            Grid = grid;
            CurrentDayPosition = currentDayPosition;
        }
    }

    public static class GridGenerator {
        private static readonly Dictionary<DayOfWeek, int> _weekdayNumber = new Dictionary<DayOfWeek, int> {
            {DayOfWeek.Monday, 0},
            {DayOfWeek.Tuesday, 1},
            {DayOfWeek.Wednesday, 2},
            {DayOfWeek.Thursday, 3},
            {DayOfWeek.Friday, 4},
            {DayOfWeek.Saturday, 5},
            {DayOfWeek.Sunday, 6}
        };
        public static int WeekdayNumber(DateTime date) {
            return _weekdayNumber[date.DayOfWeek];
        }

        public static MonthGrid GenerateMonthGrid(DateTime date) {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var firstDayNumber = WeekdayNumber(firstDayOfMonth);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);
            var lastDayNumber = WeekdayNumber(lastDayOfMonth);

            var grid = Enumerable.Repeat("", firstDayNumber)
                .Concat(
                    Enumerable.Range(1, daysInMonth)
                    .Select(day => day.ToString()))
                .Concat(
                    Enumerable.Repeat("", 6 - lastDayNumber))
                .ToTwoDimensionalArray(7);
            var currentDayPosition = new Point((date.Day - 1 + WeekdayNumber(firstDayOfMonth)) / 7, WeekdayNumber(date));
            return new MonthGrid(grid, currentDayPosition);
        }
    }
}
