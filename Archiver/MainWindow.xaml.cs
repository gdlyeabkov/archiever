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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Speech.Synthesis;

namespace Archiver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public String currentPath = @"C:\Gleb\archiever\zipsource";
        public List<string> selectedSourceFiles;
        public SpeechSynthesizer debugger;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        
        }

        public void Initialize()
        {
            debugger = new SpeechSynthesizer();
            selectedSourceFiles = new List<string>();
            GetSourceFiles();
        }

        private void AddArchieveHandler(object sender, RoutedEventArgs e)
        {
            OpenAddArchieveDialog();
        }

        public void OpenAddArchieveDialog()
        {
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            bool isSourceFilesSelected = countSelectedSourceFiles >= 1;
            if (isSourceFilesSelected)
            {
                Archiver.Dialogs.AddArchieveDialog dialog = new Archiver.Dialogs.AddArchieveDialog(selectedSourceFiles);
                dialog.Show();
                dialog.Closed += AddArcheveDialogCloseHandler;
            }
        }

        public void GetSourceFiles()
        {
            sourceFiles.Children.Clear();
            string[] sourceFilesPaths = Directory.GetFiles(currentPath);
            foreach (string sourceFilePath in sourceFilesPaths)
            {
                StackPanel sourceFile = new StackPanel();
                sourceFile.Orientation = Orientation.Horizontal;
                sourceFile.Height = 15;
                sourceFile.DataContext = sourceFilePath;
                string sourceFileName = System.IO.Path.GetFileName(sourceFilePath);
                TextBlock sourceFileNameLabel = new TextBlock();
                sourceFileNameLabel.Text = sourceFileName;
                sourceFile.Children.Add(sourceFileNameLabel);
                sourceFiles.Children.Add(sourceFile);
                sourceFile.MouseUp += SelectSourceFileHandler;
            }
        }


        public void SelectSourceFileHandler(object sender, RoutedEventArgs args)
        {
            StackPanel sourceFile = ((StackPanel)(sender));
            string sourceFileData = ((string)(sourceFile.DataContext));
            if (!((Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                selectedSourceFiles.Clear();
                foreach (StackPanel someSourceFile in sourceFiles.Children)
                {
                    someSourceFile.Background = System.Windows.Media.Brushes.White;
                }
            }
            sourceFile.Background = System.Windows.Media.Brushes.LightSkyBlue;
            bool isSelectedSourceFile = selectedSourceFiles.Contains(sourceFileData);
            bool isNotSelectedSourceFile = !isSelectedSourceFile;
            if (isNotSelectedSourceFile)
            {
                selectedSourceFiles.Add(sourceFileData);
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) > 0) {
                sourceFile.Background = System.Windows.Media.Brushes.White;
                selectedSourceFiles.RemoveAll((string path) => {
                    return path == sourceFileData;
                });
            }
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            string rawCountSelectedSourceFiles = countSelectedSourceFiles.ToString();
        }

        private void AddArcheveDialogCloseHandler(object sender, EventArgs e)
        {
            ClearSelection();
        }

        public void ClearSelection ()
        {
            selectedSourceFiles.Clear();
            GetSourceFiles();
            foreach (StackPanel someSourceFile in sourceFiles.Children)
            {
                someSourceFile.Background = System.Windows.Media.Brushes.White;
            }
        }

        private void ExtractArchieveHandler(object sender, RoutedEventArgs e)
        {
            OpenExtractArchieveDialog();
        }

        public void OpenExtractArchieveDialog()
        {
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            bool isSourceFilesSelected = countSelectedSourceFiles >= 1;
            if (isSourceFilesSelected)
            {
                int countZips = selectedSourceFiles.Where<string>((string path) => {
                    string ext = System.IO.Path.GetExtension(path);
                    bool isRar = ext == ".rar";
                    bool isZip = ext == ".zip";
                    bool isArchieve = isRar || isZip;
                    return isArchieve;
                }).Count<string>();
                bool isArchievesFound = countZips >= 1;
                if (isArchievesFound)
                {
                    Archiver.Dialogs.ExtractArchieveDialog dialog = new Archiver.Dialogs.ExtractArchieveDialog(selectedSourceFiles);
                    dialog.Show();
                    dialog.Closed += ExtractArcheveDialogCloseHandler;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Среди выбранных файлов и папок нет архивов.", "Операция с группой архивов", MessageBoxButton.OK);
                }
            }
        }

        public void ExtractArcheveDialogCloseHandler (object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void TestArchieveHandler(object sender, RoutedEventArgs e)
        {
            TestArchieve();
        }

        private void TestArchieve()
        {
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            bool isSourceFilesSelected = countSelectedSourceFiles >= 1;
            if (isSourceFilesSelected)
            {
                int countZips = selectedSourceFiles.Where<string>((string path) => {
                    string ext = System.IO.Path.GetExtension(path);
                    bool isRar = ext == ".rar";
                    bool isZip = ext == ".zip";
                    bool isArchieve = isRar || isZip;
                    return isArchieve;
                }).Count<string>();
                bool isArchievesFound = countZips >= 1;
                if (isArchievesFound)
                {
                    MessageBoxResult result = MessageBox.Show("Ошибок не обнаружено.", "Тестирование окончено", MessageBoxButton.OK);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Среди выбранных файлов и папок нет архивов.", "Операция с группой архивов", MessageBoxButton.OK);
                }
            }
        }

        private void DeleteSourceFileHandler(object sender, RoutedEventArgs e)
        {
            DeleteSourceFile();
        }

        public void DeleteSourceFile()
        {
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            bool isSourceFilesSelected = countSelectedSourceFiles >= 1;
            if (isSourceFilesSelected)
            {
                foreach (String selectedSourceFile in selectedSourceFiles)
                {
                    File.Delete(selectedSourceFile);
                }
                ClearSelection();
            }
        }

        private void FixSourceFilesHandler(object sender, RoutedEventArgs e)
        {
            FixSourceFiles();
        }

        public void FixSourceFiles()
        {

        }

        private void GetSourceFilesInfoHandler(object sender, RoutedEventArgs e)
        {
            GetSourceFilesInfo();
        }

        public void GetSourceFilesInfo()
        {
            int countSelectedSourceFiles = selectedSourceFiles.Count;
            bool isSourceFilesSelected = countSelectedSourceFiles >= 1;
            if (isSourceFilesSelected)
            {

                Archiver.Dialogs.InfoSourceFileDialog dialog = new Archiver.Dialogs.InfoSourceFileDialog(selectedSourceFiles);
                dialog.Show();
                dialog.Closed += AddArcheveDialogCloseHandler;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Не выбраны файлы. Нужно отметить файлы для обработки.", "Внимание", MessageBoxButton.OK);
            }
        }

        private void OpenMasterArchiveHandler(object sender, RoutedEventArgs e)
        {
            OpenMasterArchive();
        }

        public void OpenMasterArchive ()
        {
            Archiver.Dialogs.MasterArchieveDialog dialog = new Archiver.Dialogs.MasterArchieveDialog(selectedSourceFiles);
            dialog.Show();
        }

    }
}
