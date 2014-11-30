using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalendarPageGenerator {
    public struct MonthGrid {
        public string[,] Grid;
        public Point CurrentDayPosition;
    }

    public static class GridGenerator {
        public static int DayOfWeekNumber(DateTime date) {
            switch (date.DayOfWeek) {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static MonthGrid GenerateMonthGrid(DateTime date) {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var firstDayNumber = DayOfWeekNumber(firstDayOfMonth);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);
            var lastDayNumber = DayOfWeekNumber(lastDayOfMonth);

            var grid = Enumerable.Repeat("", firstDayNumber)
                .Concat(
                    Enumerable.Range(1, daysInMonth)
                    .Select(day => day.ToString()))
                .Concat(
                    Enumerable.Repeat("", 6 - lastDayNumber))
                .ToTwoDimensionalArray(7);
            var currentDayPosition = new Point((date.Day - 1 + DayOfWeekNumber(firstDayOfMonth)) / 7, DayOfWeekNumber(date));
            return new MonthGrid { Grid = grid, CurrentDayPosition = currentDayPosition };
        }
    }
}
