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
    public partial class Rezervasyon : Form
    {
        FirestoreDb database;
        public Rezervasyon()
        {
            InitializeComponent();
        }
        static public string yetki;
        private void btnGeri_Click(object sender, EventArgs e)
        {
            if(yetki == "Yönetici")
            {
                this.Hide();
                AnaEkran ana = new AnaEkran();
                ana.Show();
            }
            else if (yetki == "Personel")
            {
                this.Hide();
                GAnaEkran Gana = new GAnaEkran();
                Gana.Show();
            }
            
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Rezervasyon_Load(object sender, EventArgs e)
        {
            lv1();
            combomasa();
            dtp1.MinDate = DateTime.Now.AddHours(1);
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
        async void combomasa()//comboboxa masa yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Masalar")
                .OrderBy("No");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMasalar mas = docsnap.ConvertTo<CMasalar>();
                if (docsnap.Exists)
                {
                        comboMasa.Items.Add("Masa:"+mas.No.ToString()+" Kapasite:"+mas.Kapasite.ToString());
                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
            if (textBox3.Text.Length == 0)
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

        private void comboMasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboKap.Items.Clear();
            comboKapp();
        }
        async void comboKapp()//comboboxa masa yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Masalar")
                .OrderBy("No");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMasalar mas = docsnap.ConvertTo<CMasalar>();
                if (docsnap.Exists)
                {
                    if (comboMasa.SelectedItem.ToString().Contains(mas.No) && comboMasa.SelectedItem.ToString().Contains(mas.Kapasite))
                    {
                        a = mas.No.ToString();
                       
                        for(int i = 1; i <= Convert.ToInt32(mas.Kapasite); i++)
                        {
                            comboKap.Items.Add(i.ToString() + " Kişi");
                        }
                       
                    }
                }

            }

        }
        string a = "";
        string b = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count >0 && comboMasa.Text != string.Empty && comboKap.Text != string.Empty && rtAciklama.Text != string.Empty)
            {
                rez_ekle();
                comboKap.Items.Clear();
                comboMasa.Items.Clear();
                rtAciklama.Clear();
                combomasa();
                acik_rezerve();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz ve Müşteriyi Seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void rez_ekle()//rezervasyon ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            CollectionReference coll = database.Collection("Rezervasyonlar");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"MNo", a },
                {"Sayi", b },
                {"Tarih", dtp1.Text.ToString()},
                {"Aciklama", rtAciklama.Text },
                {"Ad", listView1.SelectedItems[0].SubItems[0].Text },
                {"Soyad", listView1.SelectedItems[0].SubItems[1].Text },
                {"Adres",listView1.SelectedItems[0].SubItems[3].Text },
                {"Telefon", listView1.SelectedItems[0].SubItems[2].Text },

            };
            Query Qref = database.Collection("Rezervasyonlar");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    CRezervasyonlar rez = docsnap.ConvertTo<CRezervasyonlar>();
                   if(rez.MNo.ToString() == a)
                    {
                        DateTime dt1 = DateTime.Parse(rez.Tarih);
                        DateTime dt2 = dtp1.Value;

                        TimeSpan fark = dt1.Subtract(dt2);
                        if (fark.TotalHours > 1 || fark.TotalHours < -1)
                            {
                                if(fark.TotalDays != 0)
                                {
                                sayac = 1;
                                coll.AddAsync(data);
                                     MessageBox.Show("Rezervasyon Başarıyla Oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                sayac = 1;
                                MessageBox.Show("Bu masada yakın tarihte rezervasyon vardır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                        }
                            else
                            {
                                sayac = 1;
                                MessageBox.Show("Bu masada yakın tarihte rezervasyon vardır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             }

                    }
                       
                  
                }
            


            }
            if(sayac == 0)
            {
                coll.AddAsync(data);
                sayac = 0;
                MessageBox.Show("Rezervasyon Başarıyla Oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dtp1.Value = DateTime.Now.AddHours(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rezervasyonlar rezs = new Rezervasyonlar();
            rezs.Show();
        }
        async void acik_rezerve()//masa durumu güncelleme fonksiyonu AÇIK REZERVE YAPMA
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Açık Rezerve" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", a)
               .WhereEqualTo("Durum", "Boş");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);

                }
            }

        }
       

        private void comboKap_SelectedIndexChanged(object sender, EventArgs e)
        {
            b = comboKap.Text.ToString();
        }
    }
}
