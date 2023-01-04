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
    public partial class Ayarlar : Form
    {
        FirestoreDb database;
        public Ayarlar()
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

        static public string kullanici;
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            combobox();
            Tum_Uyeleri_Al();
            label10.Text = kullanici;
        }
        async void combobox()//comboboxa yükleme fonnksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Kullanicilar");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CKullanici uyeler = docsnap.ConvertTo<CKullanici>();
                if (docsnap.Exists)
                {

                    combokul.Items.Add(uyeler.KullaniciAdi.ToString());

                    sayac++;

                }

            }
        }
        async void Tum_Uyeleri_Al()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Kullanicilar");       //Yöneticileri listviewa listeletiyoruz
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CKullanici uyeler = docsnap.ConvertTo<CKullanici>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(uyeler.Yetki.ToString());
                    listView1.Items[sayac].SubItems.Add(uyeler.KullaniciAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(uyeler.Sifre.ToString());


                    // listView1.Items[sayac].SubItems.Add(["Soyad"].ToString());
                    sayac++;

                }

            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        async void uye_gncl()//Üye güncelleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Sifre", yenisiftxt.Text },
                {"Yetki", guncelYetki.Text  },
            };

            Query Qref = database.Collection("Kullanicilar")
               .WhereEqualTo("KullaniciAdi", combokul.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);
                    listView1.Items.Clear();
                    Tum_Uyeleri_Al();
                }
            }
        }

        private void btnDegistir_Click(object sender, EventArgs e)//üye güncelleme butonu
        {
            uye_gncl();
            MessageBox.Show(combokul.Text + " " + " isimli üyenin bilgileri güncellendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            combokul.Items.Clear();
            yenisiftxt.Clear();
            guncelYetki.Items.Clear();
            combobox();
        }

        private void combokul_SelectedIndexChanged(object sender, EventArgs e)//personel getirme
        {
            guncelYetki.Items.Clear();
            yenisiftxt.Clear();
            deneme();
            guncelYetki.Items.Add("Yönetici");
            guncelYetki.Items.Add("Personel");
        }
        async void deneme()//Personel Getirme Fonksiyonu
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Kullanicilar")
               .WhereEqualTo("KullaniciAdi", combokul.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap) if (docsnap.Exists)
                {
                    CKullanici uyeler = docsnap.ConvertTo<CKullanici>();
                    if (docsnap.Exists)
                    {

                        yenisiftxt.Text += string.Format("{0}", uyeler.Sifre.ToString());
                        guncelYetki.Text += string.Format("{0}", uyeler.Yetki.ToString());
                    }
                }
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && comboYetki.Text != string.Empty)
            {
                if (textBox2.Text == textBox3.Text)
                {
                    uye_ekle();
                    MessageBox.Show("Kullanıcı başarıyla eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboYetki.Items.Clear();
                    comboYetki.Items.Add("Yönetici");
                    comboYetki.Items.Add("Personel");
                }
                else
                {
                    MessageBox.Show("Şifreler Uyuşmuyor","Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen her alanı doldurunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        async void uye_ekle()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            CollectionReference coll = database.Collection("Kullanicilar");
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {"KullaniciAdi", textBox1.Text },
                {"Sifre", textBox2.Text },
                {"Yetki", comboYetki.Text },

            };
            coll.AddAsync(data);
            listView1.Items.Clear();
            Tum_Uyeleri_Al();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboYetki.Text != string.Empty)
            {
                dokuman_sil();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboYetki.Items.Clear();
                comboYetki.Items.Add("Yönetici");
                comboYetki.Items.Add("Personel");
            }
            else
            {
                MessageBox.Show("Üye Silme işlemi için K.Adi, Şifre ve Yetkisini Giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        async void dokuman_sil()//Kullanıcı silme işlemi için
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Kullanicilar")
                .WhereEqualTo("KullaniciAdi", textBox1.Text)
                .WhereEqualTo("Sifre", textBox2.Text)
                .WhereEqualTo("Yetki", comboYetki.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                
                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                    listView1.Items.Clear();
                    Tum_Uyeleri_Al();
                    sayac++;
                    MessageBox.Show("Kullanıcı Başarıyla Silindi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if(sayac == 0)
            {
                MessageBox.Show("Böyle bir üye yok", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
