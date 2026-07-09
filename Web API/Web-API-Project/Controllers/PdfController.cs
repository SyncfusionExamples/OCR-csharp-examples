using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using System.Diagnostics;

namespace Web_API_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {

        private readonly ILogger<PdfController> _logger;

        public PdfController(ILogger<PdfController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/api/Pdf")]
        public IActionResult ConvertHTMLtoPDF()
        {
            using (OCRProcessor processor = new OCRProcessor())
            {
                FileStream fileStream = new FileStream("Input.pdf", FileMode.Open, FileAccess.Read);
                //Load an existing PDF document.
                PdfLoadedDocument document = new PdfLoadedDocument(fileStream);
                //Set OCR language.
                processor.Settings.Language = Languages.English;
                //Perform OCR with input document and tessdata (Language packs).
                processor.PerformOCR(document);
                //Create memory stream.
                MemoryStream stream = new MemoryStream();
                //Save the document to memory stream.
                document.Save(stream);
                stream.Position = 0;
                return File(stream, "application/pdf", "Output.pdf");
            }
        }
    }
}
