using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Threads2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Image_class Picture = new Image_class();
            Picture.LoadPgmImage("C:\\Users\\jacek\\source\\repos\\Net\\Threads2\\Threads2\\dog2.pgm"); //duzy obraz, wychodzi poza picture boxy. mniejszy - kubus2.pgm
            int height = Picture.image.Height;
            int width = Picture.image.Width;




            Bitmap negat = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap down = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap up = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap cont = new Bitmap(Picture.image.Width, Picture.image.Height);

            DateTime start = DateTime.UtcNow;


            Picture.Contour(cont, height, width);
            Picture.negative(negat, height, width);
            Picture.down_filter(down, 50, height, width);
            Picture.up_filter(up, 50, height, width);

            DateTime end = DateTime.UtcNow;
            TimeSpan duration = end - start;
            textBox1.Text = duration.TotalMilliseconds.ToString();




            pictureBox1.Size = Picture.image.Size;
            pictureBox1.Image = Picture.image; 
            
            pictureBox2.Size = negat.Size;
            pictureBox2.Image = negat;

            pictureBox3.Size = down.Size;
            pictureBox3.Image = down;

            pictureBox4.Size = up.Size;
            pictureBox4.Image = up;

            pictureBox5.Size = cont.Size;
            pictureBox5.Image = cont;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image_class Picture = new Image_class();





            Picture.LoadPgmImage("C:\\Users\\jacek\\source\\repos\\Net\\Threads2\\Threads2\\dog2.pgm");
            int height = Picture.image.Height;
            int width = Picture.image.Width;

            Image_class Picture_neg = new Image_class(Picture);
            Image_class Picture_up = new Image_class(Picture);
            Image_class Picture_down = new Image_class(Picture);


            Bitmap negat = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap down = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap up = new Bitmap(Picture.image.Width, Picture.image.Height);
            Bitmap cont = new Bitmap(Picture.image.Width, Picture.image.Height);

            Thread[] t = new Thread[4];

            t[0] = new Thread(() => Picture_neg.negative(negat, height, width));
            t[1] = new Thread(() => Picture_up.up_filter(up, 50, height, width));
            t[2] = new Thread(() => Picture_down.down_filter(down, 50, height, width));
            t[3] = new Thread(() => Picture.Contour(cont, height, width));


            DateTime start = DateTime.UtcNow;

            for (int i = 0; i<4; i ++) {
                t[i].Start();
            }


            for (int i = 0; i < 4; i++) {
                t[i].Join();
            }

            DateTime end = DateTime.UtcNow;
            TimeSpan duration = end - start;
            textBox1.Text = duration.TotalMilliseconds.ToString();

            pictureBox1.Size = Picture.image.Size;
            pictureBox1.Image = Picture.image;

            pictureBox2.Size = negat.Size;
            pictureBox2.Image = negat;

            pictureBox3.Size = down.Size;
            pictureBox3.Image = down;

            pictureBox4.Size = up.Size;
            pictureBox4.Image = up;

            pictureBox5.Size = cont.Size;
            pictureBox5.Image = cont;


            //DateTime end = DateTime.UtcNow;
            //TimeSpan duration = end - start;
            //textBox1.Text = duration.TotalMilliseconds.ToString();

        }
    }
}
