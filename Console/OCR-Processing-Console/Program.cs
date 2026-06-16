using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;

//Initialize the OCR processor.
using (OCRProcessor processor = new OCRProcessor())
{
    //Load an existing PDF document.
    PdfLoadedDocument document = new PdfLoadedDocument(Path.GetFullPath(@"Data/Input.pdf"));
    //Set OCR language.
    processor.Settings.Language = Languages.English;
    //Perform OCR with input document and tessdata (Language packs).
    processor.PerformOCR(document);
    //Save the PDF document.
    document.Save(Path.GetFullPath(@"Output/Output.pdf"));
    //Close the document.
    document.Close(true);
}