using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System.IO;

namespace Perform_OCR_Blazor.Data
{
    public class ExportService
    {
        public MemoryStream CreatePdf()
        {
            string docPath = Path.GetFullPath("wwwroot/Data/Input.pdf");
            string tesseractPath = Path.GetFullPath("wwwroot/Data/Tesseractbinaries/Windows");
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
            using (OCRProcessor processor = new OCRProcessor(tesseractPath))
            {
                FileStream fileStream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
                //Load a PDF document
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process
                processor.Settings.Language = Languages.English;
                string tessdataPath = Path.GetFullPath("wwwroot/Data/tessdata");
                //Process OCR by providing the PDF document and Tesseract data
                processor.PerformOCR(lDoc, tessdataPath);
                //Create memory stream.
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream.
                lDoc.Save(stream);
                return stream;
            }
        }

    }
}
