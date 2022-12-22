using Syncfusion.OCRProcessor;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;

string path = ("../../Data/.NET-Core/Tesseractbinaries/Linux/");
string docPath = ("../../Data/Input.pdf");

//Initialize the OCR processor
using (OCRProcessor processor = new OCRProcessor(path))
{
    //Load the PDF document 
    FileStream stream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
    PdfLoadedDocument lDoc = new PdfLoadedDocument(stream);

    //Language to process the OCR
    processor.Settings.Language = Languages.English;
    //Process OCR by providing loaded PDF document, Data dictionary and language
    processor.PerformOCR(lDoc, ("../../Data/Tessdata/"));

    //Save the OCR processed PDF document in the disk
    MemoryStream streamData = new MemoryStream();
    lDoc.Save(streamData);
    File.WriteAllBytes("Output.pdf", streamData.ToArray());
    lDoc.Close(true);

}
