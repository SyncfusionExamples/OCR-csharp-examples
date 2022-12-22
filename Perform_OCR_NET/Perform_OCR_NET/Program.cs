// See https://aka.ms/new-console-template for more information

using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;

string tesseractBinariesPath = Path.GetFullPath("../../../../../Data/.NET-Core/Tesseractbinaries/Windows");
string tessdataPath = Path.GetFullPath("../../../../../Data/Tessdata");
string inputFilePath = Path.GetFullPath("../../../../../Data/Input.pdf");

//Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll).
using (OCRProcessor processor = new OCRProcessor(tesseractBinariesPath))
{
    //Load an existing PDF document.
    FileStream stream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
    PdfLoadedDocument pdfLoadedDocument = new PdfLoadedDocument(stream);

    //Set OCR language to process.
    processor.Settings.Language = Languages.English;

    //Process OCR by providing the PDF document and Tesseract data.
    processor.PerformOCR(pdfLoadedDocument, tessdataPath);

    //Create file stream.
    using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
    {
        //Save the PDF document to file stream.
        pdfLoadedDocument.Save(outputFileStream);
    }

    //Close the document.
    pdfLoadedDocument.Close(true);
}
