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
    /// Логика взаимодействия для SearchSourceFilesDialog.xaml
    /// </summary>
    public partial class SearchSourceFilesDialog : Window
    {
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
