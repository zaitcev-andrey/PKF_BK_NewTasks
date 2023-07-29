using ClientWpf.ViewModels;
using System.Windows;


namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private Members
        private ClientViewModel clientViewModel;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            clientViewModel = new ClientViewModel();

            DataContext = clientViewModel;
        }
    }
}
