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
                // string data = selectedSourceFiles[0];
                Archiver.Dialogs.AddArchieveDialog dialog = new Archiver.Dialogs.AddArchieveDialog(selectedSourceFiles);
                dialog.Show();
                dialog.Closed += AddArcheveDialogCloseHandler;
            }
        }

        public void GetSourceFiles()
        {
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
            foreach (StackPanel someSourceFile in sourceFiles.Children)
            {
                someSourceFile.Background = System.Windows.Media.Brushes.White;
            }
        }

    }
}
