using ServerWpf.Model;

namespace ServerWpf.ViewModels
{
    internal class AllTestsViewModel
    {
        private AllTestsModel _model;
        
        public AllTestsViewModel()
        {
            _model = new AllTestsModel();
        }

        public AllTestsModel Model { get { return _model; } }

        public TestData GetTestBySelectedTest(object selectedItem)
        {
            return Model.GetTestData(selectedItem);
        }
    }
}
