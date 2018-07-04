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

namespace IngilizceSozluk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=desktop-ll44d11\\sqlexpress;Initial Catalog=sozluk;Integrated Security=True");

        private void veriler()
        {
            listBox1.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from ingturkce", baglanti);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                listBox1.Items.Add(oku["turkce"].ToString()+oku["ingilizce"].ToString().Trim());
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            try
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("Insert into ingturkce (turkce, ingilizce) values ('" + textBox2.Text + "','" + textBox1.Text + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                textBox1.Clear();
                textBox2.Clear();
                veriler();

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Veritabanı işlemleri");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Update ingturkce set turkce='" + textBox2.Text + "' where ingilizce='" + textBox1.Text + "'", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Clear();
            textBox2.Clear();
            veriler();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("delete from ingturkce where ingilizce='" + textBox1.Text + "'", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Clear();
            veriler();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * from ingturkce where ingilizce like '"+textBox1.Text+"%'",baglanti);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                listBox1.Items.Add(oku["turkce"].ToString() + oku["ingilizce"].ToString());
            }
            baglanti.Close();
        }
    }
}
