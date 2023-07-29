using System.Collections.Generic;

namespace ServerWpf.Model
{
    /// <summary>
    /// Класс, описывающий клиента
    /// </summary>
    internal class Client
    {
        private static int _globalIndex = 0;

        #region public Properties
        public string Name { get; set; }
        public int Index { get; set; }
        #endregion

        public Client(string _name) 
        {
            Name = _name;
            _globalIndex++;
            Index = _globalIndex;
        }
    }
}
