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
    public partial class Sifarnik : Form
    {
        public Sifarnik()
        {
            InitializeComponent();
        }

        private void Sifarnik_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            grid_pop();
        }

        public void grid_pop()
        {
            SqlConnection conn = Konekcija.Connect();
            SqlCommand comm = new SqlCommand($"Select * from {comboBox1.SelectedItem.ToString()}", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable tabela = new DataTable();
            adapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataGridView1.Refresh();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            grid_pop();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Proizvod PForma = new Proizvod();
            PForma.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Select_lokacija form = new Select_lokacija();
            form.Show();
        }

        private void Sifarnik_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void dataGridView1_AutoSizeRowsModeChanged(object sender, DataGridViewAutoSizeModeEventArgs e)
        {

        }
    }
}
