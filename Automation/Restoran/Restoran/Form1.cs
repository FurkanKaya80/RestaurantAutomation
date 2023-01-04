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
        private void btnCikis_Click(object sender, EventArgs e)//��k�� Butonu
        {
            if (MessageBox.Show("��kmak �stedi�inizden Emin Misiniz?", "Uyar� !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        async void Giris()//Giri� Fonksiyonu
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"restoran.json"; //Firebase ba�lant�lar�
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("restoran-7dde1");

            int sayac = 0;

            Query Qref = database.Collection("Kullanicilar")//Admin koleksiyonunda kullan�c� ad� ve �ifresi txtlere uyuyorsa kontrol
                .WhereEqualTo("KullaniciAdi", comboKul.Text)
                .WhereEqualTo("Sifre", txtSifre.Text);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            
            foreach (DocumentSnapshot docsnap in snap)//�stteki uyuyorsa d�ng�ye sok
            {
                CKullanici login = docsnap.ConvertTo<CKullanici>();//CKullanici s�n�f�nda get setlerle kullan�c� adi �ifre olu�turuldu firebaseden al�p onlara at�l�p �yle kullan�yoruz
                if (docsnap.Exists)
                {

                    if (comboKul.Text == login.KullaniciAdi && txtSifre.Text == login.Sifre && login.Yetki.ToString()=="Y�netici")//Foreach d�ngs��nden kendi i�inde d�n�yor d�k�manlar� uyuyorsa girdiriyor
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
                MessageBox.Show("Kullan�c� Ad� veya �ifre Hatal�","Hatal� Giri�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       
        private void btnLogin_Click(object sender, EventArgs e)//Giri� butonu
        {
            if(comboKul.Text != string.Empty && txtSifre.Text != string.Empty)//bo� alan yoksa giri� fonksiyonunu �a��r�yor
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
                MessageBox.Show("L�tfen Bo� alanlar� doldurunuz");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)//Firebasedeki �yeleri comboboxa at�yor form y�klenince
        {
            combobox();

        }
        async void combobox()//comboboxa y�kleme fonnksiyonu
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