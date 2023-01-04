using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    public partial class Siparisler : Form
    {
        FirestoreDb database;
        public Siparisler()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count == 0)
            {
                masa_bos_yapl();
            }
            siparis_silO();
            this.Hide();
            Masalar mas = new Masalar();
            mas.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (listView2.Items.Count == 0)
                {
                    masa_bos_yapl();
                }
                siparis_silO();
                Application.Exit();

            }
        }
        static public string masano;
        private void Siparisler_Load(object sender, EventArgs e)
        {
            label2.Text = masano;
            listView2.Items.Clear();
            siparisleri_getir();
            siparis_silO();
        }
        //HESAP MAKİNESİ
      

        private void btn1_Click(object sender, EventArgs e)
        {
            txt1.Text += (1).ToString();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txt1.Text += (2).ToString();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txt1.Text += (3).ToString();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txt1.Text += (4).ToString();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txt1.Text += (5).ToString();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txt1.Text += (6).ToString();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txt1.Text += (7).ToString();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txt1.Text += (8).ToString();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txt1.Text += (9).ToString();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txt1.Text += (0).ToString();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txt1.Clear();
        }

        private void btnMain_Click(object sender, EventArgs e)//ANA YEMEK BUTONU
        {
            listView1.Items.Clear();
            main();
        }
        async void main()//ANA YEMEKLERİ GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "Ana Yemek");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnFastfood_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            fast();
        }
        async void fast()//FAST FOODLARI GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "Fast Food");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            drink();
        }
        async void drink()//İÇECEKLERİ GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "İçecekler");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnSoup_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            soup();
        }
        async void soup()//ÇORBALARI GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "Çorba");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnSalad_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            salad();
        }
        async void salad()//SALATALARI GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "Salata");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }

        private void btnDessert_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            dessert();
        }
        async void dessert()//Tatlıları GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Mutfak").WhereEqualTo("Kategori", "Tatlı");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMutfak menu = docsnap.ConvertTo<CMutfak>();
                if (docsnap.Exists)
                {

                    listView1.Items.Add(menu.YemekAdi.ToString());
                    listView1.Items[sayac].SubItems.Add(menu.Fiyat.ToString());

                    sayac++;

                }

            }

        }
        // public int sayac1 = listView2.Items.Count;
        private void listView1_DoubleClick(object sender, EventArgs e)//lv1 den lv2 ye ürün yollama
        {
            int sayac1 = listView2.Items.Count;
            bool a = false;
            if (txt1.Text == string.Empty)//hesap makinesi txt boş ise tek tek ekliyor
            {
                for (int i = 0; i < listView2.Items.Count; i++)//DÖNGÜYE SOKUYORUZ
                {
                    if (listView2.Items[i].Text == listView1.SelectedItems[0].Text)//EĞER EKLEYECEĞİMİZ ÜRÜN DAHA ÖNCE VARSA ADETİ ARTIYOR SADECE
                    {
                        double y = 0;//ADET FİYATI ÇARPIP EKLİYORUZ VE DİREK FİREBASE CLİCK İLE EKLİYORUZ SONRA DURUM İLE DÜZELTİYORUZ EĞER BUTON BASARSA SİPARİŞ ONAYLANIYOR
                        double x = 0;
                        Double.TryParse(listView2.Items[i].SubItems[1].Text, out x);
                        Double.TryParse(listView1.SelectedItems[0].SubItems[1].Text, out y);
                        ++x;
                        listView2.Items[i].SubItems[2].Text = (x*y).ToString();
                        listView2.Items[i].SubItems[1].Text = "";
                        listView2.Items[i].SubItems[1].Text += x.ToString();
                        sayac1++;
                        a = true;
                        siparis_gncl2();
                    }

                }
                if(a == false) {// Ürün Daha önce eklenmediyse ekleniyor
                    siparis_ekle();
                    listView2.Items.Add(listView1.SelectedItems[0].SubItems[0].Text);
                    listView2.Items[sayac1].SubItems.Add("1");
                    listView2.Items[sayac1].SubItems.Add(listView1.SelectedItems[0].SubItems[1].Text);
                    sayac1++;
                    //a = false;
                }
               
            }
            else
            {
                for (int i = 0; i < listView2.Items.Count; i++)//DÖNGÜYE SOKUYORUZ
                {
                    if (listView2.Items[i].Text == listView1.SelectedItems[0].Text)//EĞER EKLEYECEĞİMİZ ÜRÜN DAHA ÖNCE VARSA ADETİ ARTIYOR SADECE
                    {//AYNI OLAYLAR AMA DİREK YÜKSEK ADET GİRMELİ HALİ
                        double x = 0;
                        double y = 0;
                        double z = 0;
                        Double.TryParse(listView2.Items[i].SubItems[1].Text, out x);
                        Double.TryParse(listView1.SelectedItems[0].SubItems[1].Text, out z);
                        Double.TryParse(txt1.Text, out y);
                        x = x+y ;
                        listView2.Items[i].SubItems[2].Text = (x*z).ToString();
                        listView2.Items[i].SubItems[1].Text = "";
                        listView2.Items[i].SubItems[1].Text += x.ToString();
                        sayac1++;
                        a = true;
                        txt1.Clear();
                        siparis_gncl2();
                    }

                }
                if (a == false)
                {// Ürün Daha önce eklenmediyse ekleniyor
                    double y = 0;
                    double x = 0;
                    Double.TryParse(txt1.Text, out x);
                    Double.TryParse(listView1.SelectedItems[0].SubItems[1].Text, out y);

                    listView2.Items.Add(listView1.SelectedItems[0].SubItems[0].Text);
                    listView2.Items[sayac1].SubItems.Add(txt1.Text);
                    listView2.Items[sayac1].SubItems.Add((x*y).ToString());
                    sayac1++;
                    siparis_ekle();
                    txt1.Clear();
                }
            }

        } 


        private void listView2_DoubleClick(object sender, EventArgs e)//lv2 den silme işlemi
        {
            int sayac1 = listView2.Items.Count;
            if (listView2.SelectedItems.Count > 0)
            {
                if(txt1.Text == string.Empty)
                {
                    for(int i = 0;  i < listView2.Items.Count; i++)//ÖNCE DÖNGÜ
                    {
                        double x = 0;
                        Double.TryParse(listView2.Items[i].SubItems[1].Text, out x);//adeti int çevirip x e atıyoruz
                        if ((listView2.Items[i].Text == listView2.SelectedItems[0].Text) &&  x > 1)//tek tek siliyoruz adet eksiltiyoruz
                        {
                            double y = 0;
                            Double.TryParse(listView2.SelectedItems[0].SubItems[2].Text, out y);
                            y = y / x;
                            --x;
                            listView2.Items[i].SubItems[1].Text = "";
                            listView2.Items[i].SubItems[1].Text += x.ToString();
                            listView2.Items[i].SubItems[2].Text = "";
                            listView2.Items[i].SubItems[2].Text += (x*y).ToString();
                            siparis_gncl();//ÇİFT TIKLAYARAK ÇIKARTIYORUZ BURDA DA VERİTABANINDAN EKLEMENİN TERSİ İŞTE
                        }
                       else if (listView2.Items[i].Text == listView2.SelectedItems[0].Text)//Zaten tekse listeden siliyoruz
                        {
                            siparis_sil();
                            listView2.Items.RemoveAt(i);
                            sayac1--;     
                            break;
                        }
                    }
                   
                }
                else
                {
                    for (int i = 0; i < listView2.Items.Count; i++)//ÖNCE DÖNGÜ
                    {
                       double x = 0;
                       Double.TryParse(listView2.Items[i].SubItems[1].Text, out x);//adeti int çevirip x e atıyoruz
                        double y = 0;
                        Double.TryParse(txt1.Text, out y);
                        double z = 0;
                        Double.TryParse(listView2.SelectedItems[0].SubItems[2].Text, out z);
                        if ((listView2.Items[i].Text == listView2.SelectedItems[0].Text) && y < x)//textboxtaki kadar siliyoruz adet eksiltiyoruz
                        {
                            z = z / x;
                            x = x - y;
                            listView2.Items[i].SubItems[1].Text = "";
                            listView2.Items[i].SubItems[1].Text += x.ToString();
                            listView2.Items[i].SubItems[2].Text = "";
                            listView2.Items[i].SubItems[2].Text += (x * z).ToString();
                            txt1.Clear();
                            siparis_gncl();
                        }
                        else if ((listView2.Items[i].Text == listView2.SelectedItems[0].Text) && y == x)//textboxla eşitse  listeden siliyoruz
                        {
                            siparis_sil();
                            listView2.Items.RemoveAt(i);
                            sayac1--;
                            txt1.Clear();
                            break;
                        }
                        else if((listView2.Items[i].Text == listView2.SelectedItems[0].Text) && y > x)//textboxtaki sayı siparişden fazlaysa hata veriyoruz
                        {
                            MessageBox.Show("Sipariş sayısından daha fazla ürünü silmeye çalışıyorsunuz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt1.Clear();
                        }
                    }
                }
            }
            
        }
        static public string yetki;
        private void btnSiparis_Click(object sender, EventArgs e)
        {
            if(listView2.Items.Count > 0)//lv2 nin içi doluysa 
            {
                   Sdurum_gncl();
                    Bmasa_gncl();
                Rezerve_gncl();
                // siparis_ekle();
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
            if (listView2.Items.Count == 0)
            {
                masa_bos_yapl();
            }


        }
        async void Sdurum_gncl()//Sipariş DURUMU güncelleme fonksiyonu silinenler için
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");


            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Onaylı"},
            };

            Query Qref1 = database.Collection("Siparisler")
             .WhereEqualTo("Durum", "Onaylanmadı")
             .WhereEqualTo("MNo", masano);
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                if (docsnap1.Exists)
                {
                    await docsnap1.Reference.UpdateAsync(data);
                }

            }

        }
        async void siparis_ekle()//Sipariş ekleme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            string a = "";
            if(txt1.Text == "")
            {
                a = "1";
            }
            else
            {
                a = txt1.Text;
            }

                CollectionReference coll = database.Collection("Siparisler");
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"MNo", masano },
                {"Product", listView1.SelectedItems[0].Text },
                {"Adet", a},
                 {"Fiyat", (Convert.ToDouble(listView1.SelectedItems[0].SubItems[1].Text)*Convert.ToInt32(a)).ToString()},
                {"Tarih", DateTime.Now.ToString("dd/MM/yyyy HH:mm")},
                {"Durum", "Onaylanmadı" }
            };
                coll.AddAsync(data);

            

        }
        async void siparis_sil()//Sipariş silme fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("Product", listView2.SelectedItems[0].Text)
                .WhereEqualTo("MNo", masano);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                }

            }
        }
        async void siparis_silO()//ONAYLANMAYAN Siparişleri silme fonksiyonu buton basılmamışsa geri gittiğinizde veya kapattığınızda siliniyor
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("Durum", "Onaylanmadı")
                .WhereEqualTo("MNo", masano);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.DeleteAsync();
                }

            }
        }
        async void siparis_gncl()//Sipariş güncelleme fonksiyonu silinenler için listview1 ve 2 deki çift tıklama olayları için
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");


            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Adet", listView2.SelectedItems[0].SubItems[1].Text},
                {"Fiyat", listView2.SelectedItems[0].SubItems[2].Text },
            };

            Query Qref1 = database.Collection("Siparisler")
             .WhereEqualTo("Product", listView2.SelectedItems[0].SubItems[0].Text)
             .WhereEqualTo("MNo", masano);
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                if (docsnap1.Exists)
                {
                    await docsnap1.Reference.UpdateAsync(data);
                }

            }

        }
        async void siparis_gncl2()//Sipariş güncelleme fonksiyonu SONRADAN EKLENMELER İÇİN
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            for (int i = 0; i < listView2.Items.Count; i++)
            {

                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Adet", listView2.Items[i].SubItems[1].Text},
                {"Fiyat", listView2.Items[i].SubItems[2].Text },
            };

                Query Qref1 = database.Collection("Siparisler")
                 .WhereEqualTo("Product", listView2.Items[i].SubItems[0].Text)
                 .WhereEqualTo("MNo", masano);
                QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap1 in snap1)
                {

                    if (docsnap1.Exists)
                    {
                        await docsnap1.Reference.UpdateAsync(data);
                    }

                }
            }

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
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"Durum", "Açık Rezerve" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", masano);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    Query Qref1 = database.Collection("Rezervasyonlar");
                    QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();


                    foreach (DocumentSnapshot docsnap1 in snap1)
                    {

                        if (docsnap1.Exists)
                        {
                            CRezervasyonlar rez = docsnap1.ConvertTo<CRezervasyonlar>();
                            if (rez.MNo.ToString() == masano)
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
                    else if (sayac > 0)
                    {
                        await docsnap.Reference.UpdateAsync(data1);
                        sayac = 0;
                    }

                }
            }


        }
        async void Bmasa_gncl()//masa durumu güncelleme fonksiyonu BOŞ İSE DOLU YAPMA
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Dolu" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", masano)
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
        async void Rezerve_gncl()//masa durumu güncelleme fonksiyonu REZERVE İSE AÇIK REZERVE YAPACAK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            int sayac = 0;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"Durum", "Rezerve" },
            };

            Query Qref = database.Collection("Masalar")
               .WhereEqualTo("No", masano)
               .WhereEqualTo("Durum", "Açık Rezerve");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {

                if (docsnap.Exists)
                {
                    await docsnap.Reference.UpdateAsync(data);

                }
            }

        }


        async void siparisleri_getir()//EĞER SİPARİŞ BUTONUNA BASILMIŞSA SİPARİŞ DURUMU ONAYLI OLUYOR VE TEKRAR GİRDİĞİMİZDE ÖNÜMÜZE ÇIKIYOR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Siparisler")
                .WhereEqualTo("MNo", masano)      
                .WhereEqualTo("Durum", "Onaylı");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSiparisler sip = docsnap.ConvertTo<CSiparisler>();
                if (docsnap.Exists)
                {

                    listView2.Items.Add(sip.Product.ToString());
                    listView2.Items[sayac].SubItems.Add(sip.Adet.ToString());
                    listView2.Items[sayac].SubItems.Add(sip.Fiyat.ToString());

                    sayac++;

                }
            }
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            this.Hide();
            Odeme ode = new Odeme();
            ode.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }
/* */
