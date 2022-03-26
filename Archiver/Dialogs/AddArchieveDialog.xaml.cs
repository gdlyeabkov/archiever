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

using System.Speech.Synthesis;
using Aspose.Zip;
using Aspose.Zip.Saving;

namespace Archiver.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddArchieveDialog.xaml
    /// </summary>
    public partial class AddArchieveDialog : Window
    {

        public string archievePath = @"C:\Gleb\archiever\zips\";
        public string archieveName = "compressed_file.zip";
        public SpeechSynthesizer debugger;
        public List<string> data;
        public Archiver.Dialogs.ProgressDialog progressDialog;

        public AddArchieveDialog(List<string> data)
        {
            InitializeComponent();

            Initialize(data);
        
        }

        public void Initialize(List<string> data)
        {
            this.data = data;
            debugger = new SpeechSynthesizer();
        }

        private void AddArchieveHandler(object sender, RoutedEventArgs e)
        {
            AddArchieve();
            this.Close();
        }

        private void CancelHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HelpHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddArchieve()
        {
            archieveName = generalArchieveName.Text;
            using (FileStream zipFile = File.Open(archievePath + archieveName, FileMode.Create))
            {
                using (var archive = new Archive(new ArchiveEntrySettings()))
                {
                    foreach (string dataItem in data)
                    {
                        FileStream source1 = File.Open(dataItem, FileMode.Open, FileAccess.Read);
                        string sourceFileName = System.IO.Path.GetFileName(dataItem);
                        ArchiveEntry entry = archive.CreateEntry(sourceFileName, source1);
                        entry.CompressionProgressed += CompressionProgressHandler;
                    }
                    
                    progressDialog = new Archiver.Dialogs.ProgressDialog(generalArchieveName);
                    progressDialog.Show();
                    
                    archive.Save(zipFile);
                }
            }
        }

        private void CompressionProgressHandler(object sender, ProgressEventArgs e)
        {
            ArchiveEntry entry = ((ArchiveEntry)(sender));
            ulong size = entry.CompressedSize;
            ulong progress = e.ProceededBytes;
            ulong sizePercent = size / 100;
            ulong percents = (size - progress) / sizePercent;
            generalArchieveName.DataContext = percents;
            if (progressDialog != null)
            {
                progressDialog.Close();
            }
        }

        private void SetArchieveDestinationHandler(object sender, RoutedEventArgs e)
        {
            SetArchieveDestination();
        }

        private void SetArchieveDestination()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Архив.zip";
            sfd.DefaultExt = ".zip";
            sfd.Filter = "Zip documents (.zip)|*.zip";
            bool? res = sfd.ShowDialog();
            if (res != false)
            {
                string source = sfd.FileName;
                generalArchieveName.Text = System.IO.Path.GetFileName(source);
                FileInfo fileInfo = new FileInfo(source);
                DirectoryInfo directoryInfo = fileInfo.Directory;
                string directoryInfoPath = directoryInfo.FullName;
                char pathSeparator = System.IO.Path.DirectorySeparatorChar;
                archievePath = directoryInfoPath + pathSeparator;
            }
        }

    }
}
