using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class MusteriIslemleri : Form
    {
        FirestoreDb database;
        public MusteriIslemleri()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Musteriler mus = new Musteriler();
            mus.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)//MÜŞTERİ EKLEME BUTONU
        {
            if (txtAd.Text != string.Empty && txtSoyad.Text != string.Empty && TxtTelefon.Text != string.Empty && rtxtAdres.Text != string.Empty && txtEmail.Text != string.Empty)
            {
                musteri_ekle();
                MessageBox.Show("Müşteri başarıyla eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAd.Clear();
                txtSoyad.Clear();
                txtEmail.Clear();
                rtxtAdres.Clear();
                TxtTelefon.Clear();

            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void musteri_ekle()//MÜŞTERİ EKLEME FONKSİYONU
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            CollectionReference coll = database.Collection("Musteriler");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Ad", txtAd.Text },
                {"Soyad", txtSoyad.Text },
                {"Telefon", Convert.ToInt32(TxtTelefon.Text) },
                {"Adres", rtxtAdres.Text },
                {"Mail", txtEmail.Text },

            };
            coll.AddAsync(data);
        }

        private void TxtTelefon_KeyPress(object sender, KeyPressEventArgs e)//Sadece rakam girdirme
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar !=8)
            {
                e.Handled = true;
            }
        }
        static public int Cevap;
        static public string Ad;
        static public string Soyad;
        static public int Telefon;
        static public string Adres;
        static public string Mail;

        private void btnGuncelle_Click(object sender, EventArgs e)//MÜŞTERİ GÜNCELLEME BUTONU
        {
            if (txtAd.Text != string.Empty && txtSoyad.Text != string.Empty && TxtTelefon.Text != string.Empty && rtxtAdres.Text != string.Empty && txtEmail.Text != string.Empty)
            {
                musteri_gncl();
                txtAd.Clear();
                txtSoyad.Clear();
                txtEmail.Clear();
                rtxtAdres.Clear();
                TxtTelefon.Clear();

            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MusteriIslemleri_Load(object sender, EventArgs e)
        {
            if(Cevap == 1)//ÖNCEKİ FORMDAKİ LİSTVİEWDAN VERİ ÇEKME
            {
                txtAd.Text = Ad;
                txtSoyad.Text = Soyad;
                TxtTelefon.Text += Convert.ToInt32(Telefon);
                rtxtAdres.Text = Adres;
                txtEmail.Text = Mail;
                Cevap = 0;  
            }
        }
        async void musteri_gncl()//Müşteri güncelleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Telefon", Convert.ToInt32(TxtTelefon.Text)  },
                {"Adres", rtxtAdres.Text },
                {"Mai", txtEmail.Text }
            };

            Query Qref = database.Collection("Musteriler")
               .WhereEqualTo("Ad", txtAd.Text)
               .WhereEqualTo("Soyad", txtSoyad.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);
                    sayac++;
                    MessageBox.Show("Müşteri Başarıyla Güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (sayac == 0)
            {
                MessageBox.Show("Böyle Bir Üye Yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
