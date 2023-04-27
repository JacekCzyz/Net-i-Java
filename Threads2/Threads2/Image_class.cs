using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Threads2
{
    public class Image_class
    {
        public Bitmap image { get; set; }

        public void LoadPgmImage(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                // PGM format header
                var firstLine = reader.ReadLine();
                if (!firstLine.StartsWith("P2"))
                {
                    throw new ArgumentException("Invalid PGM format.");
                }

                // Read image size
                var sizeLine = reader.ReadLine().Split(' ');
                var width = int.Parse(sizeLine[0]);
                var height = int.Parse(sizeLine[2]);

                // Read maximum color value
                var maxColorValueLine = reader.ReadLine();
                var maxColorValue = int.Parse(maxColorValueLine);

                // Read image data
                var bitmap = new Bitmap(width, height);
                for (int y = 0; y < height; y++)
                {
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
                this.image = bitmap;                
            }
        }
     }
}
