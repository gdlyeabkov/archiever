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
            List<string> results = sourceFilesPaths.Where<string>((string path) => {
                string content = File.ReadAllText(path);
                string keywordsLabelContent = keywordsLabel.Text;
                string insensitiveCaseKeywordsLabelContent = keywordsLabelContent.ToLower();
                return content.ToLower().Contains(insensitiveCaseKeywordsLabelContent);
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
