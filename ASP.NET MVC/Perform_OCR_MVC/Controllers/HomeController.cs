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
            string inputFilePath = Server.MapPath("~/Input.pdf/");
            string path = inputFilePath.Replace("Perform_OCR_MVC\\Perform_OCR_MVC\\Input.pdf\\", "");
            Stream fileStream = new FileStream(path + "Data\\Input.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll).
            using (OCRProcessor processor = new OCRProcessor())
            {
                //Load a PDF document.
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process.
                processor.Settings.Language = Languages.English;
                processor.Settings.TesseractVersion = TesseractVersion.Version3_05;           
                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(lDoc);
                //Open the document in browser after saving it.
                lDoc.Save("Output.pdf", HttpContext.ApplicationInstance.Response, Syncfusion.Pdf.HttpReadType.Save);
                //Close the document.
                lDoc.Close(true);
                return View();
            }

        }
    }
}