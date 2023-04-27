using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Threads2 {
    internal static class Program {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main() {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Bitmap image = LoadPgmImage("C:\\Users\\jacek\\source\\repos\\Net\\Threads2\\Threads2\\kubus2.pgm");


            // Create a new form and picture box
            Form form = new Form();
            PictureBox pictureBox1 = new PictureBox();

            // Set the picture box size to match the image size
            pictureBox1.Size = image.Size;

            // Set the image as the picture box content
            pictureBox1.Image = image;

            // Add the picture box to the form and show the form
            form.Controls.Add(pictureBox1);
            System.Windows.Forms.Application.Run(form);
        }

        public static Bitmap LoadPgmImage(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                // PGM format header
                var firstLine = reader.ReadLine();
                Console.WriteLine("First line: {0}", firstLine);
                if (!firstLine.StartsWith("P2")) {
                    throw new ArgumentException("Invalid PGM format.");
                }

                // Read image size
                var sizeLine = reader.ReadLine().Split(' ');
                var width = int.Parse(sizeLine[0]);
                var height = int.Parse(sizeLine[2]);
                Console.WriteLine("Width: {0}, Height: {1}", width, height);

                // Read maximum color value
                var maxColorValueLine = reader.ReadLine();
                Console.WriteLine("Maximum color value line: {0}", maxColorValueLine);
                var maxColorValue = int.Parse(maxColorValueLine);

                // Read image data
                var bitmap = new Bitmap(width, height);
                for (int y = 0; y < height; y++) {
                    var pixelValueLine = reader.ReadLine().Split(' ');
                    int counter = 0;
                    for (int x = 0; x < width; x++)
                    {
                        var pixelValue = int.Parse(pixelValueLine[counter]);
                        var colorValue = (int)(pixelValue / (double)maxColorValue * 255);
                        var color = Color.FromArgb(colorValue, colorValue, colorValue);
                        bitmap.SetPixel(x, y, color);
                        counter += 2;
                    }
                }

                return bitmap;
            }
        }

    }
}

