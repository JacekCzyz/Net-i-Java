﻿using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LAB_API
{
    public partial class Form1 : Form
    {
        private Rates_db rates;  

        Currencies currency;
        public Form1()
        {
            InitializeComponent();
            currency = new Currencies();
            currency.Fill_Mone();
            rates = new Rates_db();
            fill_combobox();
        }


        private void fill_combobox()
        {
            Dictionary<string, string> text1 = currency.return_Mone();

            comboBox1.DataSource = new BindingSource(text1, null);
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "key";



            comboBox2.DataSource = new BindingSource(text1, null);
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "key";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string from_currency = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string to_currency = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            double amount = double.Parse(textBox1.Text);
            double converted=currency.Convert(from_currency, to_currency, amount);
            textBox2.Text = converted.ToString();

            rates.Curency_rates.Add(new Rates { from = from_currency, to = to_currency, rate = (converted / amount) });
            rates.SaveChanges();

            var students = (from s in rates.Curency_rates select s).ToList<Rates>();
            foreach (var st in students)
            {
                Console.WriteLine("ID: {0}, Name: {1}, Avg: {2}, dsds:{3}", st.ID, st.from, st.to, st.rate);
            }
            Console.ReadLine();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_key = comboBox1.SelectedValue.ToString();
            string selected_value = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_key = comboBox2.SelectedValue.ToString();
            string selected_value = comboBox2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string temp = comboBox1.Text;
            comboBox1.Text = comboBox2.Text;
            comboBox2.Text = temp;
        }
    }
}