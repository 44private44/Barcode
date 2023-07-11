using Barcoad_Entities.BarcoadsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcoad_Repositories.Interfaces
{
    public interface IGenerateBarcoadRepository
    {
        public List<string> GetAllBarcodes(BarcoadsViewModel data);
    }
}
