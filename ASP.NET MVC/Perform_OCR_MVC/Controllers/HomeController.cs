using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;


namespace Perform_OCR_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PerformOCR()
        {
            string tesseract = Server.MapPath("~/TesseractBinaries/3.05/x86/");
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
            using (OCRProcessor processor = new OCRProcessor(tesseract))
            {
                FileStream fileStream = new FileStream(Server.MapPath("~/Data/Input.pdf"), FileMode.Open, FileAccess.Read);
                //Load a PDF document
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process
                processor.Settings.Language = Languages.English;
                processor.Settings.TesseractVersion = TesseractVersion.Version3_05;
                string tessData = Server.MapPath("~/LanguagePack/");
                //Process OCR by providing the PDF document and Tesseract data
                processor.PerformOCR(lDoc, tessData);
                //Create memory stream
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream
                lDoc.Save(stream);
                lDoc.Close();
                //Set the position as '0'
                stream.Position = 0;
                return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "PerformOCR_Output.pdf");
            }

        }
    }
}