using Aspose.Zip;
using Aspose.Zip.Saving;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Archiver.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для MasterArchieveDialog.xaml
    /// </summary>
    public partial class MasterArchieveDialog : Window
    {

        public List<string> data;
        public string archievePath = @"C:\Gleb\archiever\zips\";
        public string archieveName = "compressed_file.zip";

        public MasterArchieveDialog(List<string> data)
        {
            InitializeComponent();

            Initialize(data);

        }

        public void Initialize(List<string> data)
        {
            this.data = data;
        }

        private void NextHandler (object sender, RoutedEventArgs e)
        {
            Next();
        }

        public void Next()
        {
            bool isExtractArchieve = ((bool)(isExtractArchieveRadioButton.IsChecked));
            bool isCreateNewArchieve = ((bool)(isCreateNewArchieveRadioButton.IsChecked));
            bool isAddFilesToExistedArchieve = ((bool)(isAddFilesToExistedArchieveRadioButton.IsChecked));
            if (isExtractArchieve)
            {
                ExtractArchieve();
            }
            else if (isCreateNewArchieve)
            {
                // AddArchieve();
                SelectFilesForArchieve();
            }
            else if (isAddFilesToExistedArchieve)
            {

            }
        }

        public void SelectFilesForArchieve()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Multiselect = true;
            bool? res = ofd.ShowDialog();
            if (res != false)
            {
                
            }
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

        public void Cancel()
        {
            this.Close();
        }

        public void AddArchieve()
        {
            // archieveName = generalArchieveName.Text;
            using (FileStream zipFile = File.Open(archievePath + archieveName, FileMode.Create))
            {
                using (var archive = new Archive(new ArchiveEntrySettings()))
                {
                    foreach (string dataItem in data)
                    {
                        FileStream source1 = File.Open(dataItem, FileMode.Open, FileAccess.Read);
                        string sourceFileName = System.IO.Path.GetFileName(dataItem);
                        archive.CreateEntry(sourceFileName, source1);
                    }
                    archive.Save(zipFile);
                }
            }
        }

    }
}
