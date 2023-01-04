using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{

    [FirestoreData]
    internal class CMusteriler
    {
        [FirestoreProperty]
        public string Ad { get; set; }
        [FirestoreProperty]
        public string Soyad { get; set; }
        [FirestoreProperty]
        public int Telefon { get; set; }
        [FirestoreProperty]
        public string Adres { get; set; }
        [FirestoreProperty]
        public string Mail { get; set; }
    }
}
