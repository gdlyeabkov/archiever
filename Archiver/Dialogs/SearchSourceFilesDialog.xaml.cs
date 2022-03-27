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
    /// Логика взаимодействия для SearchSourceFilesDialog.xaml
    /// </summary>
    public partial class SearchSourceFilesDialog : Window
    {

        public String currentPath = @"C:\Gleb\archiever\zipsource";

        public SearchSourceFilesDialog()
        {
            InitializeComponent();
        }

        private void SearchHandler (object sender, RoutedEventArgs e)
        {
            Search();
        }

        public void Search ()
        {
            string[] sourceFilesPaths = Directory.GetFiles(currentPath);
            string sourceFileNameLabelContent = sourceFileNameLabel.Text;
            int sourceFileNameLabelContentLength = sourceFileNameLabelContent.Length;
            bool isSetSourceFileName = sourceFileNameLabelContentLength >= 1;
            string sourceFileNameLabelInsensitiveCaseContent = "";
            if (isSetSourceFileName)
            {
                sourceFileNameLabelInsensitiveCaseContent = sourceFileNameLabelContent.ToLower();
            }
            bool isSearchInSourceFiles = ((bool)(isSearchInSourceFilesCheckBox.IsChecked));
            bool isSearchInArchieves = ((bool)(isSearchInArchievesCheckBox.IsChecked));
            List<string> results = sourceFilesPaths.Where<string>((string path) => {
                bool isSourceFileNameMatches = true;
                if (isSetSourceFileName)
                {
                    string sourceFileName = System.IO.Path.GetFileName(path);
                    string insensitiveCaseSourceFileName = sourceFileName.ToLower();
                    isSourceFileNameMatches = insensitiveCaseSourceFileName.Contains(sourceFileNameLabelInsensitiveCaseContent);
                }
                string content = File.ReadAllText(path);
                string insensitiveCaseSourceFileContent = content.ToLower();
                string keywordsLabelContent = keywordsLabel.Text;
                string insensitiveCaseKeywordsLabelContent = keywordsLabelContent.ToLower();
                bool isSourceFileContentMatches = insensitiveCaseSourceFileContent.Contains(insensitiveCaseKeywordsLabelContent);
                bool isSourceFileAsArchieveMatches = true;
                string ext = System.IO.Path.GetExtension(path);
                bool isZip = ext == ".zip";
                bool isRar = ext == ".rar";
                bool isArchieve = isZip || isRar;
                bool isNotArchieve = !isArchieve;
                bool isArchieveOrNotArchieve = isArchieve || isNotArchieve;
                bool isNotSearchInArchieves = !isSearchInArchieves;
                bool isArchieveSearch = isSearchInArchieves && isArchieveOrNotArchieve;
                bool isNotArchieveSearch = isNotSearchInArchieves && isNotArchieve;
                isSourceFileAsArchieveMatches = isArchieveSearch || isNotArchieveSearch;
                bool isSearchSourceFiles = true;
                bool isSourceFileExists = File.Exists(path);
                isSearchSourceFiles = isSourceFileExists && isSearchInSourceFiles;
                bool isSourceFileMatches = isSourceFileNameMatches && isSourceFileContentMatches && isSourceFileAsArchieveMatches && isSearchSourceFiles;
                return isSourceFileMatches;
            }).ToList<string>();
            Archiver.Dialogs.SearchSourceFilesResultsDialog dialog = new Archiver.Dialogs.SearchSourceFilesResultsDialog(results);
            dialog.Show();
            Cancel();
        }

        private void CancelHandler(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        public void Cancel()
        {
            this.Close();
        }

        private void SaveSearchHandler(object sender, RoutedEventArgs e)
        {
            SaveSearch();
        }

        public void SaveSearch ()
        {
            Cancel();
        }

        private void GetHelpDialogHandler (object sender, RoutedEventArgs e)
        {
            GetHelpDialog();
        }

        public void GetHelpDialog ()
        {

        }

    }
}
