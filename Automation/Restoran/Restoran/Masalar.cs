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
using Google.Type;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using DateTime = System.DateTime;

namespace Restoran
{
    public partial class Masalar : Form
    {
        FirestoreDb database;
        public Masalar()
        {
            InitializeComponent();
        }
        static public string yetki;
        private void btnGeri_Click(object sender, EventArgs e)
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

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void masa1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            Siparisler.masano = "1";
            sip.Show();
        }

        private void masa2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            Siparisler.masano = "2";
            sip.Show();
        }

        private void masa3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            Siparisler.masano = "3";
            sip.Show();
        }

        private void masa4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            Siparisler.masano = "4";
            sip.Show();
        }

        private void masa5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            Siparisler.masano = "5";
            sip.Show();
        }

        private void masa6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            int uzunluk = masa6.Text.Length;
            Siparisler.masano = "6";
            sip.Show();
        }

        private void Masalar_Load(object sender, EventArgs e)
        {
            masadurum_getir();
            masatarih_getir();
           

        }
        async void masadurum_getir()//MASA DURUMLARINA BAĞLI OLARAK MASALARIN BOŞ VEYA DOLU OLMASI
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Masalar");//MASALAR KOLEKSİYONUNU ÇAĞIRIYORUZ
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CMasalar mas = docsnap.ConvertTo<CMasalar>();
                if (docsnap.Exists)
                {
                    foreach (Control item in this.Controls)//DÖNGÜYE SOKUYORUZ BUTONLARI
                    {
                        if (item is Button)
                        {
                            if (item.Name == "masa" + mas.No.ToString() && mas.Durum.ToString() == "Dolu")//MASA ADI İLE BUTON ADINDAKİ SAYI AYNIYSA VE DOLUYSA arkaplan değiş
                            {
                                item.BackgroundImage = Properties.Resources.red;
                            }
                            else if (item.Name == "masa" + mas.No.ToString() && mas.Durum.ToString() == "Boş")
                            {
                                item.BackgroundImage = Properties.Resources.green;
                            }
                            else if (item.Name == "masa" + mas.No.ToString() && mas.Durum.ToString() == "Rezerve")
                            {
                                item.BackgroundImage = Properties.Resources.yellow;
                            }
                            else if (item.Name == "masa" + mas.No.ToString() && mas.Durum.ToString() == "Açık Rezerve")
                            {
                                item.BackgroundImage = Properties.Resources.blue;
                            }
                        }
                    }

                }
            }
            
        }
        async void masatarih_getir()//MASA DURUMLARINA BAĞLI OLARAK MASALARIN BOŞ VEYA DOLU OLMASI
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            Query Qref1 = database.Collection("Siparisler").OrderBy("Tarih"); //ASCENDİNG SIRALIYORUZ TARİHLERİ
                // .WhereEqualTo("Durum", "Onaylı");
                // .WhereEqualTo("MNo", i);
                QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap1 in snap1)
                {
                    CSiparisler sip = docsnap1.ConvertTo<CSiparisler>();
                    if (docsnap1.Exists)
                    {
                        
                        foreach (Control item in this.Controls)//Yine döngüye sokuyoruz
                        {
                            if (item is Button)
                            {
                                if (item.Name == "masa" + sip.MNo.ToString() && sip.Durum == "Onaylı")//masa no ile sipariş durumu eşleşiyorsa alıyoruz
                                {
                                    DateTime dt1 = DateTime.Now;
                                    DateTime dt2 = DateTime.Parse(sip.Tarih);//şimdiki zamanla sipariş zamanını alıyoruz aşağıda çıkartıyoruz

                                    TimeSpan fark = dt1.Subtract(dt2);

                                    item.Text = String.Format("{0}{1}{2}",//Butonun textine yazdırıyoruz.
                                        fark.Days > 0 ? String.Format("{0} gün", fark.Days) : "",
                                        fark.Hours > 0 ? String.Format("{0} Saat", fark.Hours) : "",
                                        fark.Minutes > 0 ? String.Format("{0} Dakika", fark.Minutes) : "").Trim() + "\n\n\nMasa" + sip.MNo.ToString();
                                }

                            }
                        }
                           
                        }

                    }
                
            }

        private void masa7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            int uzunluk = masa7.Text.Length;
            Siparisler.masano = "7";
            sip.Show();
        }

        private void masa8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            int uzunluk = masa8.Text.Length;
            Siparisler.masano = "8";
            sip.Show();
        }

        private void masa9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            int uzunluk = masa9.Text.Length;
            Siparisler.masano = "9";
            sip.Show();
        }

        private void masa10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Siparisler sip = new Siparisler();
            int uzunluk = masa10.Text.Length;
            Siparisler.masano = "10";
            sip.Show();
        }
    }
    }

