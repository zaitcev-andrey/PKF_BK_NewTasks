using System.Windows;
using ServerWpf.ViewModels;

namespace ServerWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AllTestsAndServerViewModel _testsAndServerViewModel;
        private int selectedTestType = 1;
        public MainWindow()
        {
            InitializeComponent();

            _testsAndServerViewModel = new AllTestsAndServerViewModel();

            _testsAndServerViewModel.LoadServerListen();

            DataContext = _testsAndServerViewModel;
        }

        #region private Methods
        private void multipleChoiceTestsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedTestType = 1;
            string data = _testsAndServerViewModel.GetTestByIndex(multipleChoiceTestsList.SelectedItem);
            selectedTest.Text = data;
        }       

        private void sequenceTestsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedTestType = 2;
            string data = _testsAndServerViewModel.GetTestByIndex(sequenceTestsList.SelectedItem);
            selectedTest.Text = data;
        }

        private void SendTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientsList.SelectedItem != null)
            {
                if (multipleChoiceTestsList.SelectedItem != null && selectedTestType == 1)
                    _testsAndServerViewModel.SendTestToCLient(clientsList.SelectedItem, multipleChoiceTestsList.SelectedItem);
                else if(sequenceTestsList.SelectedItem != null && selectedTestType == 2)
                    _testsAndServerViewModel.SendTestToCLient(clientsList.SelectedItem, sequenceTestsList.SelectedItem);
            }
        }
        #endregion
    }
}
