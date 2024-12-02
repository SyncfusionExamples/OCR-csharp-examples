﻿## Azure Functions

The Syncfusion&reg; PDF is a [.NET Core PDF library](https://www.syncfusion.com/pdf-framework/net-core/pdf-library?_gl=1*jslmpe*_ga*OTcwNzc5NDkuMTY4MTEwMjEwNA..*_ga_WC4JKKPHH0*MTY5MDI5MjcyMi4zNjUuMS4xNjkwMjk0MjA5LjYwLjAuMA..) that supports OCR by using the Tesseract open-source engine. Using this library, perform OCR for a PDF document in Azure Functions using .NET Core.

### Steps to perform OCR on the entire PDF document in Azure Functions

Step 1: Create the Azure function project.
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions1.png" alt="Convert OCR Azure Functions Step1" width="100%" Height="Auto"/> 

Step 2: Select the framework to Azure Functions and select HTTP triggers as follows.
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions2.png" alt="Convert OCR Azure Functions Step2" width="100%" Height="Auto"/> 
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions3.png" alt="Convert OCR Azure Functions Step3" width="100%" Height="Auto"/>

Step 3: Install the [Syncfusion.PDF.OCR.NET](https://www.nuget.org/packages/Syncfusion.PDF.OCR.NET) NuGet package as a reference to your .NET Core application [NuGet.org](https://www.nuget.org/).
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions4.png" alt="Convert OCR Azure Functions Step4" width="100%" Height="Auto"/> 

Step 4: Copy the tessdata folder from the **bin->Debug->net6.0->runtimes** and paste it into the folder that contains the project file.

<img src="Perform_OCR_Azure_Functions/OCR-Images/Tessdata-path.png" alt="Convert OCR Azure Functions Tessdata Path" width="100%" Height="Auto"/> 

<img src="Perform_OCR_Azure_Functions/OCR-Images/Tessdata_Store.png" alt="Convert OCR Azure Functions Tessdata Store" width="100%" Height="Auto"/>

Step 5: Then, set Copy to output directory to give copy always the tessdata folder.

<img src="Perform_OCR_Azure_Functions/OCR-Images/Set_Copy_Always.png" alt="Convert OCR Azure Functions Tessdata Store" width="100%" Height="Auto"/>


Step 6: Include the following namespaces in the **Function1.cs** file to perform OCR for a PDF document using C#.

{% highlight c# tabtitle="C#" %}

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

{% endhighlight %}

Step 7: Add the following code sample in the Function1 class to perform OCR for a PDF document using [PerformOCR](https://help.syncfusion.com/cr/file-formats/Syncfusion.OCRProcessor.OCRProcessor.html#Syncfusion_OCRProcessor_OCRProcessor_PerformOCR_Syncfusion_Pdf_Parsing_PdfLoadedDocument_System_String_) method of the [OCRProcessor](https://help.syncfusion.com/cr/file-formats/Syncfusion.OCRProcessor.OCRProcessor.html) class in Azure Functions.

{% highlight c# tabtitle="C#" %}

[FunctionName("Function1")]
public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req, TraceWriter log, ExecutionContext executionContext)
{
    MemoryStream ms = new MemoryStream();
    try
    {
        OCRProcessor processor = new OCRProcessor();
        FileStream stream = new FileStream(Path.Combine(executionContext.FunctionAppDirectory, "Data", "Input.pdf"), FileMode.Open);
        //Load a PDF document.
        PdfLoadedDocument lDoc = new PdfLoadedDocument(stream);
        //Set OCR language to process.
        processor.Settings.Language = Languages.English;
        //Perform OCR with input document.
        string ocr = processor.PerformOCR(lDoc,Path.Combine(executionContext.FunctionAppDirectory, "tessdata"));            
        //Save the PDF document.  
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

{% endhighlight %}

Step 8: Now, check the OCR creation in the local machine.

### Steps to publish as Azure Functions 

Step 1: Right-click the project and click Publish. Then, create a new profile in the Publish Window. So, create the Azure Function App with a consumption plan.
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions5.png" alt="Convert OCR Azure Functions Step5" width="100%" Height="Auto"/>
<img src="Perform_OCR_Azure_Functions/OCR-Images/azure_step6.png" alt="Convert OCR Azure Functions Step6" width="100%" Height="Auto"/>
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions7.png" alt="Convert OCR Azure Functions Step7" width="100%" Height="Auto"/>

Step 2: After creating the profile, click Publish.
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions8.png" alt="Convert OCR Azure Functions Step8" width="100%" Height="Auto"/>

Step 3: Now, publish has been succeeded.
<img src="Perform_OCR_Azure_Functions/OCR-Images/AzureFunctions9.png" alt="Convert OCR Azure Functions Step8" width="100%" Height="Auto"/>

Step 4: Now, go to the Azure portal and select the Functions Apps. After running the service, click Get function URL > Copy. Include the URL as a query string in the URL. Then, paste it into the new browser tab. You will get a PDF document as follows.
<img src="Perform_OCR_Azure_Functions/OCR-Images/OCR-output-image.png" alt="Convert OCR Azure Functions Step9" width="100%" Height="Auto"/> 

A complete working sample can be downloaded from [GitHub](https://github.com/SyncfusionExamples/OCR-csharp-examples/tree/master/Azure/Azure%20Function).

Click [here](https://www.syncfusion.com/document-processing/pdf-framework/net-core) to explore the rich set of Syncfusion&reg; PDF library features.