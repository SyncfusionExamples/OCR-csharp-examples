using Microsoft.AspNetCore.Mvc;
using Perform_OCR_Azure_App_Services.Models;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System.Diagnostics;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Perform_OCR_Azure_App_Services.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //To get content root path of the project
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult PerformOCR()
        {
            //Initialize the OCR processor with tesseract binaries folder path
            string binaries = Path.Combine(_hostingEnvironment.ContentRootPath, "Tesseractbinaries", "Windows");
            OCRProcessor processor = new OCRProcessor(binaries);
            //Set custom temp file path location
            processor.Settings.TempFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            //Load a PDF document
            FileStream stream1 = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "Input.pdf"), FileMode.Open);
            PdfLoadedDocument lDoc = new PdfLoadedDocument(stream1);
            //Set OCR language to process
            processor.Settings.Language = Languages.English;
            //Perform OCR with input document and tessdata (Language packs)
            string tessdataPath = Path.Combine(_hostingEnvironment.ContentRootPath, "tessdata");
            string ocr = processor.PerformOCR(lDoc, tessdataPath);
            //Save the document. 
            MemoryStream stream = new MemoryStream();
            lDoc.Save(stream);
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "OCR_Azure.pdf");
        }
    }
}