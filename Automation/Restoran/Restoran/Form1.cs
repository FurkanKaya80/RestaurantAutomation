using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace Restoran
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        public Form1()
        {
            InitializeComponent();
        }
        string yetkisi = "";
        private void btnCikis_Click(object sender, EventArgs e)//Çýkýþ Butonu
        {
            if (MessageBox.Show("Çýkmak Ýstediðinizden Emin Misiniz?", "Uyarý !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        async void Giris()//Giriþ Fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json"; //Firebase baðlantýlarý
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            int sayac = 0;

            Query Qref = database.Collection("Kullanicilar")//Admin koleksiyonunda kullanýcý adý ve þifresi txtlere uyuyorsa kontrol
                .WhereEqualTo("KullaniciAdi", comboKul.Text)
                .WhereEqualTo("Sifre", txtSifre.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            
            foreach (DocumentSnapshot docsnap in snap)//üstteki uyuyorsa döngüye sok
            {
                CKullanici login = docsnap.ConvertTo<CKullanici>();//CKullanici sýnýfýnda get setlerle kullanýcý adi þifre oluþturuldu firebaseden alýp onlara atýlýp öyle kullanýyoruz
                if (docsnap.Exists)
                {

                    if (comboKul.Text == login.KullaniciAdi && txtSifre.Text == login.Sifre && login.Yetki.ToString()=="Yönetici")//Foreach döngsüünden kendi içinde dönüyor dökümanlarý uyuyorsa girdiriyor
                    {
                        yetkisi = login.Yetki.ToString();
                        Mutfak.yetki = yetkisi;
                        Masalar.yetki = yetkisi;
                        Rezervasyon.yetki = yetkisi;
                        Siparisler.yetki = yetkisi;
                        Odeme.yetki = yetkisi;
                        this.Hide();
                        AnaEkran ana = new AnaEkran();
                        ana.Show();
                        sayac++;
                    }
                    else
                    {
                        yetkisi = login.Yetki.ToString();
                        Mutfak.yetki = yetkisi;
                        Masalar.yetki = yetkisi;
                        Rezervasyon.yetki = yetkisi;
                        Siparisler.yetki = yetkisi;
                        Odeme.yetki = yetkisi;
                        this.Hide();
                        GAnaEkran garson = new GAnaEkran();
                        garson.Show();
                        sayac++;
                    }

                }
            }
           
            if(sayac == 0)
            {
                MessageBox.Show("Kullanýcý Adý veya Þifre Hatalý","Hatalý Giriþ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       
        private void btnLogin_Click(object sender, EventArgs e)//Giriþ butonu
        {
            if(comboKul.Text != string.Empty && txtSifre.Text != string.Empty)//boþ alan yoksa giriþ fonksiyonunu çaðýrýyor
            {
                Giris();
                AnaEkran.kullanici = comboKul.Text;
                Ayarlar.kullanici = comboKul.Text;
                GAnaEkran.kullanici = comboKul.Text;
                AnaEkran.yetki = yetkisi;
                GAnaEkran.yetki = yetkisi;
            }
            else
            {
                MessageBox.Show("Lütfen Boþ alanlarý doldurunuz");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)//Firebasedeki üyeleri comboboxa atýyor form yüklenince
        {
            combobox();

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

                    comboKul.Items.Add(uyeler.KullaniciAdi.ToString());

                    sayac++;

                }

            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(button1.FlatStyle == FlatStyle.Popup)
            {
                button1.FlatStyle = FlatStyle.Standard;
            }
            else
            {
                button1.FlatStyle = FlatStyle.Popup;
            }
           
            
        }
    }
}