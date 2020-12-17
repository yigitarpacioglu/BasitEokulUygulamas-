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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-G2SNSLM\SQLEXPRESS;Initial Catalog=BasitEokulUygulaması;Integrated Security=True");
        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        public void liste()
        {
            dataGridView1.DataSource = ds.ÖğrenciListesi();
        }

        
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            liste();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Klp", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember="KulupAd";
            comboBox1.ValueMember = "KulupID";
            comboBox1.DataSource = dt;
            conn.Close();
        }
        string c = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {       
            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c);
            liste();
            MessageBox.Show("Kayıt ekleme işlemi tamamlanmıştır");
            
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            liste();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtID.Text = comboBox1.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(byte.Parse(TxtID.Text));
            liste();
            MessageBox.Show("Kayıt silinmiştir");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "Erkek")
                {

                    RBErk.Checked = true;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "Kadın")
                {
                    RBKiz.Checked = true;
                }
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Sıralama gerçekleştirildi");
            }
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtAd.Text, TxtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c, byte.Parse(TxtID.Text));
            liste();
        }

        private void RBKiz_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKiz.Checked == true)
            {
                c = "Kız";
            }
          
    }

        private void RBErk_CheckedChanged(object sender, EventArgs e)
        {
            if (RBErk.Checked == true)
            {
                c = "Erkek";
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= ds.OgrenciGetir(TxtAra.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }
