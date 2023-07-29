using ServerWpf.Model;

namespace ServerWpf.ViewModels
{
    internal class AllTestsAndServerViewModel
    {
        #region private Members
        private AllTestsViewModel _allTestsVM;
        private ServerViewModel _serverVM;
        #endregion

        public AllTestsAndServerViewModel()
        {
            _allTestsVM = new AllTestsViewModel();
            _serverVM = new ServerViewModel();
        }

        #region public Properties
        public AllTestsViewModel AllTestsVM { get { return _allTestsVM; } }
        public ServerViewModel ServerVM { get { return _serverVM; } }
        #endregion

        #region public Methods
        public void LoadServerListen()
        {
            _serverVM.LoadServerListen();
        }

        public string GetTestByIndex(object selectedTest)
        {
            TestData testData = _allTestsVM.GetTestBySelectedTest(selectedTest);
            return testData.TestString;
        }

        public void SendTestToCLient(object selectedClient, object selectedTest)
        {
            TestData testData = _allTestsVM.GetTestBySelectedTest(selectedTest);
            _serverVM.SendTestToClient(selectedClient, testData);
        }
        #endregion
    }
}
