using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ProgressDialog.xaml
    /// </summary>
    public partial class ProgressDialog : Window
    {

        public TextBox generalArchieveName;
        public SpeechSynthesizer debugger;

        public ProgressDialog(TextBox generalArchieveName)
        {
            InitializeComponent();

            Initialize(generalArchieveName);

        }

        public void Initialize (TextBox generalArchieveName)
        {
            debugger = new SpeechSynthesizer();
            this.generalArchieveName = generalArchieveName;
            generalArchieveName.DataContextChanged += ProgressChanged;
        }

        public void ProgressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            int progress = (int)((TextBox)(sender)).DataContext;
            string rawProgress = progress.ToString();
            debugger.Speak("Прогресс: " + rawProgress);
            progressBar.Value = progress;
        }

    }
}
