﻿##### Example: ASP.NET MVC

# Perform OCR in ASP.NET MVC using C#

The [Syncfusion .NET OCR library](https://www.syncfusion.com/document-processing/pdf-framework/net/pdf-library/ocr-process) is used to extract text from scanned PDFs and images in ASP.NET MVC application with the help of Google's [Tesseract](https://github.com/tesseract-ocr/tesseract) Optical Character Recognition engine.  

## Steps to perform OCR on entire PDF document in ASP.NET MVC

1. Create a new C# ASP.NET Web Application (.NET Framework) project.
   <img src="Perform_OCR_MVC/ocr_images/aspnetmvc1.png" alt="convert_OCR_ASP.NET_MVC1" width="100%" Height="Auto"/>

2. In the project configuration windows, name your project and select Create.
   <img src="Perform_OCR_MVC/ocr_images/aspnetmvc2.png" alt="convert_OCR_ASP.NET_MVC2" width="100%" Height="Auto"/>
   <img src="Perform_OCR_MVC/ocr_images/aspnetmvc3.png" alt="convert_OCR_ASP.NET_MVC3" width="100%" Height="Auto"/>

3. Install [Syncfusion.Pdf.OCR.AspNet.Mvc5](https://www.nuget.org/packages/Syncfusion.Pdf.OCR.AspNet.Mvc5) NuGet package as reference to your .NET applications from [NuGet.org](https://www.nuget.org/).
   <img src="Perform_OCR_MVC/ocr_images/aspnetmvc4.png" alt="convert_OCR_ASP.NET_MVC4" width="100%" Height="Auto"/>

4. Tesseract assemblies are not added as a reference. They must be kept in the local machine, and the location of the assemblies is passed as a parameter to the OCR processor.

   ```csharp
   OCRProcessor processor = new OCRProcessor(@"TesseractBinaries/");
   ```

5. Place the Tesseract language data {E.g eng.traineddata} in the local system and provide a path to the OCR processor. Please use the OCR language data for other languages using the following this link,[Tesseract language data](https://github.com/tesseract-ocr/tessdata).

   ```csharp
   OCRProcessor processor = new OCRProcessor(@"TesseractBinaries/");
   processor.PerformOCR(lDoc,@"TessData/");
   ```

6. Include the following namespaces in the [HomeController.cs](Perform_OCR_MVC/Controllers/HomeController.cs) file.

   ```csharp
   using Syncfusion.OCRProcessor;
   using Syncfusion.Pdf.Parsing;
   ```   

7. Add a new button in the [Index.cshtml](Perform_OCR_MVC/Views/Home/Index.cshtml) as shown below.

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

8. Add a new action method named PerformOCR in [HomeController.cs](Perform_OCR_MVC/Controllers/HomeController.cs) and use the following code sample to perform OCR in the ASP.NET MVC application.

   ```csharp
   string tesseract = Server.MapPath("~/TesseractBinaries/3.05/x86/");
   //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
   using (OCRProcessor processor = new OCRProcessor(tesseract))
   {
       FileStream fileStream = new FileStream(Server.MapPath("~/Data/Input.pdf"), FileMode.Open, FileAccess.Read);
       //Load a PDF document
       PdfLoadedDocument lDoc = new PdfLoadedDocument(fileStream);
       //Set OCR language to process
       processor.Settings.Language = Languages.English;
       processor.Settings.TesseractVersion = TesseractVersion.Version3_05;
       string tessData = Server.MapPath("~/LanguagePack/");
       //Process OCR by providing the PDF document and Tesseract data
       processor.PerformOCR(lDoc, tessData);
       //Create memory stream
       MemoryStream stream = new MemoryStream();
       //Save the document to memory stream
       lDoc.Save(stream);
       lDoc.Close();
       //Set the position as '0'
       stream.Position = 0;
       return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "PerformOCR_Output.pdf");
   }
   ```

   By executing the program, you will get the PDF document as follows.
   <img src="Perform_OCR_MVC/ocr_images/OCR-output-image.png" alt="Convert OCR ASP.NET_MVC output" width="100%" Height="Auto"/>
