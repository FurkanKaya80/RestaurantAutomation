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

namespace Restoran
{
    public partial class AnaEkran : Form
    {
        FirestoreDb database;
        public AnaEkran()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)//çıkış işlemleri
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        static public string kullanici;
        static public string yetki;
        private void AnaEkran_Load(object sender, EventArgs e)//İlk formdan kullanıcı verisini alıp anaekranda labele yazdırma
        {
            labelkul.Text = kullanici;
            tarihi_gecmis();
            Bos_yap();
        }
        async void tarihi_gecmis()//masa durumu güncelleme fonksiyonu Boş yapma
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");



            Query Qref1 = database.Collection("Rezervasyonlar").WhereNotEqualTo("Ad","");
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                if (docsnap1.Exists)
                {
                    CRezervasyonlar rez = docsnap1.ConvertTo<CRezervasyonlar>();
                    DateTime dt1 = DateTime.Parse(rez.Tarih);
                    DateTime dt2 = DateTime.Now;

                    TimeSpan fark = dt1.Subtract(dt2);
                    if (fark.TotalMinutes < 0)
                    {
                        await docsnap1.Reference.DeleteAsync();
                        sayac++;
                    }


                }



            }
              

        }
        async void Bos_yap()//masa durumu güncelleme fonksiyonu Boş yapma
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Boş" },
            };


            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("Durum", "Açık Rezerve");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMasalar mas = docsnap.ConvertTo<CMasalar>();
                if (docsnap.Exists)
                {
                    Query Qref1 = database.Collection("Rezervasyonlar");
                    QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                   

                    foreach (DocumentSnapshot docsnap1 in snap1)
                    {

                        if (docsnap1.Exists)
                        {
                            CRezervasyonlar rez = docsnap1.ConvertTo<CRezervasyonlar>();
                            if (rez.MNo.ToString() == mas.No.ToString())
                            {
                                sayac++;
                            }


                        }



                    }

                    if (sayac == 0)
                    {
                        await docsnap.Reference.UpdateAsync(data);
                        sayac = 0;
                    }


                }
            }
       
           

        }

        private void btnKilit_Click(object sender, EventArgs e)//Giriş Ekranına dönme işlemi
        {
            this.Hide();
            Form1 giris = new Form1();
            giris.Show();
        }

        private void btnAyar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ayarlar ayar = new Ayarlar();
            ayar.Show();
        }

        private void btnMutfak_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mutfak mut = new Mutfak();
            mut.Show();
        }

        private void btnMusteri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Musteriler mus = new Musteriler();
            mus.Show();
        }

        private void btnMasa_Click(object sender, EventArgs e)
        {
            this.Hide();
            Masalar mas = new Masalar();
            mas.Show();
        }

        private void btnKasa_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kasa kas = new Kasa();
            kas.Show();
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {

            this.Hide();
            Raporlar rap = new Raporlar();
            rap.Show();
        }

        private void btnRezerve_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rezervasyon rez = new Rezervasyon();
            rez.Show();
        }
    }
}
