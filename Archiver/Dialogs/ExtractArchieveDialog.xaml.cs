using Aspose.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Archiver.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ExtractArchieveDialog.xaml
    /// </summary>
    public partial class ExtractArchieveDialog : Window
    {
        
        public SpeechSynthesizer debugger;
        public List<string> data;

        public ExtractArchieveDialog(List<string> data)
        {
            InitializeComponent();

            Initialize(data);
        
        }

        public void Initialize(List<string> data) {
            debugger = new SpeechSynthesizer();
            this.data = data;
        }

        private void ExtractArchieveHandler(object sender, RoutedEventArgs e)
        {
            ExtractArchieve();
        }

        public void ExtractArchieve()
        {
            foreach (String dataItem in data)
            {
                string ext = System.IO.Path.GetExtension(dataItem);
                bool isRar = ext == ".rar";
                bool isZip = ext == ".zip";
                bool isArchieve = isRar || isZip;
                if (isArchieve)
                {
                    Archive archive = new Archive(dataItem);
                    archive.ExtractToDirectory(@"C:\Gleb\archiever\zips\extracts");
                }
            }
            Cancel();
        }

        private void CancelHandler(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        public void Cancel ()
        {
            this.Close();
        }

        private void GetHelpHandler(object sender, RoutedEventArgs e)
        {

        }

    }
}
