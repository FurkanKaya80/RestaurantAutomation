using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CMutfak
    {
        [FirestoreProperty]
        public string Kategori { get; set; }
        [FirestoreProperty]
        public string YemekAdi { get; set; }
        [FirestoreProperty]
        public double Fiyat { get; set; }
    }
}
