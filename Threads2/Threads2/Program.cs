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

            Image_class Picture = new Image_class();
            Picture.LoadPgmImage("C:\\Users\\jacek\\source\\repos\\Net\\Threads2\\Threads2\\kubus2.pgm");


            // Create a new form and picture box
            Form form = new Form();
            PictureBox pictureBox1 = new PictureBox();

            // Set the picture box size to match the image size
            pictureBox1.Size = Picture.image.Size;

            // Set the image as the picture box content
            pictureBox1.Image = Picture.image;

            // Add the picture box to the form and show the form
            form.Controls.Add(pictureBox1);
            System.Windows.Forms.Application.Run(form);


            // Create a new form and picture box
            Form form2 = new Form();
            PictureBox pictureBox2 = new PictureBox();

            // Set the picture box size to match the image size
            pictureBox2.Size = Picture.image.Size;

            // Set the image as the picture box content
            pictureBox2.Image = Picture.image;

            // Add the picture box to the form and show the form
            form.Controls.Add(pictureBox2);
            System.Windows.Forms.Application.Run(form2);
        }
    }
}

