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
            string docPath = Path.GetFullPath("../../Data/Input.pdf");
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll).
            using (OCRProcessor processor = new OCRProcessor())
            {
                FileStream fileStream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
                //Load a PDF document.
                PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
                //Set OCR language to process.
                processor.Settings.Language = Languages.English;
                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(lDoc);
                //Create memory stream.
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream.
                lDoc.Save(stream);
                return stream;
            }
        }

    }
}
