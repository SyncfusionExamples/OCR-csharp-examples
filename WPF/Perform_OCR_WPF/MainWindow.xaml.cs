using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Perform_OCR_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string inputFilePath = Path.GetFullPath("../../../../Data/Input.pdf");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
            using (OCRProcessor processor = new OCRProcessor())
            {
                //Load an existing PDF document.
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(inputFilePath);

                //Set the tesseract version 
                processor.Settings.TesseractVersion = TesseractVersion.Version4_0;

                //Set OCR language to process.
                processor.Settings.Language = Languages.English;

                //Process OCR by providing the PDF document and Tesseract data.
                processor.PerformOCR(loadedDocument);

                //Save the OCR processed PDF document in the disk.
                loadedDocument.Save("OCR.pdf");
                loadedDocument.Close(true);
            }
            //This will open the PDF file so, the result will be seen in default PDF viewer.
            Process.Start("OCR.pdf");
        }
    }
}
