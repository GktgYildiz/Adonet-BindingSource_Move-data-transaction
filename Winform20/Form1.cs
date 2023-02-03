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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True; MultipleActiveResultSets = true");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select * from vw_MusterileriGetir2";
            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);

            //benim yaptığım

            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //    SqlDataReader dr = cmd1.ExecuteReader();

            //    dataGridView1.Columns.Add("CompanyName", "Kategoriler");
            //    dataGridView1.Columns.Add("City", "Sehir");
            //    dataGridView1.Columns.Add("Country", "Ulke");
            //    dataGridView1.Columns.Add("Phone", "Telefon");
            //    while (dr.Read())
            //    {
            //        dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            //    }
            //}
            //else
            //{
            //    conn.Close();
            //}

            //diğer method

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlCommand cmd1 = new SqlCommand("sp_MusterileriGetir2", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dap.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("sp_KategoriEkle", conn);
            SqlCommand cmd2 = new SqlCommand("select CategoryName,Description from Categories", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@catName", textBox1.Text);
            cmd1.Parameters.AddWithValue("@desc", textBox2.Text);

            conn.Open();
            cmd1.ExecuteNonQuery();
            SqlDataReader dr = cmd2.ExecuteReader();

            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);

                dataGridView1.DataSource = dt;
                MessageBox.Show("kategori eklendi");
            }
            else
            {
                MessageBox.Show("data yok");
            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn2 = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True; MultipleActiveResultSets = true"))
            {

                conn2.Open();

                SqlTransaction tran = conn2.BeginTransaction();

                SqlCommand cmd1 = new SqlCommand("sp_KategoriEkle", conn2, tran);
                SqlCommand cmd2 = new SqlCommand("select CategoryName,Description from Categories", conn2, tran);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@catName", textBox1.Text);
                cmd1.Parameters.AddWithValue("@desc", textBox2.Text);
                try
                {
                    cmd1.ExecuteNonQuery();
                    SqlDataReader dr = cmd2.ExecuteReader();

                    if (dr.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        dataGridView1.DataSource = dt;
                        MessageBox.Show("kategori eklendi");
                        tran.Commit();
                    }
                    else
                    {
                        MessageBox.Show("data yok");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Beklenmedik bir hatayla karşılaşıldı");
                    tran.Rollback();
                }
                conn2.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn3 = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True; MultipleActiveResultSets = true"))
            {
                if (conn3.State == ConnectionState.Closed)
                {
                    conn3.Open();

                    SqlTransaction tran = conn3.BeginTransaction();

                    SqlCommand cmd1 = new SqlCommand("sp_KategoriEkle", conn3, tran);
                    SqlCommand cmd2 = new SqlCommand("select CategoryName,Description from Categories", conn3, tran);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@catName", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@desc", textBox2.Text);
                    try
                    {
                        cmd1.ExecuteNonQuery();
                        SqlDataReader dr = cmd2.ExecuteReader();

                        if (dr.HasRows)
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            dataGridView1.DataSource = dt;
                            MessageBox.Show("kategori eklendi");
                            tran.Commit();
                        }
                        else
                        {
                            MessageBox.Show("data yok");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Beklenmedik bir hatayla karşılaşıldı");
                        tran.Rollback();
                    }
                }
                else
                {
                    conn3.Close();

                }
            }
        }
    }
}
