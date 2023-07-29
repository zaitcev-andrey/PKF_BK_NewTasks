using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class ServerModel : BaseModel
    {
        #region private Members
        
        private Socket serverSocket; // серверный сокет для прослушивания        
        private List<Socket> clients; // список всех клиентских сокетов
        private Dictionary<EndPoint, int> _ip_IndexMap; // словарь для соответсвия ip клиента с его индексом в списке клиентов
        private Dictionary<int, TestData> _testDataForClientMap; // словарь для соответствия индекса клиента с тестом, который ему отправлен

        private AllTestsModel testModel; // для получения результата по пройденному тесту

        private string _selectedTest;
        private int _selectedClientIndex;
        private int _lastClientIndex; // хранит индекс последнего подключенного клиента

        private List<Client> _clientModels;
        private StringBuilder _allClientsResults;
        private string _logWithResults;
        private string _messageAboutSendingTest;

        #endregion

        #region Constructors
        public ServerModel()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8080);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // связываем сокет с локальной точкой ipPoint
            serverSocket.Bind(ipPoint);

            clients = new List<Socket>();

            _clientModels = new List<Client>();
            _allClientsResults = new StringBuilder();

            _ip_IndexMap = new Dictionary<EndPoint, int>();
            _testDataForClientMap = new Dictionary<int, TestData>();

            testModel = new AllTestsModel();

            _selectedTest = "";
            _selectedClientIndex = -1;
            _lastClientIndex = -1;
        }
        #endregion

        #region public Properties
        public List<Client> ClientModels 
        {
            get { return _clientModels; }
            set 
            {
                _clientModels = value;
                OnPropertyChanged("ClientModels");
            }
        }

        public string LogWithResults
        {
            get { return _logWithResults; }
            set
            {
                _logWithResults = value;
                OnPropertyChanged("LogWithResults");
            }
        }

        public string MessageAboutSendingTest
        {
            get { return _messageAboutSendingTest; }
            set
            {
                _messageAboutSendingTest = value;
                OnPropertyChanged("MessageAboutSendingTest");
            }
        }

        #endregion

        #region public Methods
        /// <summary>
        /// Основной метод для прослушивания клиентов
        /// </summary>
        /// <returns></returns>
        public async Task ListenAsync()
        {
            try
            {
                serverSocket.Listen(1000);

                while (true)
                {
                    // получаем клиента после выполнения у него метода Connect(host, port)
                    Socket clientSocket = await serverSocket.AcceptAsync();

                    var nameBytes = new byte[512];
                    int bytes = clientSocket.Receive(nameBytes);
                    var userName = Encoding.UTF8.GetString(nameBytes, 0, bytes);
                    AddNewClientModel(clientSocket, userName);

                    clients.Add(clientSocket);
                    Task.Run(() => GetTestAnswerFromClientAsync(clientSocket, userName));
                    Task.Run(() => SendTestToClientAsync(clientSocket, _lastClientIndex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Этот метод определяет клиента, которому будет отправляться тест. После этого метода, когда клиент уже 
        /// известен, начнёт выполняться основной метод на отправку теста клиенту.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        public void SendTestToClient(Client client, TestData data)
        {
            _selectedTest = data.TestString;
            _selectedClientIndex = client.Index;
            if(!_testDataForClientMap.ContainsKey(_selectedClientIndex))
                _testDataForClientMap.Add(_selectedClientIndex, data);
            else _testDataForClientMap[_selectedClientIndex] = data;
        }

        /// <summary>
        /// Метод, который выполняется после закрытия сервера
        /// </summary>
        public void Disconnect()
        {
            // сначала закрываем всех клиентов
            foreach (var client in clients)
            {
                client.Close();
            }
            // затем останавливаем сервер
            serverSocket.Close();
        }
        #endregion

        #region private Methods
        /// <summary>
        /// Метод для получения данных (ответа на тест) от клиента
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <returns></returns>
        private async Task GetTestAnswerFromClientAsync(Socket clientSocket, string userName)
        {
            try
            {
                string clientAnswer = "";
                while (true)
                {
                    var clientData = new byte[1024 * 1000];
                    int receivedByteLen = clientSocket.Receive(clientData);

                    clientAnswer = Encoding.UTF8.GetString(clientData, 0, receivedByteLen);

                    // здесь получаем результат относительно пройденного теста
                    TestData data = _testDataForClientMap[_ip_IndexMap[clientSocket.RemoteEndPoint]];
                    int result = testModel.GetResultForTest(clientAnswer, data);

                    _allClientsResults.AppendLine($"Пользователь {userName} прошёл тест {TestTypesNameString.GetNameByType(data.Type)} " +
                        $"под номером {data.Index} с результатом {result} процентов");
                    LogWithResults = _allClientsResults.ToString();                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                RemoveConnection(clientSocket.RemoteEndPoint);
            }
        }

        /// <summary>
        /// Метод для отправки теста клиенту
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="clientIndex"></param>
        /// <returns></returns>
        private async Task SendTestToClientAsync(Socket clientSocket, int clientIndex)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(10);
                    if (clientIndex == _selectedClientIndex)
                    {
                        var messageBytes = Encoding.UTF8.GetBytes(_selectedTest);
                        _selectedClientIndex = -1;
                        MessageAboutSendingTest = $"Вы отправили тест пользователю {clientIndex}";
                        clientSocket.Send(messageBytes);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Метод для добавления нового клиента к списку клиентов
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="userName"></param>
        private void AddNewClientModel(Socket socket, string userName)
        {
            List<Client> tmp = new List<Client>(ClientModels);
            Client client = new Client(userName);
            tmp.Add(client);
            ClientModels = tmp;
            _lastClientIndex = client.Index;
            _ip_IndexMap.Add(socket.RemoteEndPoint, _lastClientIndex);
        }

        /// <summary>
        /// Метод удаления клиента из списка после его закрытия
        /// </summary>
        /// <param name="id"></param>
        private void RemoveConnection(EndPoint id)
        {
            Socket removedClient = clients.FirstOrDefault(cl => cl.RemoteEndPoint == id);
            if (removedClient != null)
                clients.Remove(removedClient);

            removedClient?.Close();

            List<Client> tmp = new List<Client>(ClientModels);
            Client client = ClientModels.FirstOrDefault(cl => cl.Index == _ip_IndexMap[id]);
            tmp.Remove(client);
            ClientModels = tmp;
        }
        #endregion
    }
}
