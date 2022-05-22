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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("unesite mejl i sifru");
                return;
            }
            else
            {
                try
                {
                    SqlConnection veza = Konekcija.Connect();
                    SqlCommand komanda = new SqlCommand($"Select * from korisnik where email='{textBox1.Text}'", veza);
                    SqlDataAdapter adapter = new SqlDataAdapter(komanda);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    int t = tabela.Rows.Count;
                    if (t == 1)
                    {
                        if (string.Compare(tabela.Rows[0]["lozinka"].ToString(), textBox2.Text) == 0)
                        {
                             Main_form Main = new Main_form();
                            Main.Show();
                            this.Hide();                           
                        }
                        else
                        {
                            MessageBox.Show("Pogresna sifra");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nepostojeci mejl");
                        return;
                    }
                }
                catch (Exception greska)
                {
                    MessageBox.Show(greska.Message);
                }
            }
        }
    }
}
