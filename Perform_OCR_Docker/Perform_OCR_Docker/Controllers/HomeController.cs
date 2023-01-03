using Microsoft.AspNetCore.Mvc;
using Perform_OCR_Docker.Models;
using System.Diagnostics;
using Syncfusion.Pdf;
using System.IO;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System.Reflection.Metadata;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Perform_OCR_Docker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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
        public ActionResult PerformOCR()
        {
            string docPath = _hostingEnvironment.WebRootPath + "/Data/Input.pdf";
            string tesseractPath = _hostingEnvironment.WebRootPath + "/Data/Tesseractbinaries/Linux";
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
            using (OCRProcessor processor = new OCRProcessor(tesseractPath))
            {
                FileStream fileStream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
                //Load a PDF document
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process
                processor.Settings.Language = Languages.English;
                string tessdataPath = _hostingEnvironment.WebRootPath + "/Data/tessdata";
                //Process OCR by providing the PDF document and Tesseract data
                processor.PerformOCR(lDoc, tessdataPath);
                //Create memory stream
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream
                lDoc.Save(stream);
                lDoc.Close();
                //Set the position as '0'
                stream.Position = 0;
                //Download the PDF document in the browser
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
                fileStreamResult.FileDownloadName = "Sample.pdf";
                return fileStreamResult;
            }

        }
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
    }
}