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
using Google.Rpc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restoran
{
    
    public partial class Kasa : Form
    {
        FirestoreDb database;
        public Kasa()
        {
            InitializeComponent();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaEkran ana = new AnaEkran();
            ana.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak İstediğinizden Emin Misiniz?", "Uyarı !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Kasa_Load(object sender, EventArgs e)
        {

        }


        async void gunlukrapor_getir()//GÜNLÜK SATILAN ÜRÜNLERİ VE FİYATI GETİRİYOR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");



            Query Qref = database.Collection("Adisyonlar").OrderByDescending("Tarih");
               // .WhereEqualTo("Tarih",DateTime.Now.ToString("dd/MM/yyyy"));
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            double aratoplam = 0;
            double fiyat = 0;
            double KDV = 0;
            double indirim = 0;
            string total = "";

            foreach (DocumentSnapshot docsnap in snap)
            {

                COdeme ode = docsnap.ConvertTo<COdeme>();
                if (docsnap.Exists)
                {
                    if (ode.Tarih.Contains(DateTime.Now.ToString("dd/MM/yyyy")))
                    {
                        Query Qref1 = database.Collection("Satislar").OrderByDescending("Tarih");
                        QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                        string productsi = "";
                       

                        foreach (DocumentSnapshot docsnap1 in snap1)
                        {
                           
                            CSatislar sat = docsnap1.ConvertTo<CSatislar>();
                            if (docsnap1.Exists)
                            {
                               
                                if (ode.Tarih == sat.Tarih && sat.MNo == ode.MNo)
                                {
                                   

                                    productsi += sat.Product + ", " + sat.Adet + " Adet, " + sat.Fiyat + " TL" + "\n";
                                    sayac++;
                                }


                            }
                        }
                       
                        fiyat += Convert.ToDouble(ode.Fiyat);
                        aratoplam += Convert.ToDouble(ode.AraToplam);
                        KDV += Convert.ToDouble(ode.KDV);
                        indirim += Convert.ToDouble(ode.indirim);

                        dgGun.Rows.Add(ode.AraToplam, ode.KDV, ode.indirim, ode.Fiyat, productsi, ode.Tarih);
                    }
                }
            }
            Query Qref3 = database.Collection("Mutfak");
            QuerySnapshot snap3 = await Qref3.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap3 in snap3)
            {

                CMutfak mut = docsnap3.ConvertTo<CMutfak>();
                if (docsnap3.Exists)
                {

                    Query Qref2 = database.Collection("Satislar")
                    .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    int adet = 0;
                    string product = "";
                    double para = 0;
                    string merge = "";

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap2.Exists)
                        {
                            if (sat2.Tarih.Contains(DateTime.Now.ToString("dd/MM/yyyy")))
                            {
                                product = sat2.Product.ToString();
                                adet += Convert.ToInt32(sat2.Adet);
                                para += Convert.ToDouble(sat2.Fiyat);

                                merge = product + ", " + adet + " Adet, " + para + " TL";
                            }

                        }

                    }
                    if (merge != string.Empty)
                    {
                        total += merge + "\n";
                    }

                }

            }
            for (int i = 0; i < dgGun.Rows.Count; i++)
            {
                if(dgGun.Rows[i].Cells[0].Value == string.Empty)
                {
                    dgGun.Rows.RemoveAt(i);
                }
            }
           if(sayac > 0)
            {
                dgGun.Rows.Add(aratoplam.ToString(), KDV.ToString(), indirim.ToString(), fiyat.ToString(),total,"GÜNLÜK TOPLAM");
            }
          
        }
        async void aylıkrapor_getir()//Aylık SATILAN ÜRÜNLERİ VE FİYATI GETİRİYOR
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");



            Query Qref = database.Collection("Adisyonlar").OrderByDescending("Tarih");
            // .WhereEqualTo("Tarih",DateTime.Now.ToString("dd/MM/yyyy"));
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            double aratoplam = 0;
            double fiyat = 0;
            double KDV = 0;
            double indirim = 0;
            string total = "";
           

            foreach (DocumentSnapshot docsnap in snap)
            {

                COdeme ode = docsnap.ConvertTo<COdeme>();
                if (docsnap.Exists)
                {
                    if (ode.Tarih.Contains(DateTime.Now.ToString("MM/yyyy")))
                    {
                        Query Qref1 = database.Collection("Satislar").OrderBy("Tarih");
                        QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();
                        string productsi = "";
                        

                        foreach (DocumentSnapshot docsnap1 in snap1)
                        {

                            CSatislar sat = docsnap1.ConvertTo<CSatislar>();
                            if (docsnap1.Exists)
                            {
                               
                                if (ode.Tarih == sat.Tarih && sat.MNo == ode.MNo)
                                {
                                      
                                   // total += merge + "\n";
                                    productsi += sat.Product + ", " + sat.Adet + " Adet, " + sat.Fiyat + " TL" + "\n";
                                    sayac++;
                                }     

                            }
                        }
                       
                        fiyat += Convert.ToDouble(ode.Fiyat);
                        aratoplam += Convert.ToDouble(ode.AraToplam);
                        KDV += Convert.ToDouble(ode.KDV);
                        indirim += Convert.ToDouble(ode.indirim);

                        dgAy.Rows.Add(ode.AraToplam, ode.KDV, ode.indirim, ode.Fiyat, productsi, ode.Tarih);
                    }
                }
            }
            Query Qref3 = database.Collection("Mutfak");
            QuerySnapshot snap3 = await Qref3.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap3 in snap3)
            {

                CMutfak mut = docsnap3.ConvertTo<CMutfak>();
                if (docsnap3.Exists)
                {

                    Query Qref2 = database.Collection("Satislar")
                    .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    int adet = 0;
                    string product = "";
                    double para = 0;
                    string merge = "";

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap2.Exists)
                        {
                            if (sat2.Tarih.Contains(DateTime.Now.ToString("MM/yyyy")))
                            {
                                product = sat2.Product.ToString();
                                adet += Convert.ToInt32(sat2.Adet);
                                para += Convert.ToDouble(sat2.Fiyat);

                                merge = product + ", " + adet + " Adet, " + para + " TL";
                            }
                            
                        }
                       
                    }
                    if(merge != string.Empty)
                    {
                        total += merge + "\n";
                    }
                  
                }
               
            }
            
            for (int i = 0; i < dgGun.Rows.Count; i++)
            {
                /*if (dgAy.Rows[i].Cells[0].Value == string.Empty)
                {
                    dgAy.Rows.RemoveAt(i);
                }*/
            }
            if (sayac > 0)
            {
                dgAy.Rows.Add(aratoplam.ToString(), KDV.ToString(), indirim.ToString(), fiyat.ToString(), total, "AYLIK TOPLAM");
            }

        }
        private void btnZ_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = false;
            
            dgAy.Visible = false;
            dgGun.Visible = true;
            dgGun.Rows.Clear();
            gunlukrapor_getir();
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = true;
            dgAy.Visible = true;
            dgGun.Visible = false;
            dgAy.Rows.Clear();
            aylıkrapor_getir();
        }
    }
}
