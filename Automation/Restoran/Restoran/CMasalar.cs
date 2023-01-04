using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CMasalar
    {

        [FirestoreProperty]
        public string No { get; set; }
        [FirestoreProperty]
        public string Kapasite { get; set; }
        [FirestoreProperty]
        public string Durum { get; set; }
        [FirestoreProperty]
        public string Tarih { get; set; }
    }
}
