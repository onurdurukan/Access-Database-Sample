
// (1) Onur Durukan - 25.06.2018
// (2) Aşağıdaki Kodların ana hatları, Murat YÜCEDAĞ'ın C# eğitim videolarında anlattığı konulardan yazılmış ve 
//     benim tarafımdan kişisel çalışmalar için üzerinde ekleme çıkarma ve düzenlemeler yapılmıştır. (https://www.youtube.com/channel/UCbkbOlw8snP93RJ2BhH44Qw)
// (3) Kodların çalışması için Masaüstünüzde "Kişiler" adında bir access veri tabanı dosyası oluşturunuz ve 
//     bu dosyanın yolunu (path), aşağıdaki kodlar içerisidne bulunan veri tabanı yolu üzerinde değiştiriniz. Aksi takdirde 
//     programı çalıştırdığınızda form üzerinde sağ üstte "Veri tabanına bağlanılamadı!" uyarısı alacaksınız.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Acess_Veritabanı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

            

        }
       
        //string DatabasePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Durukan\\Desktop\\Kişiler.mdb");
        OleDbCommand komut = new OleDbCommand();
        
        //Show datas in function
        private void verileriGoruntule()
        {
            listView1.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText=("Select * from Informations");
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["ID"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["Yaş"].ToString());
                ekle.SubItems.Add(oku["İlçe"].ToString());

                listView1.Items.Add(ekle);

            }
            baglanti.Close();
        }
        

        private void BtnGoruntule_Click(object sender, EventArgs e)
        {
            verileriGoruntule();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //warning without exception handling
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text)|| String.IsNullOrEmpty(textBox4.Text))
            {

                MessageBox.Show("Bilgiler Boş Bırakılamaz");
                baglanti.Close();

            }
            else { 

                OleDbCommand komut = new OleDbCommand("INSERT INTO Informations (Ad,Soyad,Yaş,İlçe) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                verileriGoruntule();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            DialogResult dialogResult = MessageBox.Show("Kaydı silmek istediğinize emin misiniz? Bu işlem geri alınamaz!!!", "UYARI!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "delete from Informations where ID = " + textBox5.Text + "";
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            textBox5.Clear();
            verileriGoruntule();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CheckConnection_Click(object sender, EventArgs e)
        {

        }
        
    

        private void Form1_Load(object sender, EventArgs e)
        {
            //warning with exception handling
            try
            {
                baglanti.Open();
                CheckConnection.Text = "Veri tabanı bağlantısı başarılı";
                baglanti.Close();
            }
            catch (Exception)
            {
                
                CheckConnection.Text = "Veri tabanına bağlanılamadı!";
            }
            
        }
    }
}

