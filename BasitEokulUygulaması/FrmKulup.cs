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

namespace BasitEokulUygulaması
{
    public partial class FrmKulup : Form
    {
        public FrmKulup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-G2SNSLM\SQLEXPRESS;Initial Catalog=BasitEokulUygulaması;Integrated Security=True");
        void liste()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Klp", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            
            TxtKulupID.Enabled = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            liste();
            BtnEkle.Cursor = Cursors.Hand;
            BtnListele.Cursor = Cursors.Hand;
            BtnGuncelle.Cursor = Cursors.Hand;
            BtnSil.Cursor = Cursors.Hand;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            liste();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Tbl_Klp(KulupAd) values(@p1)", conn);
                cmd.Parameters.AddWithValue("@p1", TxtKulupAd.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception) 
            {
                MessageBox.Show("Bu alan için tanımlanan maksimum karakter sayısı 30'dur.");              
            }
            liste();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKulupID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKulupAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from Tbl_Klp where KulupID = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", TxtKulupID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            liste();
            MessageBox.Show("Kayıt silme işlemi gerçekleştirilmiştir");
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Tbl_Klp set KulupAd = @p1 where KulupID=@p2", conn);
            cmd.Parameters.AddWithValue("@p1", TxtKulupAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtKulupID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            liste();
            MessageBox.Show("Kayıt güncelleme işlemi gerçekleştirilmiştir.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
