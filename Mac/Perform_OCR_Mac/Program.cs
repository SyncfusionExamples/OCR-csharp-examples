// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Mvc;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

//Initialize the OCR processor.
using (OCRProcessor processor = new OCRProcessor())
{
    FileStream fileStream = new FileStream("../../../Input.pdf", FileMode.Open, FileAccess.Read);
    //Load a PDF document.
    PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
    //Set OCR language to process.
    processor.Settings.Language = Languages.English;
    //Process OCR by providing the PDF document.
    processor.PerformOCR(lDoc);
    //Create file stream.
    using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
    {
        //Save the PDF document to file stream.
        lDoc.Save(outputFileStream);
    }
    //Close the document.
    lDoc.Close(true);
}
