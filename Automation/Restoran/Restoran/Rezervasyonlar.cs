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
using Google.Cloud.Firestore.V1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Rezervasyonlar : Form
    {
        FirestoreDb database;
        public Rezervasyonlar()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rezervasyon rez = new Rezervasyon();
            rez.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Rezervasyonlar_Load(object sender, EventArgs e)
        {
            lv1();
        }
        async void lv1()//listview1 e  yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Rezervasyonlar").WhereNotEqualTo("Ad", "");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CRezervasyonlar rez = docsnap.ConvertTo<CRezervasyonlar>();

                if (docsnap.Exists)
                {
                 
                        lw1.Items.Add(rez.Ad.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Soyad.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Telefon.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Adres.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.MNo.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Sayi.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Aciklama.ToString());
                        lw1.Items[sayac].SubItems.Add(rez.Tarih.ToString());


                        sayac++;
                    

                }

            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lw1.SelectedItems.Count > 0)
            {
                rez_sil();
               
            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Rezervasyonu Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void rez_sil()//Rezervasyon silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Rezervasyonlar")
                .WhereEqualTo("Ad", lw1.SelectedItems[0].SubItems[0].Text)
                .WhereEqualTo("Soyad", lw1.SelectedItems[0].SubItems[1].Text)
                 .WhereEqualTo("Tarih", lw1.SelectedItems[0].SubItems[7].Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    Bos_yap();
                    await docsnap.Reference.DeleteAsync();
                    if (MessageBox.Show("Rezervasyon Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        lw1.Items.Clear();
                        lv1();
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

            Query Qref1 = database.Collection("Rezervasyonlar");
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                if (docsnap1.Exists)
                {
                    CRezervasyonlar rez = docsnap1.ConvertTo<CRezervasyonlar>();
                    if (rez.MNo.ToString() == lw1.SelectedItems[0].SubItems[4].Text)
                    {
                        sayac++;
                    }


                }



            }

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", lw1.SelectedItems[0].SubItems[4].Text)
               .WhereEqualTo("Durum", "Açık Rezerve");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    if (sayac == 1)
                    {
                        await docsnap.Reference.UpdateAsync(data);
                    }


                }
            }

        }
    }
}
