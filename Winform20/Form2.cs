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

namespace Winform20
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True;");
        BindingSource bs;
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Categories",conn);
            conn.Open();
            bs= new BindingSource();
            SqlDataReader dr = cmd.ExecuteReader();
            bs.DataSource = dr;
            dataGridView1.DataSource = bs;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

     
    }
}
