using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace CalendarPageGenerator {
    public class PageDrawer {
        private const int _cellWidth = 60;
        private const int _cellHeight = 60;
        private const float _currentDayBorderThickness = 3;
        private const float _fontSize = 20;
        private readonly Font _textFont;
        private readonly DateTime _date;

        public PageDrawer(DateTime date) {
            _date = date;
            _textFont = new Font(FontFamily.GenericSansSerif, _fontSize);
        }

        private void DrawCenteredText(Graphics g, Rectangle rect, string text) {
            var textSize = g.MeasureString(text, _textFont);
            var dx = (rect.Size.Width - textSize.Width) / 2;
            var dy = (rect.Size.Height - textSize.Height) / 2;
            g.DrawString(text, _textFont, Brushes.Black, rect.X + dx, rect.Y + dy);
        }

        private void DrawWeekdays(Graphics g, Rectangle rect) {
            for (int i = 0; i < 7; i++) {
                var weekdayRect = new Rectangle(rect.X + i * _cellWidth, rect.Y, _cellWidth, _cellHeight);
                var weekdayNumberFromSunday = (i == 6) ? 0 : i + 1;
                DrawCenteredText(g, weekdayRect, DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames[weekdayNumberFromSunday]);
            }
        }

        private void DrawGrid(Graphics g, Rectangle rect, string[,] grid) {
            var gridHeight = grid.GetLength(0);
            var gridWidth = grid.GetLength(1);
            for (int i = 0; i < gridHeight; i++) {
                for (int j = 0; j < gridWidth; j++) {
                    var cellRect = new Rectangle(rect.X + j * _cellWidth, rect.Y + i * _cellHeight, _cellWidth, _cellHeight);
                    DrawCenteredText(g, cellRect, grid[i, j]);
                }
            }
        }

        private void DrawCurrentDay(Graphics g, float currentDayBorder, Point currentDayPosition) {
            var x = currentDayPosition.Y * _cellWidth;
            var y = (currentDayPosition.X + 1) * _cellHeight;
            g.DrawRectangle(new Pen(Brushes.Red, currentDayBorder), x, y, _cellWidth - currentDayBorder, _cellHeight - currentDayBorder);
        }

        public Bitmap DrawPage() {
            var monthGrid = GridGenerator.GenerateMonthGrid(_date);

            var gridHeight = monthGrid.Grid.GetLength(0);
            var gridWidth = monthGrid.Grid.GetLength(1);

            var pageBitmap = new Bitmap(gridWidth * _cellWidth, (gridHeight + 1) * _cellHeight);
            using (Graphics g = Graphics.FromImage(pageBitmap)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Wheat);
                var weekdaysRect = new Rectangle(0, 0, gridWidth, _cellHeight);
                var gridRect = new Rectangle(0, _cellHeight, gridWidth, gridHeight);
                DrawWeekdays(g, weekdaysRect);
                DrawGrid(g, gridRect, monthGrid.Grid);
                DrawCurrentDay(g, _currentDayBorderThickness, monthGrid.CurrentDayPosition);
            }
            return pageBitmap;
        }
    }
}
