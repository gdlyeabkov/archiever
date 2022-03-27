using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SearchSourceFilesResultsDialog.xaml
    /// </summary>
    public partial class SearchSourceFilesResultsDialog : Window
    {

        public List<string> results;
        
        public SearchSourceFilesResultsDialog(List<string> results)
        {
            InitializeComponent();

            Initialize(results);

        }

        public void Initialize(List<string> results)
        {
            this.results = results;
            OutputResults();
        }


        public void OutputResults()
        {
            foreach (string result in results)
            {
                string fileName = System.IO.Path.GetFileName(result);
                StackPanel searchedFile = new StackPanel();
                searchedFile.Orientation = Orientation.Horizontal;
                searchedFile.Height = 35;
                TextBlock searchedFileNameLabel = new TextBlock();
                searchedFileNameLabel.Text = fileName;
                searchedFile.Children.Add(searchedFileNameLabel);
                searchedFiles.Children.Add(searchedFile);
            }
            int countResults = results.Count;
            string rawCountResults = countResults.ToString();
            string foundLabelContent = "Найдено: " + rawCountResults;
            foundLabel.Text = foundLabelContent;
        }

        private void CancelHandler(object sender, RoutedEventArgs e)
        {
            Cancel();
        }

        public void Cancel () {
            this.Close();
        }

    }
}
