using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Restoran
{
    [FirestoreData]
    internal class CKategoriler
    {

        [FirestoreProperty]
        public string Kategori { get; set; }
    }
}
