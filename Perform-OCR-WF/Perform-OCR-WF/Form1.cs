using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perform_OCR_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
            using (OCRProcessor processor = new OCRProcessor(@"../../TesseractBinaries/4.0/x86"))
            {
                //Load an existing PDF document.
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument("../../Data/Input.pdf");

                //Set the tesseract version 
                processor.Settings.TesseractVersion = TesseractVersion.Version4_0;

                //Set OCR language to process.
                processor.Settings.Language = Languages.English;

                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(loadedDocument, @"../../Tessdata/");
                    
                //Save the OCR processed PDF document in the disk.
                loadedDocument.Save("OCR.pdf");
                loadedDocument.Close(true);
            }

            //This will open the PDF file so, the result will be seen in default PDF viewer.
            Process.Start("OCR.pdf");
        }
    }
}
