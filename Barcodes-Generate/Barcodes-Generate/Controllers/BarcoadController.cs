using Barcoad_Entities.BarcoadsModels;
using Barcoad_Entities.DataModels;
using Barcoad_Repositories.Interfaces;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Barcodes_Generate.Controllers
{
    public class BarcoadController : Controller
    {
        private ImagesdbContext _context;
        private readonly IGenerateBarcoadRepository _generateBarcoadRepository;
        private IWebHostEnvironment _webHostEnvironment;
        public BarcoadController(ImagesdbContext context, IGenerateBarcoadRepository generateBarcoadRepository, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _generateBarcoadRepository = generateBarcoadRepository;
            _webHostEnvironment = webHostEnvironment;
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

                // Generate Barcode
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "BarCodeFile");
                if (Directory.Exists(path))
                {
                    // Delete all existing files in the folder
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
                // Barcode 
                foreach (string barcodeText in results)
                {
                    GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(barcodeText, BarcodeWriterEncoding.Code128);
                    barcode.ResizeTo(500, 150);
                    barcode.AddBarcodeValueTextBelowBarcode();
                    barcode.ChangeBarCodeColor(Color.Blue);
                    barcode.SetMargins(10);

                    string fileName = $"{barcodeText}.png";
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "BarCodeFile/" + fileName);
                    barcode.SaveAsPng(filePath);
                }
                // Qr code 
                //foreach (string barcodeText in results)
                //{
                //    GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(barcodeText, BarcodeWriterEncoding.QRCode);
                //    barcode.ResizeTo(500, 300);
                //    barcode.AddBarcodeValueTextBelowBarcode();
                //    barcode.SetMargins(10);

                //    string fileName = $"{barcodeText}.png";
                //    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "BarCodeFile", fileName);
                //    barcode.SaveAsPng(filePath);
                //}

                // GEnerate the Qr Code By string
                //string qrCodeText = "Chalo";
                //GeneratedBarcode qrCode = IronBarCode.BarcodeWriter.CreateBarcode(qrCodeText, BarcodeWriterEncoding.QRCode);
                //qrCode.ResizeTo(500, 300);
                //qrCode.SetMargins(10);

                //string qrCodeFileName = $"{qrCodeText}.png";
                //string qrCodeFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "BarCodeFile", qrCodeFileName);
                //qrCode.SaveAsPng(qrCodeFilePath);

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
