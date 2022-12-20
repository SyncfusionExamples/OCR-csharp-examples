##### Example: ASP.NET Core

# Perform OCR in ASP.NET Core using C#

The [Syncfusion .NET OCR library](https://www.syncfusion.com/document-processing/pdf-framework/net/pdf-library/ocr-process) is used to extract text from the scanned PDFs and images in ASP.NET Core application with the help of Google's [Tesseract](https://github.com/tesseract-ocr/tesseract) Optical Character Recognition engine.  

## Steps to perform OCR on entire PDF document in ASP.NET Core application

1. Create a new C# ASP.NET Core Web Application project.
   <img src="Perform_OCR_NET_Core/ocr_images/aspnetcore1.png" alt="convert_OCR_ASPNET_CORE1" width="100%" Height="Auto"/>

2. In configuration windows, name your project and select Next.
   <img src="Perform_OCR_NET_Core/ocr_images/aspnetcore2.png" alt="convert_OCR_ASPNET_CORE2" width="100%" Height="Auto"/>
   <img src="Perform_OCR_NET_Core/ocr_images/aspnetcore3.png" alt="convert_OCR_ASPNET_CORE3" width="100%" Height="Auto"/>

3. Install [Syncfusion.PDF.OCR.NET](https://www.nuget.org/packages/Syncfusion.PDF.OCR.NET) NuGet package as reference to your .NET Standard applications from [NuGet.org](https://www.nuget.org/).   
   <img src="Perform_OCR_NET_Core/ocr_images/aspnetcore4.png" alt="convert_OCR_ASPNET_CORE4" width="100%" Height="Auto"/>

4. Tesseract assemblies are not added as a reference. They must be kept in the local machine, and the location of the assemblies is passed as a parameter to the OCR processor.

   ```csharp
   OCRProcessor processor = new OCRProcessor(@"Tesseractbinaries/Windows");
   ```

5. Place the Tesseract language data {E.g eng.traineddata} in the local system and provide a path to the OCR processor. Please use the OCR language data for other languages using the following this link,[Tesseract language data](https://github.com/tesseract-ocr/tessdata).

   ```csharp
   OCRProcessor processor = new OCRProcessor(@"Tesseractbinaries/Windows");
   processor.PerformOCR(lDoc, "tessdata/");
   ```

6. A default controller with name [HomeController.cs](Perform_OCR_NET_Core/Controllers/HomeController.cs) gets added on creation of ASP.NET Core MVC project. Include the following namespaces in that HomeController.cs file.

   ```csharp
   using Syncfusion.OCRProcessor;
   using Syncfusion.Pdf.Parsing;
   ```

7. Add a new button in [index.cshtml](Perform_OCR_NET_Core/Views/Home/Index.cshtml) as shown below.

   ```csharp
   @{Html.BeginForm("PerformOCR", "Home", FormMethod.Post);
    {
      <div>
         <input type="submit" value="Perform OCR" style="width:150px;height:27px" />
      </div>
    }
    Html.EndForm();
   }
   ```

8. Add a new action method named PerformOCR in [HomeController.cs](Perform_OCR_NET_Core/Controllers/HomeController.cs) and use the following code sample to perform OCR in the ASP.NET Core application.

   ```csharp
   string docPath = _hostingEnvironment.WebRootPath + "/Data/Input.pdf";
   string tesseractPath = _hostingEnvironment.WebRootPath + "/Data/Tesseractbinaries/Windows";
   //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
   using (OCRProcessor processor = new OCRProcessor(tesseractPath))
   {
       FileStream fileStream = new FileStream(docPath, FileMode.Open, FileAccess.Read);
       //Load a PDF document
       PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
       //Set OCR language to process
       processor.Settings.Language = Languages.English;
       string tessdataPath = _hostingEnvironment.WebRootPath + "/Data/tessdata";
       //Process OCR by providing the PDF document and Tesseract data
       processor.PerformOCR(lDoc, tessdataPath);
       //Create memory stream
       MemoryStream stream = new MemoryStream();
       //Save the document to memory stream
       lDoc.Save(stream);
       lDoc.Close();
       //Set the position as '0'
       stream.Position = 0;
       //Download the PDF document in the browser
       FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
       fileStreamResult.FileDownloadName = "Sample.pdf";
       return fileStreamResult;
   }
   ```

   By executing the program, you will get the PDF document as follows.
   <img src="Perform_OCR_NET_Core/ocr_images/OCR-output-image.png" alt="Convert OCR ASP.NET_Core output" width="100%" Height="Auto"/>
