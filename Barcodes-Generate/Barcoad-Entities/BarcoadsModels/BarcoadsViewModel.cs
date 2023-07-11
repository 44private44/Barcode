using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcoad_Entities.BarcoadsModels
{
    public class BarcoadsViewModel
    {
        public int numBarcodes { get; set; } = 0;

        public int barcodeLength { get; set; } = 0;

        public List<string> BarcoadData { get; set; } = new List<string>();
    }
}
