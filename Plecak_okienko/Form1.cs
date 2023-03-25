using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JacekiMarcin;



namespace Plecak_okienko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();

            List<Items> Item = new List<Items>();
            int a = 0;
            int b = 0;
            int seed, amount, limit;
            if (int.TryParse(textBox1.Text, out _) && int.TryParse(textBox2.Text, out _) && int.TryParse(textBox3.Text, out _))
            {
                seed = int.Parse(textBox1.Text);
                amount = int.Parse(textBox2.Text);
                limit = int.Parse(textBox3.Text);
                Generator rng = new Generator(seed);
                Backpack storage = new Backpack(limit);

                //string text_to_print;
                for (int i = 0; i < amount; i++)
                { //list of possible items
                    a = rng.rand(1, 20);
                    b = rng.rand(15, 200);
                    Item.Add(new Items(a, b));
                    textBox4.AppendText((Item[i].worth + "    " + Item[i].weight).ToString() + Environment.NewLine);

                }

                storage.add_items(Item);
                for (int k = 0; k < storage.inside.Count; k++)
                {
                    textBox5.AppendText((storage.inside[k].worth + "    " + storage.inside[k].weight).ToString() + Environment.NewLine);
                }
            }
            else
                textBox4.Text = "Wszystkie podane wartości muszą być liczbami";
        }
    }
}
