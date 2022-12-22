using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perform_OCR_WF
{
    public partial class Form1 : Form
    {
        string tesseractBinariesPath = Path.GetFullPath("../../../../Data/.NET-Framework/TesseractBinaries/4.0/x86");
        string tessdataPath = Path.GetFullPath("../../../../Data/Tessdata/");
        string inputFilePath = Path.GetFullPath("../../../../Data/Input.pdf");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //Initialize the OCR processor by providing the path of tesseract binaries.
            using (OCRProcessor processor = new OCRProcessor(tesseractBinariesPath))
            {
                //Load an existing PDF document.
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(inputFilePath);

                //Set the tesseract version 
                processor.Settings.TesseractVersion = TesseractVersion.Version4_0;

                //Set OCR language to process.
                processor.Settings.Language = Languages.English;

                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(loadedDocument, tessdataPath);
                    
                //Save the OCR processed PDF document in the disk.
                loadedDocument.Save("OCR.pdf");
                loadedDocument.Close(true);
            }

            //This will open the PDF file so, the result will be seen in default PDF viewer.
            Process.Start("OCR.pdf");
        }
    }
}
