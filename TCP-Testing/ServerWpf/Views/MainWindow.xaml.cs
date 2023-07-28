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
        public MainWindow()
        {
            InitializeComponent();

            _testsAndServerViewModel = new AllTestsAndServerViewModel();

            _testsAndServerViewModel.LoadServerListen();

            DataContext = _testsAndServerViewModel;
        }

        private void testsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string data = _testsAndServerViewModel.GetTestByIndex(testsList.SelectedItem);
            selectedTest.Text = data;
        }

        private void SendTestButton_Click(object sender, RoutedEventArgs e)
        {
            if(clientsList.SelectedItem != null && testsList.SelectedItem != null)
            {
                _testsAndServerViewModel.SendTestToCLient(clientsList.SelectedItem, testsList.SelectedItem);
            }
        }
    }
}
