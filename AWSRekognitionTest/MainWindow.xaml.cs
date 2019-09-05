//using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AWSRekognitionTest
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

        private void Window_Initialized(object sender, EventArgs e)
        {
            // 
        }

        private void ButtonChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
            openFileDlg.Title = "Select Photo";

            DialogResult dr = openFileDlg.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                Uri fileUri = new Uri(openFileDlg.FileName);
                image_selectedImage.Source = new BitmapImage(fileUri);

                Thread.Sleep(1000);
                listBox_labels.Items.Clear();
                var detectedLabels = RekognitionHelper.GetInfo(openFileDlg.FileName);

                Console.WriteLine("Detected labels for " + openFileDlg.FileName);
                foreach (var label in detectedLabels.Labels)
                    listBox_labels.Items.Add(string.Format("{0}: {1}", label.Name, label.Confidence));
            }

            
        }
    }
}
