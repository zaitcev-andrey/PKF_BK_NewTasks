using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWpf.Model
{
    internal class Client
    {
        private static int _globalIndex = 0;

        public string Name { get; set; }
        public int Index { get; set; }

        public List<string> TestResults { get; set; }
        public string LastTestResult { get; set; }

        public Client(string _name) 
        {
            Name = _name;
            _globalIndex++;
            Index = _globalIndex;
        }

        public void AddTestResult(string testResult)
        {
            TestResults.Add(testResult);
            LastTestResult = testResult;
        }
    }
}
