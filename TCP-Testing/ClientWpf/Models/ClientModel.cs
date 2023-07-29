using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientWpf.Models
{
    internal class ClientModel : BaseModel
    {
        #region private Members
        private string _host;
        private int _port;
        private string _userName;

        private Socket _clientSocket;

        private string _stringWithTest;
        private string _connectAndErrorMessage;
        private string _answer;
        private string _messageAboutSending;
        private bool _isSendAnswer;
        #endregion

        #region Constructors
        public ClientModel()
        {
            _host = "127.0.0.1";
            _port = 8080;
            _userName = "";

            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _connectAndErrorMessage = "";

            _answer = "";
            _isSendAnswer = false;
        }
        #endregion

        #region public Properties
        public string Host
        {
            get { return _host; }
            set
            {
                _host = value;
                OnPropertyChanged("Host");
            }
        }

        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string StringWithTest
        {
            get { return _stringWithTest; }
            set
            {
                _stringWithTest = value;
                OnPropertyChanged("StringWithTest");
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public string ConnectAndErrorLog
        {
            get { return _connectAndErrorMessage; }
            set
            {
                _connectAndErrorMessage = value;
                OnPropertyChanged("ConnectAndErrorLog");
            }
        }       

        public string MessageAboutSending
        {
            get { return _messageAboutSending; }
            set
            {
                _messageAboutSending = value;
                OnPropertyChanged("MessageAboutSending");
            }
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Метод для подключения клиента к серверу
        /// </summary>
        public void ConnectToServer()
        {
            try
            {
                if (string.IsNullOrEmpty(Host) || Port == 0 || string.IsNullOrEmpty(UserName))
                {
                    ConnectAndErrorLog = "Не хватает данных для подключения, проверьте их и попробуйте снова";
                    return;
                }

                // после выполнения метода Connect сервер принимает клиента методом AcceptAsync()
                _clientSocket.Connect(Host, Port);

                ConnectAndErrorLog = "Вы присоединились к серверу";

                // Отправляем имя
                var messageBytes = Encoding.UTF8.GetBytes(UserName);
                _clientSocket.Send(messageBytes);

                Task.Run(() => SendAnswersAsync());
                Task.Run(() => ReceiveTestAsync());
            }
            catch (Exception ex)
            {
                ConnectAndErrorLog = ex.Message;
            }
        }

        /// <summary>
        /// В этом методе активируется отправка сообщения
        /// </summary>
        public void SendAnswer()
        {
            _isSendAnswer = true;
        }
        #endregion

        #region private Methods
        /// <summary>
        /// Метод для отправки ответа на сервер
        /// </summary>
        /// <returns></returns>
        private async Task SendAnswersAsync()
        {
            while (true)
            {
                await Task.Delay(10);
                if (_isSendAnswer)
                {
                    if (!string.IsNullOrEmpty(Answer))
                    {
                        var messageBytes = Encoding.UTF8.GetBytes(Answer);
                        MessageAboutSending = "Ответ отправлен на сервер";

                        _clientSocket.Send(messageBytes);
                        StringWithTest = "";
                        Answer = "";
                    }
                    _isSendAnswer = false;
                }
            }
        }

        /// <summary>
        /// Метод для получения теста от сервера
        /// </summary>
        /// <returns></returns>
        private async Task ReceiveTestAsync()
        {
            while (true)
            {
                await Task.Delay(10);
                var clientData = new byte[1024 * 1000];
                int receivedByteLen = _clientSocket.Receive(clientData);

                StringWithTest = Encoding.UTF8.GetString(clientData, 0, receivedByteLen);
                MessageAboutSending = "";
            }
        }
        #endregion
    }
}
