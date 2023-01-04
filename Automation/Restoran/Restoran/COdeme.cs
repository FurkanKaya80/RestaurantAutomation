using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class COdeme
    {
        [FirestoreProperty]
        public string MNo { get; set; }
        [FirestoreProperty]
        public string Fiyat { get; set; }
        [FirestoreProperty]
        public string AraToplam { get; set; }
        [FirestoreProperty]
        public string KDV { get; set; }
        [FirestoreProperty]
        public string Odemetur { get; set; }
        [FirestoreProperty]
        public string indirim { get; set; }
        [FirestoreProperty]
        public string Tarih { get; set; }
    }
}

