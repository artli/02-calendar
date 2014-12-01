using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CalendarPageGenerator;
using System.Drawing.Imaging;
using System.IO;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 1 || args[0] == "/?") {
                Console.WriteLine("You should provide exactly one argument - the date to generate a calendar page for.");
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(args[0], out date)) {
                Console.WriteLine("Error while parsing your date: '" + args[0] + "'");
                return;
            }

            var image = new PageDrawer(date).DrawPage();

            var filename = "calendar.png";
            try {
                var fileInfo = new FileInfo(filename);
                using (var file = fileInfo.OpenWrite())
                    image.Save(file, ImageFormat.Png);
            } catch (IOException ex) {
                Console.WriteLine("Error while writing to " + filename);
            }
        }
    }
}
