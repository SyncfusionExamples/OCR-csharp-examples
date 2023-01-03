using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Azure.WebJobs.Host;

namespace Perform_OCR_Azure_Functions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req, TraceWriter log, ExecutionContext executionContext)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                string path = Path.GetFullPath(Path.Combine(executionContext.FunctionAppDirectory, "bin\\Tesseractbinaries\\Windows"));
                OCRProcessor processor = new OCRProcessor(path);
                FileStream stream = new FileStream(Path.Combine(executionContext.FunctionAppDirectory, "Data", "Input.pdf"), FileMode.Open);
                //Load a PDF document.
                PdfLoadedDocument lDoc = new PdfLoadedDocument(stream);
                //Set OCR language to process.
                processor.Settings.Language = Languages.English;
                //Perform OCR with input document and tessdata (Language packs).
                string ocr = processor.PerformOCR(lDoc, Path.Combine(executionContext.FunctionAppDirectory, "tessdata"));
                //Save the PDF document  
                lDoc.Save(ms);
                ms.Position = 0;
            }
            catch (Exception ex)
            {
                //Add a page to the document.
                PdfDocument document = new PdfDocument();
                PdfPage page = document.Pages.Add();
                //Create PDF graphics for the page.
                PdfGraphics graphics = page.Graphics;
                //Set the standard font.
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 6);
                //Draw the text.
                graphics.DrawString(ex.ToString(), font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));
                ms = new MemoryStream();
                //Save the PDF document.  
                document.Save(ms);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(ms.ToArray());
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Output.pdf"
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            return response;
        }
    }
}
