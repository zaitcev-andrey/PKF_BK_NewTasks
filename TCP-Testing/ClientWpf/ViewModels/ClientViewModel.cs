using ClientWpf.Commands;
using ClientWpf.Models;

namespace ClientWpf.ViewModels
{
    internal class ClientViewModel
    {
        private ClientModel _model;

        public ClientViewModel()
        {
            _model = new ClientModel();
        }

        public ClientModel Model { get { return _model; } }

        #region private Commands
        private RelayCommand _connectToServerCommand;
        private RelayCommand _sendAnswerCommand;
        #endregion

        #region public Commands
        public RelayCommand ConnectToServerCommand
        {
            get
            {
                return _connectToServerCommand ??
                (_connectToServerCommand = new RelayCommand(obj =>
                {
                    Model.ConnectToServer();
                }));
            }
        }
        public RelayCommand SendAnswerCommand
        {
            get
            {
                return _sendAnswerCommand ??
                (_sendAnswerCommand = new RelayCommand(obj =>
                {
                    Model.SendAnswer();
                },
                (obj) => !string.IsNullOrEmpty(Model.StringWithTest)
                ));
            }
        }
        #endregion
    }
}
