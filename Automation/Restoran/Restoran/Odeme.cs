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
using System.Drawing.Printing;

namespace Restoran
{
    public partial class Odeme : Form
    {
        FirestoreDb database;
        public Odeme()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            sip.Show();
        }

        private void Odeme_Load(object sender, EventArgs e)
        {
            label11.Text = Siparisler.masano;
            siparisleri_getir();
            siparisleri_topla();
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox1.Checked == true)
            {
                textBox1.Visible = true;
            }
            else
            {
                textBox1.Visible = false;
            }
        }
        async void siparisleri_getir()//EĞER SİPARİŞ BUTONUNA BASILMIŞSA SİPARİŞ DURUMU ONAYLI OLUYOR VE TEKRAR GİRDİĞİMİZDE ÖNÜMÜZE ÇIKIYOR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("MNo", label11.Text)
                .WhereEqualTo("Durum", "Onaylı");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSiparisler sip = docsnap.ConvertTo<CSiparisler>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(sip.Product.ToString());
                    listView1.Items[sayac].SubItems.Add(sip.Adet.ToString());
                    listView1.Items[sayac].SubItems.Add(sip.Fiyat.ToString());

                    sayac++;

                }
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        async void siparisleri_topla()//Fiyatı belirliyor 
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("MNo", label11.Text)
                .WhereEqualTo("Durum", "Onaylı");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            double b = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {              
                CSiparisler sip = docsnap.ConvertTo<CSiparisler>();
                if (docsnap.Exists)
                {
                    double a = Convert.ToDouble(sip.Fiyat);
                    b += a;
                    lblFiyat.Text = "";
                    lblFiyat.Text += b.ToString(); ; 

                    sayac++;

                }
            }
            lblTop.Text = (Convert.ToDouble(lblFiyat.Text) - Convert.ToDouble(lblindir.Text)).ToString();

            lblKDV.Text = (Convert.ToDouble(lblTop.Text)*(18.0 / 100.0)).ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//İNDİRİMİ ALMAMIZ İÇİN
        {
            
            if(textBox1.Text != string.Empty)
            {
                lblindir.Text = textBox1.Text;
                siparisleri_topla();
            }
            else
            {
                lblindir.Text = "0";
                siparisleri_topla();
            }
            
        }

        private void btnOzet_Click(object sender, EventArgs e)//HESAP ÖZETİNİ PRİNTLENEBİLİR HALE GETİRİYORUZ
        {

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);

            PrintDialog printdlg = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

            // preview the assigned document or you can create a different previewButton for it
            printPrvDlg.Document = pd;
            printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below

        }
        Font Baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb = new SolidBrush(Color.Black);
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//PRİNT SAYFASINDA YAZANLAR
        {
            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("DPÜ Cafe", Baslik, sb, 350, 100, st);

            e.Graphics.DrawString("_____________________________________________", Baslik, sb, 350, 120, st);
            e.Graphics.DrawString("Ürün Adı            Adet            Fiyat", Baslik, sb, 150, 250, st);
            e.Graphics.DrawString("__________________________________________________________", Baslik, sb, 150, 280, st);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                e.Graphics.DrawString(listView1.Items[i].SubItems[0].Text, icerik, sb, 150, 350 + i * 30, st);
                e.Graphics.DrawString(listView1.Items[i].SubItems[1].Text, icerik, sb, 350, 350 + i * 30, st);
                e.Graphics.DrawString(listView1.Items[i].SubItems[2].Text, icerik, sb, 480, 350 + i * 30, st);

            }
            e.Graphics.DrawString("_____________________________________________________________________________", altBaslik, sb, 150, 350 + 30 * listView1.Items.Count, st);
            e.Graphics.DrawString("İndirim Tutarı : ..............." + lblindir.Text + "TL", altBaslik, sb, 250, 350 + 30 * (listView1.Items.Count + 1), st);
            e.Graphics.DrawString("KDV Tutarı : ..................." + lblKDV.Text + "TL", altBaslik, sb, 250, 350 + 30 * (listView1.Items.Count + 2), st);
            e.Graphics.DrawString("Toplam Tutar : ................." + lblFiyat.Text + "TL", altBaslik, sb, 250, 350 + 30 * (listView1.Items.Count + 3), st);
            e.Graphics.DrawString("Ödediğiniz Tutar : ............." + lblTop.Text + "TL", altBaslik, sb, 250, 350 + 30 * (listView1.Items.Count + 4), st);





        }
        async void masa_bos_yapl()//masa durumu güncelleme fonksiyonu MASAYI BOŞ YAPAR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Boş" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", label11.Text)
               .WhereEqualTo("Durum", "Dolu");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);

                }
            }


        }
        async void masa_bos_yapRez()//masa durumu güncelleme fonksiyonu MASAYI BOŞ YAPAR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;
 

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Boş" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", label11.Text)
               .WhereEqualTo("Durum", "Rezerve");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);
                    Query Qref1 = database.Collection("Rezervasyonlar")
                        .WhereEqualTo("MNo", label11.Text);
                    QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                    int sayac1 = 0;

                    foreach (DocumentSnapshot docsnap1 in snap1)
                    {

                        if (docsnap1.Exists)
                        {
                            if (sayac1 <= 0)
                            {
                                await docsnap1.Reference.DeleteAsync();
                                sayac1++;
                            }
                          

                        }

                    }
                }

            }
        }
        async void siparis_silOnaylı()//ONAYLI Siparişleri silme fonksiyonu buton basılmamışsa geri gittiğinizde veya kapattığınızda siliniyor
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("Durum", "Onaylı")
                .WhereEqualTo("MNo", label11.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                }

            }
        }
        async void Satis_ekle()//Satis ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
           

            for (int i = 0; i < listView1.Items.Count; i++) {

                Query Qref = database.Collection("Mutfak")
               .WhereEqualTo("YemekAdi", listView1.Items[i].Text);
                QuerySnapshot snap = await Qref.GetSnapshotAsync();
                string kat = "";

                foreach (DocumentSnapshot docsnap in snap)
                {
                    CMutfak mut = docsnap.ConvertTo<CMutfak>();
                    if (docsnap.Exists)
                    {
                        kat = mut.Kategori.ToString();
                        CollectionReference coll = database.Collection("Satislar");
                        Dictionary<string, object> data = new Dictionary<string, object>()
             {
                {"MNo", label11.Text },
                {"Product", listView1.Items[i].Text },
                {"Adet", listView1.Items[i].SubItems[1].Text},
                {"Fiyat", listView1.Items[i].SubItems[2].Text},
                {"Kategori", kat},
                {"Tarih", DateTime.Now.ToString("dd/MM/yyyy HH:mm")},
             };
                        coll.AddAsync(data);
                    }
                }


            }



        }
        async void Adisyon_ekle()//Adisyon ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");



                CollectionReference coll = database.Collection("Adisyonlar");
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"MNo", label11.Text },
                {"Fiyat", lblFiyat.Text },
                {"AraToplam", lblTop.Text },
                {"KDV", lblKDV.Text},
                 {"OdemeTur", odemetur},
                {"Tarih", DateTime.Now.ToString("dd/MM/yyyy HH:mm")},
                {"indirim", lblindir.Text},
            };
                coll.AddAsync(data);


        }
        static public string yetki;
        string odemetur = "";//ÖDEME TÜRÜNÜ ALABİLMEMİZ İÇİN
        private void btnHKapat_Click(object sender, EventArgs e)
        {

            if (radioNakit.Checked == true)
            {
                odemetur = "Nakit";
            }
            if (radioKredi.Checked == true)
            {
                odemetur = "Kredi";
            }
            if (radioKupon.Checked == true)
            {
                odemetur = "Kupon";
            }



            if (radioKredi.Checked == true || radioNakit.Checked == true || radioKupon.Checked == true){//HERHANGİ BİR RADİOBUTTON  seçilmişse

                Satis_ekle();
                Adisyon_ekle();
                siparis_silOnaylı();
                masa_bos_yapl();
                masa_bos_yapRez();
                if (MessageBox.Show("Ödeme Başarıyla Alındı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (yetki == "Yönetici")
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

            }
            else
            {
                MessageBox.Show("Lütfen Ödeme türünü seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioNakit_CheckedChanged(object sender, EventArgs e)
        {
          /* if (radioNakit.Checked == true)
            {
                odemetur = "Nakit";
            }*/
        }

        private void radioKredi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioKupon_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
