using System.Threading.Tasks;

using ServerWpf.Model;

namespace ServerWpf.ViewModels
{
    internal class ServerViewModel
    {
        private ServerModel _model;

        public ServerViewModel()
        {
            _model = new ServerModel();
        }

        public ServerModel Model { get { return _model; } }

        #region public Methods
        public void LoadServerListen()
        {
            Task.Run(() => _model.ListenAsync());
        }

        public void SendTestToClient(object selectedClient, TestData data)
        {
            Client client = selectedClient as Client;
            _model.SendTestToClient(client, data);
        }
        #endregion
    }
}
