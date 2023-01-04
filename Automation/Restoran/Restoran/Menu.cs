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
using static Grpc.Core.Metadata;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Mutfak : Form
    {
        FirestoreDb database;
        public Mutfak()
        {
            InitializeComponent();
        }
        static public string yetki;
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

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

        private void Mutfak_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//Kategori radiosu
        {
           if (radioButton2.Checked == true)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = true;
                listView1.Visible = false;
                listView2.Visible = true;
                listView2.Items.Clear();
                lv2();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//Ürün ekleme radiosu
        {
            if (radioButton1.Checked == true)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                comboBox1.Items.Clear();
                combokategori();
                listView1.Visible = true;
                listView2.Visible = false;
                listView1.Items.Clear();
                lv1();
            }
        }
        async void lv1()//listview1 e  yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                // CMutfak menu = docsnap.ConvertTo<CMutfak>();
               
                if (docsnap.Exists)
                {
                    CMutfak menu = docsnap.ConvertTo<CMutfak>();
                    if (comboBox1.Items.Contains(menu.Kategori.ToString())) {
                        listView1.Items.Add(menu.Kategori.ToString());
                        listView1.Items[sayac].SubItems.Add(menu.YemekAdi.ToString());
                        listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                        sayac++;

                    }

                }

            }

        }
        async void lv2()//listview2 e  yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Kategoriler");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {
                    int sonuc = sayac;
                    sonuc += 1;
                    listView2.Items.Add(menu.Kategori.ToString());
                    listView2.Items[sayac].SubItems.Add(sonuc.ToString());

                    sayac++;

                }

            }

        }
        async void combokategori()//comboboxa kategorileri yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Kategoriler");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {
                    if (!comboBox1.Items.Contains(menu.Kategori.ToString()))
                    {
                        comboBox1.Items.Add(menu.Kategori.ToString());

                        sayac++;
                    }



                }

            }

        }
        async void yemek_ekle()//yemek ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            CollectionReference coll = database.Collection("Mutfak");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Kategori", comboBox1.Text },
                {"YemekAdi", textBox1.Text },
                {"Fiyat", Convert.ToDouble(textBox2.Text)}

            };
            coll.AddAsync(data);
            MessageBox.Show("Yemek Başarıyla Eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUEkle_Click(object sender, EventArgs e)//yemek ekleme butonu
        {
            if(textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboBox1.Text != string.Empty)
            {
                yemek_ekle();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
                combokategori();
                listView1.Items.Clear();
                lv1();
            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUGuncel_Click(object sender, EventArgs e)//yemek güncelleme butonu
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboBox1.Text != string.Empty)
            {
                yemek_gncl();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
                combokategori();

            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void yemek_gncl()//Yemek güncelleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Kategori", comboBox1.Text },
                {"Fiyat", Convert.ToDouble(textBox2.Text)  },
            };

            Query Qref = database.Collection("Mutfak")
               .WhereEqualTo("YemekAdi", textBox1.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);
                    listView1.Items.Clear();
                    lv1();
                    sayac++;
                    MessageBox.Show("Güncelleme Başarılı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if(sayac == 0)
            {
                MessageBox.Show("Böyle Bir Yemek Yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnUSil_Click(object sender, EventArgs e)//yemek silme butonu
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboBox1.Text != string.Empty)
            {
                yemek_sil();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
                combokategori();
            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void yemek_sil()//Yemek silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Mutfak")
                .WhereEqualTo("YemekAdi", textBox1.Text)
                .WhereEqualTo("Fiyat", Convert.ToDouble(textBox2.Text))
                .WhereEqualTo("Kategori", comboBox1.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                // Culgenuyeler uyeler = docsnap.ConvertTo<Culgenuyeler>();
                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                    listView1.Items.Clear();
                    lv1();
                    sayac++;
                    MessageBox.Show("Yemek Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if (sayac == 0)
            {
                MessageBox.Show("Böyle Bir Yemek Yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnUBul_Click(object sender, EventArgs e)//Yemek bulma kategoriye veya yemek adına göre
        {
            if (comboBox1.Text != string.Empty)
            {
                listView1.Items.Clear();
                lv1_bulK();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
                combokategori();
            }
            else if (textBox1.Text != string.Empty)
            {
                listView1.Items.Clear();
                lv1_bulA();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Items.Clear();
                combokategori();
            }
            else
            {
                MessageBox.Show("Arama İşlemi için Lütfen Kategori veya Yemek Adı seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void lv1_bulK()//listview1 de arama  fonnksiyonu KATEGORİYE GÖRE
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", comboBox1.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.Kategori.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }
        async void lv1_bulA()//listview1 de arama  fonnksiyonu YEMEK ADI GÖRE
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("YemekAdi", textBox1.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.Kategori.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnKEkle_Click(object sender, EventArgs e)//Kategori ekleme butonu
        {
            if (txtKtg.Text != string.Empty)
            {
                kategori_ekle();
                txtKtg.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void kategori_ekle()//Kategori ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            CollectionReference coll = database.Collection("Kategoriler");
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {"Kategori", txtKtg.Text },
            };
            if (!comboBox1.Items.Contains(txtKtg.Text))
            {
                coll.AddAsync(data);
                listView2.Items.Clear();
                lv2();
                MessageBox.Show("Kategori Başarıyla Eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtKtg.Clear();
                MessageBox.Show("Böyle Bir Kategori Zaten Mevcut", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
               
        }

        private void btnKSil_Click(object sender, EventArgs e)//Kategori silme butonu
        {
            if (txtKtg.Text != string.Empty)
            {
                kategori_sil();
                kategoridendolayı_sil();
                txtKtg.Clear();

            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void kategori_sil()//Kategori silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Kategoriler")
                .WhereEqualTo("Kategori", txtKtg.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                    listView2.Items.Clear();
                    lv2();
                    sayac++;
                    MessageBox.Show("Kategori Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (sayac == 0)
            {
                MessageBox.Show("Böyle Bir Kategori Yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        

    }
        async void kategoridendolayı_sil()//Kategoriden dolayı ürün silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");


            Query Qref1 = database.Collection("Mutfak")
               .WhereEqualTo("Kategori", txtKtg.Text);
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();


            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                if (docsnap1.Exists)
                {
                    await docsnap1.Reference.DeleteAsync();
                }
            }
        }
            private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }
    }
}
