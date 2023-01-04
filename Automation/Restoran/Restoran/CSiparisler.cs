using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CSiparisler
    {
        [FirestoreProperty]
        public string MNo { get; set; }
        [FirestoreProperty]
        public string Product { get; set; }
        [FirestoreProperty]
        public string Adet { get; set; }
        [FirestoreProperty]
        public string Fiyat { get; set; }
        [FirestoreProperty]
        public string Tarih { get; set; }
        [FirestoreProperty]
        public string Durum { get; set; }
    }
}
