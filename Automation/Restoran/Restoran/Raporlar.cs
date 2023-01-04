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
using Gigasoft.ProEssentials.Enums;

namespace Restoran
{
    public partial class Raporlar : Form
    {
        FirestoreDb database;
        public Raporlar()
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

        private void btnMain_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "Ana Yemekler";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            main();
        }
        async void main()//ANA YEMEKLERİ GETİRME
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "Ana Yemek");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {

                    if( Convert.ToDateTime(sat.Tarih) >= dTP1.Value && dTP2.Value >= Convert.ToDateTime(sat.Tarih)) {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }
                  



                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "Ana Yemek");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {
               
                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {
                   
                   
                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "Ana Yemek")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }

                        }
                       

                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";

                    
                    oran = (adet / total) * 100;
                   
                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }
          


            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
          //  graf.PeData.Subsets = 2;
          //  graf.PeColor.BitmapGradientMode = false;
          //  graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
          //  graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
         //   graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }
        private void graf_Click(object sender, EventArgs e)
        {

        }

          private void graf_PeDataHotSpot(object sender, Gigasoft.ProEssentials.EventArg.DataHotSpotEventArgs e)
          {
              MessageBox.Show("Subset " + e.SubsetIndex.ToString() +
             ", Point " + e.PointIndex.ToString() + ", " +
                graf.PeData.Y[e.SubsetIndex, e.PointIndex].ToString());
          }

        private void btnFastfood_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "Fast Food";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            fast_food();
        }
        async void fast_food()//FAST FOOD Grafik
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "Fast Food");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                    {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }



                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "Fast Food");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "Fast Food")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }

                        }


                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
          //  graf.PeColor.BitmapGradientMode = false;
          //  graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
          //  graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
           // graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "İçecekler";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            drinks();
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }
        async void drinks()//İÇECEK GRAFİK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "İçecekler");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                    {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }



                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "İçecekler");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "İçecekler")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }

                        }


                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
           // graf.PeColor.BitmapGradientMode = false;
           // graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
          //  graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
           // graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void btnSoup_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "Çorba";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            soup();
        }
        async void soup()//ÇORBA GRAFİK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "Çorba");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                    {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }


                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "Çorba");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "Çorba")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }
                        }


                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
          //  graf.PeColor.BitmapGradientMode = false;
           // graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
         //   graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
         //   graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
           graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void btnSalad_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "Salata";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            salad();
        }
        async void salad()//SALATA GRAFİK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "Salata");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                    {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }




                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "Salata");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "Salata")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }

                        }


                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
         //   graf.PeColor.BitmapGradientMode = false;
          //  graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
         //   graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
         //   graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void btnDessert_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "Tatlı";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;
            dessert();
        }
        async void dessert()//TATLI GRAFİK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar").WhereEqualTo("Kategori", "Tatlı");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                    {

                        total += float.Parse(sat.Adet);
                        //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                        sayac++;
                    }



                }

            }
            Query Qref1 = database.Collection("Mutfak").WhereEqualTo("Kategori", "Tatlı");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CMutfak mut = docsnap1.ConvertTo<CMutfak>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar").WhereEqualTo("Kategori", "Tatlı")
                       .WhereEqualTo("Product", mut.YemekAdi.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }
                        }


                    }
                    graf.PeString.PointLabels[sayac1] = mut.YemekAdi.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
         //   graf.PeColor.BitmapGradientMode = false;
          //  graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
         //   graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
          //  graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            graf.PeString.MainTitle = "TÜM ÜRÜNLER";
            graf.PeString.SubTitle = "Grafiği";
            graf.Visible = true;         
            all_product();
           

        }
        async void all_product()//TÜM ÜRÜN GRAFİK
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");
            Query Qref = database.Collection("Satislar");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();
            int sayac = 0;
            float total = 0;

            foreach (DocumentSnapshot docsnap in snap)
            {
                CSatislar sat = docsnap.ConvertTo<CSatislar>();
                if (docsnap.Exists)
                {
                    if(sat.Adet.ToString() != "") {

                        if (Convert.ToDateTime(sat.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat.Tarih))
                        {

                            total += float.Parse(sat.Adet);
                            //  graf.PeString.SubsetLabels[sayac] = mut.Kategori.ToString();
                            sayac++;
                        }

                    }



                }

            }
            Query Qref1 = database.Collection("Kategoriler");
            //    .WhereEqualTo("Products", mut.YemekAdi.ToString());
            QuerySnapshot snap1 = await Qref1.GetSnapshotAsync();

            int sayac1 = 0;

            foreach (DocumentSnapshot docsnap1 in snap1)
            {

                CKategoriler kat = docsnap1.ConvertTo<CKategoriler>();
                if (docsnap1.Exists)
                {


                    Query Qref2 = database.Collection("Satislar")
                       .WhereEqualTo("Kategori", kat.Kategori.ToString());
                    QuerySnapshot snap2 = await Qref2.GetSnapshotAsync();
                    float oran = 0;
                    int sayac2 = 0;
                    float adet = 0;

                    foreach (DocumentSnapshot docsnap2 in snap2)
                    {

                        CSatislar sat2 = docsnap2.ConvertTo<CSatislar>();
                        if (docsnap1.Exists)
                        {
                            if (Convert.ToDateTime(sat2.Tarih) >= Convert.ToDateTime(dTP1.Text) && Convert.ToDateTime(dTP2.Text) >= Convert.ToDateTime(sat2.Tarih))
                            {
                                adet += float.Parse(sat2.Adet);

                                sayac2++;

                            }
                        }


                    }
                    graf.PeString.PointLabels[sayac1] = kat.Kategori.ToString() + " " + adet.ToString() + "x";


                    oran = (adet / total) * 100;

                    //MessageBox.Show(""+oran+"", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   graf.PeData.X[sayac1,0] = oran;
                    graf.PeData.X[0, sayac1] = oran;
                    // graf.PeData.Y[0,sayac1] = oran;
                    sayac1++;
                    

                }

            }



            graf.PeData.Points = sayac1; // Noktalar = Sütunlar //
                                         //  graf.PeData.Subsets = 2;
         //   graf.PeColor.BitmapGradientMode = false;
         //   graf.PeColor.QuickStyle = Gigasoft.ProEssentials.Enums.QuickStyle.LightShadow;
        //    graf.PeLegend.Location = Gigasoft.ProEssentials.Enums.LegendLocation.Left;
          //  graf.PePlot.DataShadows = Gigasoft.ProEssentials.Enums.DataShadows.ThreeDimensional;

            graf.PeUserInterface.HotSpot.Data = true;
            graf.PeFunction.ReinitializeResetImage();
            graf.Refresh();
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            dTP1.Value = DateTime.Now;
            dTP2.Value = DateTime.Now;
        }
    }
}
