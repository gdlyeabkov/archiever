using Aspose.Zip;
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
    /// Логика взаимодействия для InfoSourceFileDialog.xaml
    /// </summary>
    public partial class InfoSourceFileDialog : Window
    {

        public List<string> data;

        public InfoSourceFileDialog(List<string> data)
        {
            InitializeComponent();

            Initialize(data);
        
        }

        public void Initialize(List <string> data)
        {
            this.data = data;
            GetFilesCount();
            GetFoldersCount();
            GetArchievesCount();
            GetTotalSize();
            GetFilesSize();
        }

        public void GetFilesCount()
        {
            int filesCounter = 0;
            foreach (string dataItem in data)
            {
                bool isFileDetected = File.Exists(dataItem);
                if (isFileDetected)
                {
                    filesCounter++;
                }
            }
            filesCountLabel.Text = filesCounter.ToString();
        }

        public void GetFoldersCount()
        {
            int foldersCounter = 0;
            foreach (string dataItem in data)
            {
                bool isFolderDetected = Directory.Exists(dataItem);
                if (isFolderDetected)
                {
                    foldersCounter++;
                }
            }
            foldersCountLabel.Text = foldersCounter.ToString();
        }

        public void GetArchievesCount()
        {
            int archievesCounter = 0;
            foreach (string dataItem in data)
            {
                bool isFileDetected = File.Exists(dataItem);
                if (isFileDetected)
                {
                    string ext = System.IO.Path.GetExtension(dataItem);
                    bool isRar = ext == ".rar";
                    bool isZip = ext == ".zip";
                    bool isArchieveDetected = isRar || isZip;
                    if (isArchieveDetected)
                    {
                        archievesCounter++;
                    }
                }
            }
            archievesCountLabel.Text = archievesCounter.ToString();
        }

        public void GetTotalSize()
        {
            long totalSize = 0;
            foreach (string dataItem in data)
            {
                bool isFileDetected = File.Exists(dataItem);
                bool isFolderDetected = Directory.Exists(dataItem);
                if (isFileDetected)
                {
                    FileInfo fileInfo = new FileInfo(dataItem);
                    totalSize += fileInfo.Length;
                }
                else if (isFolderDetected)
                {
                    DirectoryInfo folderInfo = new DirectoryInfo(dataItem);
                    FileInfo[] fileInfos = folderInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        totalSize += fileInfo.Length;
                    }
                }
            }
            totalSizeLabel.Text = totalSize.ToString();
        }

        public void GetFilesSize()
        {
            long filesSize = 0;
            foreach (string dataItem in data)
            {
                bool isFileDetected = File.Exists(dataItem);
                if (isFileDetected)
                {
                    FileInfo fileInfo = new FileInfo(dataItem);
                    filesSize += fileInfo.Length;
                }
            }
            filesSizeLabel.Text = filesSize.ToString();
        }

        private void OkHandler(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void CancelHandler(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void GetHelpHandler (object sender, RoutedEventArgs e)
        {
            
        }

        public void Cancel ()
        {
            this.Close();
        }

    }
}
