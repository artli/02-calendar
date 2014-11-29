using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CalendarPageGenerator {
    public class PageDrawer {
        public int CellWidth = 40;
        public int CellHeight = 40;
        public Font TextFont = new Font(FontFamily.GenericSansSerif, 20);
        DateTime Date;

        public PageDrawer(DateTime date) {
            Date = date;
        }

        void DrawText(Graphics g, Rectangle rect, string text) {
            g.DrawString(text, TextFont, Brushes.Black, rect);
        }

        Bitmap DrawGrid(string[,] grid) {
            var gridHeight = grid.GetLength(0);
            var gridWidth = grid.GetLength(1);
            var gridBitmap = new Bitmap(CellWidth * gridWidth, CellHeight * gridHeight);
            using (Graphics g = Graphics.FromImage(gridBitmap)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Wheat);
                for (int i = 0; i < gridHeight; i++) {
                    for (int j = 0; j < gridWidth; j++) {
                        var rect = new Rectangle(j * CellWidth, i * CellHeight, CellWidth, CellHeight);
                        DrawText(g, rect, grid[i, j]);
                    }
                }
            }
            return gridBitmap;
        }

        public Bitmap DrawPage() {
            var monthGrid = GridGenerator.GenerateMonthGrid(Date);
            var gridBitmap = DrawGrid(monthGrid.Grid);
            return gridBitmap;
        }
    }
}
