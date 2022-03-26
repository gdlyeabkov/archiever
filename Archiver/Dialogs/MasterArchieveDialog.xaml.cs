using Aspose.Zip;
using Aspose.Zip.Saving;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MasterArchieveDialog.xaml
    /// </summary>
    public partial class MasterArchieveDialog : Window
    {

        public List<string> data;
        public string defaultArchieveExtractPath = @"C:\Gleb\archiever\zips\extracts\";
        public string defaultArchievePath = @"C:\Gleb\archiever\zips\";
        public string archievePath = @"C:\Gleb\archiever\zips\";
        public string archieveName = "compressed_file.zip";
        public string masterAction = "";
        public int masterActionIndex = 0;
        public SpeechSynthesizer debugger;

        public MasterArchieveDialog(List<string> data)
        {
            InitializeComponent();

            Initialize(data);

        }

        public void Initialize(List<string> data)
        {
            this.data = data;
            debugger = new SpeechSynthesizer();
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
            bool isFirstActionIndex = masterActionIndex == 0;
            bool isSecondActionIndex = masterActionIndex == 1;
            bool isThirdActionIndex = masterActionIndex == 2;
            if (isExtractArchieve)
            {
                if (isFirstActionIndex)
                {
                    backBtn.IsEnabled = true;
                    masterAction = "extract";
                    masterActionIndex++;
                    masterActionTabControl.SelectedIndex = 3;
                }
                else if (isSecondActionIndex)
                {
                    masterActionIndex++;
                    masterActionTabControl.SelectedIndex = 4;
                    nextBtn.Content = "Готово";
                    archieveName = extractedFolderNameBox.Text;
                    archievePath = defaultArchieveExtractPath;
                }
                else if (isThirdActionIndex)
                {
                    ExtractArchieve();
                }
            }
            else if (isCreateNewArchieve)
            {
                if (isFirstActionIndex)
                {
                    SelectFilesForArchieve();
                }
                else if (isSecondActionIndex)
                {
                    masterActionIndex++;
                    masterActionTabControl.SelectedIndex = 2;
                    nextBtn.Content = "Готово";
                    archieveName = createArchieveNameBox.Text;
                    archievePath = defaultArchievePath;
                }
                else if (isThirdActionIndex)
                {
                    AddArchieve();
                    Cancel();
                }
            }
            else if (isAddFilesToExistedArchieve)
            {
                if (isFirstActionIndex)
                {
                    SelectFilesForAddToArchieve();
                }
                else if (isSecondActionIndex)
                {
                    masterActionIndex++;
                    masterActionTabControl.SelectedIndex = 6;
                    nextBtn.Content = "Готово";
                    archieveName = addedArchieveNameBox.Text;
                    archievePath = defaultArchievePath;
                }
                else if (isThirdActionIndex) {
                    AddToArchieve();
                    Cancel();
                }
            }
        }

        public void SelectFilesForArchieve()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Выберите файлы, которые нужно добавить";
            ofd.Multiselect = true;
            bool? res = ofd.ShowDialog();
            if (res != false)
            {
                masterActionTabControl.SelectedIndex = 1;
                masterAction = "create";
                masterActionIndex++;
                backBtn.IsEnabled = true;
                string[] fileNames = ofd.FileNames;
                data.Clear();
                data = fileNames.ToList<string>();
            }
        }

        public void ExtractArchieve ()
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
                    archive.ExtractToDirectory(archievePath);
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

        public void AddToArchieve()
        {
            using (FileStream zipFile = File.Open(archievePath + archieveName, FileMode.Open))
            {
                using (var archive = new Archive(zipFile))
                {
                    var entries = archive.Entries;
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

        private void BackHandler (object sender, RoutedEventArgs e)
        {
            bool isNotStartScreen = backBtn.IsEnabled;
            if (isNotStartScreen)
            { 
                masterActionIndex--;
                bool isStartScreen = masterActionIndex == 0;
                if (isStartScreen)
                {
                    backBtn.IsEnabled = false;
                }
                bool isExtractAction = masterAction == "extract";
                bool isCreateAction = masterAction == "create";
                bool isAddAction = masterAction == "add";
                bool isFirstActionIndex = masterActionIndex == 0;
                bool isSecondActionIndex = masterActionIndex == 1;
                if (isExtractAction)
                {
                    if (isFirstActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 0;
                    }
                    else if (isSecondActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 3;
                    }
                    archievePath = defaultArchievePath;
                }
                else if (isCreateAction)
                {
                    if (isFirstActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 0;
                    }
                    else if (isSecondActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 1;
                    }
                    archievePath = defaultArchievePath;
                }
                else if (isAddAction)
                {
                    if (isFirstActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 0;
                    }
                    else if (isSecondActionIndex)
                    {
                        masterActionTabControl.SelectedIndex = 5;
                    }
                    archievePath = defaultArchievePath;
                }
                nextBtn.Content = "Далее";
            }
        }

        private void CancelHandler (object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        private void OpenArchieveNameHandler(object sender, RoutedEventArgs e)
        {
            OpenArchieveName();
        }

        public void OpenArchieveName()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "Выберите папку назначения и имя нового архива";
            sfd.FileName = "Архив.zip";
            sfd.DefaultExt = ".zip";
            sfd.Filter = "Zip documents (.zip)|*.zip";
            bool? res = sfd.ShowDialog();
            if (res != false)
            {
                string fullPath = sfd.FileName;
                FileInfo fileInfo = new FileInfo(fullPath);
                DirectoryInfo directoryInfo = fileInfo.Directory;
                string directoryPath = directoryInfo.FullName;
                char directoryPathSeparator = System.IO.Path.DirectorySeparatorChar;
                archievePath = directoryPath + directoryPathSeparator;
                archieveName = System.IO.Path.GetFileName(fullPath);
                createArchieveNameBox.Text = archieveName;
            }
        }

        private void OpenExtractArchieveNameHandler(object sender, RoutedEventArgs e)
        {
            OpenExtractArchieveName();
        }

        public void OpenExtractArchieveName ()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "Поиск архива";
            sfd.FileName = "Архив.zip";
            sfd.DefaultExt = ".zip";
            sfd.Filter = "Zip documents (.zip)|*.zip";
            bool? res = sfd.ShowDialog();
            if (res != false)
            {
                string fullPath = sfd.FileName;
                FileInfo fileInfo = new FileInfo(fullPath);
                DirectoryInfo directoryInfo = fileInfo.Directory;
                string directoryPath = directoryInfo.FullName;
                char directoryPathSeparator = System.IO.Path.DirectorySeparatorChar;
                archievePath = directoryPath + directoryPathSeparator;
                archieveName = System.IO.Path.GetFileName(fullPath);
                extractedArchieveNameBox.Text = archieveName;
                data.Clear();
                data.Add(fullPath);
            }
        }

        private void OpenExtractFolderNameHandler(object sender, RoutedEventArgs e)
        {
            OpenExtractFolderName();
        }

        public void OpenExtractFolderName()
        {
            System.Windows.Forms.FolderBrowserDialog ofd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res = ofd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                archievePath = ofd.SelectedPath;
                // masterActionTabControl.SelectedIndex = 5;
                extractedFolderNameBox.Text = archievePath;
            }
        }

        public void SelectFilesForAddToArchieve ()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Выберите файлы, которые нужно добавить";
            ofd.Multiselect = true;
            bool? res = ofd.ShowDialog();
            if (res != false)
            {
                masterAction = "add";
                masterActionIndex++;
                backBtn.IsEnabled = true;
                string[] fileNames = ofd.FileNames;
                data.Clear();
                data = fileNames.ToList<string>();
                masterActionTabControl.SelectedIndex = 5;
            }
        }

        private void OpenAddedArchieveNameHandler(object sender, RoutedEventArgs e)
        {
            OpenAddArchieveName();
        }

        public void OpenAddArchieveName ()
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Title = "Поиск архива";
            sfd.FileName = "Архив.zip";
            sfd.DefaultExt = ".zip";
            sfd.Filter = "Zip documents (.zip)|*.zip";
            bool? res = sfd.ShowDialog();
            if (res != false)
            {
                string fullPath = sfd.FileName;
                FileInfo fileInfo = new FileInfo(fullPath);
                DirectoryInfo directoryInfo = fileInfo.Directory;
                string directoryPath = directoryInfo.FullName;
                char directoryPathSeparator = System.IO.Path.DirectorySeparatorChar;
                archievePath = directoryPath + directoryPathSeparator;
                archieveName = System.IO.Path.GetFileName(fullPath);
                addedArchieveNameBox.Text = archieveName;
                /*data.Clear();
                data.Add(fullPath);*/
            }
        }

    }
}
