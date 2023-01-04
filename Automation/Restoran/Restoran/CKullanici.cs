using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CKullanici
    {
        [FirestoreProperty]
        public string KullaniciAdi { get; set; }
        [FirestoreProperty]
        public string Sifre { get; set; }
        [FirestoreProperty]
        public string Yetki { get; set; }
    }
}
