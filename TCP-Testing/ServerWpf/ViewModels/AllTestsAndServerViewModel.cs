using ServerWpf.Commands;
using ServerWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.ViewModels
{
    internal class AllTestsAndServerViewModel
    {
        private AllTestsViewModel _allTestsVM;
        private ServerViewModel _serverVM;

        public AllTestsAndServerViewModel()
        {
            _allTestsVM = new AllTestsViewModel();
            _serverVM = new ServerViewModel();
        }

        public AllTestsViewModel AllTestsVM { get { return _allTestsVM; } }
        public ServerViewModel ServerVM { get { return _serverVM; } }

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
    }
}
