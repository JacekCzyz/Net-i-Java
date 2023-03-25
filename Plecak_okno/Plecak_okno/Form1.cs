using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plecak_okno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int seed = int.Parse(textBox1.Text);
            int amount = int.Parse(textBox2.Text);
            int limit = int.Parse(textBox3.Text);

            textBox4.Text = seed.ToString();
        }

    }
}
