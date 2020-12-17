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
    public partial class Ogrenci_Not_Ekranı : Form
    {
        public Ogrenci_Not_Ekranı()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-G2SNSLM\SQLEXPRESS;Initial Catalog=BasitEokulUygulaması;Integrated Security=True");
        public string numara;
        private void Ogrenci_Not_Ekranı_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select DersAd, Sınav1, Sınav2, Sınav3, Proje, Ortalama, Durum from Tbl_Not inner join Tbl_Ders on Tbl_Not.DersID=Tbl_Ders.DersID where OgrID = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", numara);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            // forma isim yazdırma

            conn.Open();
            SqlCommand cmd2 = new SqlCommand("Select OgrID, OgrAd, OgrSoyad from Tbl_Ogrenci where OgrID=@p1", conn);
            cmd2.Parameters.AddWithValue("@p1", numara);
            SqlDataReader sdr = cmd2.ExecuteReader();
            
            while(sdr.Read())
            {
                this.Text = sdr[0].ToString() + "-" + sdr[1] + " " + sdr[2];
            }
            conn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            this.Hide();
        }
    }
}
