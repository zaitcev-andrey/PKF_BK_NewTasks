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
        private Dictionary<EndPoint, int> _ip_IndexMap; // нужен для того, чтобы понимали от какого клиента получили ответ
        private Dictionary<int, TestData> _testDataForClientMap;

        private AllTestsModel testModel;

        private string _selectedTest;
        private int _selectedClientIndex;
        private int _lastClientIndex;

        private List<Client> _clientModels;
        private StringBuilder _allClientsResults;
        private string _logWithResults;

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
                //Console.WriteLine("Сервер запущен, ожидается подключение клиентов...");

                while (true)
                {
                    // получаем клиента после выполнения у него метода Connect(host, port)
                    Socket clientSocket = await serverSocket.AcceptAsync();

                    var nameBytes = new byte[512];
                    int bytes = clientSocket.Receive(nameBytes);
                    var userName = Encoding.UTF8.GetString(nameBytes, 0, bytes);
                    AddNewClientModel(clientSocket, userName);

                    clients.Add(clientSocket);
                    Task.Run(() => GetMessageFromClientAsync(clientSocket, userName));
                    Task.Run(() => SendMessageToClientAsync(clientSocket, _lastClientIndex));
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
        /// Метод для получения данных от клиента и рассылки этих данных всем остальным клиентам 
        /// кроме того, от которого данные были получены
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <returns></returns>
        private async Task GetMessageFromClientAsync(Socket clientSocket, string userName)
        {
            try
            {
                string clientAnswer = "";
                while (true)
                {
                    var clientData = new byte[1024 * 10000]; // до 10мб
                    int receivedByteLen = clientSocket.Receive(clientData);

                    clientAnswer = Encoding.UTF8.GetString(clientData, 0, receivedByteLen);
                    // здесь проверяем результат относительно пройденного теста
                    TestData data = _testDataForClientMap[_ip_IndexMap[clientSocket.RemoteEndPoint]];
                    bool result = testModel.GetResultForTest(clientAnswer, data);
                    int valueResult = result ? 100 : 0;
                    _allClientsResults.AppendLine($"Пользователь {userName} прошёл тест {TestTypesNameString.GetNameByType(data.Type)} " +
                        $"под номером {data.Index} с результатом {valueResult} процентов");
                    LogWithResults = _allClientsResults.ToString();                    
                }
            }
            catch (Exception ex)
            {
                // !!! переписать. Сделать переменную строки, которая выводится в лог в view. А так же теперь broadcast будет отправлять тест именно нашему клиенту, а не всем остальным

                //if (ex.Message == "Удаленный хост принудительно разорвал существующее подключение")
                //    Console.WriteLine($"{userName} вышел из чата");
                //else Console.WriteLine(ex.Message);
            }
            finally
            {
                RemoveConnection(clientSocket.RemoteEndPoint);
            }
        }

        private async Task SendMessageToClientAsync(Socket clientSocket, int clientIndex)
        {
            try
            {
                while (true)
                {
                    if (clientIndex == _selectedClientIndex)
                    {
                        var messageBytes = Encoding.UTF8.GetBytes(_selectedTest);
                        _selectedClientIndex = -1;
                        clientSocket.Send(messageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                // !!! переписать. Сделать переменную строки, которая выводится в лог в view. А так же теперь broadcast будет отправлять тест именно нашему клиенту, а не всем остальным

                //if (ex.Message == "Удаленный хост принудительно разорвал существующее подключение")
                //    Console.WriteLine($"{userName} вышел из чата");
                //else Console.WriteLine(ex.Message);
            }
            finally
            {
                RemoveConnection(clientSocket.RemoteEndPoint);
            }
            
        }

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
        }
        #endregion
    }
}
