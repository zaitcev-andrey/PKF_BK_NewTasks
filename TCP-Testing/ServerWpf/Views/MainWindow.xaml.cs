using System.Windows;

using ServerWpf.ViewModels;

namespace ServerWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AllTestsViewModel testsViewModel;
        public MainWindow()
        {
            InitializeComponent();

            testsViewModel = new AllTestsViewModel();

            DataContext = testsViewModel;
        }

        private void testsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {            
            string data = testsViewModel.GetTestByIndex(testsList.SelectedItem);
            selectedTest.Text = data;
        }

        
    }
}
