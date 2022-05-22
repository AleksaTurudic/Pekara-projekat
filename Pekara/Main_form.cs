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
    public partial class Main_form : Form
    {
        int racun = 0;
        public Main_form()
        {
            InitializeComponent();
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            grid_pop();
        }

        public void grid_pop()
        {
            SqlConnection conn = Konekcija.Connect();
            SqlCommand comm = new SqlCommand($"Select ime, cena from proizvod", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable tabela = new DataTable();
            adapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            for (int i = 0; i < dataGridView1.Columns.Count - 1; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                int colw = dataGridView1.Columns[i].Width;
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns[i].Width = colw;
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ime = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ime"].Value.ToString();
            int cena = (int)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["cena"].Value;
            racun += cena;
            listBox1.Items.Add(ime +' '+ cena.ToString() + " din");
            textBox1.Text = $"Vas racun iznosi: {racun} RSD";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                MessageBox.Show("Izaberite proizvode pre nego sto kupite");
            else
            {
                MessageBox.Show("Vas racun iznosi " + racun.ToString() + " din.");
                racun = 0;
                listBox1.Items.Clear();
                textBox1.Text = "Vas racun iznosi: 0 RSD";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            racun = 0;
            listBox1.Items.Clear();
            textBox1.Text = "Vas racun iznosi: 0 RSD";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //admin kontrola ovde da bi se lakse ulazilo (ne mora preko logina)
            Sifarnik sifarnik = new Sifarnik();
            sifarnik.Show();
           
        }

        private void Main_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

     
    }
}
