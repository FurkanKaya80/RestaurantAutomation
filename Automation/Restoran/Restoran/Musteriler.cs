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
using Restoran;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Musteriler : Form
    {

        FirestoreDb database;
        public Musteriler()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaEkran ana = new AnaEkran();
            ana.Show();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            this.Hide();
            MusteriIslemleri isl = new MusteriIslemleri();
            isl.Show();
        }

        private void Musteriler_Load(object sender, EventArgs e)
        {
            lv1();
        }
        async void lv1()//listview1 e  yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Musteriler");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    CMusteriler musteri = docsnap.ConvertTo<CMusteriler>();

                    listView1.Items.Add(musteri.Ad.ToString());
                    listView1.Items[sayac].SubItems.Add(musteri.Soyad.ToString());
                    listView1.Items[sayac].SubItems.Add(musteri.Telefon.ToString());
                    listView1.Items[sayac].SubItems.Add(musteri.Adres.ToString());
                    listView1.Items[sayac].SubItems.Add(musteri.Mail.ToString());

                    sayac++;


                }

            }

        }

        private void btnGuncel_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count >0)
            {
                MusteriIslemleri isl = new MusteriIslemleri();
                MusteriIslemleri.Cevap = 1;
                MusteriIslemleri.Ad = listView1.SelectedItems[0].SubItems[0].Text;
                MusteriIslemleri.Soyad = listView1.SelectedItems[0].SubItems[1].Text;
                MusteriIslemleri.Telefon = Convert.ToInt32(listView1.SelectedItems[0].SubItems[2].Text);
                MusteriIslemleri.Adres = listView1.SelectedItems[0].SubItems[3].Text;
                MusteriIslemleri.Mail = listView1.SelectedItems[0].SubItems[4].Text;
                this.Hide();
                isl.Show();
            }
            else
            {
                MessageBox.Show("Lütfen Güncellemek İstediğiniz Üyeyi Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                musteri_sil();
            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Üyeyi Seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void musteri_sil()//Müşteri silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Musteriler")
                .WhereEqualTo("Ad", listView1.SelectedItems[0].SubItems[0].Text)
                .WhereEqualTo("Soyad", listView1.SelectedItems[0].SubItems[1].Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
  
                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync(); 
                    if(MessageBox.Show("Müşteri Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        listView1.Items.Clear();
                        lv1();
                    }
                  
                }

            }
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//AD TEXTBOXI DEĞİŞİNCE OLAN ŞEYLER
        {
            if (textBox1.Text.Length == 0)
            {
                lv1();
            }
            else
            {

                listView1.Items.Clear();
                ada_gore();
            }
        }
        async void ada_gore()//ADA GÖRE SIRALAMA FONKSİYONU
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Musteriler")
                 .WhereEqualTo("Ad", textBox1.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap) if (docsnap.Exists)
                {
                    CMusteriler musteri = docsnap.ConvertTo<CMusteriler>();
                    if (docsnap.Exists)
                    {
                        listView1.Items.Add(musteri.Ad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Soyad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Telefon.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Adres.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Mail.ToString());

                        sayac++;
                    }
                }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                lv1();
            }
            else
            {

                listView1.Items.Clear();
                soyada_gore();
            }
        }
        async void soyada_gore()//SOAYDA GÖRE SIRALAMA FONKSİYONU
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Musteriler")
                 .WhereEqualTo("Soyad", textBox2.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap) if (docsnap.Exists)
                {
                    CMusteriler musteri = docsnap.ConvertTo<CMusteriler>();
                    if (docsnap.Exists)
                    {
                        listView1.Items.Add(musteri.Ad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Soyad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Telefon.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Adres.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Mail.ToString());

                        sayac++;
                    }
                }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length == 0)
            {
                lv1();
            }
            else
            {

                listView1.Items.Clear();
                tele_gore();
            }
        }
        async void tele_gore()//TELEFONA GÖRE SIRALAMA FONKSİYONU
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Musteriler")
                 .WhereEqualTo("Telefon", Convert.ToInt32(textBox3.Text));
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap) if (docsnap.Exists)
                {
                    CMusteriler musteri = docsnap.ConvertTo<CMusteriler>();
                    if (docsnap.Exists)
                    {
                        listView1.Items.Add(musteri.Ad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Soyad.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Telefon.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Adres.ToString());
                        listView1.Items[sayac].SubItems.Add(musteri.Mail.ToString());

                        sayac++;
                    }
 
                }

        }
       
    }
}
