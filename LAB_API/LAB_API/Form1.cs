using System; 
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
/*          rates.Curency_rates.RemoveRange(rates.Curency_rates);
            rates.SaveChanges();
            rates.Database.ExecuteSqlCommand("TRUNCATE TABLE [Rates]");*/
            fill_combobox();
            fill_listbox();
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

        private void fill_listbox() {
            var rate = (from s in rates.Curency_rates select s).ToList<Rates>();
            foreach (var st in rate) {
                listBox1.Items.Add(String.Format("ID: {0}, From: {1}, To: {2}, Rate:{3}", st.ID, st.from, st.to, st.rate));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string from_currency = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string to_currency = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            double amount = double.Parse(textBox1.Text);
            double converted = 0;

            if (rates.Curency_rates.Any(r => r.from == from_currency && r.to == to_currency)) {
                MessageBox.Show("Conversion rate already exists in database.");

                var rate = rates.Curency_rates.FirstOrDefault(r => r.from == from_currency && r.to == to_currency);
                converted = amount * rate.rate;
                textBox2.Text = converted.ToString();               
            }

            else {
                converted = currency.Convert(from_currency, to_currency, amount);
                textBox2.Text = converted.ToString();
                rates.Curency_rates.Add(new Rates { from = from_currency, to = to_currency, rate = (converted / amount) });
                rates.SaveChanges();
                listBox1_SelectedIndexChanged(sender, e);
            }
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            listBox1.Items.Clear();
            var rate = (from s in rates.Curency_rates select s).ToList<Rates>();
            foreach (var st in rate) {
                listBox1.Items.Add(String.Format("ID: {0}, From: {1}, To: {2}, Rate:{3}", st.ID, st.from, st.to, st.rate));
            }
        }
    }
}
