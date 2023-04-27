using System;
using System.CodeDom.Compiler;
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
        public int max_color;

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
                this.max_color = maxColorValue;

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

        public void negative(Bitmap temp, int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    lock(this)
                    {
                        int r = this.image.GetPixel(j, i).R; //one is enough, because R=G=B
                        var Rval = (int)(this.max_color - r);

                        var negat_color = System.Drawing.Color.FromArgb(Rval, Rval, Rval);
                        temp.SetPixel(j, i, negat_color);
                    }
                }
            }
        }

        public void up_filter(Bitmap temp, int prog, int height, int width) {
            for (int i = 0;i < height; i++)
            {
                for (int j = 0; j < width; j++) {
                    lock(this)
                    {
                        int r = this.image.GetPixel(j, i).R;
                        if (r <= (prog * 0.01 * this.max_color))
                        {
                            var color = System.Drawing.Color.FromArgb(0, 0, 0);
                            temp.SetPixel(j, i, color);
                        }
                        else
                        {
                            var color = System.Drawing.Color.FromArgb(this.max_color, this.max_color, this.max_color);
                            temp.SetPixel(j, i, color);
                        }
                    }
                }
            }
        }

        public void down_filter(Bitmap temp, int prog, int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    lock (this)
                    {
                        int r = this.image.GetPixel(j, i).R;
                        if (r >= (prog * 0.01 * this.max_color))
                        {
                            var color = System.Drawing.Color.FromArgb(0, 0, 0);
                            temp.SetPixel(j, i, color);
                        }
                        else
                        {
                            var color = System.Drawing.Color.FromArgb(this.max_color, this.max_color, this.max_color);
                            temp.SetPixel(j, i, color);
                        }
                    }
                }
            }
        }


        public void Contour(Bitmap temp, int height, int width)
        {
            for (int i=0;i< height; i++) 
            {
                for(int j = 0; j < width; j++)
                {
                    lock (this)
                    {
                        int cur_checked = this.image.GetPixel(j, i).R;
                        int result_r;
                        if (i < this.image.Height - 1 && j < this.image.Width - 1) /*konturowanie piskeli "w srodku" tablicy - porownanie z pikselem po prawej i ponizej*/
                        {
                            int cheched_up = this.image.GetPixel(j, i + 1).R;
                            int cheched_right = this.image.GetPixel(j + 1, i).R;

                            if (cheched_up != cur_checked || cheched_right != cur_checked)
                            {
                                result_r = (Math.Abs(cheched_up - cur_checked) + Math.Abs(cheched_right - cur_checked)) / 2;
                                var color = System.Drawing.Color.FromArgb(result_r, result_r, result_r);
                                temp.SetPixel(j, i, color);
                            }
                            else
                            {
                                var color = System.Drawing.Color.FromArgb(0, 0, 0);
                                temp.SetPixel(j, i, color);
                            }
                        }
                        else if (i == this.image.Height - 1 && j < this.image.Width - 1) /*konturowanie w ostatnim rzedzie - porownanie z pikselem po prawej*/
                        {
                            int cheched_right = this.image.GetPixel(j + 1, i).R;

                            if (cheched_right != cur_checked)
                            {
                                result_r = (Math.Abs(cheched_right - cur_checked)) / 2;
                                var color = System.Drawing.Color.FromArgb(result_r, result_r, result_r);
                                temp.SetPixel(j, i, color);
                            }
                            else
                            {
                                var color = System.Drawing.Color.FromArgb(0, 0, 0);
                                temp.SetPixel(j, i, color);
                            }
                        }
                        else if (i < this.image.Height - 1 && j == this.image.Width - 1) /*konturowanie w ostatniej kolumnie - porownanie z pikselem powyzej*/
                        {
                            int cheched_up = this.image.GetPixel(j, i + 1).R;
                            if (cheched_up != cur_checked)
                            {
                                result_r = (Math.Abs(cheched_up - cur_checked)) / 2;
                                var color = System.Drawing.Color.FromArgb(result_r, result_r, result_r);
                                temp.SetPixel(j, i, color);
                            }
                            else
                            {
                                var color = System.Drawing.Color.FromArgb(0, 0, 0);
                                temp.SetPixel(j, i, color);
                            }
                        }
                    }
                }
            }
        } 
    }
}
