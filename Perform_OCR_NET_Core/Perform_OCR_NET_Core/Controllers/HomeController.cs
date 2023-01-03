using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Perform_OCR_NET_Core.Models;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Perform_OCR_NET_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult PerformOCR()
        {
            string tesseractPath = Path.GetFullPath("../../Data/.NET-Core/Tesseractbinaries/Windows");
            string docPath = Path.GetFullPath("../../Data/Input.pdf");
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll).
            using (OCRProcessor processor = new OCRProcessor(tesseractPath))
            {
                FileStream fileStream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
                //Load a PDF document.
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process.
                processor.Settings.Language = Languages.English;
                string tessdataPath = Path.GetFullPath("../../Data/Tessdata");
                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(lDoc, tessdataPath);
                //Create memory stream.
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream.
                lDoc.Save(stream);
                lDoc.Close();
                //Set the position as '0'
                stream.Position = 0;
                //Download the PDF document in the browser.
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
                fileStreamResult.FileDownloadName = "Sample.pdf";
                return fileStreamResult;
            }

        }
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
    }
}
