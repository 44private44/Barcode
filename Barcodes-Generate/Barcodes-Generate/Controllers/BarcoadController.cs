using Barcoad_Entities.BarcoadsModels;
using Barcoad_Entities.DataModels;
using Barcoad_Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Barcodes_Generate.Controllers
{
    public class BarcoadController : Controller
    {
        private ImagesdbContext _context;
        private readonly IGenerateBarcoadRepository _generateBarcoadRepository;
        public BarcoadController(ImagesdbContext context, IGenerateBarcoadRepository generateBarcoadRepository)
        {
            _context = context;
            _generateBarcoadRepository = generateBarcoadRepository;
        }

        public IActionResult GenerateBarcoad()
        {
            return View();
        }

        // Submit Barcoad method
        [HttpPost]
        public IActionResult BarcoadGeneratedData(BarcoadsViewModel data)
        {
            try
            {
                var results = _generateBarcoadRepository.GetAllBarcodes(data);
                var model = new BarcoadsViewModel();
                model.BarcoadData = results.ToList();
                model.barcodeLength = data.barcodeLength;
                model.numBarcodes = data.numBarcodes;   
                return PartialView("_BarcoadGeneratedData", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
