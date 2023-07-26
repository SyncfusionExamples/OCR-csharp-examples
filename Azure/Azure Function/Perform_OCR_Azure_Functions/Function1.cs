using System;
using System.IO;
using System.Threading.Tasks;
using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using System.Net.Http;
using Syncfusion.Pdf.Parsing;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Perform_OCR_Azure_Functions
{
    public static class Function1
    {
        [FunctionName("Function1")]
       public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req, TraceWriter log, ExecutionContext executionContext)
		{
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Your Licensing Key");
			MemoryStream ms = new MemoryStream();
			try
			{
				OCRProcessor processor = new OCRProcessor();

				processor.Settings.TempFolder = Path.Combine(executionContext.FunctionAppDirectory, "Data");

				FileStream stream = new FileStream(Path.Combine(executionContext.FunctionAppDirectory, "Data", "Region.pdf"), FileMode.Open);

				PdfLoadedDocument lDoc = new PdfLoadedDocument(stream);

				processor.Settings.Language = Languages.English;

				string ocr = processor.PerformOCR(lDoc, Path.Combine(executionContext.FunctionAppDirectory, "tessdata"));
				ms = new MemoryStream();
				//Save the PDF document  
				lDoc.Save(ms);
				ms.Position = 0;
			}
			catch(Exception ex)
			{
				//Create a new PDF document.
				PdfDocument document = new PdfDocument();
				//Add a page to the document.
				PdfPage page = document.Pages.Add();
				//Create PDF graphics for the page.
				PdfGraphics graphics = page.Graphics;

				//Set the standard font.
				PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 5);
				//Draw the text.
				graphics.DrawString(ex.ToString(), font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));

				//Creating the stream object.
				ms = new MemoryStream();
				//Save the document into memory stream.
				document.Save(ms);
				//Close the document.
				document.Close(true);
				ms.Position = 0;
			}
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new ByteArrayContent(ms.ToArray());
			response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = "OCR_Azure_Function.pdf"
			};
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
			return response;
		}
    }
}
