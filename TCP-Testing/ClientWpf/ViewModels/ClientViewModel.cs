using ClientWpf.Commands;
using ClientWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf.ViewModels
{
    internal class ClientViewModel
    {
        #region private Members
        private ClientModel _model;
        #endregion

        #region Constructors
        public ClientViewModel()
        {
            _model = new ClientModel();
        }
        #endregion

        #region public Properties
        public ClientModel Model { get { return _model; } }
        #endregion

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
