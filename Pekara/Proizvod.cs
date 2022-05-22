using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pekara
{
    public partial class Proizvod : Form
    {
        int t=0;
        DataTable Tabela;
        public Proizvod()
        {
            InitializeComponent();
        }

        private void Proizvod_Load(object sender, EventArgs e)
        {
            refresh_data();
            Data_input();
        }
        public void refresh_data()
        {
            SqlConnection conn = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from proizvod", conn);
            Tabela = new DataTable();
            adapter.Fill(Tabela);
        }
        public void Data_input()
        {
            if (t == -1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                button1.Enabled = false;
                button2.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
            }
            else
            {
                textBox1.Text = Tabela.Rows[t]["id_proizvoda"].ToString();
                textBox2.Text = Tabela.Rows[t]["ime"].ToString();
                textBox3.Text = Tabela.Rows[t]["tip"].ToString();
                textBox4.Text = Tabela.Rows[t]["cena"].ToString();
                textBox5.Text = Tabela.Rows[t]["kalorije"].ToString();
            }
            if (t == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button6.Enabled = true;
                button7.Enabled = true;
            }
            else if (t == Tabela.Rows.Count - 1)
            {
                button2.Enabled = true;
                button1.Enabled = true;
                button6.Enabled = false;
                button7.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t = 0;
            Data_input();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t--;
            Data_input();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            t = Tabela.Rows.Count - 1;
            Data_input();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            t++;
            Data_input();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string naredba = $"Insert into Proizvod (ime,tip,cena,kalorije) values ('{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";
            SqlConnection conn = Konekcija.Connect();
            SqlCommand adapter = new SqlCommand(naredba, conn);
            try
            {
                conn.Open();
                adapter.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            refresh_data();
            Data_input();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string naredba = "Delete from proizvod where id_proizvoda=" + textBox1.Text;
            SqlConnection conn= Konekcija.Connect();
            SqlCommand adapter = new SqlCommand(naredba, conn);
            try
            {
                conn.Open();
                adapter.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            t--;
            refresh_data();
            Data_input();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string naredba = $"Update Proizvod set ime='{textBox2.Text}', tip='{textBox3.Text}',cena='{textBox4.Text}', kalorije='{textBox5.Text}'" +
                "where id_proizvoda=" + textBox1.Text;
            SqlConnection conn = Konekcija.Connect();
            SqlCommand adapter = new SqlCommand(naredba, conn);
            try
            {
                conn.Open();
                adapter.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            refresh_data();
            Data_input();
        }
    }
}
