using System;
using System.Collections.Generic;
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
//using System.Windows.Shapes;

// Be sure you have these namespaces!
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Ch19_P10_DataParallelismWithForEach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            // This will be updated shortly
        }
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            //ProcessFiles();

            // start a new task to process the files.
            Task.Factory.StartNew(() => ProcessFiles());

            //Task.Run(() =>
            //{
            //    ProcessFiles();
            //});
        }
        private void ProcessFiles()
        {
            MessageBox.Show(" Thread ID = " + Thread.CurrentThread.ManagedThreadId,
                            " In Process File Method ");

            // Load up all *.jpg files, and make a new folder for the modified data.
            string[] files = Directory.GetFiles(@"C:\Users\Public\Saved Pictures", "*.jpg");
            string newDir = @"C:\Users\Public\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            // Process the image data in a blocking manner.
            foreach (string currentFile in files)
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));
                    // Print out the ID of the thread processing the current image.
                    this.Title = $"Processing {filename} on thread = {Thread.CurrentThread.ManagedThreadId}";
                    //Thread.Sleep(500);
                }
            }

            //// Process the image data in a blocking manner. 
            //Parallel.ForEach(files, currentFile =>
            //{
            //    string filename = Path.GetFileName(currentFile);
            //    using (Bitmap bitmap = new Bitmap(currentFile))
            //    {
            //        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            //        bitmap.Save(Path.Combine(newDir, filename));
            //        // Print out the ID of the thread processing the current image.
            //        this.Dispatcher.Invoke((Action)delegate
            //        {
            //            this.Title = $"Processing {filename} on thread = {Thread.CurrentThread.ManagedThreadId}";
            //        });
            //    }
            //});

            this.Title = "Processing Done";
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button CLick done on Thread ID = " + Thread.CurrentThread.ManagedThreadId);
            ProcessFiles();
        }
    }
}
