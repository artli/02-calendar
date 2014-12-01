using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace CalendarPageGenerator {
    public class PageDrawer {
        public int CellWidth = 60;
        public int CellHeight = 60;
        public Font TextFont = new Font(FontFamily.GenericSansSerif, 20);
        DateTime Date;

        public PageDrawer(DateTime date) {
            Date = date;
        }

        void DrawCenteredText(Graphics g, Rectangle rect, string text) {
            var textSize = g.MeasureString(text, TextFont);
            var dx = (rect.Size.Width - textSize.Width) / 2;
            var dy = (rect.Size.Height - textSize.Height) / 2;
            g.DrawString(text, TextFont, Brushes.Black, rect.X + dx, rect.Y + dy);
        }

        void DrawWeekdays(Graphics g, Rectangle rect) {
            for (int i = 0; i < 7; i++) {
                var weekdayRect = new Rectangle(rect.X + i * CellWidth, rect.Y, CellWidth, CellHeight);
                var weekdayNumberFromSunday = (i == 6) ? 0 : i + 1;
                DrawCenteredText(g, weekdayRect, DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames[weekdayNumberFromSunday]);
            }
        }

        void DrawGrid(Graphics g, Rectangle rect, string[,] grid) {
            var gridHeight = grid.GetLength(0);
            var gridWidth = grid.GetLength(1);
            for (int i = 0; i < gridHeight; i++) {
                for (int j = 0; j < gridWidth; j++) {
                    var cellRect = new Rectangle(rect.X + j * CellWidth, rect.Y + i * CellHeight, CellWidth, CellHeight);
                    DrawCenteredText(g, cellRect, grid[i, j]);
                }
            }
        }

        void DrawCurrentDay(Graphics g, float currentDayBorder, Point currentDayPosition) {
            var x = currentDayPosition.Y * CellWidth;
            var y = (currentDayPosition.X + 1) * CellHeight;
            g.DrawRectangle(new Pen(Brushes.Red, currentDayBorder), x, y, CellWidth - currentDayBorder, CellHeight - currentDayBorder);
        }

        public Bitmap DrawPage() {
            var monthGrid = GridGenerator.GenerateMonthGrid(Date);

            var gridHeight = monthGrid.Grid.GetLength(0);
            var gridWidth = monthGrid.Grid.GetLength(1);

            var pageBitmap = new Bitmap(gridWidth * CellWidth, (gridHeight + 1) * CellHeight);
            using (Graphics g = Graphics.FromImage(pageBitmap)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Wheat);
                var weekdaysRect = new Rectangle(0, 0, gridWidth, CellHeight);
                var gridRect = new Rectangle(0, CellHeight, gridWidth, gridHeight);
                DrawWeekdays(g, weekdaysRect);
                DrawGrid(g, gridRect, monthGrid.Grid);
                DrawCurrentDay(g, 3, monthGrid.CurrentDayPosition);
            }
            return pageBitmap;
        }
    }
}
