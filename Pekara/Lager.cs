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
    public partial class Lager : Form
    {
        int adresa;
        public Lager(int lokacija_id)
        {
            adresa = lokacija_id;
            InitializeComponent();
        }

        private void Lager_Load(object sender, EventArgs e)
        {
            Kolicina.Minimum = -100;
            grid_pop();
        }
        public void grid_pop()
        {
            SqlConnection conn = Konekcija.Connect();
            SqlCommand comm = new SqlCommand($"Select  lager.id_proizvoda, ime, kolicina from lager join proizvod on lager.id_proizvoda=proizvod.id_proizvoda where lager.id_lokacija={adresa}", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable tabela = new DataTable();
            adapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = (int) dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["id_proizvoda"].Value;
            string naredba = $"Update lager set kolicina=kolicina+{Kolicina.Value} " +
                $"where id_proizvoda={id}";
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
            grid_pop();
            Kolicina.Value = 0;
        }
    }
}
