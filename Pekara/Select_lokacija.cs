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
    public partial class Select_lokacija : Form
    {
        public Select_lokacija()
        {
            InitializeComponent();
        }

        private void Select_lokacija_Load(object sender, EventArgs e)
        {
            combo_pop();
        }
        public void combo_pop()
        {
            SqlConnection conn = Konekcija.Connect();
            SqlCommand comm = new SqlCommand("Select * from lokacija", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable tabela = new DataTable();
            adapter.Fill(tabela);
            comboBox1.DataSource = tabela;
            comboBox1.DisplayMember = "adresa";
            comboBox1.ValueMember = "id_lokacija";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lager lager = new Lager((int) comboBox1.SelectedValue);
            lager.Show();
            this.Close();
        }
    }
}
