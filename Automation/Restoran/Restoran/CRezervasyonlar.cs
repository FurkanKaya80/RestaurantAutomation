using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CRezervasyonlar
    {
        [FirestoreProperty]
        public string Tarih { get; set; }
        [FirestoreProperty]
        public string MNo { get; set; }
        [FirestoreProperty]
        public string Sayi { get; set; }
        [FirestoreProperty]
        public string Aciklama { get; set; }
        [FirestoreProperty]
        public string Ad { get; set; }
        [FirestoreProperty]
        public string Soyad { get; set; }
        [FirestoreProperty]
        public string Adres { get; set; }
        [FirestoreProperty]
        public string Telefon { get; set; }
    }
}
