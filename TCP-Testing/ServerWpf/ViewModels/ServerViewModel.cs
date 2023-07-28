using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        public void LoadServerListen()
        {
            Task.Run(() => _model.ListenAsync());
            //await _model.ListenAsync();
        }

        public void SendTestToClient(object selectedClient, TestData data)
        {
            Client client = selectedClient as Client;
            _model.SendTestToClient(client, data);
        }
    }
}
